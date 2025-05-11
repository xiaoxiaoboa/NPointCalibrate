using System.ComponentModel;

namespace WindowsFormsApp1.Views.Forms {
    partial class ToolBlock {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.file_item = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFile_item = new System.Windows.Forms.ToolStripMenuItem();
            this.resultPma_item = new System.Windows.Forms.ToolStripMenuItem();
            this.PMA_item = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1277, 659);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cogToolBlockEditV21);
            this.panel3.Controls.Add(this.menuStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1277, 659);
            this.panel3.TabIndex = 1;
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(0, 28);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(1277, 631);
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            this.cogToolBlockEditV21.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.file_item, this.resultPma_item });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1277, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // file_item
            // 
            this.file_item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.saveFile_item });
            this.file_item.Name = "file_item";
            this.file_item.Size = new System.Drawing.Size(51, 24);
            this.file_item.Text = "文件";
            // 
            // saveFile_item
            // 
            this.saveFile_item.Name = "saveFile_item";
            this.saveFile_item.Size = new System.Drawing.Size(138, 24);
            this.saveFile_item.Text = "保存文件";
            this.saveFile_item.Click += new System.EventHandler(this.saveFile_item_Click);
            // 
            // resultPma_item
            // 
            this.resultPma_item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.PMA_item });
            this.resultPma_item.Name = "resultPma_item";
            this.resultPma_item.Size = new System.Drawing.Size(51, 24);
            this.resultPma_item.Text = "结果";
            // 
            // PMA_item
            // 
            this.PMA_item.Name = "PMA_item";
            this.PMA_item.Size = new System.Drawing.Size(173, 24);
            this.PMA_item.Text = "查看PMA结果";
            // 
            // ToolBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 659);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ToolBlock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToolBlock";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ToolStripMenuItem file_item;
        private System.Windows.Forms.ToolStripMenuItem saveFile_item;

        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;

        private System.Windows.Forms.ToolStripMenuItem PMA_item;

        private System.Windows.Forms.ToolStripMenuItem resultPma_item;

        private System.Windows.Forms.MenuStrip menuStrip1;

        private System.Windows.Forms.Panel panel3;

        private System.Windows.Forms.Panel panel1;

        #endregion
    }
}