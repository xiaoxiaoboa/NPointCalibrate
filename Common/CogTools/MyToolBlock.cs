using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public bool GetCalibrateResult(out double x, out double y) {
            x = 0;
            y = 0;
            if (_calibrateToolBlock == null) return false;
            if (_calibrateToolBlock.RunStatus.Result != CogToolResultConstants.Accept) return false;
            if (!_calibrateToolBlock.Outputs.Contains("X") && !_calibrateToolBlock.Outputs.Contains("Y")) return false;

            var outputX = _calibrateToolBlock.Outputs["X"];
            var outputY = _calibrateToolBlock.Outputs["Y"];

            if (outputX?.Value == null || outputY?.Value == null) return false;

            x = (double)outputX.Value;
            y = (double)outputY.Value;


            return true;
        }

        public T GetTool<T>(CogToolBlock toolBlock, string name) where T : CogToolBase {
            if (toolBlock?.Tools == null)
                return null;

            if (toolBlock.Tools.Contains(name)) {
                return toolBlock.Tools[name] as T;
            }

            return null;
        }

        // 获取PMA结果
        // public List<MyPmaResult> GetPmaResult() {
        //     var myPmaResults = new List<MyPmaResult>();
        //     if (_cogToolBlock.Tools["CogPMAlignTool1"] is CogPMAlignTool pma) {
        //         if (pma.RunStatus.Result == CogToolResultConstants.Accept && pma.Results != null) {
        //             myPmaResults.AddRange(from CogPMAlignResult r in pma.Results
        //                 select new MyPmaResult(r.Score, r.GetPose().RotationX, r.GetPose().TranslationY));
        //         }
        //     }
        //
        //     return myPmaResults;
        // }
    }
}