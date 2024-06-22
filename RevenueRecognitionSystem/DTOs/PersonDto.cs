namespace RevenueRecognitionSystem.DTOs;

public class PersonDto
{
    public int Id { get; set; }
    
    public string Address { get; set; }
    
    public string Email { get; set; }

    public string PhoneNumber { get; set; }
   
    // Attributes for Legal persons
    
    public string Name { get; set; }
    
    public string Krs { get; set; }
    
    // Attributes for Natural persons
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Pesel { get; set; }

    public bool Active { get; set; }
    
    
    public string PersonType { get; set; }  // Property to indicate if a Person is of Legal or Natural type


    // Constructor for Natural persons
    public PersonDto(int id, string firstName, string lastName, string pesel, bool active,
        string address, string email, string phoneNumber, string personType)
    {
        Id = id;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Pesel = pesel;
        Active = active;
        PersonType = personType;
    }


    // Constructor for Legal persons
    public PersonDto(int id, string name, string krs, 
        string address, string email, string phoneNumber, string personType)
    {
        Id = id;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        Name = name;
        Krs = krs;
        PersonType = personType;
    }
    
    
    
}