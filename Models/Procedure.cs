namespace VetManager.Models;

public class Procedure
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int HospitalId { get; set; }
    public DateTime DateTime { get; set; }
    public string FormattedDateTime { get => DateTime.Year.ToString().PadLeft(4, '0') + "-" + DateTime.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Day.ToString().PadLeft(2, '0') + "T" + DateTime.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Minute.ToString().PadLeft(2, '0');  }
    public string Type { get; set; }
    public string Description { get; set; }

    public Procedure() { }

    public Procedure(int id, int pacientId, int doctorId, int hospitalId, DateTime dateTime, string type, string description)
    {
        Id = id;
        PatientId = pacientId;
        DoctorId = doctorId;
        HospitalId = hospitalId;
        DateTime = dateTime;
        Type = type;
        Description = description;
    }
}