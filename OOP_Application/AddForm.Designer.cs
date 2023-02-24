
namespace OOP_Application
{
    partial class AddForm
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
            this.captionLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelsPanel = new System.Windows.Forms.Panel();
            this.typeLabel = new System.Windows.Forms.Label();
            this.fieldsPanel = new System.Windows.Forms.Panel();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.passengersGV = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bagWeightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.labelsPanel.SuspendLayout();
            this.fieldsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.passengersGV)).BeginInit();
            this.SuspendLayout();
            // 
            // captionLabel
            // 
            this.captionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.captionLabel.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.captionLabel.Location = new System.Drawing.Point(0, 0);
            this.captionLabel.Name = "captionLabel";
            this.captionLabel.Size = new System.Drawing.Size(697, 34);
            this.captionLabel.TabIndex = 2;
            this.captionLabel.Text = "Add vehicle";
            this.captionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 632);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(697, 51);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelsPanel);
            this.panel2.Controls.Add(this.fieldsPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(697, 598);
            this.panel2.TabIndex = 6;
            // 
            // labelsPanel
            // 
            this.labelsPanel.Controls.Add(this.typeLabel);
            this.labelsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelsPanel.Location = new System.Drawing.Point(0, 0);
            this.labelsPanel.Name = "labelsPanel";
            this.labelsPanel.Size = new System.Drawing.Size(257, 598);
            this.labelsPanel.TabIndex = 5;
            // 
            // typeLabel
            // 
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.typeLabel.Font = new System.Drawing.Font("Century Gothic", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.typeLabel.Location = new System.Drawing.Point(0, 0);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(257, 50);
            this.typeLabel.TabIndex = 3;
            this.typeLabel.Text = "Type";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // fieldsPanel
            // 
            this.fieldsPanel.Controls.Add(this.passengersGV);
            this.fieldsPanel.Controls.Add(this.typeComboBox);
            this.fieldsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.fieldsPanel.Location = new System.Drawing.Point(263, 0);
            this.fieldsPanel.Name = "fieldsPanel";
            this.fieldsPanel.Size = new System.Drawing.Size(434, 598);
            this.fieldsPanel.TabIndex = 6;
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Car",
            "Truck",
            "Plane",
            "Helicopter"});
            this.typeComboBox.Location = new System.Drawing.Point(0, 0);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(217, 31);
            this.typeComboBox.TabIndex = 0;
            this.typeComboBox.SelectedValueChanged += new System.EventHandler(this.typeComboBox_SelectedValueChanged);
            // 
            // passengersGV
            // 
            this.passengersGV.AllowUserToResizeColumns = false;
            this.passengersGV.AllowUserToResizeRows = false;
            this.passengersGV.BackgroundColor = System.Drawing.SystemColors.Control;
            this.passengersGV.ColumnHeadersHeight = 35;
            this.passengersGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.passengersGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.ageColumn,
            this.bagWeightColumn});
            this.passengersGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.passengersGV.Location = new System.Drawing.Point(0, 448);
            this.passengersGV.MultiSelect = false;
            this.passengersGV.Name = "passengersGV";
            this.passengersGV.RowHeadersWidth = 10;
            this.passengersGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.passengersGV.RowTemplate.Height = 24;
            this.passengersGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.passengersGV.Size = new System.Drawing.Size(407, 109);
            this.passengersGV.TabIndex = 1;
            this.passengersGV.Visible = false;
            // 
            // nameColumn
            // 
            this.nameColumn.Frozen = true;
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.MinimumWidth = 6;
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.Width = 125;
            // 
            // ageColumn
            // 
            this.ageColumn.Frozen = true;
            this.ageColumn.HeaderText = "Age";
            this.ageColumn.MinimumWidth = 6;
            this.ageColumn.Name = "ageColumn";
            this.ageColumn.Width = 125;
            // 
            // bagWeightColumn
            // 
            this.bagWeightColumn.Frozen = true;
            this.bagWeightColumn.HeaderText = "Bag weight";
            this.bagWeightColumn.MinimumWidth = 6;
            this.bagWeightColumn.Name = "bagWeightColumn";
            this.bagWeightColumn.Width = 125;
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(697, 683);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.captionLabel);
            this.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add vehicle";
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.panel2.ResumeLayout(false);
            this.labelsPanel.ResumeLayout(false);
            this.fieldsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.passengersGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label captionLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel labelsPanel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Panel fieldsPanel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.DataGridView passengersGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bagWeightColumn;
    }
}