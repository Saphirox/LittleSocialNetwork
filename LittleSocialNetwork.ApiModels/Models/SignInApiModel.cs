using System.ComponentModel.DataAnnotations;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class SignInApiModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public static class SignInApiModelExtension
    {
        public static User To(this SignInApiModel model)
        {
            return new User
            {
                Email = model.Email,
                Password = model.Password
            };
        }
    }
}