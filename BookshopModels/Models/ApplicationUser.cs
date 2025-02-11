using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookshopModels.Models;
public class ApplicationUser : IdentityUser {
    [Required]
    public string Name{get;set;} =null!;

    public string? StreetAdress {get;set;}
    public string? City {get;set;}

    public string? State {get;set;}

    public string? PostalCode {get;set;}


}