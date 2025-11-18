using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeRoadmap.Core.Entities;

namespace ChallengeRoadmap.Core.Interfaces
{
    public interface IChallengeService
    {
        Task<IEnumerable<Roadmap>> GetAllRoadmapsAsync();
        Task<Roadmap> GetRoadmapByIdAsync(int id);
        Task<IEnumerable<Challenge>> GetChallengesByRoadmapAsync(int roadmapId);
        Task<Challenge> GetChallengeByIdAsync(int id);
        Task<UserProgress> StartChallengeAsync(int userId, int challengeId);
        Task<UserProgress> SubmitChallengeAsync(int userId, int challengeId, string userSolution);
        Task<IEnumerable<UserProgress>> GetUserProgressAsync(int userId);
    }
}