namespace EMS.Domain;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public PersonalDetails PersonalDetails { get; set; }
}
public class PersonalDetails
{
    public int PersonalDetailsId { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}