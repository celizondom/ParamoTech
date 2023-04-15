namespace Sat.Recruitment.Common.Models
{
    /// <summary>
    /// Users model.
    /// </summary>
    public class Users
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}