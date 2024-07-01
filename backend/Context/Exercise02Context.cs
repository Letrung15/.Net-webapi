using System;
using System.Linq;
using VuongLeTrung.Exercise02.Models;
using Microsoft.EntityFrameworkCore;

namespace VuongLeTrung.Exercise02.Context
{
    public class Exercise02Context : DbContext
    {
        public Exercise02Context()
        {
        }

        public Exercise02Context(DbContextOptions<Exercise02Context> options) : base(options)
        {
        }

        // DbSet cho các thực thể
        public DbSet<Product> Products { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<CardItem> CardItems { get; set; }
        public DbSet<ProductShipping> ProductShippings { get; set; }

        public DbSet<ProductCoupon> ProductCoupons { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<StaffAccount> StaffAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Slideshow> Slideshows { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Card> Cards { get; set; }


        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Tag> Tags { get; set; } // Thêm DbSet cho thực thể Tag
        public DbSet<ProductTag> ProductTags { get; set; } // Thêm DbSet cho thực thể ProductTag
        public DbSet<ProductAttribute> ProductAttributes { get; set; } // Thêm DbSet cho thực thể ProductAttribute
        public DbSet<AttributeValue> AttributeValues { get; set; } // Thêm DbSet cho thực thể AttributeValue
        public DbSet<VuongLeTrung.Exercise02.Models.Attribute> Attributes { get; set; } // Thêm DbSet cho thực thể Attribute
        public DbSet<Variant> Variants { get; set; }
        // public DbSet<VuongLeTrung.Exercise02.Models.VariantAttributeValue> VariantAttributeValues { get; set; }

        ///ccccccccccccccccccccccccccccccc
        public DbSet<VariantOption> VariantOptions { get; set; }
        public DbSet<ShippingZone> ShippingZones { get; set; }
        public DbSet<ShippingRate> ShippingRates { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<ShippingZoneCountry> ShippingZoneCountrys { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ShippingZoneCountry> ShippingZoneCountries { get; set; }





        // Phương thức cấu hình quan hệ giữa các thực thể
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProductAttributeValue>()
    .HasOne(pav => pav.ProductAttribute)
    .WithMany()
    .HasForeignKey(pav => pav.ProductAttributeId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductAttributeValue>()
                .HasOne(pav => pav.AttributeValue)
                .WithMany()
                .HasForeignKey(pav => pav.AttributeValueId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductSupplier>()
               .HasKey(ps => new { ps.ProductId, ps.SupplierId });





            modelBuilder.Entity<OrderItem>()
              .HasOne(g => g.Product)
              .WithMany(p => p.OrderItems)
              .HasForeignKey(g => g.ProductId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductCoupon>()
                           .HasKey(pc => new { pc.CouponId, pc.ProductId });

            modelBuilder.Entity<ProductCoupon>()
               .HasOne(g => g.Coupon)
               .WithMany(p => p.ProductCoupons)
               .HasForeignKey(g => g.CouponId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductCoupon>()
               .HasOne(g => g.Product)
               .WithMany(p => p.ProductCoupons)
               .HasForeignKey(g => g.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            //   modelBuilder.Entity<ProductShipping>()
            // .HasKey(pc => new { pc.ShippingId, pc.ProductId });

            // modelBuilder.Entity<ProductShipping>()
            //     .HasOne(g => g.Product)
            //     .WithMany(p => p.ProductShippings)
            //     .HasForeignKey(g => g.ProductId)
            //     .OnDelete(DeleteBehavior.Restrict);

            //      modelBuilder.Entity<ProductShipping>()
            //     .HasOne(g => g.Shipping)
            //     .WithMany(p => p.ProductShippings)
            //     .HasForeignKey(g => g.ShippingId)
            //     .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<CardItem>()
               .HasOne(g => g.Product)
               .WithMany(p => p.CardItems)
               .HasForeignKey(g => g.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gallery>()
                .HasOne(g => g.Product)
                .WithMany(p => p.Galleries)
                .HasForeignKey(g => g.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<Sell>()
            // .HasOne(g => g.Product)
            // .WithMany(p => p.Sells)
            // .HasForeignKey(g => g.ProductId)
            // .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ n-n giữa Product và Category
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            // Cấu hình quan hệ n-n giữa Product và Tag
            modelBuilder.Entity<ProductTag>()
                .HasKey(pt => new { pt.ProductId, pt.TagId });

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);

            // Cấu hình quan hệ giữa Product và Attribute
            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAttributes)
                .HasForeignKey(pa => pa.ProductId);

            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pa => pa.Attribute)
                .WithMany(a => a.ProductAttributes)
                .HasForeignKey(pa => pa.AttributeId);

            // Cấu hình quan hệ 1-n giữa Attribute và AttributeValue
            modelBuilder.Entity<AttributeValue>()
                .HasOne(av => av.Attribute)
                .WithMany(a => a.AttributeValues)
                .HasForeignKey(av => av.AttributeId);

            // thieeeeeeeeeeeeeeeeeeeeeeuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu

            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId);

            // Cấu hình quan hệ giữa VariantAttributeValue và VariantValue



            //         modelBuilder.Entity<VariantAttributeValue>()
            // .HasKey(pt => new { pt.VariantId2, pt.AttributeValueId });

            //         modelBuilder.Entity<VariantAttributeValue>()
            //  .HasOne(vav => vav.Variant)
            //  .WithMany(v => v.VariantAttributeValues)
            //  .HasForeignKey(vav => vav.VariantId2)  // This should be VariantId, not VariantId2
            //  .OnDelete(DeleteBehavior.NoAction);

            //         modelBuilder.Entity<VariantAttributeValue>()
            //             .HasOne(vav => vav.AttributeValue)
            //             .WithMany(t => t.VariantAttributeValues)
            //             .HasForeignKey(vav => vav.AttributeValueId)
            //             .OnDelete(DeleteBehavior.NoAction);







            modelBuilder.Entity<OrderStatus>().HasData(
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Delivered", Color = "#5ae510", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Unreached", Color = "#ff03d3", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Paid", Color = "#4caf50", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Confirmed", Color = "#00d4cb", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Processing", Color = "#ab5ae9", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Pending", Color = "#ffe224", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "On Hold", Color = "#d6d6d6", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Shipped", Color = "#71f9f7", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Cancelled", Color = "#FD9F3D", Privacy = "public" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Refused", Color = "#FF532F", Privacy = "private" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Awaiting Return", Color = "#000", Privacy = "private" },
                   new OrderStatus { Id = Guid.NewGuid(), StatusName = "Returned", Color = "#000", Privacy = "private" }
               );

            modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = Guid.NewGuid(), TagName = "New Arrivals", Icon = "NewArrivals" },
            new Tag { Id = Guid.NewGuid(), TagName = "Trending", Icon = "Trending" },
            new Tag { Id = Guid.NewGuid(), TagName = "Top Rated", Icon = "TopRated" },
            new Tag { Id = Guid.NewGuid(), TagName = "Deal Of The Day", Icon = "DealOfTheDay" },
            new Tag { Id = Guid.NewGuid(), TagName = "New Products", Icon = "New Products" }

        );
            modelBuilder.Entity<Role>().HasData(
                    new Role { Id = 1, RoleName = "Store Administrator", Privileges = "[\"super_admin_privilege\", \"admin_read_privilege\", \"admin_create_privilege\", \"admin_update_privilege\", \"admin_delete_privilege\", \"staff_read_privilege\", \"staff_create_privilege\", \"staff_update_privilege\", \"staff_delete_privilege\"]" },
                    new Role { Id = 2, RoleName = "Sales Manager", Privileges = "[\"admin_read_privilege\", \"admin_create_privilege\", \"admin_update_privilege\", \"admin_delete_privilege\", \"staff_read_privilege\", \"staff_create_privilege\", \"staff_update_privilege\", \"staff_delete_privilege\"]" },
                    new Role { Id = 3, RoleName = "Sales Staff", Privileges = "[\"staff_read_privilege\", \"staff_create_privilege\", \"staff_update_privilege\", \"staff_delete_privilege\"]" },
                    new Role { Id = 4, RoleName = "Guest", Privileges = "[\"staff_read_privilege\"]" },
                    new Role { Id = 5, RoleName = "Investor", Privileges = "[\"admin_read_privilege\", \"staff_read_privilege\"]" }
                );



            // Thêm các thuộc tính
            var colorAttributeId = Guid.NewGuid();
            var sizeAttributeId = Guid.NewGuid();
            modelBuilder.Entity<VuongLeTrung.Exercise02.Models.Attribute>().HasData(
                new VuongLeTrung.Exercise02.Models.Attribute { Id = colorAttributeId, AttributeName = "Color" },
                new VuongLeTrung.Exercise02.Models.Attribute { Id = sizeAttributeId, AttributeName = "Size" }
            );



            modelBuilder.Entity<AttributeValue>().HasData(
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = colorAttributeId, Value = "black", Color = "#000" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = colorAttributeId, Value = "white", Color = "#FFF" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = colorAttributeId, Value = "red", Color = "#FF0000" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = sizeAttributeId, Value = "S" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = sizeAttributeId, Value = "M" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = sizeAttributeId, Value = "L" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = sizeAttributeId, Value = "XL" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = sizeAttributeId, Value = "2XL" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = sizeAttributeId, Value = "3XL" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = sizeAttributeId, Value = "4XL" },
                new AttributeValue { Id = Guid.NewGuid(), AttributeId = sizeAttributeId, Value = "5XL" }
            );

        }
    }
}
