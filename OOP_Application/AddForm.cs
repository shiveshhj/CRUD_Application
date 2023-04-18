using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;
using OOP_Application.Serializable_Classes;

namespace OOP_Application
{
    public partial class AddForm : Form
    {
        private Label passengersLabel = new Label();
        private DataGridView passengersGV = new DataGridView();
        public static int mode = ADD_MODE;
        public const int ADD_MODE = 0;
        public const int VIEW_MODE = 1;
        public const int EDIT_MODE = 2;
        public static Vehicle currentVehicle;
        public static int editIndex;
        private const int EXCEPT_PASSENGERS = 3;

        public AddForm()
        {
            InitializeComponent();
        }

        #region Creation of controls on the form according to object structure
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
            textBox.Size = new Size(407, 36);
            textBox.MaxLength = 15;
            textBox.Font = new Font("Century Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            textBox.KeyUp += new KeyEventHandler(this.Control_KeyUp);
            textBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
            fieldsPanel.Controls.SetChildIndex(textBox, 0);
        }

        private void CreateNumericUpDown(NumericUpDown numericUpDown, string name, int maxValue, int count)
        {
            fieldsPanel.Controls.Add(numericUpDown);
            numericUpDown.Location = new Point(0, 46 + 36 * count + (count > 0 ? 15 * count : 0));
            numericUpDown.Font = new Font("Century Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            numericUpDown.Name = name + "NumericUpDown";
            numericUpDown.Size = new Size(407, 36);
            numericUpDown.Maximum = maxValue;
            numericUpDown.Minimum = (maxValue == 2023) ? 1900 : 0;
            numericUpDown.Value = (maxValue == 2023) ? 2023 : 0;
            numericUpDown.KeyUp += new KeyEventHandler(this.Control_KeyUp);
            numericUpDown.Leave += new EventHandler(this.NumericUpDown_Leave);
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
            comboBox.Size = new Size(407, 36);
            comboBox.TabIndex = 0;
            comboBox.SelectedIndex = 0;
            comboBox.KeyUp += new KeyEventHandler(this.Control_KeyUp);
            fieldsPanel.Controls.SetChildIndex(comboBox, 0);
        }

        private void CreateDataGridView()
        {
            CreateLabel(passengersLabel, "Passengers", 13.8F, FontStyle.Bold);
            fieldsPanel.Controls.Add(passengersGV);
            passengersGV.AllowUserToResizeColumns = false;
            passengersGV.AllowUserToResizeRows = false;
            passengersGV.BackgroundColor = SystemColors.Control;
            passengersGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            passengersGV.ColumnHeadersHeight = 35;
            passengersGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            passengersGV.EditMode = DataGridViewEditMode.EditOnKeystroke;
            passengersGV.Location = new Point(0, 448);
            passengersGV.MultiSelect = false;
            passengersGV.Name = "passengersGV";
            passengersGV.RowHeadersWidth = 10;
            passengersGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            passengersGV.RowTemplate.Height = 24;
            passengersGV.ScrollBars = ScrollBars.Vertical;
            passengersGV.Size = new Size(407, 109);
            passengersGV.TabIndex = 1;
            passengersGV.CellValueChanged += new DataGridViewCellEventHandler(this.passengersGV_CellValueChanged);
            passengersGV.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.passengersGV_EditingControlShowing);
            passengersGV.KeyDown += new KeyEventHandler(this.passengersGV_KeyDown);
            passengersGV.KeyPress += new KeyPressEventHandler(this.passengersGV_KeyPress);
            passengersGV.Visible = true;
            passengersGV.Location = passengersLabel.Location;
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
            FieldInfo[] subFields = field.FieldType.GetFields();
            string[] strFields = new string[subFields.Length - 1];
            for (int i = 0; i < subFields.Length - 1; i++)
            {
                if (subFields[i + 1].Name == ("value__")) continue;
                strFields[i] = subFields[i + 1].Name;
            }
            CreateComboBox(new ComboBox(), field.Name, strFields, count++);
        }

        private void CreateSpecificControls()
        {
            int count = 0;
            FieldInfo[] thisFields = GetThisFields();
            for (int i = 0; i < thisFields.Length; i++)
            {
                var field = thisFields[i];
                if (field.FieldType == typeof(Driver))
                {
                    CreateLabel(new Label(), "Driver", 13.8F, FontStyle.Bold);
                    count++;
                    var driverFields = field.FieldType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    for (int j = driverFields.Length - 1; j >= 0; j--)
                    {
                        object[] attribute = driverFields[j].GetCustomAttributes(typeof(NameAttribute), false);
                        CreateLabel(new Label(), "Driver's " + ((NameAttribute)(attribute[0])).Name, 10.8F, FontStyle.Italic);
                        CreateControl(driverFields[j], ref count);
                    }
                }
                else
                if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    CreateDataGridView();
                    continue;
                }
                else
                {
                    object[] MyAttribute = field.GetCustomAttributes(typeof(NameAttribute), false);
                    CreateLabel(new Label(), ((NameAttribute)(MyAttribute[0])).Name, 13.8F, FontStyle.Bold);
                    CreateControl(field, ref count);
                }
            }
        }

        private FieldInfo[] GetThisFields()
        {
            Type type = Type.GetType("OOP_Application." + typeComboBox.Text);
            var allFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return allFields;
        }
        private void CreateLayout()
        {
            CreateSpecificControls();
            addButton.Visible = true;
            this.Size = new Size(this.Size.Width, passengersGV.Location.Y + 250);
            this.CenterToScreen();
        }
        #endregion

        #region Input validation
        private bool nonNumberEntered = false;
        private bool backspaceEntered = false;

        private bool IsInputCorrect()
        {
            for (int i = 0; i < fieldsPanel.Controls.Count; i++)
            {
                var control = fieldsPanel.Controls[i];
                Type controlType = control.GetType();
                if (controlType == typeof(TextBox) && ((control as TextBox).Text.Length == 0 || (control as TextBox).Text.Length > 15))
                    return false;
                if (controlType == typeof(NumericUpDown) && ((control as NumericUpDown).Value > Int32.MaxValue))
                    return false;
                if (controlType == typeof(ComboBox) && ((control as ComboBox).SelectedIndex == -1))
                    return false;
            }
            if (passengersGV.RowCount == 1)
                return false;
            for (int i = 0; i < passengersGV.RowCount - 1; i++)
            {
                if (passengersGV.Rows[i].Cells[0].Value == null || passengersGV.Rows[i].Cells[1].Value == null || passengersGV.Rows[i].Cells[2].Value == null)
                    return false;
                if (passengersGV.Rows[i].Cells[0].Value.ToString().Length > 11 || passengersGV.Rows[i].Cells[0].Value.ToString().Length == 0)
                    return false;
                if (passengersGV.Rows[i].Cells[1].Value.ToString().Length == 0)
                    return false;
                int value;
                try
                {
                    value = Int32.Parse(passengersGV.Rows[i].Cells[1].Value.ToString());
                }
                catch (Exception)
                {
                    return false;
                }
                if (value < 0 || value > Int32.MaxValue)
                    return false;
                try
                {
                    value = Int32.Parse(passengersGV.Rows[i].Cells[2].Value.ToString());
                }
                catch (Exception)
                {
                    return false;
                }
                if (value < 0 || value > Int32.MaxValue)
                    return false;

            }
            return true;
        }

        private void passengersGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl tb)
            {
                tb.KeyDown -= passengersGV_KeyDown;
                tb.KeyDown += passengersGV_KeyDown;
                tb.KeyPress -= passengersGV_KeyPress;
                tb.KeyPress += passengersGV_KeyPress;
            }
        }
        private void passengersGV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (backspaceEntered)
            {
                backspaceEntered = false;
                return;
            }
            switch (passengersGV.CurrentCell.ColumnIndex)
            {
                case 0:
                    {
                        if (!nonNumberEntered || (passengersGV.CurrentCell.EditedFormattedValue.ToString().Length > 10))
                            e.Handled = true;
                        break;
                    }
                case 1:
                    {
                        if (nonNumberEntered || ((string)passengersGV.CurrentCell.EditedFormattedValue != "" && Int32.Parse((string)passengersGV.CurrentCell.EditedFormattedValue) > 100))
                            e.Handled = true;
                        break;
                    }
                case 2:
                    {
                        if (nonNumberEntered || ((string)passengersGV.CurrentCell.EditedFormattedValue != "" && Int32.Parse((string)passengersGV.CurrentCell.EditedFormattedValue) > 100))
                            e.Handled = true;
                        break;
                    }
                default:
                    break;
            }
        }

        private void passengersGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                backspaceEntered = true;
            nonNumberEntered = false;
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                    {
                        nonNumberEntered = true;
                    }
                }
            }
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (mode != VIEW_MODE)
                addButton.Enabled = IsInputCorrect();
        }
        private void passengersGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (mode != VIEW_MODE)
                addButton.Enabled = IsInputCorrect();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z\s\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void NumericUpDown_Leave(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Text == "")
                (sender as NumericUpDown).Text = "0";
        }

        #endregion

        #region Creation of the new vehicle
        Dictionary<Type, Type> dictionary = new Dictionary<Type, Type>(){
            { typeof(Car), typeof(Factories.CarFactory) },
            { typeof(Truck), typeof(Factories.TruckFactory) },
            { typeof(Plane), typeof(Factories.PlaneFactory) },
            { typeof(Helicopter), typeof(Factories.HelicopterFactory) }
        };

        private void CreateObject(bool add)
        {
            Type type = Type.GetType("OOP_Application." + typeComboBox.SelectedItem.ToString());
            Factories.VehicleFactory vehicleFactory = (Factories.VehicleFactory)Activator.CreateInstance(dictionary[type]);
            List<object> fields = new List<object>();
            for (int i = fieldsPanel.Controls.Count - EXCEPT_PASSENGERS; i > -1; i--)
            {
                fields.Add(fieldsPanel.Controls[i].Text);
            }
            fields.Add(CreatePassengers());
            Vehicle vehicle = vehicleFactory.createVehicle(fields);
            if (add)
            {
                MainForm.vehicles.Add(vehicle);
            }
            else
            {
                MainForm.vehicles[editIndex] = vehicle;
            }
        }

        private List<Passenger> CreatePassengers()
        {
            List<Passenger> passengers = new List<Passenger>();
            for (int i = 0; i < passengersGV.RowCount - 1; i++)
            {
                passengers.Add(new Passenger(Int32.Parse(passengersGV.Rows[i].Cells[2].Value.ToString()), passengersGV.Rows[i].Cells[0].Value.ToString(), Int32.Parse(passengersGV.Rows[i].Cells[1].Value.ToString())));
            }
            return passengers;
        }
        #endregion

        #region Filling in fields of the created vehicle
        private void FullfillFields(bool enable)
        {
            string itemName = currentVehicle.GetType().ToString();
            itemName = itemName.Substring(itemName.LastIndexOf('.') + 1);
            foreach (var item in typeComboBox.Items)
            {
                if ((string)item == itemName)
                {
                    typeComboBox.SelectedItem = item;
                }
            }
            FillDriverFields(enable);
            FillPassengersFields(enable);
            foreach (var field in currentVehicle.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                string componentName;
                if (field.FieldType == typeof(String))
                {
                    componentName = field.Name + "TextBox";
                    var textBox = GetComponentByName(componentName);
                    (textBox as TextBox).Text = (string)currentVehicle.GetType().GetField(field.Name).GetValue(currentVehicle);
                    (textBox as TextBox).Enabled = enable;

                }
                else if (field.FieldType == typeof(int))
                {
                    componentName = field.Name + "NumericUpDown";
                    var numericUpDown = GetComponentByName(componentName);
                    (numericUpDown as NumericUpDown).Value = (int)currentVehicle.GetType().GetField(field.Name).GetValue(currentVehicle);
                    (numericUpDown as NumericUpDown).Enabled = enable;
                }
                else if (field.FieldType == typeof(Car.CarType) || field.FieldType == typeof(Plane.PlaneType))
                {
                    componentName = field.Name + "ComboBox";
                    var comboBox = GetComponentByName(componentName);
                    foreach (var item in (comboBox as ComboBox).Items)
                    {
                        var val = currentVehicle.GetType().GetField(field.Name).GetValue(currentVehicle);
                        if ((string)item == val.ToString())
                        {
                            (comboBox as ComboBox).SelectedItem = item;
                        }
                    }
                    (comboBox as ComboBox).Enabled = enable;
                }
            }
        }

        private void FillDriverFields(bool enable)
        {
            var nameTextBox = (TextBox)GetComponentByName("nameTextBox");
            var ageNumericUpDown = (NumericUpDown)GetComponentByName("ageNumericUpDown");
            var categoryComboBox = (ComboBox)GetComponentByName("categoryComboBox");
            nameTextBox.Text = currentVehicle.driver.name;
            nameTextBox.Enabled = enable;
            ageNumericUpDown.Value = currentVehicle.driver.age;
            ageNumericUpDown.Enabled = enable;
            foreach (var item in categoryComboBox.Items)
                if ((string)item == currentVehicle.driver.category.ToString())
                    categoryComboBox.SelectedItem = item;
            categoryComboBox.Enabled = enable;
        }

        private void FillPassengersFields(bool enable)
        {
            passengersGV.AllowUserToDeleteRows = enable;
            passengersGV.EditMode = enable ? DataGridViewEditMode.EditOnKeystroke : DataGridViewEditMode.EditProgrammatically;
            foreach (var passenger in currentVehicle.passengers)
            {
                passengersGV.Rows.Add(new object[] { passenger.name, passenger.age, passenger.bagWeight });
            }
        }

        private Component GetComponentByName(string name)
        {
            for (int i = 0; i < fieldsPanel.Controls.Count; i++)
            {
                if (name == fieldsPanel.Controls[i].Name)
                    return fieldsPanel.Controls[i];
            }
            return null;
        }
        #endregion

        private void DeleteOldLayout()
        {
            int count = labelsPanel.Controls.Count;
            for (int i = 0; i < count && labelsPanel.Controls.Count > 1; i++)
            {
                Control c = labelsPanel.Controls[1];
                if (c != passengersLabel && c != typeLabel)
                {
                    labelsPanel.Controls.Remove(c);
                    c.Dispose();
                }
            }
            count = fieldsPanel.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                Control c = fieldsPanel.Controls[0];
                if (c != passengersGV && c != typeComboBox)
                {
                    fieldsPanel.Controls.Remove(c);
                    c.Dispose();
                }
            }
        }

        private void typeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            labelsPanel.Visible = fieldsPanel.Visible = false;
            DeleteOldLayout();
            CreateLayout();
            labelsPanel.Visible = fieldsPanel.Visible = true;
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            FillTypeComboBox();
            passengersGV.Columns.AddRange(new DataGridViewColumn[] { nameColumn, ageColumn, bagWeightColumn});
            switch (mode)
            {
                case EDIT_MODE:
                    {
                        captionLabel.Text = "Edit vehicle";
                        addButton.Text = "Confirm changes";
                        addButton.Enabled = true;
                        FullfillFields(true);
                        break;
                    }
                case VIEW_MODE:
                    {
                        captionLabel.Text = "View vehicle";
                        addButton.Text = "Go back";
                        addButton.Enabled = true;
                        typeComboBox.Enabled = false;
                        FullfillFields(false);
                        break;
                    }
            }
        }

        private void FillTypeComboBox()
        {
            foreach (var type in dictionary.Keys)
            {
                object[] MyAttribute = type.GetCustomAttributes(typeof(NameAttribute), false);
                typeComboBox.Items.Add(((NameAttribute)(MyAttribute[0])).Name);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            switch (mode)
            {
                case ADD_MODE:
                    {
                        CreateObject(true);
                        break;
                    }
                case EDIT_MODE:
                    {
                        CreateObject(false);
                        break;
                    }
            }
            Close();
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
