using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DatabaseContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DatabaseContext
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            (Microsoft.EntityFrameworkCore.DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        #region Tabales
        public DbSet<Models.Personnel> Personnel { get; set; }

        public DbSet<Models.Salary> Salaries { get; set; }

        #endregion /Tabales

        #region override void OnModelCreating
        protected override void OnModelCreating
            (Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region *********** This code For Set Decimal precision  ***********
            //******** Set ALL Field
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // EF Core 5
                property.SetPrecision(18);
                property.SetScale(0);
            }
            //******** Set ALL Field
            #endregion


            #region model: Salary
            modelBuilder.Entity<Models.Salary>()
                .HasOne(salary => salary.Personnel)
                .WithMany(personnel => personnel.Salaries)
                .HasForeignKey(salary => salary.IdPersonnel)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
                .IsRequired(required: true)
                ;
            #endregion /model: Salary
		}
        #endregion /override void OnModelCreating
    }
}
