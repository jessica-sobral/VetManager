namespace VetManager.Models;

public class Anamnesis
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Symptoms { get; set; }
    public string Diagnosis { get; set; }
    public string Observations { get; set; }
   
    public Anamnesis() { }

    public Anamnesis(int id, int patientId, string symptoms, string diagnosis, string observations) {
        Id = id;
        PatientId = patientId;
        Symptoms = symptoms;
        Diagnosis = diagnosis;
        Observations = observations;
    }
}