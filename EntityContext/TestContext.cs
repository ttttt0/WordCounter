using Microsoft.EntityFrameworkCore;

namespace EntityContext
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ip)
                    .HasMaxLength(250)
                    .HasColumnName("ip");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.ToTable("words");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Count)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("count");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("user_id");

                entity.Property(e => e.Word1)
                    .HasMaxLength(250)
                    .HasColumnName("word");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Words)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("words_user_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
