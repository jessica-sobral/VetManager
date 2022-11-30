using Microsoft.AspNetCore.Mvc;
using VetManager.Models;

namespace VetManager.Controllers;

public class AnamnesisController : Controller
{
    private readonly VetManagerContext _context;

    public AnamnesisController(VetManagerContext context)
    {
        _context = context;
    }

     public IActionResult Index()
    {
        return View(_context.Anamnesis);
    }

    public IActionResult Show(int id)
    {
        Anamnesis anamnesis = _context.Anamnesis.Find(id);

        if(anamnesis == null)
        {
            TempData["MessageError"] = $"Anamnese com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        Patient patient = _context.Patients.Find(anamnesis.PatientId);

        ViewData["Patient"] = new Patient(
            patient.Id,
            patient.Name,
            patient.Age,
            patient.Species,
            patient.BloodType,
            patient.TutorId
        );

        return View(anamnesis);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] int id, [FromForm] int patientId, [FromForm] string symptoms, [FromForm] string diagnosis, [FromForm] string observations)
    {
        Anamnesis anamnesis = new Anamnesis(id, patientId, symptoms, diagnosis, observations);

        if(_context.Patients.Find(anamnesis.PatientId) == null)
        {
            TempData["MessageError"] = $"Paciente com ID {patientId} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Anamnesis.Find(anamnesis.Id) != null)
        {
            TempData["MessageError"] = $"Anamnese com ID {id} já existe.";
            return RedirectToAction("Create");
        }
        
        _context.Anamnesis.Add(anamnesis);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Anamnese com ID {anamnesis.Id} cadastrado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Update(int id)
    {
        Anamnesis anamnesis = _context.Anamnesis.Find(id);

        if(anamnesis == null)
        {
            TempData["MessageError"] = $"Doutor com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        return View(anamnesis);
    }

    [HttpPost]
    public IActionResult Save([FromForm] int id, [FromForm] int patientId, [FromForm] string symptoms, [FromForm] string diagnosis, [FromForm] string observations)
    {
        Anamnesis anamnesis = _context.Anamnesis.Find(id);

        if(_context.Patients.Find(patientId) == null)
        {
            TempData["MessageError"] = $"Paciente com ID {patientId} não existe.";
            return RedirectToAction("Update", new { id = id });
        }

        if(anamnesis == null)
        {
            TempData["MessageError"] = $"Anamnese com ID {id} não existe.";
            return RedirectToAction("Update", new { id = id });
        }

        anamnesis.PatientId = patientId;
        anamnesis.Symptoms = symptoms;
        anamnesis.Diagnosis = diagnosis;
        anamnesis.Observations = observations;

        _context.Anamnesis.Update(anamnesis);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Anamnese com ID {id} atualizado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Delete(int id)
    {
        Anamnesis anamnesis = _context.Anamnesis.Find(id);

        if(anamnesis == null)
        {
            TempData["MessageError"] = $"Anamnese com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        _context.Anamnesis.Remove(anamnesis);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Anamnese com ID {id} removido com sucesso.";
        return RedirectToAction("Index");
    }
} 