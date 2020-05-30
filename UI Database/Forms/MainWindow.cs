using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using UI_Database.Events;
using UI_Database.Forms;
using System.Data.Linq;

namespace UI_Database
{
    public partial class MainForm : Form
    {
        protected SqlConnection _connection;
        protected FilterChangeEventArgs _currentFilterArgs = new FilterChangeEventArgs();
        protected DataClassesDataContext _dc;

        public MainForm()
        {
            InitializeComponent();
            ConnectToDatabase();
        }
        /// <summary>
        /// Establishes connection to the database
        /// </summary>
        private void ConnectToDatabase()
        {
            string dbPath = @"C:\Users\Jack\Desktop\Технология разработки\12. UI Database\UI Database\Database.mdf";
            _connection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True");
            _dc = new DataClassesDataContext(_connection);
        }

        /// <summary>
        /// Load data from .xml file to the database
        /// </summary>
        private void LoadXmlBtn_Click(object sender, EventArgs e)
        {
            DataSet data = new DataSet();
            data.ReadXml("Data.xml");

            try
            {
               _connection.Open();

                // Удаляем что было до этого в базе данных
                string strSQL = "DELETE FROM People";
                SqlCommand command = new SqlCommand(strSQL, _connection);
                command.ExecuteNonQuery();

                // Добавляем новые элементы из xml файла
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    List<string> vals = new List<string>();
                    foreach (string v in row.ItemArray) vals.Add($"'{v}'");

                    string values = string.Join(", ", vals);
                    strSQL =
                        $@"INSERT INTO People
                        VALUES ({values})";
                    command = new SqlCommand(strSQL, _connection);
                    command.ExecuteNonQuery();
                }

                _connection.Close();
                UpdateGrid();
            }

            catch (SqlException ex)
            {
                MessageBox.Show($"Something went wrong while reading XML file: {ex.Message}");
            }
        }

        /// <summary>
        /// Clears database
        /// </summary>
        private void ClearDatabaseBtn_Click(object sender, EventArgs e)
        {
            DataSet data = new DataSet();
            data.ReadXml("Data.xml");

            try
            {
                _connection.Open();

                string strSQL = "DELETE FROM People";
                SqlCommand command = new SqlCommand(strSQL, _connection);
                command.ExecuteNonQuery();
                
                _connection.Close();
                UpdateGrid();
            }

            catch (SqlException ex)
            {
                MessageBox.Show($"Something went wrong while clearing the database: {ex.Message}");
            }
        }

        /// <summary>
        /// Refreshes data in DataGridView to represent current condition of the database
        /// </summary>
        private void UpdateGrid()
        {
            _dc = new DataClassesDataContext(_connection);
            peopleDataGridView.DataSource = _dc.People;
        }

        /// <summary>
        /// Opens filter window
        /// </summary>
        private void setFilterBtn_Click(object sender, EventArgs e)
        {
            var departments = _dc.People.GroupBy(x => x.Department_ID)
                                .Select(y => y.First().Department_ID.ToString()).ToList();

            var filterWindow = new FilterWindow(departments, _currentFilterArgs);
            filterWindow.FilterChangeEvent += new EventHandler<FilterChangeEventArgs>(this.OnFilterChange);
            filterWindow.ShowDialog();
        }

        /// <summary>
        /// React to event from filter menu
        /// </summary>
        private void OnFilterChange(object sender, FilterChangeEventArgs e)
        {
            _currentFilterArgs = e;
            ApplyFilter();
        }

        /// <summary>
        /// Applies filter and shows result in DataGridView
        /// </summary>
        private void ApplyFilter()
        {
            try
            {
                peopleDataGridView.DataSource = _dc.People.Where(person =>
                    person.Department_ID.ToString().Contains(_currentFilterArgs.DepartmentID)
                    && person.Last_Name.ToLower().Contains(_currentFilterArgs.LastName.ToLower())
                    && person.Hire_Date.Contains(_currentFilterArgs.HireDate)
                    && person.Sex.Contains(_currentFilterArgs.Sex));
            }
            catch (NullReferenceException)
            {
                throw new FormatException("The data have incorrect format. Some entries might be missing " +
                    "at least one of the following key elements: Employee_ID, Department_ID, Last_Name, Hire_Date, Sex.");
            }

        }

        /// <summary>
        /// Clears filter
        /// </summary>
        private void clearBtn_Click(object sender, EventArgs e) => UpdateGrid();

    }
}
