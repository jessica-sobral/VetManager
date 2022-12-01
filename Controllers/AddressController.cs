using Microsoft.AspNetCore.Mvc;
using VetManager.Models;

namespace VetManager.Controllers;

public class AddressController : Controller
{
    private readonly VetManagerContext _context;

    public AddressController(VetManagerContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Addresses);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] int id, [FromForm] string zipCode, [FromForm] string street, [FromForm] string number, [FromForm] string district, [FromForm] string city, [FromForm] string state)
    {
        try
        {
            if(zipCode.Length != 8) throw new FormatException();
            
            int bg = int.Parse(zipCode);
        }
        catch(FormatException)
        {
            TempData["MessageError"] = $"O CEP deve ser digitado apenas em números e sem qualquer tipo de pontuação.";
            return RedirectToAction("Create");
        }

        Address address = new Address(id, zipCode, street, number, district, city, state);

        if(_context.Addresses.Find(address.Id) != null)
        {
            TempData["MessageError"] = $"Endereço com ID {address.Id} já existe.";
            return RedirectToAction("Create");
        }
        
        _context.Addresses.Add(address);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Endereço com ID {address.Id} cadastrado com sucesso.";
        return RedirectToAction("Index");
    }

    public IActionResult Update(int id)
    {
        Address address = _context.Addresses.Find(id);

        if(address == null)
        {
            TempData["MessageError"] = $"Endereço com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        return View(address);
    }

    [HttpPost]
    public IActionResult Save([FromForm] int id, [FromForm] string zipCode, [FromForm] string street, [FromForm] string number, [FromForm] string district, [FromForm] string city, [FromForm] string state)
    {
        try
        {
            if(zipCode.Length != 8) throw new FormatException();
            
            int bg = int.Parse(zipCode);
        }
        catch(FormatException)
        {
            TempData["MessageError"] = $"O CEP deve ser digitado apenas em números e sem qualquer tipo de pontuação.";
            return RedirectToAction("Create");
        }

        Address address = _context.Addresses.Find(id);

        if(address == null)
        {
            TempData["MessageError"] = $"Endereço com ID {id} não existe.";
            return RedirectToAction("Update");
        }

        address.ZipCode = zipCode;
        address.Street = street;
        address.Number = number;
        address.District = district;
        address.City = city;
        address.State = state;

        _context.Addresses.Update(address);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Endereço com ID {id} atualizado com sucesso.";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        Address address = _context.Addresses.Find(id);

        if(address == null)
        {
            TempData["MessageError"] = $"Endereço com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        foreach(var hospital in _context.Hospitals)
        {
            if(hospital.AddressId == address.Id)
            {
                TempData["MessageError"] = $"Endereço com ID {id} pertence a algum hospital.";
                return RedirectToAction("Index");
            }
        }

        foreach(var doctor in _context.Doctors)
        {
            if(doctor.AddressId == address.Id)
            {
                TempData["MessageError"] = $"Endereço com ID {id} pertence a algum médico.";
                return RedirectToAction("Index");
            }
        }

        foreach(var tutor in _context.Tutors)
        {
            if(tutor.AddressId == address.Id)
            {
                TempData["MessageError"] = $"Endereço com ID {id} pertence a algum paciente.";
                return RedirectToAction("Index");
            }
        }

        _context.Addresses.Remove(address);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Endereço com ID {id} removido com sucesso.";
        return RedirectToAction("Index");
    }
}