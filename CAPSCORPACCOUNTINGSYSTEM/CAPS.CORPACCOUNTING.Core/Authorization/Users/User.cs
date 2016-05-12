﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.Organizations;
using Microsoft.AspNet.Identity;

namespace CAPS.CORPACCOUNTING.Authorization.Users
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User : AbpUser<User>
    {
        public const int MinPlainPasswordLength = 6;

        public virtual Guid? ProfilePictureId { get; set; }

        public virtual bool ShouldChangePasswordOnNextLogin { get; set; }

        public virtual long? UserLinkId { get; set; }

        public virtual int? LajitId { get; set; }

        //Can add application specific user properties here

        /// <summary>
        /// Creates admin <see cref="User"/> for a tenant.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="emailAddress">Email address</param>
        /// <param name="password">Password</param>
        /// <returns>Created <see cref="User"/> object</returns>
        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
                   {
                       TenantId = tenantId,
                       UserName = AdminUserName,
                       Name = AdminUserName,
                       Surname = AdminUserName,
                       EmailAddress = emailAddress,
                       Password = new PasswordHasher().HashPassword(password)
                   };
        }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public long? DefaultOrganizationId { get; set; }

        [ForeignKey("DefaultOrganizationId")]
        public OrganizationUnit DefaultOrganization { get; set; }
    }
}