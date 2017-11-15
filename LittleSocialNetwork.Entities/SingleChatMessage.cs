using System;
using LittleSocialNetwork.Common.Definitions.Enums;

namespace LittleSocialNetwork.Entities
{
    public class SingleChatMessage : IEntity
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime? LastEdited { get; set; }
        public DeletedMessage IsDeleted { get; set; }
        public long FromId { get; set; }
        public UserProfile From { get; set; }
        public long ToId { get; set; }
        public UserProfile To { get; set; }
    }
}