using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YiyiCook.Core.Models;

namespace YiyiCook.EntityFrameworkCore
{
    public class YiyiCookDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public YiyiCookDbContext(DbContextOptions<YiyiCookDbContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<FoodClassfy> FoodClassfy { get; set; }
        public virtual DbSet<FoodImg> FoodImg { get; set; }
        public virtual DbSet<FoodIngredient> FoodIngredient { get; set; }
        public virtual DbSet<FoodIngredientSource> FoodIngredientSource { get; set; }
        public virtual DbSet<FoodOrder> FoodOrder { get; set; }
        public virtual DbSet<FoodOrderItem> FoodOrderItem { get; set; }
        public virtual DbSet<FoodProduceProcess> FoodProduceProcess { get; set; }
        public virtual DbSet<FoodProduceProcessImg> FoodProduceProcessImg { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=YiyiCook;User ID=sa;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.Fcid).HasColumnName("fcid");

                entity.Property(e => e.HeadImgUrl)
                    .HasColumnName("headImgUrl")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20);

                entity.Property(e => e.ProduceVideoUrl)
                    .HasColumnName("produceVideoUrl")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModificationTime)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.VideoUrl)
                    .HasColumnName("videoUrl")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FoodClassfy>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20);

                entity.Property(e => e.LastModificationTime)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FoodImg>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FoodIngredient>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.Fiid).HasColumnName("fiid");

                entity.Property(e => e.Num).HasColumnName("num");
            });

            modelBuilder.Entity<FoodIngredientSource>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20);

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasColumnName("unitName")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<FoodOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.LastModificationTime)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FoodOrderItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100);

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.Foid).HasColumnName("foid");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.LastModificationTime)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FoodProduceProcess>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(20);

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.RankNum).HasColumnName("rankNum");

                entity.Property(e => e.LastModificationTime)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FoodProduceProcessImg>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fppid).HasColumnName("fppid");

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.LastModificationTime)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
