using EmailOTP.Domain.Enums;


namespace EmailOTP.Domain.Service
{
    public interface IOTPService
    {

        /// <summary>
        /// Validates the given email and generates the OTP for the same.
        /// </summary>
        /// <param name="email">Holds the email address.</param>
        /// <returns> STATUS_EMAIL_OK: email containing OTP has been sent successfully.
        /// STATUS_EMAIL_FAIL: email address does not exist or sending to the email has failed.
        /// STATUS_EMAIL_INVALID: email address is invalid.</returns>
        public Task<EmailStatus> GenerateOTPAsync(string email);


        /// <summary>
        /// Validates the given mail and otp are matching and it is not expired.
        /// </summary>
        /// <param name="email">Holds the email address.</param>
        /// <param name="input">Holds the otp value.</param>
        /// <returns>
        /// STATUS_OTP_OK: OTP is valid and checked 
        /// STATUS_OTP_FAIL: OTP is wrong after 10 tries
        /// STATUS_OTP_TIMEOUT: timeout after 1 min 
        /// </returns>
        Task<OTPStatus> CheckOtpAsync(string email, Stream input);
    }

}
