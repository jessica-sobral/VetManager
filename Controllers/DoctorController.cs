using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using VetManager.Models;

namespace VetManager.Controllers;

public class DoctorController : Controller
{
    private readonly VetManagerContext _context;

    public DoctorController(VetManagerContext context)
    {
        _context = context;
    }

     public IActionResult Index()
    {
        return View(_context.Doctors);
    }

    public IActionResult Show(int id)
    {
        Doctor doctor = _context.Doctors.Find(id);

        if(doctor == null)
        {
            TempData["MessageError"] = $"Doctor com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        ViewData["Address"] = _context.Addresses.Find(doctor.AddressId);

        return View(doctor);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] int id, [FromForm] string name, [FromForm] string speciality, [FromForm] string cpf, [FromForm] int addressId, [FromForm] string telephone)
    {
        if(id <= 0)
        {
            TempData["MessageError"] = $"O ID deve ser um número inteiro positivo.";
            return RedirectToAction("Create");
        }

        try
        {
            if(cpf.Length != 11) throw new FormatException();
            
            BigInteger bg = BigInteger.Parse(cpf);
        }
        catch(FormatException)
        {
            TempData["MessageError"] = $"O CPF deve ser digitado apenas em números e sem qualquer tipo de pontuação.";
            return RedirectToAction("Create");
        }

        Doctor doctor = new Doctor(id, name, speciality, cpf, addressId, telephone);

        if(_context.Addresses.Find(addressId) == null)
        {
            TempData["MessageError"] = $"Endereço com ID {addressId} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Doctors.Find(id) != null)
        {
            TempData["MessageError"] = $"Doutor com ID {id} já existe.";
            return RedirectToAction("Create");
        }

        foreach(var doc in _context.Doctors)
        {
            if(doc.Cpf == doctor.Cpf)
            {
                TempData["MessageError"] = $"CPF já cadastrado.";
                return RedirectToAction("Create");
            }
        }
        
        _context.Doctors.Add(doctor);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Doutor com ID {doctor.Id} cadastrado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Update(int id)
    {
        Doctor doctor = _context.Doctors.Find(id);

        if(doctor == null)
        {
            TempData["MessageError"] = $"Doutor com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        return View(doctor);
    }

    [HttpPost]
    public IActionResult Save([FromForm] int id, [FromForm] string name, [FromForm] string speciality, [FromForm] string cpf, [FromForm] int addressId, [FromForm] string telephone)
    {
        try
        {
            if(cpf.Length != 11) throw new FormatException();
            
            BigInteger bg = BigInteger.Parse(cpf);
        }
        catch(FormatException)
        {
            TempData["MessageError"] = $"O CPF deve ser digitado apenas em números e sem qualquer tipo de pontuação.";
            return RedirectToAction("Create");
        }
        
        Doctor doctor = _context.Doctors.Find(id);

        if(_context.Addresses.Find(addressId) == null)
        {
            TempData["MessageError"] = $"Médico com ID {addressId} não existe.";
            return RedirectToAction("Update", new { id = id });
        }

        if(doctor == null)
        {
            TempData["MessageError"] = $"Doutor com ID {id} não existe.";
            return RedirectToAction("Update", new { id = id });
        }

        foreach(var doc in _context.Doctors)
        {
            if(doc.Cpf == doctor.Cpf && doc.Id != doctor.Id)
            {
                TempData["MessageError"] = $"CPF já cadastrado.";
                return RedirectToAction("Create");
            }
        }

        doctor.Name = name;
        doctor.Cpf = cpf;
        doctor.Speciality = speciality;
        doctor.AddressId = addressId;
        doctor.Telephone = telephone;

        _context.Doctors.Update(doctor);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Médico com ID {id} atualizado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Delete(int id)
    {
        Doctor doctor = _context.Doctors.Find(id);

        if(doctor == null)
        {
            TempData["MessageError"] = $"Médico com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        foreach (var procedure in _context.Procedures)
        {
            if(procedure.DoctorId == doctor.Id)
            {
                TempData["MessageError"] = $"Médico com ID {id} tem procedimento(s).";
                return RedirectToAction("Index");
            }
        }

        _context.Doctors.Remove(doctor);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Médico com ID {id} removido com sucesso.";
        return RedirectToAction("Index");
    }
} 