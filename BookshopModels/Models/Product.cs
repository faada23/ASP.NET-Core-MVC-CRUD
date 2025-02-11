using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookshopModels.Models;

public class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }
    //[ValidateNever]
    public Category? Category { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = "Title should be between 2 and 50 characters")]
    [Required]
    public string Title{ get; set; } =null!;

    [StringLength(500, MinimumLength = 4, ErrorMessage = "Description should be between 4 and 500 characters")]
    public string? Description { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 5, ErrorMessage = "ISBN should be between 5 and 25 characters")]
    public string ISBN {get;set;} =null!;

    [Required]
    [StringLength(35, MinimumLength = 4, ErrorMessage = "Author should be between 4 and 35 characters")]
    public string Author { get; set;} =null!;

    [Display(Name = "List Price")]
    [Range(1, 1000)]
    public double ListPrice { get; set; }

    [Display(Name = "Price for 1-50")]
    [Range(1, 1000)]
    public double Price { get; set; }

    [Display(Name = "Price for 51-100")]
    [Range(1, 1000)]    
    public double Price50 { get; set; }

    [Display(Name = "Price for 100+")]
    [Range(1, 1000)]
    public double Price100 { get; set; }  

    [ValidateNever]  
    public string ImageUrl { get; set; } =null!;


}