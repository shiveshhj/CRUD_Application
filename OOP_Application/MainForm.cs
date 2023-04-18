using OOP_Application.Factories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OOP_Application
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public static List<Vehicle> vehicles = new List<Vehicle>();

        private void DisplayVehicles()
        {
            while (vehiclesGV.Rows.Count > 0)
                vehiclesGV.Rows.RemoveAt(0);
            int i = 1;
            foreach (Vehicle vehicle in vehicles)
            {
                vehiclesGV.Rows.Add(new object[] {
                    i,
                    vehicle.GetType().Name,
                    vehicle.Brand,
                    vehicle.Price,
                    vehicle.SeatsAmount
                });
                i++;
            }
            if (vehiclesGV.RowCount == 0)
            {
                editButton.Enabled = false;
                viewButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            AddForm.mode = AddForm.ADD_MODE;
            addForm.ShowDialog();
            DisplayVehicles();
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            AddForm.mode = AddForm.VIEW_MODE;
            AddForm.currentVehicle = vehicles[vehiclesGV.CurrentRow.Index];
            addForm.ShowDialog();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            AddForm.mode = AddForm.EDIT_MODE;
            AddForm.currentVehicle = vehicles[vehiclesGV.CurrentRow.Index];
            AddForm.editIndex = vehiclesGV.CurrentRow.Index;
            addForm.ShowDialog();
            DisplayVehicles();
        }
        private void vehiclesGV_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            viewButton.Enabled = editButton.Enabled = deleteButton.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Car car = new Car(Car.CarType.Coupe, 4, 1000, "brrr", 500, 2022, 5, new Driver("John", 35, Driver.Category.B), new List<Passenger> { new Passenger(20, "Bob", 20) });
            Plane plane = new Plane(Plane.PlaneType.Passenger, 2000, "scrr", 1500, 2020, 50, new Driver("Bill", 30, Driver.Category.F), new List<Passenger> { new Passenger(22, "Alice", 21), new Passenger(25, "Cob", 21) });
            vehicles.Add(car);
            vehicles.Add(plane);
            DisplayVehicles();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Are you sure?", "Delete vehicle", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            Vehicle temp = vehicles[vehiclesGV.CurrentRow.Index];
            vehicles.RemoveAt(vehiclesGV.CurrentRow.Index);
            temp.driver = null;
            for (int i = 0; i < temp.passengers.Count; i++)
            {
                temp.passengers[i] = null;
            }
            temp.passengers = null;
            temp = null;
            vehiclesGV.Rows.RemoveAt(vehiclesGV.CurrentRow.Index);
            if (vehiclesGV.Rows.Count == 0)
            {
                viewButton.Enabled = editButton.Enabled = deleteButton.Enabled = false;
            }
            DisplayVehicles();
        }

        public enum SerializationType
        {
            [Name("Text files|*.txt")]
            Text,
            [Name("Binary files|*.bin")]
            Binary,
            [Name("Xml files|*.xml")]
            Xml
        }

        readonly Dictionary<SerializationType, Type> serializers = new Dictionary<SerializationType, Type>(){
            { SerializationType.Text, typeof(TextFactory) },
            { SerializationType.Binary, typeof(BinaryFactory) },
            { SerializationType.Xml, typeof(XmlFactory) }
        };

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = GetFileFilter();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            OpenFile(openFileDialog.FileName);
        }

        public void OpenFile(string fileName)
        {
            var serializerType = (SerializationType)(openFileDialog.FilterIndex - 1);
            SerializerFactory serializerFactory = (SerializerFactory)Activator.CreateInstance(serializers[serializerType]);
            ISerializer serializer = serializerFactory.CreateSerializer();
            try
            {
                vehicles = serializer.Deserialize(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DisplayVehicles();
        }

        private string GetFileFilter()
        {
            string filter = "";
            var fields = typeof(SerializationType).GetFields();
            for (int i = 1; i < fields.Length; i++)
            {
                var field = fields[i];
                filter += ((NameAttribute)field.GetCustomAttributes(typeof(NameAttribute), false)[0]).Name + "|";
            }
            return filter.Remove(filter.Length - 1, 1);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = GetFileFilter();
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            var serializerType = (SerializationType)(saveFileDialog.FilterIndex - 1);
            string filename = saveFileDialog.FileName;
            SerializerFactory serializerFactory = (SerializerFactory)Activator.CreateInstance(serializers[serializerType]);
            ISerializer serializer = serializerFactory.CreateSerializer();
            serializer.Serialize(vehicles, filename);
        }
    }

}
