//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace NC.Wpf.App.Migrations
//{
//    /// <inheritdoc />
//    public partial class Initial : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "TSamples",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
//                    Type = table.Column<string>(type: "nvarchar(20)", nullable: true),
//                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
//                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
//                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
//                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
//                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_TSamples", x => x.Id);
//                });
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "TSamples");
//        }
//    }
//}
