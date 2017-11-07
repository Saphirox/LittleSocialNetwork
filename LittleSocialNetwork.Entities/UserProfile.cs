using System;
using LittleSocialNetwork.Common.Definitions.Enums;

namespace LittleSocialNetwork.Entities
{
    public class UserProfile : IEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
    }
}