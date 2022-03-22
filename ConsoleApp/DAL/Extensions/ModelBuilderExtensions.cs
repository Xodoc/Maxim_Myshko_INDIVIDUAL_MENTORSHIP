using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using System;
using System.Collections.Generic;

namespace DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        private static readonly string _password = "BestPassword";

        private static string[] _roleIds;
        private static string[] _userIds;

        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.FillRoles()
                   .FillUsers()
                   .FillUserRoles();

            return builder;
        }

        public static ModelBuilder FillRoles(this ModelBuilder builder)
        {
            var roles = new IdentityRole[]
            {
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = RoleTypes.Admin.ToString(),
                    NormalizedName = RoleTypes.Admin.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = RoleTypes.User.ToString(),
                    NormalizedName = RoleTypes.User.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }            
            };

            _roleIds = new string[roles.Length];

            for (int i = 0; i < roles.Length; i++)
            {
                _roleIds[i] = roles[i].Id;
            }

            builder.Entity<IdentityRole>().HasData(roles);

            return builder;
        }

        public static ModelBuilder FillUsers(this ModelBuilder builder)
        {
            var userName1 = "Ivan Ivanov";
            var email1 = "admin@gmail.com";
            var userName2 = "Peter Petrov";
            var email2 = "user@gmail.com";

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var users = new IdentityUser[]
            {
                new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userName1,
                    NormalizedUserName = userName1.ToUpper(),
                    Email = email1,
                    NormalizedEmail = email1.ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = passwordHasher.HashPassword(null, _password),
                    SecurityStamp = "E5BBMDK3I3PX6MZCUDSP2TGQMJNHIOU7",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = "+123656787",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userName2,
                    NormalizedUserName = userName2.ToUpper(),
                    Email = email2,
                    NormalizedEmail = email2.ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = passwordHasher.HashPassword(null, _password),
                    SecurityStamp = "M3ZDA3WQP6J2ZVGKBIZHOE7GKC4BR2ZF",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = "+125656787",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            };

            _userIds = new string[users.Length];

            for (int i = 0; i < users.Length; i++)
            {
                _userIds[i] = users[i].Id;
            }

            builder.Entity<IdentityUser>().HasData(users);

            return builder;
        }

        public static ModelBuilder FillUserRoles(this ModelBuilder builder)
        {
            var userRoles = new List<IdentityUserRole<string>>();

            for (int i = 0; i < _roleIds.Length; i++)
            {
                userRoles.Add(new IdentityUserRole<string>
                {
                    UserId = _userIds[i],
                    RoleId = _roleIds[i],
                });
            }

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            return builder;
        }
    }
}
