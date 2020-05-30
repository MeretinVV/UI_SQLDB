using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Database.Events
{
    public class FilterChangeEventArgs : EventArgs
    {
        public string LastName { get; internal set; }
        public string DepartmentID { get; internal set; }
        public string Sex { get; internal set; }
        public string HireDate { get; internal set; }
        public FilterChangeEventArgs(string lastName, string departmentID, string sex, string hireDate)
        {
            LastName = lastName;
            DepartmentID = departmentID;
            Sex = sex;
            HireDate = hireDate;
        }
        public FilterChangeEventArgs()
        {
            LastName = string.Empty;
            DepartmentID = string.Empty;
            Sex = string.Empty;
            HireDate = string.Empty;
        }
    }
}
