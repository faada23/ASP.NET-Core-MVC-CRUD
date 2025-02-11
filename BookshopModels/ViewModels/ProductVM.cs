using Microsoft.AspNetCore.Mvc.Rendering;
using BookshopModels.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookshopModels.ViewModels;

public class ProductVM
{
    public Product Product { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}