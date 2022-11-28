namespace VetManager.Models;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }

    public Address() { }

    public Address(int id, string street, string district, string city, string zipCode) {
        Id = id;
        Street = street;
        District = district;
        City = city;
        ZipCode = zipCode;
    }
}