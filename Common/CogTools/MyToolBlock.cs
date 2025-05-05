using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Common.CogTools {
    public class MyToolBlock {
        private static readonly Lazy<MyToolBlock> _instance = new Lazy<MyToolBlock>(() => new MyToolBlock());


        private CogToolBlock _calibrateToolBlock;
        private CogToolBlock _identificationToolBlock;

        public CogToolBlock CalibrateToolBlock => _calibrateToolBlock;
        public CogToolBlock IdentificationToolBlock => _identificationToolBlock;

        public static MyToolBlock Instance => _instance.Value;

        private MyToolBlock() { }

        // toolblock vpp
        public readonly string CalibrateVppPath = Path.Combine(Application.StartupPath, "vpp/calibrate.vpp");

        public readonly string IdentificationVppPath =
            Path.Combine(Application.StartupPath, "vpp/calibNPointToNPoint.vpp");

        private void SetToolBLock(CogToolBlock cogToolBlock, LoadToolBlock loadToolBlock) {
            switch (loadToolBlock) {
                case LoadToolBlock.Calibrate:
                    _calibrateToolBlock = cogToolBlock;
                    break;
                case LoadToolBlock.Identification:
                    _identificationToolBlock = cogToolBlock;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(loadToolBlock), loadToolBlock, null);
            }
        }

        public void SaveVpp(CogToolBlock toolBlock, string vppPath) {
            CogSerializer.SaveObjectToFile(toolBlock, vppPath,
                typeof(BinaryFormatter),
                CogSerializationOptionsConstants.Minimum);
        }

        // 加载toolblock vpp
        public async Task<(string, LogLevel)> LoadToolBlockVpp(string toolBlockVppPath, LoadToolBlock loadToolBlock) {
            if (!File.Exists(toolBlockVppPath)) {
                SetToolBLock(new CogToolBlock(), loadToolBlock);
                return ("未发现ToolBlock vpp文件，new了一个", LogLevel.Info);
            }

            return await Task.Run(() => {
                try {
                    var tb = CogSerializer.LoadObjectFromFile(toolBlockVppPath) as CogToolBlock;
                    SetToolBLock(tb, loadToolBlock);
                    return ("发现vpp文件，加载成功", LogLevel.Info);
                }
                catch (Exception exception) {
                    return ($"加载ToolBlock文件发生未知错误：{exception.Message}", LogLevel.Error);
                }
            });
        }

        // 获取标定结果
        public (bool, Dictionary<string, object>) GetToolBlockOutputsResults(CogToolBlock toolBlock,
            IEnumerable<string> keys) {
            var values = new Dictionary<string, object>();

            if (toolBlock == null) return (false, values);
            if (toolBlock.RunStatus.Result != CogToolResultConstants.Accept) return (false, values);

            foreach (var key in keys) {
                if (toolBlock.Outputs.Contains(key)) {
                    var terminal = toolBlock.Outputs[key];

                    if (terminal?.Value == null) {
                        return (false, values);
                    }

                    values[key] = terminal.Value;
                }
            }

            return (true, values);
        }


        public T GetTool<T>(CogToolBlock toolBlock, string name) where T : CogToolBase {
            if (toolBlock?.Tools == null)
                return null;

            if (toolBlock.Tools.Contains(name)) {
                return toolBlock.Tools[name] as T;
            }

            return null;
        }

        // 获取record
        public ICogRecord GetRecord(CogToolBlock toolBlock, string name) {
            if (toolBlock == null) return null;
            if (toolBlock.RunStatus.Result != CogToolResultConstants.Accept) return null;

            var records = toolBlock.CreateLastRunRecord().SubRecords;

            return records.ContainsKey(name) ? records[name] : null;
        }
    }
}