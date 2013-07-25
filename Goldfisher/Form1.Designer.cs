namespace Goldfisher
{
	partial class FormGoldfisher
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
            this.btnBelcher = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBelcher
            // 
            this.btnBelcher.Location = new System.Drawing.Point(10, 10);
            this.btnBelcher.Name = "btnBelcher";
            this.btnBelcher.Size = new System.Drawing.Size(75, 30);
            this.btnBelcher.TabIndex = 0;
            this.btnBelcher.Text = "Belcher";
            this.btnBelcher.UseVisualStyleBackColor = true;
            this.btnBelcher.Click += new System.EventHandler(this.btnBelcher_Click);
            // 
            // FormGoldfisher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 363);
            this.Controls.Add(this.btnBelcher);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGoldfisher";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goldfisher";
            this.Load += new System.EventHandler(this.FormGoldfisher_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnBelcher;
	}
}

