using System;
using System.Collections.Generic;

namespace BETSTS.Contract.Repository.Models.User
{
    public class UserEntity : Entity
    {
        public string UserName { get; set; }

        // Password
        public string PasswordHash { get; set; }
        public DateTimeOffset? PasswordLastUpdatedTime { get; set; }

        // Phone
        public string Phone { get; set; }
        public DateTimeOffset? PhoneConfirmedTime { get; set; }

        // OTP
        public string OTP { get; set; }
        public DateTimeOffset? OTPExpireTime { get; set; }

        // Email
        public string Email { get; set; }

        public DateTimeOffset? EmailConfirmedTime { get; set; }

        public string ConfirmEmailToken { get; set; }

        public DateTimeOffset? ConfirmEmailTokenExpireTime { get; set; }

        // Set Password
        public string SetPasswordToken { get; set; }

        public DateTimeOffset? SetPasswordTokenExpireTime { get; set; }

        // Ban
        public DateTimeOffset? BannedTime { get; set; }

        public string BannedRemark { get; set; }

        // Profile
        public virtual UserProfileEntity Profile { get; set; }

        //is Admin
        public int? IsAdmin { get; set; }

        public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
        public AmoutEntity Amout { get; set; }
        public virtual ICollection<UserBetEntity> UserBets { get; set; }

    }
}