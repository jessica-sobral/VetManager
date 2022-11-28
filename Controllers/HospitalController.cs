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
}
