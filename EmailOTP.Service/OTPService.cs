using EmailOTP.Domain.Enums;
using EmailOTP.Domain.Models;
using EmailOTP.Domain.Repositories;
using EmailOTP.Domain.Service;
using EmailOTP.Domain.Validators;

namespace EmailOTP.Service
{
   
    public class OTPService : IOTPService
    {
        private readonly IEmailService _emailService;
        private readonly IEmailOTPRepository _emailOTPRepository;

        #region Public_Methods
        public OTPService(IEmailService emailService, IEmailOTPRepository emailOTPRepository)
        {
            _emailService = emailService;
            _emailOTPRepository = emailOTPRepository;
        }

        public async Task<EmailStatus> GenerateOTPAsync(string email)
        {
            try
            {
                var emailValidator = new EmailValidators();

                if (!emailValidator.Validate(email).IsValid)
                {
                    return EmailStatus.STATUS_EMAIL_INVALID;
                }

                string otp = new Random().Next(10000, 99999).ToString();

                string emailBody = $"Your OTP Code is {otp}. The code is valid for 1 minute";

                bool wasMailSent = await _emailService.SendEmailAsync(email, otp, emailBody);

                if (wasMailSent)
                {
                    UserOTPLog userOTPLog = new UserOTPLog()
                    {
                        Email = email,
                        OTP = otp,
                        ExpirationTime = DateTime.UtcNow.AddMinutes(1),
                    };

                    await _emailOTPRepository.SaveUserOTPAsync(userOTPLog);
                    return EmailStatus.STATUS_EMAIL_OK;
                }

                return EmailStatus.STATUS_EMAIL_FAIL;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        

        public async Task<OTPStatus> CheckOtpAsync(string email, Stream input)
        {
            try
            {
                var buffer = new byte[6];
                await input.ReadAsync(buffer);
                var userInput = System.Text.Encoding.UTF8.GetString(buffer).TrimEnd('\0');

                var otpEntry = await _emailOTPRepository.GetOTPInfoByEmailAsync(email);
                if (otpEntry == null || otpEntry.ExpirationTime <= DateTime.UtcNow)
                {
                    return OTPStatus.STATUS_OTP_TIMEOUT;
                }


                if (otpEntry.Attempts >= 10)
                {
                    return OTPStatus.STATUS_OTP_FAIL;
                }
                otpEntry.Attempts++;

                await _emailOTPRepository.UpdateUserOTPAsync(otpEntry);

                if (otpEntry.OTP == userInput)
                {
                    return OTPStatus.STATUS_OTP_OK;
                }
                return OTPStatus.STATUS_OTP_FAIL;
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

    }
}
