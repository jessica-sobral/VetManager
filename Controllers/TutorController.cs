using Microsoft.AspNetCore.Mvc;
using VetManager.Models;

namespace VetManager.Controllers;

public class TutorController : Controller
{
    private readonly VetManagerContext _context;

    public TutorController(VetManagerContext context)
    {
        _context = context;
    }

     public IActionResult Index()
    {
        return View(_context.Tutors);
    }

    public IActionResult Show(int id)
    {
        Tutor tutor = _context.Tutors.Find(id);

        if(tutor == null)
        {
            TempData["MessageError"] = $"Tutor com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        Address address = _context.Addresses.Find(tutor.AddressId);

        ViewData["Address"] = new Address(
            address.Id,
            address.ZipCode,
            address.Street,
            address.Number,
            address.District,
            address.City,
            address.State
        );

        return View(tutor);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] int id, [FromForm] string name, [FromForm] string cpf, [FromForm] int addressId, [FromForm] string telephone)
    {
        Tutor tutor = new Tutor(id, name, cpf, addressId, telephone);

        if(_context.Addresses.Find(tutor.AddressId) == null)
        {
            TempData["MessageError"] = $"Endereço com ID {tutor.Id} não existe.";
            return RedirectToAction("Create");
        }

        if(_context.Tutors.Find(tutor.Id) != null)
        {
            TempData["MessageError"] = $"Tutor com ID {tutor.Id} já existe.";
            return RedirectToAction("Create");
        }
        
        _context.Tutors.Add(tutor);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Tutor com ID {tutor.Id} cadastrado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Update(int id)
    {
        Tutor tutor = _context.Tutors.Find(id);

        if(tutor == null)
        {
            TempData["MessageError"] = $"Tutor com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        return View(tutor);
    }

    [HttpPost]
    public IActionResult Save([FromForm] int id, [FromForm] string name, [FromForm] string cpf, [FromForm] int addressId, [FromForm] string telephone)
    {
        Tutor tutor = _context.Tutors.Find(id);

        if(_context.Addresses.Find(tutor.AddressId) == null)
        {
            TempData["MessageError"] = $"Endereço com ID {tutor.Id} não existe.";
            return RedirectToAction("Update", new{ id = id });
        }

        if(tutor == null)
        {
            TempData["MessageError"] = $"Tutor com ID {id} não existe.";
            return RedirectToAction("Update", new{ id = id });
        }

        tutor.Name = name;
        tutor.Cpf = cpf;
        tutor.AddressId = addressId;
        tutor.Telephone = telephone;

        _context.Tutors.Update(tutor);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Tutor com ID {id} atualizado com sucesso.";
        return RedirectToAction("Show", new { id = id });
    }

    public IActionResult Delete(int id)
    {
        Tutor tutor = _context.Tutors.Find(id);

        if(tutor == null)
        {
            TempData["MessageError"] = $"Tutor com ID {id} não existe.";
            return RedirectToAction("Index");
        }

        _context.Tutors.Remove(tutor);
        _context.SaveChanges();

        TempData["MessageSuccess"] = $"Tutor com ID {id} removido com sucesso.";
        return RedirectToAction("Index");
    }
} 