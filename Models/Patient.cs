namespace VetManager.Models;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Species { get; set; }
    public string BloodType { get; set; }
    public int TutorId { get; set; }
   
    public Patient() { }

    public Patient(int id, string name, int age, string species, string bloodType, int tutorId) {
        Id = id;
        Name = name;
        Age = age;
        Species = species;
        BloodType = bloodType;
        TutorId = tutorId;
    }
}