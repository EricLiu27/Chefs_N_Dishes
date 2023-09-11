using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Chefs_N_Dishes.Models;
using Microsoft.EntityFrameworkCore;

namespace Chefs_N_Dishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;

    private MyContext _context;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("dishes")]
    public ViewResult ViewAllDish()
    {
        List<Dish> Dishes = _context.Dishes.Include(d => d.Cook).OrderByDescending(d => d.CreatedAt).ToList();
        return View(Dishes);
    }



    // This is where you can create a dish
    [HttpGet("dishes/new")]
    public ViewResult NewDish()
    {
        List<Chef> Chefs = _context.Chefs.OrderByDescending(c => c.CreatedAt).ToList();


        ViewBag.Chefs = Chefs;
        return View("NewDish");
    }


    // processing the creating of the dish and redirects to the view all dishes on the home page
    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {
            return NewDish();
        }

        _context.Add(newDish);
        _context.SaveChanges();
        return RedirectToAction("ViewAllDish");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
