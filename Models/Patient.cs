namespace VetManager.Models;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string BirthDateString { get =>  this.BirthDate.Year.ToString().PadLeft(4, '0') + "-" + this.BirthDate.Month.ToString().PadLeft(2, '0') + "-" + this.BirthDate.Day.ToString().PadLeft(2, '0'); }
    public string BirthDateBrString { get =>  this.BirthDate.Day.ToString().PadLeft(2, '0') + "/" + this.BirthDate.Month.ToString().PadLeft(2, '0') + "/" + this.BirthDate.Year.ToString().PadLeft(4, '0'); }
    public string Species { get; set; }
    public string BloodType { get; set; }
    public int TutorId { get; set; }
   
    public Patient() { }

    public Patient(int id, string name, DateTime birthDate, string species, string bloodType, int tutorId) {
        Id = id;
        Name = name;
        BirthDate = birthDate;
        Species = species;
        BloodType = bloodType;
        TutorId = tutorId;
    }

    public int GetAge()
    {
        int age = DateTime.Now.Year - this.BirthDate.Year;

        if(DateTime.Now.Month < this.BirthDate.Month || (DateTime.Now.Month == this.BirthDate.Month && DateTime.Now.Day < this.BirthDate.Day))
        {
            return age - 1;
        }

        return age;
    }
}