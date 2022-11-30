using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VetManager.Models;

namespace VetManager.Controllers;

public class HospitalController : Controller
{
    private readonly VetManagerContext _context;

    public HospitalController(VetManagerContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Hospitals);
    }

    public IActionResult Show(int id)
    {
        Hospital hospital = _context.Hospitals.Find(id);

        if(hospital == null)
        {
            TempData["MessageError"] = $"Hospital com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        Address address = _context.Addresses.Find(hospital.AddressId);

        ViewData["Address"] = new Address(
            address.Id,
            address.ZipCode,
            address.Street,
            address.Number,
            address.District,
            address.City,
            address.State
        );

        return View(hospital);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] int id, [FromForm] string name, [FromForm] string telephone1, [FromForm] string telephone2, [FromForm] int addressId)
    {
        Hospital hospital = new Hospital(id, name, telephone1, telephone2, addressId);

        if(_context.Addresses.Find(hospital.AddressId) == null)
        {
            TempData["MessageError"] = $"Endereço com ID {hospital.Id} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Hospitals.Find(hospital.Id) != null)
        {
            TempData["MessageError"] = $"Hospital com ID {hospital.Id} já existe.";
            return RedirectToAction("Create");
        }
        
        _context.Hospitals.Add(hospital);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Hospital com ID {hospital.Id} cadastrado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Update(int id)
    {
        Hospital hospital = _context.Hospitals.Find(id);

        if(hospital == null)
        {
            TempData["MessageError"] = $"Hospital com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        return View(hospital);
    }

    [HttpPost]
    public IActionResult Save([FromForm] int id, [FromForm] string name, [FromForm] string telephone1, [FromForm] string telephone2, [FromForm] int addressId)
    {
        Hospital hospital = _context.Hospitals.Find(id);

        if(_context.Addresses.Find(hospital.AddressId) == null)
        {
            TempData["MessageError"] = $"Endereço com ID {hospital.Id} não existe.";
            return RedirectToAction("Create");
        }

        if(hospital == null)
        {
            TempData["MessageError"] = $"Hospital com ID {id} não existe.";
            return RedirectToAction("Update");
        }

        if(_context.Addresses.Find(hospital.AddressId) == null)
        {
            TempData["MessageError"] = $"Endereço com ID {id} não existe.";
            return RedirectToAction("Update");
        }

        hospital.Name = name;
        hospital.Telephone1 = telephone1;
        hospital.Telephone2 = telephone2;
        hospital.AddressId = addressId;

        _context.Hospitals.Update(hospital);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Hospital com ID {id} atualizado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Delete(int id)
    {
        Hospital hospital = _context.Hospitals.Find(id);

        if(hospital == null)
        {
            TempData["MessageError"] = $"Hospital com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        _context.Hospitals.Remove(hospital);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Hospital com ID {id} removido com sucesso.";
        return RedirectToAction("Index");
    }
}
