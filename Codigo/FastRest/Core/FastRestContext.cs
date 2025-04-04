using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core
{
    public partial class FastRestContext : DbContext
    {
        public FastRestContext()
        {
        }

        public FastRestContext(DbContextOptions<FastRestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Menucategory> Menucategory { get; set; }
        public virtual DbSet<Orderproducts> Orderproducts { get; set; }
        public virtual DbSet<Ordertable> Ordertable { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=123456;database=mydb");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menucategory>(entity =>
            {
                entity.ToTable("menucategory");

                entity.HasIndex(e => e.RestaurantId)
                    .HasName("fk_menucategory_restaurant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantId).HasColumnName("restaurantId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Menucategory)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("fk_menuCategory_restaurant");
            });

            modelBuilder.Entity<Orderproducts>(entity =>
            {
                entity.ToTable("orderproducts");

                entity.HasIndex(e => e.OrderId)
                    .HasName("fk_OD_OP");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_product_OP");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("fk_OD_OP");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_product_OP");
            });

            modelBuilder.Entity<Ordertable>(entity =>
            {
                entity.ToTable("ordertable");

                entity.HasIndex(e => e.RestaurantId)
                    .HasName("fk_order_restaurant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConsumptionMethod)
                    .IsRequired()
                    .HasColumnName("consumptionMethod")
                    .HasColumnType("enum('TAKEWAY','DINE_IN')");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurantId");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("enum('PENDING','IN_PREPARATION','FINISHED')");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Ordertable)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("fk_order_restaurant");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.MenuCategoryId)
                    .HasName("fk_product_menuCategory");

                entity.HasIndex(e => e.RestaurantId)
                    .HasName("fk_product_restaurant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("imageUrl")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MenuCategoryId).HasColumnName("menuCategoryId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurantId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.MenuCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.MenuCategoryId)
                    .HasConstraintName("fk_product_menuCategory");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("fk_product_restaurant");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");

                entity.HasIndex(e => e.Slug)
                    .HasName("slug")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvatarImageUrl)
                    .IsRequired()
                    .HasColumnName("avatarImageUrl")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CoverImageUrl)
                    .IsRequired()
                    .HasColumnName("coverImageUrl")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
