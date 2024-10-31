using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hackathon.Shared.Entities; 


namespace Hackathon.API.Data
{
    public static class SeedDb
    {
        public static void SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            // Comprobar si la base de datos ya tiene datos
            if (context.HackathonEntities.Any())
            {
                return; // La base de datos ya está poblada
            }

            // Crear datos de ejemplo para HackathonEntities
            var hackathonEntities = new List<HackathonEntity>
            {
                new HackathonEntity { Name = "Hackathon 2023", MainTheme = "Innovación" },
                new HackathonEntity { Name = "Hackathon 2024", MainTheme = "Tecnología" }
            };

            context.HackathonEntities.AddRange(hackathonEntities);
            context.SaveChanges();

            // Crear datos para Team
            var teams = new List<Team>
            {
                new Team { Name = "Team Alpha", HackathonId = hackathonEntities[0].Id },
                new Team { Name = "Team Beta", HackathonId = hackathonEntities[0].Id },
                new Team { Name = "Team Gamma", HackathonId = hackathonEntities[1].Id }
            };


            context.Teams.AddRange(teams);
            context.SaveChanges();

            // Crear datos para Participant
            var participants = new List<Participant>
            {
                new Participant { FullName = "Juan Pablo", Email = "juan.pablo@example.com", TeamId = teams[0].Id },
                new Participant { FullName = "Maria Gomez", Email = "maria.gomez@example.com", TeamId = teams[1].Id },
                new Participant { FullName = "Carlos Perez", Email = "carlos.perez@example.com", TeamId = teams[2].Id }
            };

            context.Participants.AddRange(participants);
            context.SaveChanges();

            // Crear datos para Role
            var roles = new List<Role>
            {
                new Role { Name = "Developer" },
                new Role { Name = "Designer" },
                new Role { Name = "Project Manager" }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Crear datos para Mentor
            var mentors = new List<Mentor>
            {
                new Mentor { Name = "Dr. Smith" },
                new Mentor { Name = "Ms. Johnson" }
            };

            context.Mentors.AddRange(mentors);
            context.SaveChanges();

            // Crear datos para Evaluation
            var evaluations = new List<Evaluation>
            {
                new Evaluation { Score = 90, Feedback = "Excellent work!", EvaluationDate = DateTime.Now, ProjectId = 1, MentorId = mentors[0].Id },
                new Evaluation { Score = 85, Feedback = "Good job!", EvaluationDate = DateTime.Now, ProjectId = 2, MentorId = mentors[1].Id }
            };

            context.Evaluations.AddRange(evaluations);
            context.SaveChanges();

            // Crear datos para Criterion
            var criteria = new List<Criterion>
            {
                new Criterion { Name = "Innovation", MaxScore = 100, EvaluationId = evaluations[0].Id },
                new Criterion { Name = "Design", MaxScore = 100, EvaluationId = evaluations[1].Id }
            };

            context.Criteria.AddRange(criteria);
            context.SaveChanges();

            // Crear datos para Award
            var awards = new List<Award>
            {
                new Award { Title = "Best Innovation", HackathonId = hackathonEntities[0].Id, Description = "Awarded for the most innovative project." },
                new Award { Title = "Best Teamwork", HackathonId = hackathonEntities[1].Id, Description = "Awarded for outstanding teamwork." }
            };

            context.Awards.AddRange(awards);
            context.SaveChanges();

            // Crear datos para Organization
            var organizations = new List<Organization>
            {
                new Organization { Name = "Tech Inc." },
                new Organization { Name = "Innovate LLC" }
            };

            context.Organizations.AddRange(organizations);
            context.SaveChanges();

            // Crear datos para Sponsor
            var sponsors = new List<Sponsor>
            {
                new Sponsor { Name = "Company A", Contribution = "Gold Sponsor", HackathonId = hackathonEntities[0].Id },
                new Sponsor { Name = "Company B", Contribution = "Silver Sponsor", HackathonId = hackathonEntities[1].Id }
            };

            context.Sponsors.AddRange(sponsors);
            context.SaveChanges();

            // Crear datos para Schedule
            var schedules = new List<Schedule>
            {
                new Schedule { Activity = "Opening Ceremony", HackathonId = hackathonEntities[0].Id, EventDate = DateTime.Now.AddDays(1) },
                new Schedule { Activity = "Closing Ceremony", HackathonId = hackathonEntities[1].Id, EventDate = DateTime.Now.AddDays(18) }
            };

            context.Schedules.AddRange(schedules);
            context.SaveChanges();
        }
    }
}
