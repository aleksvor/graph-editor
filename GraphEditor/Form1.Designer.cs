namespace GraphEditor
{
    partial class Form1
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
            this.selectButton = new System.Windows.Forms.Button();
            this.deleteALLButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.drawEdgeButton = new System.Windows.Forms.Button();
            this.drawVertexButton = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sheet = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(11, 41);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(100, 45);
            this.selectButton.TabIndex = 14;
            this.selectButton.Text = "Выделить";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // deleteALLButton
            // 
            this.deleteALLButton.Location = new System.Drawing.Point(12, 245);
            this.deleteALLButton.Name = "deleteALLButton";
            this.deleteALLButton.Size = new System.Drawing.Size(99, 45);
            this.deleteALLButton.TabIndex = 13;
            this.deleteALLButton.Text = "Удалить все";
            this.deleteALLButton.UseVisualStyleBackColor = true;
            this.deleteALLButton.Click += new System.EventHandler(this.deleteALLButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(12, 194);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(99, 45);
            this.deleteButton.TabIndex = 12;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // drawEdgeButton
            // 
            this.drawEdgeButton.Location = new System.Drawing.Point(12, 143);
            this.drawEdgeButton.Name = "drawEdgeButton";
            this.drawEdgeButton.Size = new System.Drawing.Size(100, 45);
            this.drawEdgeButton.TabIndex = 11;
            this.drawEdgeButton.Text = "Ребро";
            this.drawEdgeButton.UseVisualStyleBackColor = true;
            this.drawEdgeButton.Click += new System.EventHandler(this.drawEdgeButton_Click);
            // 
            // drawVertexButton
            // 
            this.drawVertexButton.Location = new System.Drawing.Point(11, 92);
            this.drawVertexButton.Name = "drawVertexButton";
            this.drawVertexButton.Size = new System.Drawing.Size(45, 45);
            this.drawVertexButton.TabIndex = 10;
            this.drawVertexButton.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(612, 19);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(244, 288);
            this.propertyGrid1.TabIndex = 15;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(879, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.загрузитьToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // sheet
            // 
            this.sheet.BackColor = System.Drawing.SystemColors.Window;
            this.sheet.Location = new System.Drawing.Point(122, 34);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(472, 415);
            this.sheet.TabIndex = 18;
            this.sheet.TabStop = false;
            this.sheet.Click += new System.EventHandler(this.sheet_Click);
            this.sheet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 482);
            this.Controls.Add(this.sheet);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.deleteALLButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.drawEdgeButton);
            this.Controls.Add(this.drawVertexButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button deleteALLButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button drawEdgeButton;
        private System.Windows.Forms.Button drawVertexButton;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.PictureBox sheet;


    }
}

