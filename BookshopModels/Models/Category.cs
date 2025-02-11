using System;
using System.ComponentModel.DataAnnotations;

namespace BookshopModels.Models;

public class Category
{
    public int Id { get; set; }

    [Display(Name = "Category Name")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Category Name should be between 3 and 20 characters")]
    public string Name { get; set; } =null!;

    [Display(Name = "Display Order")]
    [Range (1, 100, ErrorMessage = "Display Order must be between 1 and 100.")]
    public int DisplayOrder { get; set; }

}
