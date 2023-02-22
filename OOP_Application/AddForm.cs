using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Application
{
    public partial class AddForm : Form
    {
        private Label brandLabel = new Label();
        private Label priceLabel = new Label();
        private Label seatsLabel = new Label();
        private Label driverLabel = new Label();
        private Label passengersLabel = new Label();

        private TextBox brandTextBox = new TextBox(); 
        private NumericUpDown priceNumericUpDown = new NumericUpDown(); 
        private NumericUpDown seatsNumericUpDown = new NumericUpDown();

        public AddForm()
        {
            InitializeComponent();
        }

        private void CreateLabel(Label label, string name)
        {
            labelsPanel.Controls.Add(label);
            label.Dock = DockStyle.Top;
            label.Font = new Font("Century Gothic", 13.8F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)));
            label.Location = new Point(0, 0);
            label.Name = name + "label";
            label.Size = new Size(223, 50);
            label.Margin = new Padding(0, 5, 0, 5);
            label.Text = name;
            label.TextAlign = ContentAlignment.TopRight;
            labelsPanel.Controls.SetChildIndex(label, 0);
        }

        private void CreateTextBox(TextBox textBox, string name, int count)
        {
            fieldsPanel.Controls.Add(textBox);
            textBox.Location = new Point(0, 46 + 36 * count + (count > 0 ? 15 * count : 0));
            textBox.Name = name + "TextBox";
            textBox.Size = new Size(217, 36);
            textBox.MaxLength = 15;
            fieldsPanel.Controls.SetChildIndex(textBox, 0);
        }
         
        private void CreateNumericUpDown(NumericUpDown numericUpDown, string name, int count)
        {
            fieldsPanel.Controls.Add(numericUpDown);
            numericUpDown.Location = new Point(0, 46 + 36 * count + (count > 0 ? 15 * count : 0));
            numericUpDown.Name = name + "NumericUpDown";
            numericUpDown.Size = new Size(217, 36);
            numericUpDown.Maximum = Int32.MaxValue;
            fieldsPanel.Controls.SetChildIndex(numericUpDown, 0);
        }

        private void CreateLayout()
        {
            int count = 0;
            CreateLabel(brandLabel, "Brand");
            CreateTextBox(brandTextBox, "brand", count++);
            CreateLabel(priceLabel, "Price");
            CreateNumericUpDown(priceNumericUpDown, "price", count++);
            CreateLabel(seatsLabel, "Seats");
            CreateNumericUpDown(seatsNumericUpDown, "seats", count++);
            CreateLabel(driverLabel, "Driver");
            CreateLabel(passengersLabel, "Passengers");
        }

        private void typeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            CreateLayout();
        }


    }
}
