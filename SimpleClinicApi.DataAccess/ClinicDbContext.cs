using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess
{
   public class ClinicDbContext(DbContextOptions<ClinicDbContext> options) : IdentityDbContext(options)
   {
      private IDbContextTransaction? _currentTransaction;

      public DbSet<Patient> Patients
      {
         get;
         set;
      }

      public DbSet<Doctor> Doctors
      {
         get;
         set;
      }

      public DbSet<Visit> Visits
      {
         get;
         set;
      }

      public DbSet<VisitProcedure> VisitProcedures
      {
         get;
         set;
      }

      public DbSet<VisitMedication> VisitMedications
      {
         get;
         set;
      }

      public DbSet<Procedure> Procedures
      {
         get;
         set;
      }

      public DbSet<Medication> Medications
      {
         get;
         set;
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Patient>()
                     .HasIndex(p => p.FullName)
                     .HasDatabaseName("IX_Patient_FullName");

         modelBuilder.Entity<Patient>()
                     .HasIndex(p => p.PhoneNumber)
                     .HasDatabaseName("IX_Patient_PhoneNumber");

         modelBuilder.Entity<Doctor>()
                     .HasIndex(d => d.FullName)
                     .HasDatabaseName("IX_Doctor_FullName");

         modelBuilder.Entity<Doctor>()
                     .HasIndex(d => d.Specialty)
                     .HasDatabaseName("IX_Doctor_Specialty");

         modelBuilder.Entity<Visit>()
                     .HasIndex(v => new
                     {
                        v.PatientId,
                        v.VisitDate
                     })
                     .HasDatabaseName("IX_Visit_Patient_VisitDate");

         modelBuilder.Entity<Visit>()
                     .HasIndex(v => v.DoctorId)
                     .HasDatabaseName("IX_Visit_DoctorId");

         modelBuilder.Entity<VisitProcedure>()
                     .HasIndex(vp => new
                     {
                        vp.VisitId,
                        vp.ProcedureId
                     })
                     .HasDatabaseName("IX_VisitProcedure_Visit_Procedure");

         modelBuilder.Entity<VisitMedication>()
                     .HasIndex(vm => new
                     {
                        vm.VisitId,
                        vm.MedicationId
                     })
                     .HasDatabaseName("IX_VisitMedication_Visit_Medication");

         modelBuilder.Entity<Procedure>()
                     .HasIndex(p => p.Name)
                     .HasDatabaseName("IX_Procedure_Name");

         modelBuilder.Entity<Medication>()
                     .HasIndex(m => m.Name)
                     .HasDatabaseName("IX_Medication_Name");

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

      public void BeginTransaction()
      {
         if (_currentTransaction != null)
         {
            return;
         }

         if (!Database.IsInMemory())
         {
            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
         }
      }

      public void CommitTransaction()
      {
         try
         {
            _currentTransaction?.Commit();
         }
         catch
         {
            RollbackTransaction();

            throw;
         }
         finally
         {
            if (_currentTransaction != null)
            {
               _currentTransaction.Dispose();
               _currentTransaction = null;
            }
         }
      }

      public void RollbackTransaction()
      {
         try
         {
            _currentTransaction?.Rollback();
         }
         finally
         {
            if (_currentTransaction != null)
            {
               _currentTransaction.Dispose();
               _currentTransaction = null;
            }
         }
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         optionsBuilder.UseSqlite("Data Source=clinic.db")
                       .UseSeeding(Seed)
                       // Seed data asynchronously inside UseAsyncSeeding (EF Core 9)
                       .UseAsyncSeeding(SeedAsync);
      }

      private void Seed(DbContext arg1, bool _)
      {
         SeedAsync(arg1, _).Wait();
      }

      private async Task SeedAsync(DbContext arg1, bool _, CancellationToken cancellationToken = default)
      {
         var context = (ClinicDbContext) arg1;

         if (await context.Patients.AnyAsync(cancellationToken))
         {
            return;
         }

         // Patients
         var patient1 = new Patient
         {
            FullName = "Ivan Ivanov",
            DateOfBirth = new DateTime(1985, 5, 12),
            PhoneNumber = "+7 900 123 45 67",
            Address = "Moscow, Lenin St, 10"
         };

         var patient2 = new Patient
         {
            FullName = "Anna Petrova",
            DateOfBirth = new DateTime(1990, 8, 25),
            PhoneNumber = "+7 901 234 56 78",
            Address = "Saint Petersburg, Nevsky Ave, 15"
         };

         var patient3 = new Patient
         {
            FullName = "Sergey Sidorov",
            DateOfBirth = new DateTime(1978, 3, 3),
            PhoneNumber = "+7 902 345 67 89",
            Address = "Novosibirsk, Sovetskaya St, 5"
         };

         await context.Patients.AddRangeAsync(new[]
         {
            patient1, patient2, patient3
         }, cancellationToken);

         // Doctors
         var doctor1 = new Doctor
         {
            FullName = "Dr. Sergey Smirnov",
            Specialty = "Therapist",
            PhoneNumber = "+7 912 345 67 89"
         };

         var doctor2 = new Doctor
         {
            FullName = "Dr. Elena Kuznetsova",
            Specialty = "Surgeon",
            PhoneNumber = "+7 913 123 45 67"
         };

         var doctor3 = new Doctor
         {
            FullName = "Dr. Alexey Volkov",
            Specialty = "Pediatrician",
            PhoneNumber = "+7 914 456 78 90"
         };

         await context.Doctors.AddRangeAsync(new[]
         {
            doctor1, doctor2, doctor3
         }, cancellationToken);

         // Procedures
         var proc1 = new Procedure
         {
            Name = "Complete Blood Count",
            Description = "Blood test to assess general health",
            Cost = 500m
         };

         var proc2 = new Procedure
         {
            Name = "Chest X-Ray",
            Description = "Radiographic examination of lungs",
            Cost = 1500m
         };

         var proc3 = new Procedure
         {
            Name = "ECG",
            Description = "Electrocardiogram for heart monitoring",
            Cost = 1200m
         };

         await context.Procedures.AddRangeAsync(new[]
         {
            proc1, proc2, proc3
         }, cancellationToken);

         // Medications
         var med1 = new Medication
         {
            Name = "Ibuprofen",
            Description = "Pain reliever and anti-inflammatory",
            Cost = 200m
         };

         var med2 = new Medication
         {
            Name = "Amoxicillin",
            Description = "Broad-spectrum antibiotic",
            Cost = 800m
         };

         var med3 = new Medication
         {
            Name = "Paracetamol",
            Description = "Pain reliever and fever reducer",
            Cost = 150m
         };

         await context.Medications.AddRangeAsync(new[]
         {
            med1, med2, med3
         }, cancellationToken);

         await context.SaveChangesAsync(cancellationToken);

         // Visits with new IsCompleted field
         var visit1 = new Visit
         {
            PatientId = patient1.Id,
            DoctorId = doctor1.Id,
            VisitDate = DateTime.UtcNow.AddDays(-10),
            Notes = "Patient complained of headache",
            IsCompleted = true
         };

         var visit2 = new Visit
         {
            PatientId = patient2.Id,
            DoctorId = doctor2.Id,
            VisitDate = DateTime.UtcNow.AddDays(-5),
            Notes = "Scheduled check-up",
            IsCompleted = false
         };

         var visit3 = new Visit
         {
            PatientId = patient3.Id,
            DoctorId = doctor3.Id,
            VisitDate = DateTime.UtcNow.AddDays(-1),
            Notes = "Routine pediatric consultation",
            IsCompleted = true
         };

         await context.Visits.AddRangeAsync(new[]
         {
            visit1, visit2, visit3
         }, cancellationToken);

         await context.SaveChangesAsync(cancellationToken);

         // VisitProcedures assignments
         var vp1 = new VisitProcedure
         {
            VisitId = visit1.Id,
            ProcedureId = proc1.Id,
            Notes = "Assigned with additional monitoring"
         };

         var vp2 = new VisitProcedure
         {
            VisitId = visit2.Id,
            ProcedureId = proc2.Id,
            Notes = "Recommended repeat in 6 months"
         };

         var vp3 = new VisitProcedure
         {
            VisitId = visit3.Id,
            ProcedureId = proc3.Id,
            Notes = "For heart rhythm evaluation"
         };

         await context.VisitProcedures.AddRangeAsync(new[]
         {
            vp1, vp2, vp3
         }, cancellationToken);

         // VisitMedications assignments
         var vm1 = new VisitMedication
         {
            VisitId = visit1.Id,
            MedicationId = med1.Id,
            Dosage = "200 mg twice daily",
            Notes = "After meals"
         };

         var vm2 = new VisitMedication
         {
            VisitId = visit2.Id,
            MedicationId = med2.Id,
            Dosage = "500 mg three times daily",
            Notes = "Course of 7 days"
         };

         var vm3 = new VisitMedication
         {
            VisitId = visit3.Id,
            MedicationId = med3.Id,
            Dosage = "500 mg every 6 hours",
            Notes = "If fever persists"
         };

         await context.VisitMedications.AddRangeAsync(new[]
         {
            vm1, vm2, vm3
         }, cancellationToken);

         await context.SaveChangesAsync(cancellationToken);
      }
   }
}