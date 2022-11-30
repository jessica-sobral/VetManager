namespace VetManager.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public string Cpf { get; set; }
    public int AddressId { get; set; }
    public string Telephone { get; set; }
   
    public Doctor() { }

    public Doctor(int id, string name, string speciality, string cpf, int addressId, string telephone) {
        Id = id;
        Name = name;
        Speciality = speciality;
        Cpf = cpf;
        AddressId = addressId;
        Telephone = telephone;
    }
}