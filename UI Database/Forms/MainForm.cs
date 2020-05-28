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
        private SqlConnectionStringBuilder connectionStrBuilder;
        private FilterChangeEventArgs _currentFilterArgs = new FilterChangeEventArgs();

        public MainForm()
        {
            InitializeComponent();
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            connectionStrBuilder = new SqlConnectionStringBuilder();
            connectionStrBuilder.IntegratedSecurity = true;
            connectionStrBuilder.DataSource = @"(LocalDB)\MSSQLLocalDB";
            connectionStrBuilder.AttachDBFilename =
                Path.GetFullPath(@"C:\Users\Jack\Desktop\Технология разработки\12. UI Database\UI Database\Database.mdf");
        }

        private void peopleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.peopleBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet.People". При необходимости она может быть перемещена или удалена.
            this.peopleTableAdapter.Fill(this.databaseDataSet.People);

        }


        private void LoadXmlBtn_Click(object sender, EventArgs e)
        {
            DataSet data = new DataSet();
            data.ReadXml("Data.xml");

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionStrBuilder.ToString();
                try
                {
                    connection.Open();

                    // Удаляем что было до этого в базе данных
                    string strSQL = "DELETE FROM People";
                    SqlCommand command = new SqlCommand(strSQL, connection);
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
                        command = new SqlCommand(strSQL, connection);
                        command.ExecuteNonQuery();
                    }

                    UpdateGrid();
                }

                catch (SqlException ex)
                {
                    MessageBox.Show($"Something went wrong while reading XML file: {ex.Message}");
                }
            }
        }

        private void ClearDatabaseBtn_Click(object sender, EventArgs e)
        {
            DataSet data = new DataSet();
            data.ReadXml("Data.xml");

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionStrBuilder.ToString();
                try
                {
                    connection.Open();

                    string strSQL = "DELETE FROM People";
                    SqlCommand command = new SqlCommand(strSQL, connection);
                    command.ExecuteNonQuery();

                    UpdateGrid();
                }
                
                catch (SqlException ex)
                {
                    MessageBox.Show($"Something went wrong while clearing the database: {ex.Message}");
                }
            }
        }


        // Вот в этом методе должно происходить обновление таблицы, чтобы она
        // выглядела как текущее состояние базы данных
        private void UpdateGrid()
        {
            this.peopleTableAdapter.Fill(this.databaseDataSet.People);
            peopleBindingSource.ResetBindings(false);

            //this.peopleBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.databaseDataSet);
            //peopleBindingSource.DataSource = this.databaseDataSet.People;
            //peopleDataGridView.DataSource = null;
            peopleDataGridView.DataSource = peopleBindingSource.DataSource;
            peopleDataGridView.Refresh();


        }

        private void setFilterBtn_Click(object sender, EventArgs e)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext(connectionStrBuilder.ToString()))
            {
                var departments = dc.People.GroupBy(x => x.Department_ID)
                                  .Select(y => y.First().Department_ID.ToString()).ToList();

                
                var filterWindow = new FilterWindow(departments, _currentFilterArgs);
                filterWindow.FilterChangeEvent += new EventHandler<FilterChangeEventArgs>(this.OnFilterChange);
                filterWindow.ShowDialog();
                
            }
        }

        private void OnFilterChange(object sender, FilterChangeEventArgs e)
        {
            _currentFilterArgs = e;
            ApplyFilter();
            UpdateGrid();
        }

        // В этом методе проблема с преобразованием
        private void ApplyFilter()
        {
            using (DataClassesDataContext dc = new DataClassesDataContext(connectionStrBuilder.ToString()))
            {
                try
                {
                    // Это отфильтрованные данные, но я не очень понимаю как их отобразить
                    // явные преобразования в DataSet или DataTable не работают.
                    var filteredData = 
                        from person in dc.People
                        where person.Department_ID.ToString().Contains(_currentFilterArgs.DepartmentID)
                        && person.Last_Name.ToLower().Contains(_currentFilterArgs.LastName.ToLower())
                        && person.Hire_Date.Contains(_currentFilterArgs.HireDate)
                        && person.Sex.Contains(_currentFilterArgs.Sex)
                        select person;
                    

                    //this.peopleTableAdapter.Fill();
                    //peopleDataGridView.DataSource = filteredData;
                }
                catch (NullReferenceException)
                {
                    throw new FormatException("The data have incorrect format. Some entries might be missing " +
                        "at least one of the following key elements: Employee_ID, Department_ID, Last_Name, Hire_Date, Sex.");
                }
                
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            UpdateGrid();
        }
    }
}
