namespace EmailOTP.Domain.Service
{
    public interface IEmailService
    {
        /// <summary>
        /// This method handles the email sending process.
        /// </summary>
        /// <param name="toEmail">Receipient Mail Id.</param>
        /// <param name="subject">Subject line for the email.</param>
        /// <param name="body">Body for the email.</param>
        /// <returns>Return true if email sent successfully, else false.</returns>
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }
}
