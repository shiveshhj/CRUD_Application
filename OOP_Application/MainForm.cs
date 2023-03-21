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

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Are you sure?", "Delete vehicle", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            Vehicle temp = vehicles[vehiclesGV.CurrentRow.Index];
            vehicles.RemoveAt(vehiclesGV.CurrentRow.Index);
            temp.driver = null;
            for(int i = 0; i < temp.passengers.Count; i++)
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
    }

}
