using Microsoft.AspNetCore.Mvc;
using VetManager.Models;

namespace VetManager.Controllers;

public class PatientController : Controller
{
    private readonly VetManagerContext _context;

    public PatientController(VetManagerContext context)
    {
        _context = context;
    }

     public IActionResult Index()
    {
        return View(_context.Patients);
    }

    public IActionResult Show(int id)
    {
        Patient patient = _context.Patients.Find(id);

        if(patient == null)
        {
            TempData["MessageError"] = $"Patiente com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        Tutor tutor = _context.Tutors.Find(patient.TutorId);

        ViewData["Tutor"] = new Tutor(
            tutor.Id,
            tutor.Name,
            tutor.Cpf,
            tutor.AddressId,
            tutor.Telephone
        );

        return View(patient);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] int id, [FromForm] string name, [FromForm] int age, [FromForm] string species, [FromForm] string bloodType, [FromForm] int tutorId)
    {
        Patient patient = new Patient(id, name, age, species, bloodType, tutorId);

        if(_context.Tutors.Find(patient.TutorId) == null)
        {
            TempData["MessageError"] = $"Tutor com ID {patient.TutorId} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Patients.Find(patient.Id) != null)
        {
            TempData["MessageError"] = $"Patiente com ID {patient.Id} já existe.";
            return RedirectToAction("Create");
        }
        
        _context.Patients.Add(patient);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Patiente com ID {patient.Id} cadastrado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Update(int id)
    {
        Patient patient = _context.Patients.Find(id);

        if(patient == null)
        {
            TempData["MessageError"] = $"Patiente com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        return View(patient);
    }

    [HttpPost]
    public IActionResult Save([FromForm] int id, [FromForm] string name, [FromForm] int age, [FromForm] string species, [FromForm] string bloodType, [FromForm] int tutorId)
    {
        Patient patient = _context.Patients.Find(id);

        if(_context.Tutors.Find(patient.TutorId) == null)
        {
            TempData["MessageError"] = $"Tutor com ID {patient.TutorId} não existe.";
            return RedirectToAction("Update", new{ id = id });
        }

        if(patient == null)
        {
            TempData["MessageError"] = $"Tutor com ID {id} não existe.";
            return RedirectToAction("Update", new{ id = id });
        }

        patient.Name = name;
        patient.Age = age;
        patient.Species = species;
        patient.BloodType = bloodType;
        patient.TutorId = tutorId;

        _context.Patients.Update(patient);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Paciente com ID {id} atualizado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Delete(int id)
    {
        Patient patient = _context.Patients.Find(id);

        if(patient == null)
        {
            TempData["MessageError"] = $"Paciente com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        _context.Patients.Remove(patient);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Paciente com ID {id} removido com sucesso.";
        return RedirectToAction("Index");
    }

} 