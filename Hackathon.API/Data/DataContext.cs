using Microsoft.EntityFrameworkCore;
using Hackathon.Shared.Entities;

namespace Hackathon.API.Data
{
    public class DataContext : DbContext
    {
        // Constructor que recibe las opciones de configuración del DbContext
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSets que representan cada entidad en la base de datos
        public DbSet<Hackathon.Shared.Entities.Hackathon> Hackathons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Criterion> Criteria { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        // Método para configurar las propiedades y relaciones de cada entidad
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de propiedades de la entidad Hackathon
            modelBuilder.Entity<Hackathon.Shared.Entities.Hackathon>()
                .Property(h => h.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Hackathon.Shared.Entities.Hackathon>()
                .Property(h => h.MainTheme)
                .HasMaxLength(200)
                .IsRequired();

            // Configuración de propiedades de la entidad Team
            modelBuilder.Entity<Team>()
                .Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Configuración de propiedades de la entidad Participant
            modelBuilder.Entity<Participant>()
                .Property(p => p.FullName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Participant>()
                .Property(p => p.Email)
                .HasMaxLength(150)
                .IsRequired();

            // Configuración de propiedades de la entidad Role
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Configuración de propiedades de la entidad Project
            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .Property(p => p.Status)
                .HasMaxLength(50)
                .IsRequired();

            // Configuración de propiedades de la entidad Mentor
            modelBuilder.Entity<Mentor>()
                .Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Configuración de propiedades de la entidad Award
            modelBuilder.Entity<Award>()
                .Property(a => a.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Award>()
                .Property(a => a.Description)
                .HasMaxLength(500);

            // Configuración de propiedades de la entidad Criterion
            modelBuilder.Entity<Criterion>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Configuración de propiedades de la entidad Evaluation
            modelBuilder.Entity<Evaluation>()
                .Property(e => e.Feedback)
                .HasMaxLength(500);

            // Configuración de propiedades de la entidad Organization
            modelBuilder.Entity<Organization>()
                .Property(o => o.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Configuración de propiedades de la entidad Sponsor
            modelBuilder.Entity<Sponsor>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Sponsor>()
                .Property(s => s.Contribution)
                .HasMaxLength(200);

            // Configuración de propiedades de la entidad Schedule
            modelBuilder.Entity<Schedule>()
                .Property(s => s.Activity)
                .HasMaxLength(200);

            // Configuración de relaciones entre entidades

            // Relación Hackathon - Teams (uno a muchos)
            modelBuilder.Entity<Hackathon.Shared.Entities.Hackathon>()
                .HasMany(h => h.Teams)
                .WithOne(t => t.Hackathon)
                .HasForeignKey(t => t.HackathonId);

            // Relación Hackathon - Schedules (uno a muchos)
            modelBuilder.Entity<Hackathon.Shared.Entities.Hackathon>()
                .HasMany(h => h.Schedules)
                .WithOne(s => s.Hackathon)
                .HasForeignKey(s => s.HackathonId);

            // Relación Hackathon - Awards (uno a muchos)
            modelBuilder.Entity<Hackathon.Shared.Entities.Hackathon>()
                .HasMany(h => h.Awards)
                .WithOne(a => a.Hackathon)
                .HasForeignKey(a => a.HackathonId);

            // Relación Hackathon - Sponsors (uno a muchos)
            modelBuilder.Entity<Hackathon.Shared.Entities.Hackathon>()
                .HasMany(h => h.Sponsors)
                .WithOne(s => s.Hackathon)
                .HasForeignKey(s => s.HackathonId);

            // Relación Team - Projects (uno a muchos)
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Projects)
                .HasForeignKey(p => p.TeamId);

            // Relación Team - Participants (uno a muchos)
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Participants)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            // Relación Evaluation - Project (muchos a uno)
            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.Project)
                .WithMany(p => p.Evaluations)
                .HasForeignKey(e => e.ProjectId);

            // Relación Evaluation - Mentor (muchos a uno)
            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.Mentor)
                .WithMany(m => m.Evaluations)
                .HasForeignKey(e => e.MentorId);

            // Relación Award - Hackathon (uno a muchos)
            modelBuilder.Entity<Award>()
                .HasOne(a => a.Hackathon)
                .WithMany(h => h.Awards)
                .HasForeignKey(a => a.HackathonId);

            // Relación Criterion - Evaluation (muchos a uno)
            modelBuilder.Entity<Criterion>()
                .HasOne(c => c.Evaluation)
                .WithMany(e => e.Criteria)
                .HasForeignKey(c => c.EvaluationId);

            // Relación Sponsor - Hackathon (uno a muchos)
            modelBuilder.Entity<Sponsor>()
                .HasOne(s => s.Hackathon)
                .WithMany(h => h.Sponsors)
                .HasForeignKey(s => s.HackathonId);
        }
    }
}
