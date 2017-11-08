using System.ComponentModel.DataAnnotations;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class RestorePasswordViaEmailApiModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string NewPassword { get; set; }
    }
}