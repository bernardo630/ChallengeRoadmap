using ChallengeRoadmap.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChallengeRoadmap.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Roadmap> Roadmaps { get; set; } = null!;
        public DbSet<Challenge> Challenges { get; set; } = null!;
        public DbSet<UserProgress> UserProgresses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Roadmap>()
                .HasMany(r => r.Challenges)
                .WithOne(c => c.Roadmap)
                .HasForeignKey(c => c.RoadmapId);

            modelBuilder.Entity<UserProgress>()
                .HasOne(up => up.User)
                .WithMany(u => u.Progresses)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserProgress>()
                .HasOne(up => up.Challenge)
                .WithMany(c => c.UserProgresses)
                .HasForeignKey(up => up.ChallengeId);

            // Seed data
            modelBuilder.Entity<Roadmap>().HasData(
                new Roadmap { Id = 1, Title = "Lógica de Programação", Description = "Domine os fundamentos da programação", Difficulty = DifficultyLevel.Beginner, Order = 1 },
                new Roadmap { Id = 2, Title = "Algoritmos Avançados", Description = "Aprenda algoritmos complexos", Difficulty = DifficultyLevel.Intermediate, Order = 2 },
                new Roadmap { Id = 3, Title = "Estruturas de Dados", Description = "Estruturas essenciais para programação", Difficulty = DifficultyLevel.Intermediate, Order = 3 },
                new Roadmap { Id = 4, Title = "Programação em C#", Description = "Domine a linguagem C#", Difficulty = DifficultyLevel.Beginner, Order = 4 },
                new Roadmap { Id = 5, Title = "Programação em Python", Description = "Aprenda Python do zero", Difficulty = DifficultyLevel.Beginner, Order = 5 }
            );

            modelBuilder.Entity<Challenge>().HasData(
                new Challenge 
                { 
                    Id = 1, 
                    Title = "Hello World", 
                    Description = "Seu primeiro programa em C#", 
                    Instructions = "Escreva um programa que exiba 'Hello, World!' na console.",
                    StarterCode = "using System;\n\nclass Program\n{\n    static void Main()\n    {\n        // Seu código aqui\n    }\n}",
                    Solution = "using System;\n\nclass Program\n{\n    static void Main()\n    {\n        Console.WriteLine(\\\"Hello, World!\\\");\n    }\n}",
                    Points = 10,
                    Order = 1,
                    RoadmapId = 4
                },
                new Challenge 
                { 
                    Id = 2, 
                    Title = "Calculadora Básica", 
                    Description = "Implemente uma calculadora simples", 
                    Instructions = "Crie uma calculadora que some dois números fornecidos.",
                    StarterCode = "using System;\n\nclass Program\n{\n    static void Main()\n    {\n        int a = 5;\n        int b = 3;\n        // Seu código aqui\n    }\n}",
                    Solution = "using System;\n\nclass Program\n{\n    static void Main()\n    {\n        int a = 5;\n        int b = 3;\n        int soma = a + b;\n        Console.WriteLine($\\\"A soma é: {soma}\\\");\n    }\n}",
                    Points = 20,
                    Order = 2,
                    RoadmapId = 4
                }
            );
        }
    }
}