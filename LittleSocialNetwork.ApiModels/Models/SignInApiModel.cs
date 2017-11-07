using System.ComponentModel.DataAnnotations;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class SignInApiModel
    {
        public string Email { get; set; }
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