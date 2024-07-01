﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exercise02.Migrations
{
    /// <inheritdoc />
    public partial class CreateEXcerise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iso = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    upper_name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    iso3 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    num_code = table.Column<short>(type: "smallint", nullable: true),
                    phone_code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    registered_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    privileges = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cards", x => x.id);
                    table.ForeignKey(
                        name: "FK_cards_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "customer_addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address_line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address_line2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    dial_code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    postal_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_addresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_customer_addresses_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staff_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    placeholder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_staff_accounts_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_staff_accounts_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_staff_accounts_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attribute_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_attributes_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_attributes_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    category_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    category_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    placeholder = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_categories_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_categories_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_categories_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "coupons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    discount_value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    discount_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    times_used = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    max_usage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    order_amount_limit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    coupon_start_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    coupon_end_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coupons", x => x.id);
                    table.ForeignKey(
                        name: "FK_coupons_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_coupons_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seen = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    receive_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    notification_expiry_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_staff_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order_statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    privacy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_statuses", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_statuses_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_order_statuses_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    product_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sku = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sale_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    compare_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    buying_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    short_description = table.Column<string>(type: "nvarchar(165)", maxLength: 165, nullable: true),
                    product_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_type = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    published = table.Column<bool>(type: "bit", nullable: true),
                    disable_out_of_stock = table.Column<bool>(type: "bit", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_products_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "shipping_zones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    free_shipping = table.Column<bool>(type: "bit", nullable: false),
                    rate_type = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_zones", x => x.id);
                    table.ForeignKey(
                        name: "FK_shipping_zones_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_shipping_zones_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "slideshows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_slideshows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_slideshows_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_slideshows_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supplier_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    company = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address_line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address_line2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: false),
                    city = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.id);
                    table.ForeignKey(
                        name: "FK_suppliers_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suppliers_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_suppliers_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tag_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_staff_accounts_created_by",
                        column: x => x.created_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tags_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "attribute_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attribute_value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attribute_values", x => x.id);
                    table.ForeignKey(
                        name: "FK_attribute_values_attributes_attribute_id",
                        column: x => x.attribute_id,
                        principalTable: "attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    coupon_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_status_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_approved_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    order_delivered_carrier_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    order_delivered_customer_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_coupons_coupon_id",
                        column: x => x.coupon_id,
                        principalTable: "coupons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_orders_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_orders_order_statuses_order_status_id",
                        column: x => x.order_status_id,
                        principalTable: "order_statuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_orders_staff_accounts_updated_by",
                        column: x => x.updated_by,
                        principalTable: "staff_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "card_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    card_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_card_items_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "cards",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_card_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "galleries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    placeholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_thumbnail = table.Column<bool>(type: "bit", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_galleries", x => x.id);
                    table.ForeignKey(
                        name: "FK_galleries_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_attributes_attributes_attribute_id",
                        column: x => x.attribute_id,
                        principalTable: "attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_attributes_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_categories",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_categories", x => new { x.product_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_product_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_categories_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_coupons",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    coupon_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_coupons", x => new { x.coupon_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_product_coupons_coupons_coupon_id",
                        column: x => x.coupon_id,
                        principalTable: "coupons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_coupons_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_shippings_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    shipping_charge = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    free = table.Column<bool>(type: "bit", nullable: false),
                    estimated_days = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_shippings_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_shippings_info_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sells",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sells", x => x.id);
                    table.ForeignKey(
                        name: "FK_sells_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "variants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variant_attribute_value_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variants", x => x.id);
                    table.ForeignKey(
                        name: "FK_variants_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipping_country_zones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    shipping_zone_id = table.Column<int>(type: "int", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_country_zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shipping_country_zones_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shipping_country_zones_shipping_zones_shipping_zone_id",
                        column: x => x.shipping_zone_id,
                        principalTable: "shipping_zones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipping_rates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    shipping_zone_id = table.Column<int>(type: "int", nullable: false),
                    weight_unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    min_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    max_value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    no_max = table.Column<bool>(type: "bit", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_rates", x => x.id);
                    table.ForeignKey(
                        name: "FK_shipping_rates_shipping_zones_shipping_zone_id",
                        column: x => x.shipping_zone_id,
                        principalTable: "shipping_zones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_supplier",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_supplier", x => new { x.product_id, x.supplier_id });
                    table.ForeignKey(
                        name: "FK_product_supplier_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_supplier_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_tags",
                columns: table => new
                {
                    tag_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_tags", x => new { x.product_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_product_tags_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "variant_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sale_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    compare_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    buying_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    sku = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variant_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_variant_options_galleries_image_id",
                        column: x => x.image_id,
                        principalTable: "galleries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_variant_options_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_attribute_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_attribute_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attribute_value_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_attribute_values", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_attribute_values_attribute_values_attribute_value_id",
                        column: x => x.attribute_value_id,
                        principalTable: "attribute_values",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_product_attribute_values_product_attributes_product_attribute_id",
                        column: x => x.product_attribute_id,
                        principalTable: "product_attributes",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "id", "attribute_name", "CreatedAt", "created_by", "updated_by" },
                values: new object[,]
                {
                    { new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), "Size", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2741), null, null },
                    { new Guid("932b3b04-0e9c-43a0-87e0-806cfdb69daf"), "Color", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2738), null, null }
                });

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "color", "CreatedAt", "created_by", "privacy", "status_name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("143876b5-017c-4aad-bd30-cdfefd6f8dd2"), "#FD9F3D", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2418), null, "public", "Cancelled", null },
                    { new Guid("2547d9fc-8dd0-439b-8ab0-93749adc31e9"), "#ab5ae9", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2397), null, "public", "Processing", null },
                    { new Guid("286b2862-fdd0-4638-be05-5b9f794f56e1"), "#5ae510", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2348), null, "public", "Delivered", null },
                    { new Guid("67e6f407-8536-43da-9072-40dd8314a86a"), "#4caf50", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2393), null, "public", "Paid", null },
                    { new Guid("79746bfd-9ed9-4cef-92bc-8e9ace420191"), "#d6d6d6", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2415), null, "public", "On Hold", null },
                    { new Guid("800a69e8-1f80-4774-8a5f-e37d43f7262c"), "#ffe224", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2398), null, "public", "Pending", null },
                    { new Guid("8c5138d9-3359-458a-bd77-fcddf663f127"), "#FF532F", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2429), null, "private", "Refused", null },
                    { new Guid("aba3e26e-6600-4ae6-8546-152e56f5de32"), "#000", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2432), null, "private", "Returned", null },
                    { new Guid("afc44c0a-b979-4c9c-83d5-8d83327c4f1e"), "#ff03d3", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2390), null, "public", "Unreached", null },
                    { new Guid("b0754809-6238-4ad5-98a3-a4a8a18fbfe5"), "#00d4cb", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2395), null, "public", "Confirmed", null },
                    { new Guid("c0ccc656-3b76-4f62-bef7-3b23e712be98"), "#71f9f7", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2417), null, "public", "Shipped", null },
                    { new Guid("fb0bf273-708a-4b89-a002-cf8c96f98969"), "#000", new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2431), null, "private", "Awaiting Return", null }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "privileges", "role_name" },
                values: new object[,]
                {
                    { 1, "[\"super_admin_privilege\", \"admin_read_privilege\", \"admin_create_privilege\", \"admin_update_privilege\", \"admin_delete_privilege\", \"staff_read_privilege\", \"staff_create_privilege\", \"staff_update_privilege\", \"staff_delete_privilege\"]", "Store Administrator" },
                    { 2, "[\"admin_read_privilege\", \"admin_create_privilege\", \"admin_update_privilege\", \"admin_delete_privilege\", \"staff_read_privilege\", \"staff_create_privilege\", \"staff_update_privilege\", \"staff_delete_privilege\"]", "Sales Manager" },
                    { 3, "[\"staff_read_privilege\", \"staff_create_privilege\", \"staff_update_privilege\", \"staff_delete_privilege\"]", "Sales Staff" },
                    { 4, "[\"staff_read_privilege\"]", "Guest" },
                    { 5, "[\"admin_read_privilege\", \"staff_read_privilege\"]", "Investor" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "CreatedAt", "created_by", "icon", "tag_name", "updated_by" },
                values: new object[,]
                {
                    { new Guid("2cb8408a-7222-4dad-a565-71cf1177cc38"), new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2680), null, "Shirts", "Shirts", null },
                    { new Guid("af47253b-17b5-4aec-bfa7-f9c1e5bdacb1"), new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2675), null, "BeautyHealth", "Beauty Health", null },
                    { new Guid("b0f5a65e-a8d1-4773-94fd-7c935c953eae"), new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2682), null, "Accessories", "Accessories", null },
                    { new Guid("bae3e661-906e-4a69-b767-76ddb17a924b"), new DateTime(2024, 4, 2, 10, 18, 0, 788, DateTimeKind.Utc).AddTicks(2671), null, "Tools", "Tools", null }
                });

            migrationBuilder.InsertData(
                table: "attribute_values",
                columns: new[] { "id", "attribute_id", "color", "attribute_value" },
                values: new object[,]
                {
                    { new Guid("0ff126d2-fb4b-47e0-b973-1989796c308c"), new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), null, "2XL" },
                    { new Guid("18950f2f-7e58-4096-b210-c8b7649907b9"), new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), null, "4XL" },
                    { new Guid("2f91dee3-b5b9-4886-bae9-e226cd60b507"), new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), null, "L" },
                    { new Guid("656c79a4-8cdb-43a9-921b-cc248580a793"), new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), null, "3XL" },
                    { new Guid("88c68238-db34-40fb-873e-af6671b3abfb"), new Guid("932b3b04-0e9c-43a0-87e0-806cfdb69daf"), "#000", "black" },
                    { new Guid("9484c9af-7b8c-4648-87d4-24f856062e67"), new Guid("932b3b04-0e9c-43a0-87e0-806cfdb69daf"), "#FF0000", "red" },
                    { new Guid("9ab5e159-711e-4512-8086-c84925568c21"), new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), null, "XL" },
                    { new Guid("bbd1db1b-fcb7-4c07-8e05-1ba2b67bb15a"), new Guid("932b3b04-0e9c-43a0-87e0-806cfdb69daf"), "#FFF", "white" },
                    { new Guid("da116964-a42e-4c63-b844-8a99a864c26f"), new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), null, "M" },
                    { new Guid("e2729eff-4ead-4be8-9107-007d56f9664e"), new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), null, "5XL" },
                    { new Guid("ecddcf82-61c4-4b12-a420-bee28521296f"), new Guid("7f2bb139-fba4-42ab-9cbc-16028b9136da"), null, "S" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_attribute_values_attribute_id",
                table: "attribute_values",
                column: "attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_attributes_created_by",
                table: "attributes",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_attributes_updated_by",
                table: "attributes",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_card_items_card_id",
                table: "card_items",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "IX_card_items_product_id",
                table: "card_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_cards_customer_id",
                table: "cards",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_created_by",
                table: "categories",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_updated_by",
                table: "categories",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_coupons_created_by",
                table: "coupons",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_coupons_updated_by",
                table: "coupons",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_customer_addresses_customer_id",
                table: "customer_addresses",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_galleries_product_id",
                table: "galleries",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_account_id",
                table: "notifications",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_statuses_created_by",
                table: "order_statuses",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_order_statuses_updated_by",
                table: "order_statuses",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_orders_coupon_id",
                table: "orders",
                column: "coupon_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_order_status_id",
                table: "orders",
                column: "order_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_updated_by",
                table: "orders",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_product_attribute_values_attribute_value_id",
                table: "product_attribute_values",
                column: "attribute_value_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_attribute_values_product_attribute_id",
                table: "product_attribute_values",
                column: "product_attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_attributes_attribute_id",
                table: "product_attributes",
                column: "attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_attributes_product_id",
                table: "product_attributes",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_categories_category_id",
                table: "product_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_coupons_product_id",
                table: "product_coupons",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_shippings_info_product_id",
                table: "product_shippings_info",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_supplier_supplier_id",
                table: "product_supplier",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_tags_tag_id",
                table: "product_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_created_by",
                table: "products",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_products_updated_by",
                table: "products",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_sells_product_id",
                table: "sells",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_country_zones_country_id",
                table: "shipping_country_zones",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_country_zones_shipping_zone_id",
                table: "shipping_country_zones",
                column: "shipping_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_rates_shipping_zone_id",
                table: "shipping_rates",
                column: "shipping_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_zones_created_by",
                table: "shipping_zones",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_zones_updated_by",
                table: "shipping_zones",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_slideshows_created_by",
                table: "slideshows",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_slideshows_updated_by",
                table: "slideshows",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_staff_accounts_created_by",
                table: "staff_accounts",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_staff_accounts_role_id",
                table: "staff_accounts",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_accounts_updated_by",
                table: "staff_accounts",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_country_id",
                table: "suppliers",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_created_by",
                table: "suppliers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_updated_by",
                table: "suppliers",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_tags_created_by",
                table: "tags",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_tags_updated_by",
                table: "tags",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_variant_options_image_id",
                table: "variant_options",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_variant_options_product_id",
                table: "variant_options",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_variants_product_id",
                table: "variants",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card_items");

            migrationBuilder.DropTable(
                name: "customer_addresses");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "product_attribute_values");

            migrationBuilder.DropTable(
                name: "product_categories");

            migrationBuilder.DropTable(
                name: "product_coupons");

            migrationBuilder.DropTable(
                name: "product_shippings_info");

            migrationBuilder.DropTable(
                name: "product_supplier");

            migrationBuilder.DropTable(
                name: "product_tags");

            migrationBuilder.DropTable(
                name: "sells");

            migrationBuilder.DropTable(
                name: "shipping_country_zones");

            migrationBuilder.DropTable(
                name: "shipping_rates");

            migrationBuilder.DropTable(
                name: "slideshows");

            migrationBuilder.DropTable(
                name: "variant_options");

            migrationBuilder.DropTable(
                name: "variants");

            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "attribute_values");

            migrationBuilder.DropTable(
                name: "product_attributes");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "shipping_zones");

            migrationBuilder.DropTable(
                name: "galleries");

            migrationBuilder.DropTable(
                name: "coupons");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "order_statuses");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "staff_accounts");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}