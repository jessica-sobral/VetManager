namespace VetManager.Models;

public class Address
{
    public int Id { get; set; }
    public string ZipCode { get; set; }
    public string FormattedZipCode { get => ZipCode.Substring(0, 5) + "-" + ZipCode.Substring(5, 3); }
    public string Street { get; set; }
    public string Number { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public Address() { }

    public Address(int id, string zipCode, string street, string number, string district, string city, string state) {
        Id = id;
        ZipCode = zipCode;
        Street = street;
        Number = number;
        District = district;
        City = city;
        State = state;
    }
}