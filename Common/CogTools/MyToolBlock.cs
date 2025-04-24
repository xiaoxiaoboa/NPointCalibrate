using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Common.CogTools {
    public class MyToolBlock {
        private readonly object _lock = new object();
        private CogToolBlock _cogToolBlock;
        private string _vppPath;


        public string VppPath => _vppPath;

        public CogToolBlock ToolBlock{
            get {
                lock (_lock) {
                    return _cogToolBlock;
                }
            }
        }

        // 加载toolblock vpp
        public async Task<(string, LogLevel)> LoadToolBlockVpp(string toolBlockVppPath) {
            _vppPath = toolBlockVppPath;

            if (!File.Exists(toolBlockVppPath)) {
                _cogToolBlock = new CogToolBlock();
                return ("未发现ToolBlock vpp文件，new了一个", LogLevel.Info);
            }

            return await Task.Run(() => {
                try {
                    var tb = CogSerializer.LoadObjectFromFile(toolBlockVppPath) as CogToolBlock;
                    _cogToolBlock = tb;

                    return ("发现vpp文件，加载成功", LogLevel.Info);
                }
                catch (Exception exception) {
                    return ($"加载ToolBlock文件发生未知错误：{exception.Message}", LogLevel.Error);
                }
            });
        }


        // 获取PMA结果
        public List<MyPmaResult> GetPmaResult() {
            var myPmaResults = new List<MyPmaResult>();
            if (_cogToolBlock.Tools["CogPMAlignTool1"] is CogPMAlignTool pma) {
                if (pma.RunStatus.Result == CogToolResultConstants.Accept && pma.Results != null) {
                    myPmaResults.AddRange(from CogPMAlignResult r in pma.Results
                        select new MyPmaResult(r.Score, r.GetPose().RotationX, r.GetPose().TranslationY));
                }
            }

            return myPmaResults;
        }
    }
}