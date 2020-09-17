using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedLikeColum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("12563110-0d8c-417c-b539-7ae4854f3b83"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("287243c9-583b-4638-9e5a-87afeda23530"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6beff27e-32b7-4b31-bf91-c74181dc3910"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6dca59d8-1348-4c0c-8b80-adf0ccc742cd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7c0efb9c-bcd8-438e-8eb6-f65c3f86854d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("851dd67d-a418-422b-ac52-d72904c5dd01"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("956d1ef1-4281-47f4-a80b-34e94c78ef02"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9ad9515a-4a47-4df2-9ade-4902315e3014"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c3a244f6-589b-4f8e-a21f-94c564b8a11a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d488cedf-213b-49e8-818b-64eaa3d3bc9e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e7614950-eb39-4764-b276-95f0eb4d6411"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ef754ff5-b7fb-4c60-b654-63cbe60aa4ed"));

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "UserPostLikes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "UserCommentLikes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("2374f24f-cfa3-4331-ba58-6d2a0a083a63"), false, "Politics" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("c62e7abc-76e2-491f-8863-002731d3e3f0"), false, "Economics" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("1149ae42-6988-4219-88e2-4f13a2695c34"), false, "India" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("983ea53e-9d91-44f9-b1a2-695852134982"), false, "World" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("3d19102a-ca17-4fbe-8898-35af03d3a543"), false, "Sports" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("c6e2cc57-3480-476d-9049-931048b62d53"), false, "Random" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("c2c7074b-120b-43de-96ad-c3566d8792b9"), false, "Entertainment" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("7e139316-0173-46ca-9a71-2c862dabdad8"), false, "Good Life" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("4d4c1a00-2437-43b8-8c3f-a095b6cf98e1"), false, "Fashion And Style" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("e047042f-b266-4051-bf63-62e3e89738f4"), false, "Writing" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("456d694e-3990-4f11-8acb-82f0658c5066"), false, "Computers" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("eb3e0cfc-8485-48c9-bcd8-d4e7e6228feb"), false, "Philosophy" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1149ae42-6988-4219-88e2-4f13a2695c34"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2374f24f-cfa3-4331-ba58-6d2a0a083a63"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3d19102a-ca17-4fbe-8898-35af03d3a543"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("456d694e-3990-4f11-8acb-82f0658c5066"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4d4c1a00-2437-43b8-8c3f-a095b6cf98e1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7e139316-0173-46ca-9a71-2c862dabdad8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("983ea53e-9d91-44f9-b1a2-695852134982"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c2c7074b-120b-43de-96ad-c3566d8792b9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c62e7abc-76e2-491f-8863-002731d3e3f0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c6e2cc57-3480-476d-9049-931048b62d53"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e047042f-b266-4051-bf63-62e3e89738f4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("eb3e0cfc-8485-48c9-bcd8-d4e7e6228feb"));

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "UserPostLikes");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "UserCommentLikes");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("956d1ef1-4281-47f4-a80b-34e94c78ef02"), false, "Politics" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("d488cedf-213b-49e8-818b-64eaa3d3bc9e"), false, "Economics" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("851dd67d-a418-422b-ac52-d72904c5dd01"), false, "India" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("c3a244f6-589b-4f8e-a21f-94c564b8a11a"), false, "World" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("ef754ff5-b7fb-4c60-b654-63cbe60aa4ed"), false, "Sports" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("9ad9515a-4a47-4df2-9ade-4902315e3014"), false, "Random" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("6beff27e-32b7-4b31-bf91-c74181dc3910"), false, "Entertainment" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("287243c9-583b-4638-9e5a-87afeda23530"), false, "Good Life" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("e7614950-eb39-4764-b276-95f0eb4d6411"), false, "Fashion And Style" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("7c0efb9c-bcd8-438e-8eb6-f65c3f86854d"), false, "Writing" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("6dca59d8-1348-4c0c-8b80-adf0ccc742cd"), false, "Computers" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Value" },
                values: new object[] { new Guid("12563110-0d8c-417c-b539-7ae4854f3b83"), false, "Philosophy" });
        }
    }
}
