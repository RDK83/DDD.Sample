using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogue.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LU_DeliveryMethod",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LU_DeliveryMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LU_ManufacturerClassification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LU_ManufacturerClassification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LU_MediaType",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LU_MediaType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MerchantCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Address_AddressLine1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address_AddressLine2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_AddressLine3 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_AddressLine4 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_County = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Address_PostalCode_CountryId = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Address_PostalCode_Postcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ProductTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ProductTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    WarehouseCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Address_AddressLine1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address_AddressLine2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_AddressLine3 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_AddressLine4 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_County = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Address_PostalCode_CountryId = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Address_PostalCode_Postcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseMerchant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    MerchantId = table.Column<int>(type: "int", nullable: false),
                    PreferenceOrder = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseMerchant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TaxClass = table.Column<byte>(type: "tinyint", nullable: false),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false),
                    ProductSubTypeID = table.Column<int>(type: "int", nullable: false),
                    ProductTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ManufacturerClassificationID = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SF_Products_SF_LU_ManufacturerClassification",
                        column: x => x.ManufacturerClassificationID,
                        principalTable: "LU_ManufacturerClassification",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SF_MS_MediaStorage_SF_MS_MediaType",
                        column: x => x.TypeId,
                        principalTable: "LU_MediaType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSubType",
                columns: table => new
                {
                    ProductSubTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false),
                    ProductSubType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubType", x => x.ProductSubTypeID);
                    table.ForeignKey(
                        name: "FK_ProductSubType_ProductType_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseDeliveryMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    DeliveryMethodId = table.Column<string>(type: "char(2)", nullable: false),
                    CountryIsoCode = table.Column<string>(type: "char(3)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    GrossCost_Currency = table.Column<string>(type: "char(3)", nullable: false),
                    GrossCost_Value = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    MaxLeadTime = table.Column<byte>(type: "tinyint", nullable: false),
                    MinLeadTime = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseDeliveryMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DL_WarehouseDeliveryMethod_LU_DeliveryMethod",
                        column: x => x.DeliveryMethodId,
                        principalTable: "LU_DeliveryMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseDeliveryMethod_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    MerchantId = table.Column<int>(type: "int", nullable: false),
                    StockLevel = table.Column<int>(type: "int", nullable: false),
                    GrossPrice_Currency = table.Column<string>(type: "char(3)", nullable: false),
                    GrossPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_Product_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMedia",
                columns: table => new
                {
                    ProductCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    MediaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMedia", x => new { x.ProductCode, x.MediaID });
                    table.ForeignKey(
                        name: "FK_ProductMedia_Product_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "uc_ManClass",
                table: "LU_ManufacturerClassification",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_TypeId",
                table: "Media",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_MerchantCode",
                table: "Merchant",
                column: "MerchantCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ProductCode",
                table: "Offer",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ManufacturerClassificationID",
                table: "Product",
                column: "ManufacturerClassificationID");

            migrationBuilder.CreateIndex(
                name: "uc_SubType",
                table: "ProductSubType",
                columns: new[] { "ProductTypeID", "ProductSubType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uc_ProductType",
                table: "ProductType",
                column: "ProductType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_WarehouseCode",
                table: "Warehouse",
                column: "WarehouseCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseDeliveryMethod_DeliveryMethodId",
                table: "WarehouseDeliveryMethod",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseDeliveryMethod_WarehouseId",
                table: "WarehouseDeliveryMethod",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMerchant_PreferenceOrder",
                table: "WarehouseMerchant",
                column: "PreferenceOrder",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "ProductMedia");

            migrationBuilder.DropTable(
                name: "ProductSubType");

            migrationBuilder.DropTable(
                name: "WarehouseDeliveryMethod");

            migrationBuilder.DropTable(
                name: "WarehouseMerchant");

            migrationBuilder.DropTable(
                name: "LU_MediaType");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "LU_DeliveryMethod");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "LU_ManufacturerClassification");
        }
    }
}
