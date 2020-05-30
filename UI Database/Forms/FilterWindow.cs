using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UI_Database.Events;

namespace UI_Database.Forms
{
    public partial class FilterWindow : Form
    {
        public event EventHandler<FilterChangeEventArgs> FilterChangeEvent;

        public FilterWindow(IEnumerable<string> departments, FilterChangeEventArgs previousFilterSettings)
        {
            InitializeComponent();

            departmentIDFilterField.Items.Clear();
            foreach (string dptID in departments)
            {
                if (!departmentIDFilterField.Items.Contains(dptID)) departmentIDFilterField.Items.Add(dptID);
            }

            RestoreFilterSettings(previousFilterSettings);
        }

        private void ApplyFilterBtn_Click(object sender, EventArgs e)
        {
            FilterChangeEvent?.Invoke(this, new FilterChangeEventArgs(
                lastNameFilterField.Enabled ? lastNameFilterField.Text : string.Empty,
                departmentIDFilterField.Enabled && departmentIDFilterField.SelectedItem != null
                ? departmentIDFilterField.SelectedItem.ToString() : string.Empty,
                sexFilterField.Enabled && sexFilterField.SelectedItem != null ? sexFilterField.SelectedItem.ToString() : string.Empty,
                hireDateFilterField.Enabled ? hireDateFilterField.Value.ToShortDateString() : string.Empty));
            this.Close();
        }

        private void CancelFilterChangesBtn_Click(object sender, EventArgs e) => this.Close();
        private void FilterOption_CheckedChanged(object sender, EventArgs e)
           => Controls[(sender as CheckBox).Name + "Field"].Enabled = (sender as CheckBox).Checked;
        
        /// <summary>
        /// Restores filter window state to the currently applied filter
        /// </summary>
        /// <param name="filterArgs">settings of currently applied filter</param>
        private void RestoreFilterSettings(FilterChangeEventArgs filterArgs)
        {
            if (filterArgs.DepartmentID != string.Empty)
            {
                departmentIDFilter.Checked = true;
                departmentIDFilterField.SelectedItem = filterArgs.DepartmentID;
            }
            if (filterArgs.LastName != string.Empty)
            {
                lastNameFilter.Checked = true;
                lastNameFilterField.Text = filterArgs.LastName;
            }
            if (filterArgs.Sex != string.Empty)
            {
                sexFilter.Checked = true;
                sexFilterField.SelectedItem = filterArgs.Sex;
            }
            if (filterArgs.HireDate != string.Empty)
            {
                hireDateFilter.Checked = true;
                hireDateFilterField.Text = filterArgs.HireDate;
            }
        }
    }
}
