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

        Address address = _context.Addresses.Find(doctor.AddressId);

        ViewData["Address"] = new Address(
            address.Id,
            address.ZipCode,
            address.Street,
            address.Number,
            address.District,
            address.City,
            address.State
        );

        return View(doctor);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] int id, [FromForm] string name, [FromForm] string speciality, [FromForm] string cpf, [FromForm] int addressId, [FromForm] string telephone)
    {
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
        Doctor doctor = _context.Doctors.Find(id);

        if(_context.Addresses.Find(addressId) == null)
        {
            TempData["MessageError"] = $"Endereço com ID {addressId} não existe.";
            return RedirectToAction("Update", new { id = id });
        }

        if(doctor == null)
        {
            TempData["MessageError"] = $"Doutor com ID {id} não existe.";
            return RedirectToAction("Update", new { id = id });
        }

        doctor.Name = name;
        doctor.Cpf = cpf;
        doctor.Speciality = speciality;
        doctor.AddressId = addressId;
        doctor.Telephone = telephone;

        _context.Doctors.Update(doctor);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Doutor com ID {id} atualizado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Delete(int id)
    {
        Doctor doctor = _context.Doctors.Find(id);

        if(doctor == null)
        {
            TempData["MessageError"] = $"Doutor com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        _context.Doctors.Remove(doctor);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Doutor com ID {id} removido com sucesso.";
        return RedirectToAction("Index");
    }
} 