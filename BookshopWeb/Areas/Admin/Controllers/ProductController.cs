using Microsoft.AspNetCore.Mvc;
using BookshopModels.Models;
using BookshopDataAccess.Repository.IRepository;
using BookshopModels.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using BookshopUtility;

namespace BookshopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork UnitOfWork;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            UnitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            List<Product> ProductsList = UnitOfWork.Product.GetAll("Category").ToList();

            return View(ProductsList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
              CategoryList = UnitOfWork.Category.GetAll()
                .Select(i => new SelectListItem
                    {
                        Text = i.Name, Value = i.Id.ToString()
                    }),  
            };

            if (id == null || id == 0)
            {   
                //create
                productVM.Product = new Product();
                return View(productVM);
            }
            else
            {   
                //update
                productVM.Product = UnitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM,IFormFile? file)
        {   
            if (ModelState.IsValid)
            {   
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);    
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    if(!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, ProductVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create)){
                        file.CopyTo(fileStream);
                    }
                    
                    ProductVM.Product.ImageUrl = @"\images\products\" + fileName;

                }
                
                if(ProductVM.Product.Id == 0)
                {
                    UnitOfWork.Product.Add(ProductVM.Product);
                }
                else
                {
                    UnitOfWork.Product.Update(ProductVM.Product);
                }

                UnitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            else{
                ProductVM.CategoryList = UnitOfWork.Category.GetAll().Select(i => new SelectListItem{
                    Text = i.Name, Value = i.Id.ToString()
                    });

                return View(ProductVM);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = UnitOfWork.Product.GetAll("Category").ToList();
            return Json(new { data = objProductList });
        }
        #endregion

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productToBeDeleted = UnitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            UnitOfWork.Product.Delete(productToBeDeleted);
            UnitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
