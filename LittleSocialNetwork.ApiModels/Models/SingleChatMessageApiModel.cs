using System;
using System.ComponentModel.DataAnnotations;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class SingleChatMessageApiModel
    {
        public long? Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime? PostTime { get; set; }
        public DateTime? LastEdited { get; set; }
        public long? FromId { get; set; }
        public long ToId { get; set; }
        public string FullName { get; set; }

        public static SingleChatMessageApiModel From(SingleChatMessage entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new SingleChatMessageApiModel
            {
                FullName = entity.From.FirstName + " " + entity.From.LastName,
                Text = entity.Text,
                FromId = entity.FromId,
                ToId = entity.ToId,
                PostTime = entity.PostTime,
                LastEdited = DateTime.UtcNow
            };
        }
    }

    public static class SingleChatMessageApiModelExtension
    {
        public static SingleChatMessage To(this SingleChatMessageApiModel model, long currentUserId)
        {
            return new SingleChatMessage
            {
                Text = model.Text,
                FromId = currentUserId,
                ToId = model.ToId,
                PostTime = model.PostTime ?? DateTime.UtcNow,
                LastEdited = DateTime.UtcNow
            };
        }
    }
}