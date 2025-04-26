using System;
using System.Windows.Forms;
using WindowsFormsApp1.Common;

namespace WindowsFormsApp1.Views.Forms {
    public partial class NPointCalibrate : Form {
        public NPointCalibrate() {
            InitializeComponent();

            PlcControl.Instance.OnNPointCalibrateChanged += OnOnNPointCalibrateChanged;
        }

        private void OnOnNPointCalibrateChanged(int serialNumber) {
            throw new NotImplementedException();
        }
    }
}   