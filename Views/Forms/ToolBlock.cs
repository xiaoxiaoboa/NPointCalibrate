using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Views.Forms {
    public partial class ToolBlock : Form {
        private MyToolBlock _myToolBlock;


        public ToolBlock(MyToolBlock myToolBlock) {
            InitializeComponent();
            _myToolBlock = myToolBlock;
            cogToolBlockEditV21.Subject = myToolBlock.ToolBlock;
        }

        // 保存文件
        private async void saveFile_item_Click(object sender, EventArgs e) {
            DialogResult dialogResult;
            if (_myToolBlock.ToolBlock.Inputs.Count == 0 && _myToolBlock.ToolBlock.Outputs.Count == 0 &&
                _myToolBlock.ToolBlock.Tools.Count == 0) {
                dialogResult = MessageBox.Show(@"文件还没有修改过，确定保存吗？", @"保存提示", MessageBoxButtons.OKCancel);
            }
            else {
                dialogResult = MessageBox.Show(@"文件有修改痕迹，确定保存吗？", @"保存提示", MessageBoxButtons.OKCancel);
            }

            switch (dialogResult) {
                case DialogResult.OK:
                    var (msg, logLevel) = await Task.Run(() => {
                        try {
                            if (!File.Exists(_myToolBlock.VppPath)) {
                                Directory.CreateDirectory("./vpp/");
                            }

                            CogSerializer.SaveObjectToFile(_myToolBlock.ToolBlock, _myToolBlock.VppPath,
                                typeof(BinaryFormatter),
                                CogSerializationOptionsConstants.Minimum);
                            return ("保存成功", LogLevel.Info);
                        }
                        catch (Exception exception) {
                            return ($"保存失败：{exception.Message}", LogLevel.Error);
                        }
                    });
                    Logger.Instance.AddLog(msg, logLevel);
                    return;
                case DialogResult.Cancel:
                    Logger.Instance.AddLog("取消保存");
                    return;
            }
        }
    }
}