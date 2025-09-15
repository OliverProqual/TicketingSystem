namespace TicketingSystem
{
    partial class OperatorDashBoard
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.YourTickets = new System.Windows.Forms.Label();
            this.UnassignedLabel = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Logout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(469, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "Operator Dashboard";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(57, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(182, 264);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // YourTickets
            // 
            this.YourTickets.AutoSize = true;
            this.YourTickets.Location = new System.Drawing.Point(57, 81);
            this.YourTickets.Name = "YourTickets";
            this.YourTickets.Size = new System.Drawing.Size(82, 16);
            this.YourTickets.TabIndex = 2;
            this.YourTickets.Text = "Your Tickets";
            // 
            // UnassignedLabel
            // 
            this.UnassignedLabel.AutoSize = true;
            this.UnassignedLabel.Location = new System.Drawing.Point(400, 81);
            this.UnassignedLabel.Name = "UnassignedLabel";
            this.UnassignedLabel.Size = new System.Drawing.Size(127, 16);
            this.UnassignedLabel.TabIndex = 4;
            this.UnassignedLabel.Text = "Unassigned Tickets";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(403, 123);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(182, 264);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
            // 
            // Logout
            // 
            this.Logout.Location = new System.Drawing.Point(658, 338);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(92, 75);
            this.Logout.TabIndex = 5;
            this.Logout.Text = "Log out";
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // OperatorDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.UnassignedLabel);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.YourTickets);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "OperatorDashBoard";
            this.Text = "OperatorDashBoard";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label YourTickets;
        private System.Windows.Forms.Label UnassignedLabel;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button Logout;
    }
}