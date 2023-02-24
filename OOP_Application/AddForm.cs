using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace OOP_Application
{
    public partial class AddForm : Form
    {
        /*private Label brandLabel = new Label();
        private Label priceLabel = new Label();
        private Label seatsLabel = new Label();
        private Label yearLabel = new Label();
        private Label driverLabel = new Label();
        private Label nameLabel = new Label();
        private Label ageLabel = new Label();
        private Label categoriesLabel = new Label();*/
        private Label passengersLabel = new Label();

        /*private TextBox brandTextBox = new TextBox(); 
        private NumericUpDown priceNumericUpDown = new NumericUpDown(); 
        private NumericUpDown seatsNumericUpDown = new NumericUpDown();
        private NumericUpDown yearNumericUpDown = new NumericUpDown();
        private TextBox nameTextBox = new TextBox();
        private NumericUpDown ageNumericUpDown = new NumericUpDown();
        private ComboBox categoryComboBox = new ComboBox();*/

        public AddForm()
        {
            InitializeComponent();
        }

        private void CreateLabel(Label label, string name, float fontSize, FontStyle fontStyle)
        {
            labelsPanel.Controls.Add(label);
            label.Dock = DockStyle.Top;
            label.Font = new Font("Century Gothic", fontSize, fontStyle | FontStyle.Italic, GraphicsUnit.Point, ((byte)(204)));
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
            textBox.Font = new Font("Century Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            fieldsPanel.Controls.SetChildIndex(textBox, 0);
        }
         
        private void CreateNumericUpDown(NumericUpDown numericUpDown, string name, int maxValue, int count)
        {
            fieldsPanel.Controls.Add(numericUpDown);
            numericUpDown.Location = new Point(0, 46 + 36 * count + (count > 0 ? 15 * count : 0));
            numericUpDown.Font = new Font("Century Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            numericUpDown.Name = name + "NumericUpDown";
            numericUpDown.Size = new Size(217, 36);
            numericUpDown.Maximum = maxValue;
            numericUpDown.Minimum = (maxValue == 2023) ? 1900 : 0;
            numericUpDown.Value = (maxValue == 2023) ? 2023 : 0;
            fieldsPanel.Controls.SetChildIndex(numericUpDown, 0);
        }

        private void CreateComboBox(ComboBox comboBox, string name, string[] items, int count)
        {
            fieldsPanel.Controls.Add(comboBox);
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(items);
            comboBox.Location = new Point(0, 46 + 36 * count + (count > 0 ? 15 * count : 0));
            comboBox.Name = name + "ComboBox";
            comboBox.Size = new System.Drawing.Size(217, 36);
            comboBox.TabIndex = 0;
            fieldsPanel.Controls.SetChildIndex(comboBox, 0);
        }

        private void CreateControl(FieldInfo field, ref int count)
        {
            if (field.FieldType == typeof(String))
            {
                CreateTextBox(new TextBox(), field.Name, count++);
                return;
            }
            if (field.FieldType == typeof(int))
            {
                CreateNumericUpDown(new NumericUpDown(), field.Name, Int32.MaxValue, count++);
                return;
            }
            if (field.FieldType == typeof(Car.CarType))
            {   
                CreateComboBox(new ComboBox(), field.Name, new string[] { "Hatchback", "Sedan", "Pickup", "Coupe", "Minivan", "StationWagon" }, count++);
            }
            else
            {
                CreateComboBox(new ComboBox(), field.Name, new string[] { "Passenger", "Cargo", "Millitary" }, count++);
            }
        }

        private void CreateSpecificControls(ref int count)
        {
            FieldInfo[] thisFields = GetThisFields();
            foreach (var field in thisFields)
            {
                object[] MyAttribute = field.GetCustomAttributes(typeof(NameAttribute), false);
                CreateLabel(new Label(), ((NameAttribute)(MyAttribute[0])).Name, 13.8F, FontStyle.Bold);
                CreateControl(field, ref count);
            }
        }

        private FieldInfo[] GetThisFields()
        {
            Type type = Type.GetType("OOP_Application." + typeComboBox.Text);
            var obj = Activator.CreateInstance(type);
            var allFields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var baseFileds = obj.GetType().BaseType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo[] thisFields = new FieldInfo[allFields.Length - baseFileds.Length];
            for (int i = 0; i < thisFields.Length; i++)
                thisFields[i] = allFields[i];
            return thisFields;
        }
        private void CreateLayout()
        {
            int count = 0;
            CreateLabel(new Label(), "Brand", 13.8F, FontStyle.Bold);
            CreateTextBox(new TextBox(), "brand", count++);
            CreateLabel(new Label(), "Price", 13.8F, FontStyle.Bold);
            CreateNumericUpDown(new NumericUpDown(), "price", Int32.MaxValue, count++);
            CreateLabel(new Label(), "Seats", 13.8F, FontStyle.Bold);
            CreateNumericUpDown(new NumericUpDown(), "seats", Int32.MaxValue, count++);
            CreateLabel(new Label(), "Year", 13.8F, FontStyle.Bold);
            CreateNumericUpDown(new NumericUpDown(), "year", 2023, count++);
            CreateSpecificControls(ref count);
            CreateLabel(new Label(), "Driver", 13.8F, FontStyle.Bold);
            count++;
            CreateLabel(new Label(), "Driver's name", 10.8F, FontStyle.Italic);
            CreateTextBox(new TextBox(), "brand", count++);
            CreateLabel(new Label(), "Driver's age", 10.8F, FontStyle.Italic);
            CreateNumericUpDown(new NumericUpDown(), "age", 150, count++);
            CreateLabel(new Label(), "Driver's category", 10.8F, FontStyle.Italic);
            CreateComboBox(new ComboBox(), "category", new string[] { "A", "B", "C", "D", "F", "I", "PPL", "FAA" }, count++);
            CreateLabel(passengersLabel, "Passengers", 13.8F, FontStyle.Bold);
            passengersGV.Visible = true;
            passengersGV.Location = passengersLabel.Location;
        }

        private void typeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            CreateLayout();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }
    }
}
