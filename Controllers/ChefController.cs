using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Chefs_N_Dishes.Models;
using Microsoft.EntityFrameworkCore;

namespace Chefs_N_Dishes.Controllers;

public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;

    private MyContext _context;

    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> chefs = _context.Chefs.OrderByDescending(c => c.CreatedAt).ToList();
        return View(chefs);
    }



    // This is where you can create a dish
    [HttpGet("chefs/new")]
    public ViewResult NewChef()
    {
        return View();
    }
    // processing the creating of the dish and redirects to the view all dishes on the home page
    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (!ModelState.IsValid)
        {
            return View("NewChef");
        }
        _context.Add(newChef);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
