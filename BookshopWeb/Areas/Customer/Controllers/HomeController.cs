using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookshopDataAccess.Data;
using BookshopModels.Models;
using BookshopDataAccess.Repository.IRepository;

namespace BookshopWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{   
    private readonly ILogger<HomeController> _logger;

    public readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;

    }

    public IActionResult Index()
    {
        IEnumerable<Product> productsList = _unitOfWork.Product.GetAll(includeProperties: "Category");
        return View(productsList);
    }

    public IActionResult Details(int id)
    {
        var product = _unitOfWork.Product.Get(x=>x.Id==id,includeProperties: "Category");
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
