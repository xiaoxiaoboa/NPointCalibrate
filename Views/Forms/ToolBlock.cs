using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Common.CogTools;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Views.Forms {
    public partial class ToolBlock : Form {
        private CogToolBlock _toolBlock;
        private string _vppPath;


        public ToolBlock(CogToolBlock toolBlock, LoadToolBlock loadToolBlock) {
            InitializeComponent();
            _toolBlock = toolBlock;
            cogToolBlockEditV21.Subject = toolBlock;

            // 确定vppPath
            switch (loadToolBlock) {
                case LoadToolBlock.Calibrate:
                    _vppPath = MyToolBlock.Instance.CalibrateVppPath;
                    break;
                case LoadToolBlock.Identification:
                    _vppPath = MyToolBlock.Instance.IdentificationVppPath;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(loadToolBlock), loadToolBlock, null);
            }
        }

        // 保存文件
        private async void saveFile_item_Click(object sender, EventArgs e) {
            DialogResult dialogResult;
            if (_toolBlock.Inputs.Count == 0 && _toolBlock.Outputs.Count == 0 &&
                _toolBlock.Tools.Count == 0) {
                dialogResult = MessageBox.Show(@"文件还没有修改过，确定保存吗？", @"保存提示", MessageBoxButtons.OKCancel);
            }
            else {
                dialogResult = MessageBox.Show(@"文件有修改痕迹，确定保存吗？", @"保存提示", MessageBoxButtons.OKCancel);
            }

            switch (dialogResult) {
                case DialogResult.OK:
                    var (msg, logLevel) = await Task.Run(() => {
                        try {
                            if (!File.Exists(_vppPath)) {
                                Directory.CreateDirectory("./vpp/");
                            }

                            CogSerializer.SaveObjectToFile(_toolBlock, _vppPath,
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