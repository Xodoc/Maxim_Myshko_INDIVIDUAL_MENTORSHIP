using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitySubscription_Subscription_SubscriptionsId",
                table: "CitySubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_AspNetUsers_UserId",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

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

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "Subscriptions");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_UserId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d321008-41b9-4cbd-bfe4-c39646ce5c7d", "a612f821-ca73-47ae-afeb-a2824b54de7f", "Admin", "ADMIN" },
                    { "b26d50be-a645-4a4e-bc13-7e0f986df31d", "0250fce9-e71d-4b78-9854-2aa178f2bddc", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c4543cca-35f1-410c-a231-63f229da70fa", 0, "37a0d390-6c4f-4187-ab7e-0e114453ef60", "admin@gmail.com", false, true, null, "ADMIN@GMAIL.COM", "IVAN IVANOV", "AQAAAAEAACcQAAAAEGwcSPW0dIzB0aRaPBIPh2K/qOzjK7I/JYKgTXZI3y47KDJ+8Q5aYydmuDUPCc+6XA==", "+123656787", false, "E5BBMDK3I3PX6MZCUDSP2TGQMJNHIOU7", false, "Ivan Ivanov" },
                    { "2741fb88-83ff-43de-a203-9bb488d044d3", 0, "e8284721-561f-4575-82e2-401080558a2f", "user@gmail.com", false, true, null, "USER@GMAIL.COM", "PETER PETROV", "AQAAAAEAACcQAAAAEJZmPRboeGX0lh3ETEAFuj2nQ4axEPAMkvZTFsFc2B/lh/yAXH3ISQpJE1+DKL64Qw==", "+125656787", false, "M3ZDA3WQP6J2ZVGKBIZHOE7GKC4BR2ZF", false, "Peter Petrov" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3d321008-41b9-4cbd-bfe4-c39646ce5c7d", "c4543cca-35f1-410c-a231-63f229da70fa" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b26d50be-a645-4a4e-bc13-7e0f986df31d", "2741fb88-83ff-43de-a203-9bb488d044d3" });

            migrationBuilder.AddForeignKey(
                name: "FK_CitySubscription_Subscriptions_SubscriptionsId",
                table: "CitySubscription",
                column: "SubscriptionsId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_AspNetUsers_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitySubscription_Subscriptions_SubscriptionsId",
                table: "CitySubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_AspNetUsers_UserId",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b26d50be-a645-4a4e-bc13-7e0f986df31d", "2741fb88-83ff-43de-a203-9bb488d044d3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3d321008-41b9-4cbd-bfe4-c39646ce5c7d", "c4543cca-35f1-410c-a231-63f229da70fa" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d321008-41b9-4cbd-bfe4-c39646ce5c7d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b26d50be-a645-4a4e-bc13-7e0f986df31d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2741fb88-83ff-43de-a203-9bb488d044d3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c4543cca-35f1-410c-a231-63f229da70fa");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "Subscription");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscription",
                newName: "IX_Subscription_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CitySubscription_Subscription_SubscriptionsId",
                table: "CitySubscription",
                column: "SubscriptionsId",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_AspNetUsers_UserId",
                table: "Subscription",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
