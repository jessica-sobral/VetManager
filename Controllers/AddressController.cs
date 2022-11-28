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
}