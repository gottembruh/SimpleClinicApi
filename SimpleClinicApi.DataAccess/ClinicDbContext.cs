using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess
{
    public class ClinicDbContext : IdentityDbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitProcedure> VisitProcedures { get; set; }
        public DbSet<VisitMedication> VisitMedications { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Medication> Medications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Индексы для таблицы Patients
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.FullName)
                .HasDatabaseName("IX_Patient_FullName");
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.PhoneNumber)
                .HasDatabaseName("IX_Patient_PhoneNumber");

            // Индексы для таблицы Doctors
            modelBuilder.Entity<Doctor>()
                .HasIndex(d => d.FullName)
                .HasDatabaseName("IX_Doctor_FullName");
            modelBuilder.Entity<Doctor>()
                .HasIndex(d => d.Specialty)
                .HasDatabaseName("IX_Doctor_Specialty");

            // Индексы для таблицы Visits
            modelBuilder.Entity<Visit>()
                .HasIndex(v => new { v.PatientId, v.VisitDate })
                .HasDatabaseName("IX_Visit_Patient_VisitDate");
            modelBuilder.Entity<Visit>()
                .HasIndex(v => v.DoctorId)
                .HasDatabaseName("IX_Visit_DoctorId");

            // Индексы для таблицы VisitProcedures
            modelBuilder.Entity<VisitProcedure>()
                .HasIndex(vp => new { vp.VisitId, vp.ProcedureId })
                .HasDatabaseName("IX_VisitProcedure_Visit_Procedure");

            // Индексы для таблицы VisitMedications
            modelBuilder.Entity<VisitMedication>()
                .HasIndex(vm => new { vm.VisitId, vm.MedicationId })
                .HasDatabaseName("IX_VisitMedication_Visit_Medication");

            // Индексы для Procedures
            modelBuilder.Entity<Procedure>()
                .HasIndex(p => p.Name)
                .HasDatabaseName("IX_Procedure_Name");

            // Индексы для Medications
            modelBuilder.Entity<Medication>()
                .HasIndex(m => m.Name)
                .HasDatabaseName("IX_Medication_Name");

            // Связи и каскадные операции
            modelBuilder.Entity<VisitProcedure>()
                .HasOne(vp => vp.Visit)
                .WithMany(v => v.VisitProcedures)
                .HasForeignKey(vp => vp.VisitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VisitProcedure>()
                .HasOne(vp => vp.Procedure)
                .WithMany()
                .HasForeignKey(vp => vp.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VisitMedication>()
                .HasOne(vm => vm.Visit)
                .WithMany(v => v.VisitMedications)
                .HasForeignKey(vm => vm.VisitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VisitMedication>()
                .HasOne(vm => vm.Medication)
                .WithMany()
                .HasForeignKey(vm => vm.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}