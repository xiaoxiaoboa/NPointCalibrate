using System;
using Cognex.VisionPro;

namespace WindowsFormsApp1.Interface {
    public interface IMyControl {
        event EventHandler MaximizeRestoreRequested;

        void SetGraphic(ICogImage image);

        void SetLabelText(string text);
        
        void Label_DoubleClick(object sender, EventArgs e);
    }
}