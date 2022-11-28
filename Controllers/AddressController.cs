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
        Address address = new Address(id, zipCode, street, number, district, city, state);

        if(_context.Addresses.Find(address.Id) == null)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }
        else
        {
            TempData["MessageError"] = $"Endereço com ID {address.Id} já existe.";
            return RedirectToAction("Create");
        }

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

        if(address != null)
        {
            _context.Addresses.Remove(address);
            _context.SaveChanges();

            TempData["MessageSuccess"] = $"Endereço com ID {id} removido com sucesso.";
            return RedirectToAction("Index");
        }


        TempData["MessageError"] = $"Endereço com ID {id} não existe.";
        return RedirectToAction("Index");
    }
}