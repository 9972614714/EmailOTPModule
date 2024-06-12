
using System.ComponentModel;

namespace EmailOTP.Domain.Enums
{
    public enum EmailStatus
    {
        [Description("STATUS_EMAIL_OK")]
        STATUS_EMAIL_OK,
        [Description("STATUS_EMAIL_FAIL")]
        STATUS_EMAIL_FAIL,
        [Description("STATUS_EMAIL_INVALID")]
        STATUS_EMAIL_INVALID
    }

    public enum OTPStatus
    {
        [Description("STATUS_OTP_OK")]
        STATUS_OTP_OK,

        [Description("STATUS_OTP_FAIL")]
        STATUS_OTP_FAIL,

        [Description("STATUS_OTP_TIMEOUT")]
        STATUS_OTP_TIMEOUT
    }
}
