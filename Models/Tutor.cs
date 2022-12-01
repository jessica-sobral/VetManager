namespace VetManager.Models;

public class Tutor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string FormattedCpf { get => Cpf.Substring(0,3) + "." + Cpf.Substring(3, 3) + "." + Cpf.Substring(6, 3) + "-" + Cpf.Substring(9, 2); }
    public int AddressId { get; set; }
    public string Telephone { get; set; }
   
    public Tutor() { }

    public Tutor(int id, string name, string cpf, int addressId, string telephone) {
        Id = id;
        Name = name;
        Cpf = cpf;
        AddressId = addressId;
        Telephone = telephone;
    }
}