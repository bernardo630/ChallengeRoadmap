using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeRoadmap.Core.Entities;
using ChallengeRoadmap.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeRoadmap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengesController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Challenge>> GetChallenge(int id)
        {
            var challenge = await _challengeService.GetChallengeByIdAsync(id);
            if (challenge == null)
                return NotFound();

            return Ok(challenge);
        }

        [HttpPost("{challengeId}/start")]
        public async Task<ActionResult<UserProgress>> StartChallenge(int challengeId)
        {
            // Simulando usuário ID 1 - em produção, usar autenticação
            var userId = 1;
            var progress = await _challengeService.StartChallengeAsync(userId, challengeId);
            return Ok(progress);
        }

        [HttpPost("{challengeId}/submit")]
        public async Task<ActionResult<UserProgress>> SubmitChallenge(int challengeId, [FromBody] string userSolution)
        {
            // Simulando usuário ID 1 - em produção, usar autenticação
            var userId = 1;
            var progress = await _challengeService.SubmitChallengeAsync(userId, challengeId, userSolution);
            return Ok(progress);
        }

        [HttpGet("progress")]
        public async Task<ActionResult<IEnumerable<UserProgress>>> GetUserProgress()
        {
            // Simulando usuário ID 1 - em produção, usar autenticação
            var userId = 1;
            var progress = await _challengeService.GetUserProgressAsync(userId);
            return Ok(progress);
        }
    }
}