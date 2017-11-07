using System;
using System.ComponentModel.DataAnnotations;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class RegisterApiModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
        [Required, Compare(nameof(Password))]
        public string ConfirmPassoword { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(16)]
        public string FirstName { get; set; }
        [Required, StringLength(16)]
        public string LastName { get; set; }
        [EnumDataType(typeof(Sex))]
        public Sex Sex { get; set; }
    }

    public static class RegisterApiModelExtensions
    {
        public static User To(this RegisterApiModel model, UserRole role)
        {
            return new User
            {
                Role = role,
                Password = model.Password,
                Email = model.Email,
                UserProfile = new UserProfile
                {
                    BirthDate = model.BirthDate,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Sex = model.Sex
                }
            };
        }
    }
}