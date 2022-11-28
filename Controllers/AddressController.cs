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
}