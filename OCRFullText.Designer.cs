
namespace ax_tool
{
    partial class OCRFullText
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtImageFolder = new System.Windows.Forms.TextBox();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnOpenImageFolder = new System.Windows.Forms.Button();
            this.btnOpenOutputFolder = new System.Windows.Forms.Button();
            this.progressBarOCR = new System.Windows.Forms.ProgressBar();
            this.btnRunOCR = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.backgroundWorkerOCR = new System.ComponentModel.BackgroundWorker();
            this.btnSelectPreprocessedFolder = new System.Windows.Forms.Button();
            this.txtPreprocessedImageFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOCRServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(305, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Compare OCR Quality";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Orginal Image folder";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 275);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Output folder";
            // 
            // txtImageFolder
            // 
            this.txtImageFolder.Location = new System.Drawing.Point(39, 171);
            this.txtImageFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtImageFolder.Name = "txtImageFolder";
            this.txtImageFolder.Size = new System.Drawing.Size(623, 27);
            this.txtImageFolder.TabIndex = 3;
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(39, 300);
            this.txtOutputFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(623, 27);
            this.txtOutputFolder.TabIndex = 4;
            // 
            // btnOpenImageFolder
            // 
            this.btnOpenImageFolder.Location = new System.Drawing.Point(689, 171);
            this.btnOpenImageFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenImageFolder.Name = "btnOpenImageFolder";
            this.btnOpenImageFolder.Size = new System.Drawing.Size(94, 29);
            this.btnOpenImageFolder.TabIndex = 5;
            this.btnOpenImageFolder.Text = "Select";
            this.btnOpenImageFolder.UseVisualStyleBackColor = true;
            this.btnOpenImageFolder.Click += new System.EventHandler(this.btnOpenImageFolder_Click);
            // 
            // btnOpenOutputFolder
            // 
            this.btnOpenOutputFolder.Location = new System.Drawing.Point(689, 300);
            this.btnOpenOutputFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenOutputFolder.Name = "btnOpenOutputFolder";
            this.btnOpenOutputFolder.Size = new System.Drawing.Size(94, 29);
            this.btnOpenOutputFolder.TabIndex = 6;
            this.btnOpenOutputFolder.Text = "Select";
            this.btnOpenOutputFolder.UseVisualStyleBackColor = true;
            this.btnOpenOutputFolder.Click += new System.EventHandler(this.btnOpenOutputFolder_Click);
            // 
            // progressBarOCR
            // 
            this.progressBarOCR.Location = new System.Drawing.Point(39, 366);
            this.progressBarOCR.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarOCR.Name = "progressBarOCR";
            this.progressBarOCR.Size = new System.Drawing.Size(624, 29);
            this.progressBarOCR.TabIndex = 7;
            // 
            // btnRunOCR
            // 
            this.btnRunOCR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunOCR.Location = new System.Drawing.Point(689, 366);
            this.btnRunOCR.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRunOCR.Name = "btnRunOCR";
            this.btnRunOCR.Size = new System.Drawing.Size(94, 39);
            this.btnRunOCR.TabIndex = 9;
            this.btnRunOCR.Text = "Run";
            this.btnRunOCR.UseVisualStyleBackColor = true;
            this.btnRunOCR.Click += new System.EventHandler(this.btnRunOCR_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(35, 341);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(77, 20);
            this.lbStatus.TabIndex = 10;
            this.lbStatus.Text = "Progress";
            // 
            // backgroundWorkerOCR
            // 
            this.backgroundWorkerOCR.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerOCR_DoWork);
            this.backgroundWorkerOCR.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerOCR_ProgressChanged);
            this.backgroundWorkerOCR.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerOCR_RunWorkerCompleted);
            // 
            // btnSelectPreprocessedFolder
            // 
            this.btnSelectPreprocessedFolder.Location = new System.Drawing.Point(689, 236);
            this.btnSelectPreprocessedFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectPreprocessedFolder.Name = "btnSelectPreprocessedFolder";
            this.btnSelectPreprocessedFolder.Size = new System.Drawing.Size(94, 29);
            this.btnSelectPreprocessedFolder.TabIndex = 13;
            this.btnSelectPreprocessedFolder.Text = "Select";
            this.btnSelectPreprocessedFolder.UseVisualStyleBackColor = true;
            this.btnSelectPreprocessedFolder.Click += new System.EventHandler(this.btnSelectPreprocessedFolder_Click);
            // 
            // txtPreprocessedImageFolder
            // 
            this.txtPreprocessedImageFolder.Location = new System.Drawing.Point(39, 236);
            this.txtPreprocessedImageFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPreprocessedImageFolder.Name = "txtPreprocessedImageFolder";
            this.txtPreprocessedImageFolder.Size = new System.Drawing.Size(623, 27);
            this.txtPreprocessedImageFolder.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 211);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(216, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Pre-processed image folder";
            // 
            // txtOCRServer
            // 
            this.txtOCRServer.Location = new System.Drawing.Point(39, 101);
            this.txtOCRServer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOCRServer.Name = "txtOCRServer";
            this.txtOCRServer.Size = new System.Drawing.Size(623, 27);
            this.txtOCRServer.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 76);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(265, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "AX Server (Set empty to run local)";
            // 
            // OCRFullText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 433);
            this.Controls.Add(this.txtOCRServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSelectPreprocessedFolder);
            this.Controls.Add(this.txtPreprocessedImageFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnRunOCR);
            this.Controls.Add(this.progressBarOCR);
            this.Controls.Add(this.btnOpenOutputFolder);
            this.Controls.Add(this.btnOpenImageFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.txtImageFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OCRFullText";
            this.Text = "AX OCR Compare";
            this.Load += new System.EventHandler(this.OCRFullText_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtImageFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnOpenImageFolder;
        private System.Windows.Forms.Button btnOpenOutputFolder;
        private System.Windows.Forms.ProgressBar progressBarOCR;
        private System.Windows.Forms.Button btnRunOCR;
        private System.Windows.Forms.Label lbStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorkerOCR;
        private System.Windows.Forms.Button btnSelectPreprocessedFolder;
        private System.Windows.Forms.TextBox txtPreprocessedImageFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOCRServer;
        private System.Windows.Forms.Label label4;
    }
}

