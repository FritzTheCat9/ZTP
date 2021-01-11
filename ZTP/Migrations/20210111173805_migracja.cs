using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZTP.Migrations
{
    public partial class migracja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Town = table.Column<string>(nullable: false),
                    PostCode = table.Column<string>(nullable: false),
                    Street = table.Column<string>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    ShippingMethodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.ShippingMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    AdminRights = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Promotion = table.Column<bool>(nullable: false),
                    VAT = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(nullable: false),
                    ShippingMethodID = table.Column<int>(nullable: false),
                    PaymentMethodID = table.Column<int>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_ShippingMethods_ShippingMethodID",
                        column: x => x.ShippingMethodID,
                        principalTable: "ShippingMethods",
                        principalColumn: "ShippingMethodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    ProductOrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.ProductOrderID);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressID", "Country", "CustomerID", "HouseNumber", "PostCode", "Street", "Town" },
                values: new object[,]
                {
                    { 1, "Polska", 1, 12, "12-123", "Miła", "Białystok" },
                    { 2, "Polska", 2, 32, "23-543", "Piękna", "Łomża" },
                    { 3, "Polska", 3, 43, "56-765", "Bursztynowa", "Gdańsk" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { 1, "Laptopy" },
                    { 2, "Smartfony" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "Name" },
                values: new object[,]
                {
                    { 1, "Szybki przelew" },
                    { 2, "Przelew" },
                    { 3, "Karta" }
                });

            migrationBuilder.InsertData(
                table: "ShippingMethods",
                columns: new[] { "ShippingMethodID", "Name" },
                values: new object[,]
                {
                    { 1, "Kurier UPS" },
                    { 2, "Kurier DPD" },
                    { 3, "Poczta Polska" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "AddressID", "AdminRights", "FirstName", "LastName", "Login", "Password" },
                values: new object[,]
                {
                    { 1, 1, true, "Jan", "Kowalski", "kowalski", "kowalski" },
                    { 2, 2, false, "Robert", "Nowak", "nowak", "nowak" },
                    { 3, 3, false, "Krzysztof", "Piekarski", "piekarski", "piekarski" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "DateAdded", "Description", "Image", "Name", "Price", "Promotion", "Quantity", "VAT" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2018, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dobry laptop LENOVO", "~/Images/Laptop LENOVO.jpg", "Laptop LENOVO", 4300m, false, 20, 23 },
                    { 2, 1, new DateTime(2019, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dobry laptop HUAWEI", "~/Images/Laptop HUAWEI.png", "Laptop HUAWEI", 5000m, false, 59, 23 },
                    { 3, 2, new DateTime(2017, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dobry smartfon HUAWEI P30", "~/Images/Smartfon HUAWEI P30.jpg", "Smartfon HUAWEI P30", 2999m, true, 67, 23 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "CustomerID", "OrderStatus", "PaymentMethodID", "Price", "ShippingMethodID" },
                values: new object[,]
                {
                    { 1, 1, 0, 3, 4300m, 1 },
                    { 2, 2, 1, 2, 5000m, 2 },
                    { 4, 2, 2, 1, 2999m, 3 },
                    { 3, 3, 1, 1, 2999m, 2 },
                    { 5, 3, 2, 3, 9300m, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProductOrders",
                columns: new[] { "ProductOrderID", "OrderID", "ProductID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 4, 4, 3 },
                    { 3, 3, 3 },
                    { 5, 5, 1 },
                    { 6, 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressID",
                table: "Customers",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodID",
                table: "Orders",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingMethodID",
                table: "Orders",
                column: "ShippingMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_OrderID",
                table: "ProductOrders",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductID",
                table: "ProductOrders",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
