using Excercise_6.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise_6.Data
{
    public class SixContext : DbContext
    {

        // Komendy:
        // 1) Migracja początkowa: Add-Migration Init  
        // 2) Zastosowanie migracji do bazy: Update-Database 
        // Add-Migration AddPropertyConstraints 
        // Add-Migration Test 
 
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public SixContext(DbContextOptions options) : base(options) 
        {

        }

        protected SixContext() 
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();

                modelBuilder.Entity<Medicament>().HasData(
                new Medicament { IdMedicament = 1, Name = "Medicament A", Description = "Description A", Type = "Type A" },
                new Medicament { IdMedicament = 2, Name = "Medicament B", Description = "Description B", Type = "Type B" }
                );
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);
                //entity.Property(e => e.Date).HasColumnType("date").IsRequired();
                //entity.Property(e => e.DueDate).HasColumnType("date").IsRequired();
                entity.HasOne(e => e.Doctor).
                    WithMany().
                    HasForeignKey(e => e.IdDoctor).
                    OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Patient).
                    WithMany().
                    HasForeignKey(e => e.IdPatient).
                    OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Prescription>().HasData(
                new Prescription { IdPrescription = 1, Date = DateTime.Now, IdPatient = 1, IdDoctor = 1 },
                new Prescription { IdPrescription = 2, Date = DateTime.Now, IdPatient = 2, IdDoctor = 2 }
                );
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                //entity.Property(e => e.Birthdate).HasColumnType("date").IsRequired();

                modelBuilder.Entity<Patient>().HasData(
                new Patient { IdPatient = 1, FirstName = "Alice", LastName = "Johnson" },
                new Patient { IdPatient = 2, FirstName = "Bob", LastName = "Smith" }
                );
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                modelBuilder.Entity<Doctor>().HasData(
                new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
                new Doctor { IdDoctor = 2, FirstName = "Jane", LastName = "Smith", Email = "janesmith@example.com" }
                );
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription });

                entity.HasOne(e => e.Medicament)
                    .WithMany(m => m.PrescriptionMedicaments)
                    .HasForeignKey(e => e.IdMedicament)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Prescription)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(e => e.IdPrescription)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<PrescriptionMedicament>().HasData(
                new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 2, Details = "Take twice a day" },
                new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 2, Dose = 1, Details = "Take once a day" }
                );
            });

        }
    }
}
