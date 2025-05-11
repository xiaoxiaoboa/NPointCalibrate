using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace WindowsFormsApp1.Views.Forms {
    public partial class CenterCalibrateResults : Form {
        private Dictionary<int, PointF> _points;

        public CenterCalibrateResults(Dictionary<int, PointF> points) {
            InitializeComponent();
            _points = points;
        }


        private void CenterCalibrateResults_Load(object sender, EventArgs e) {
            
            
            label4.Text = _points[1].X.ToString(CultureInfo.CurrentCulture);
            label5.Text = _points[1].Y.ToString(CultureInfo.CurrentCulture);
            label7.Text = _points[2].X.ToString(CultureInfo.CurrentCulture);
            label8.Text = _points[2].Y.ToString(CultureInfo.CurrentCulture);
            label10.Text= _points[3].X.ToString(CultureInfo.CurrentCulture);
            label11.Text = _points[3].Y.ToString(CultureInfo.CurrentCulture);
            label13.Text = _points[4].X.ToString(CultureInfo.CurrentCulture);
            label14.Text = _points[4].Y.ToString(CultureInfo.CurrentCulture);
        }
    }
}