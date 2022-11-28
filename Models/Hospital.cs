namespace VetManager.Models;

public class Hospital
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Telephone1 { get; set; }
    public string Telephone2 { get; set; }
    public int AddressId { get; set; }

    public Hospital() { }

    public Hospital(int id, string name, string telephone1, string telephone2, int addressId)
    {
        Id = id;
        Name = name;
        Telephone1 = telephone1;
        Telephone2 = telephone2;
        AddressId = addressId;
    }
}