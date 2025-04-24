using System;
using Cognex.VisionPro.CalibFix;

namespace WindowsFormsApp1.Common.CogTools {
    public class MyIdentificationControl {
        private static readonly MyIdentificationControl _instance = new MyIdentificationControl();
        private readonly object _lock = new object();
        private CogCalibNPointToNPointTool _calibNPointToNPoint;
        
        
    }
}