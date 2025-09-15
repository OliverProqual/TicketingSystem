namespace TicketingSystem
{
    partial class UnassignedTicketView
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
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblCompleted = new System.Windows.Forms.Label();
            this.lblAccepted = new System.Windows.Forms.Label();
            this.lblRequested = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.Label();
            this.lblTicketId = new System.Windows.Forms.Label();
            this.Accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(537, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "Unassigned Ticket";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(503, 236);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 24);
            this.cmbStatus.TabIndex = 22;
            // 
            // lblCompleted
            // 
            this.lblCompleted.AutoSize = true;
            this.lblCompleted.Location = new System.Drawing.Point(299, 236);
            this.lblCompleted.Name = "lblCompleted";
            this.lblCompleted.Size = new System.Drawing.Size(87, 16);
            this.lblCompleted.TabIndex = 19;
            this.lblCompleted.Text = "lblCompleted";
            // 
            // lblAccepted
            // 
            this.lblAccepted.AutoSize = true;
            this.lblAccepted.Location = new System.Drawing.Point(162, 236);
            this.lblAccepted.Name = "lblAccepted";
            this.lblAccepted.Size = new System.Drawing.Size(79, 16);
            this.lblAccepted.TabIndex = 18;
            this.lblAccepted.Text = "lblAccepted";
            // 
            // lblRequested
            // 
            this.lblRequested.AutoSize = true;
            this.lblRequested.Location = new System.Drawing.Point(500, 161);
            this.lblRequested.Name = "lblRequested";
            this.lblRequested.Size = new System.Drawing.Size(88, 16);
            this.lblRequested.TabIndex = 17;
            this.lblRequested.Text = "lblRequested";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(299, 161);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(55, 16);
            this.lblEmail.TabIndex = 16;
            this.lblEmail.Text = "lblEmail";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(162, 161);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(78, 16);
            this.lblCustomer.TabIndex = 15;
            this.lblCustomer.Text = "lblCustomer";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(497, 97);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 16);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "status";
            // 
            // txtDescription
            // 
            this.txtDescription.AutoSize = true;
            this.txtDescription.Location = new System.Drawing.Point(296, 96);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(87, 16);
            this.txtDescription.TabIndex = 13;
            this.txtDescription.Text = "txtDescription";
            // 
            // lblTicketId
            // 
            this.lblTicketId.AutoSize = true;
            this.lblTicketId.Location = new System.Drawing.Point(159, 97);
            this.lblTicketId.Name = "lblTicketId";
            this.lblTicketId.Size = new System.Drawing.Size(73, 16);
            this.lblTicketId.TabIndex = 12;
            this.lblTicketId.Text = "LblTicketId";
            // 
            // Accept
            // 
            this.Accept.Location = new System.Drawing.Point(302, 334);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(105, 56);
            this.Accept.TabIndex = 23;
            this.Accept.Text = "Accept";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // UnassignedTicketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblCompleted);
            this.Controls.Add(this.lblAccepted);
            this.Controls.Add(this.lblRequested);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblTicketId);
            this.Controls.Add(this.label1);
            this.Name = "UnassignedTicketView";
            this.Text = "UnassignedTicketView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblCompleted;
        private System.Windows.Forms.Label lblAccepted;
        private System.Windows.Forms.Label lblRequested;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label txtDescription;
        private System.Windows.Forms.Label lblTicketId;
        private System.Windows.Forms.Button Accept;
    }
}