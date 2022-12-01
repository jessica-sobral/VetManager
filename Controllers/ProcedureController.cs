using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VetManager.Models;

namespace VetManager.Controllers;

public class ProcedureController : Controller
{
    private readonly VetManagerContext _context;

    public ProcedureController(VetManagerContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Procedures);
    }

    public IActionResult Show(int id)
    {
        Procedure procedure = _context.Procedures.Find(id);

        if(procedure == null)
        {
            TempData["MessageError"] = $"Procedimento com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        ViewData["Patient"] = _context.Patients.Find(procedure.PatientId);
        ViewData["Doctor"] = _context.Doctors.Find(procedure.DoctorId);
        ViewData["Hospital"] = _context.Hospitals.Find(procedure.HospitalId);

        return View(procedure);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] int id, [FromForm] int patientId, [FromForm] int doctorId, [FromForm] int hospitalId, [FromForm] string dateTime, [FromForm] string type, [FromForm] string description)
    {
        var day = Int32.Parse(dateTime.Substring(8, 2));
        var month = Int32.Parse(dateTime.Substring(5, 2));
        var year = Int32.Parse(dateTime.Substring(0, 4));
        var hour = Int32.Parse(dateTime.Substring(11, 2));
        var minute = Int32.Parse(dateTime.Substring(14, 2));

        Procedure procedure = new Procedure(id, patientId, doctorId, hospitalId, new DateTime(year, month, day, hour, minute, 0), type, description);

        if(_context.Patients.Find(procedure.PatientId) == null)
        {
            TempData["MessageError"] = $"Paciente com ID {procedure.PatientId} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Doctors.Find(procedure.DoctorId) == null)
        {
            TempData["MessageError"] = $"Médico com ID {procedure.DoctorId} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Hospitals.Find(procedure.HospitalId) == null)
        {
            TempData["MessageError"] = $"Hospital com ID {procedure.HospitalId} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Procedures.Find(procedure.Id) != null)
        {
            TempData["MessageError"] = $"Procedimento com ID {procedure.Id} já existe.";
            return RedirectToAction("Create");
        }
        
        _context.Procedures.Add(procedure);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Procedimento com ID {procedure.Id} cadastrado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Update(int id)
    {
        Procedure procedure = _context.Procedures.Find(id);

        if(procedure == null)
        {
            TempData["MessageError"] = $"Procedimento com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        return View(procedure);
    }

    [HttpPost]
    public IActionResult Save([FromForm] int id, [FromForm] int patientId, [FromForm] int doctorId, [FromForm] int hospitalId, [FromForm] string dateTime, [FromForm] string type, [FromForm] string description)
    {
        Procedure procedure = _context.Procedures.Find(id);

        if(_context.Patients.Find(procedure.PatientId) == null)
        {
            TempData["MessageError"] = $"Paciente com ID {procedure.PatientId} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Doctors.Find(procedure.DoctorId) == null)
        {
            TempData["MessageError"] = $"Médico com ID {procedure.DoctorId} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Hospitals.Find(procedure.HospitalId) == null)
        {
            TempData["MessageError"] = $"Hospital com ID {procedure.HospitalId} não existe.";
            return RedirectToAction("Create");
        }

        var day = Int32.Parse(dateTime.Substring(8, 2));
        var month = Int32.Parse(dateTime.Substring(5, 2));
        var year = Int32.Parse(dateTime.Substring(0, 4));
        var hour = Int32.Parse(dateTime.Substring(11, 2));
        var minute = Int32.Parse(dateTime.Substring(14, 2));

        procedure.PatientId = patientId;
        procedure.DoctorId = doctorId;
        procedure.HospitalId = hospitalId;
        procedure.DateTime = new DateTime(year, month, day, hour, minute, 0);
        procedure.Type = type;
        procedure.Description = description;

        _context.Procedures.Update(procedure);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Procedimento com ID {id} atualizado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Delete(int id)
    {
        Procedure procedure = _context.Procedures.Find(id);

        if(procedure == null)
        {
            TempData["MessageError"] = $"Procedimento com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        _context.Procedures.Remove(procedure);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Procedimento com ID {id} removido com sucesso.";
        return RedirectToAction("Index");
    }
}
