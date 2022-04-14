using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48de1a38-8f07-4f88-9448-807ec61d3f75", "3239c7c5-a248-4ccb-8e17-62cd048f0a13", "Admin", "ADMIN" },
                    { "5e255383-c123-4d42-84af-e4cebf796d23", "d5f7a1cf-b30c-44ad-b8a2-bd7b1a54c812", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "e9bdeed7-d7e1-462a-bbe3-65a7bad6660f", 0, "c83e927f-f286-4133-9791-dd4521a5a858", "admin@gmail.com", false, true, null, "ADMIN@GMAIL.COM", "IVAN IVANOV", "AQAAAAEAACcQAAAAEBo3RPe5K+oHez8pj3E5MHMXN+oKtA4I5fNYBw8GC7A6VFWEw+zvbDvxbGggpzZxig==", "+123656787", false, "E5BBMDK3I3PX6MZCUDSP2TGQMJNHIOU7", false, "Ivan Ivanov" },
                    { "fda6077e-e829-41f7-8ad3-342a9c16786c", 0, "ab61f68b-83fc-4499-bb52-c902baffbdc6", "user@gmail.com", false, true, null, "USER@GMAIL.COM", "PETER PETROV", "AQAAAAEAACcQAAAAENMFlnSfkHXkFUU4xY1eBoNxzLv1CtQrwip8/U0xANo1qatWfVeVXLyikQD5niXDXw==", "+125656787", false, "M3ZDA3WQP6J2ZVGKBIZHOE7GKC4BR2ZF", false, "Peter Petrov" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "48de1a38-8f07-4f88-9448-807ec61d3f75", "e9bdeed7-d7e1-462a-bbe3-65a7bad6660f" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5e255383-c123-4d42-84af-e4cebf796d23", "fda6077e-e829-41f7-8ad3-342a9c16786c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "48de1a38-8f07-4f88-9448-807ec61d3f75", "e9bdeed7-d7e1-462a-bbe3-65a7bad6660f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5e255383-c123-4d42-84af-e4cebf796d23", "fda6077e-e829-41f7-8ad3-342a9c16786c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48de1a38-8f07-4f88-9448-807ec61d3f75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e255383-c123-4d42-84af-e4cebf796d23");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9bdeed7-d7e1-462a-bbe3-65a7bad6660f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fda6077e-e829-41f7-8ad3-342a9c16786c");
        }
    }
}
