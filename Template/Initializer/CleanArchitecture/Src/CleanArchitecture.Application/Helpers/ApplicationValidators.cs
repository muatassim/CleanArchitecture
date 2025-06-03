using System.Net.Mail;
namespace CleanArchitecture.Application.Helpers
{
    public class ApplicationValidators
    {
        public bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public bool IsEmpty(string val) => string.IsNullOrEmpty(val);
    }
}
