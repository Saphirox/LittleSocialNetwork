using System;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.DataAccess.Extensions
{
    public static class SingleChatMessageExtensions
    {
        public static void Update(this SingleChatMessage dest, SingleChatMessage source)
        {
            dest.Text = source.Text;
            dest.LastEdited = DateTime.UtcNow;
        }
    }
}