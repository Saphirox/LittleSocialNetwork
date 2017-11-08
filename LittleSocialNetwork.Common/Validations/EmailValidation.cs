using System.ComponentModel.DataAnnotations;

namespace LittleSocialNetwork.Common.Validations
{
    public static class EmailValidation
    {
        public static bool IsValidEmail(this string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   new EmailAddressAttribute().IsValid(email);
        }
    }
}