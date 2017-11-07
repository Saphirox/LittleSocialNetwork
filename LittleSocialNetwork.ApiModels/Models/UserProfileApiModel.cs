using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class UserProfileApiModel
    {
        public long Id { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(16)]
        public string FirstName { get; set; }
        [Required, StringLength(16)]
        public string LastName { get; set; }
        [EnumDataType(typeof(Sex))]
        public Sex Sex { get; set; }

        public static UserProfileApiModel From(UserProfile entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new UserProfileApiModel
            {
                Id = entity.Id,
                Sex = entity.Sex,
                BirthDate = entity.BirthDate,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }

    public static class UserProfileExtension
    {
        public static UserProfile To(this UserProfileApiModel model)
        {
            return new UserProfile
            {
                Id = model.Id,
                BirthDate = model.BirthDate,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Sex = model.Sex
            };
        }
    }
}