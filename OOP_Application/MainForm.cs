using OOP_Application.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            AddForm.mode = AddForm.ADD_MODE;
            addForm.ShowDialog();
            DisplayVehicles();
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            AddForm.mode = AddForm.VIEW_MODE;
            AddForm.currentVehicle = vehicles[vehiclesGV.CurrentRow.Index];
            addForm.ShowDialog();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            AddForm.mode = AddForm.EDIT_MODE;
            AddForm.currentVehicle = vehicles[vehiclesGV.CurrentRow.Index];
            AddForm.editIndex = vehiclesGV.CurrentRow.Index;
            addForm.ShowDialog();
            DisplayVehicles();
        }
        private void VehiclesGV_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            viewButton.Enabled = editButton.Enabled = deleteButton.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CreatePlugins();
            Car car = new Car(Car.CarType.Coupe, 4, 1000, "brand", 500, 2022, 5, new Driver("John", 35, Driver.Category.B), new List<Passenger> { new Passenger(20, "Bob", 20) });
            Plane plane = new Plane(Plane.PlaneType.Passenger, 2000, "cool brand", 1500, 2020, 50, new Driver("Bill", 30, Driver.Category.F), new List<Passenger> { new Passenger(22, "Alice", 21), new Passenger(25, "Cob", 21) });
            vehicles.Add(car);
            vehicles.Add(plane);
            DisplayVehicles();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
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

        Dictionary<string, IArchiverPlugin> plugins;
        private void CreatePlugins()
        {
            plugins = new Dictionary<string, IArchiverPlugin>();
            string rootDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.."));
            string[] binDirectories = Directory.GetDirectories(rootDirectory, "bin", SearchOption.AllDirectories);
            foreach (string binDirectory in binDirectories)
            {
                string[] dllFiles = Directory.GetFiles(binDirectory, "*.dll", SearchOption.AllDirectories);
                if (dllFiles.Length > 1) continue;
                foreach (string pluginFile in dllFiles)
                {
                    Assembly pluginAssembly = Assembly.LoadFile(pluginFile);
                    Type[] types = pluginAssembly.GetExportedTypes();
                    foreach (Type type in types)
                    {
                        if (typeof(IArchiverPlugin).IsAssignableFrom(type))
                        {
                            IArchiverPlugin plugin = (IArchiverPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Extension.Trim('*'), plugin);
                        }
                    }
                }
            }
            /*string[] pluginFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"), "*.dll", SearchOption.TopDirectoryOnly);
            foreach (string pluginFile in pluginFiles)
            {
                Assembly pluginAssembly = Assembly.LoadFile(pluginFile);
                Type[] types = pluginAssembly.GetExportedTypes();
                foreach (Type type in types)
                {
                    if (typeof(IArchiverPlugin).IsAssignableFrom(type))
                    {
                        IArchiverPlugin plugin = (IArchiverPlugin)Activator.CreateInstance(type);
                        plugins.Add(plugin.Extension.Trim('*'), plugin);
                    }
                }
            }*/
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            CreatePlugins();
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
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    fileStream.CopyTo(memoryStream);
                }
                try
                {
                    plugins[Path.GetExtension(fileName)].Decompress(memoryStream);
                }
                catch (KeyNotFoundException)
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }
                try
                {
                    vehicles = serializer.Deserialize(memoryStream);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error in file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
                string[] serializerAttribute = ((NameAttribute)field.GetCustomAttributes(typeof(NameAttribute), false)[0]).Name.Split('|');
                string serializerName = serializerAttribute[0];
                string serializerExtension = serializerAttribute[1];
                foreach (var plugin in plugins)
                {
                    serializerExtension +=  ";" + serializerAttribute[1] + plugin.Value.Extension.Trim('*');
                }
                filter += serializerName + '|' + serializerExtension + '|';
            }
            return filter.Remove(filter.Length - 1, 1);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePlugins();
            saveFileDialog.Filter = GetFileFilter();
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            var serializerType = (SerializationType)(saveFileDialog.FilterIndex - 1);
            string fileName = saveFileDialog.FileName;
            SerializerFactory serializerFactory = (SerializerFactory)Activator.CreateInstance(serializers[serializerType]);
            ISerializer serializer = serializerFactory.CreateSerializer();
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(vehicles, fileStream);
                try
                {
                    plugins[Path.GetExtension(fileName)].Compress(fileStream);
                }
                catch (KeyNotFoundException) { }
            }
        }
    }
}
