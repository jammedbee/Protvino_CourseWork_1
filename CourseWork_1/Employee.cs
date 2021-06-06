using System;

namespace PeopleManagementApp.Entities
{
    public class Employee
    {
        public int Employee_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PassportNumber { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime EndDateOfEmployment { get; set; }
        public bool IsFired { get; set; }
        public string DismissalReason { get; set; }
    }
}
