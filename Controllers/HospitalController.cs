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
}
