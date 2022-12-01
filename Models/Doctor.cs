namespace VetManager.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public string Cpf { get; set; }
    public string FormattedCpf { get => Cpf.Substring(0,3) + "." + Cpf.Substring(3, 3) + "." + Cpf.Substring(6, 3) + "-" + Cpf.Substring(9, 2); }
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