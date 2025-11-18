using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeRoadmap.Core.Entities;
using ChallengeRoadmap.Core.Interfaces;
using ChallengeRoadmap.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChallengeRoadmap.Infrastructure.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly ApplicationDbContext _context;

        public ChallengeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Roadmap>> GetAllRoadmapsAsync()
        {
            return await _context.Roadmaps
                .Include(r => r.Challenges)
                .Where(r => r.IsActive)
                .OrderBy(r => r.Order)
                .ToListAsync();
        }

        public async Task<Roadmap> GetRoadmapByIdAsync(int id)
        {
            return await _context.Roadmaps
                .Include(r => r.Challenges.Where(c => c.IsActive).OrderBy(c => c.Order))
                .FirstOrDefaultAsync(r => r.Id == id && r.IsActive);
        }

        public async Task<IEnumerable<Challenge>> GetChallengesByRoadmapAsync(int roadmapId)
        {
            return await _context.Challenges
                .Where(c => c.RoadmapId == roadmapId && c.IsActive)
                .OrderBy(c => c.Order)
                .ToListAsync();
        }

        public async Task<Challenge> GetChallengeByIdAsync(int id)
        {
            return await _context.Challenges
                .Include(c => c.Roadmap)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<UserProgress> StartChallengeAsync(int userId, int challengeId)
        {
            var existingProgress = await _context.UserProgresses
                .FirstOrDefaultAsync(up => up.UserId == userId && up.ChallengeId == challengeId);

            if (existingProgress != null)
            {
                if (existingProgress.Status == ProgressStatus.NotStarted)
                {
                    existingProgress.Status = ProgressStatus.InProgress;
                    existingProgress.StartedAt = DateTime.UtcNow;
                }
                return existingProgress;
            }

            var progress = new UserProgress
            {
                UserId = userId,
                ChallengeId = challengeId,
                Status = ProgressStatus.InProgress,
                StartedAt = DateTime.UtcNow,
                Attempts = 0
            };

            await _context.UserProgresses.AddAsync(progress);
            await _context.SaveChangesAsync();

            return progress;
        }

        public async Task<UserProgress> SubmitChallengeAsync(int userId, int challengeId, string userSolution)
        {
            var progress = await _context.UserProgresses
                .FirstOrDefaultAsync(up => up.UserId == userId && up.ChallengeId == challengeId);

            if (progress == null)
            {
                progress = new UserProgress
                {
                    UserId = userId,
                    ChallengeId = challengeId,
                    Status = ProgressStatus.InProgress,
                    StartedAt = DateTime.UtcNow
                };
                await _context.UserProgresses.AddAsync(progress);
            }

            progress.UserSolution = userSolution;
            progress.Attempts++;
            
            // Simples verificação - em produção, usar um sistema mais robusto
            var challenge = await GetChallengeByIdAsync(challengeId);
            if (userSolution.Contains("Console.WriteLine") && !userSolution.Contains("// Seu código aqui"))
            {
                progress.Status = ProgressStatus.Completed;
                progress.CompletedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return progress;
        }

        public async Task<IEnumerable<UserProgress>> GetUserProgressAsync(int userId)
        {
            return await _context.UserProgresses
                .Include(up => up.Challenge)
                .ThenInclude(c => c.Roadmap)
                .Where(up => up.UserId == userId)
                .ToListAsync();
        }
    }
}