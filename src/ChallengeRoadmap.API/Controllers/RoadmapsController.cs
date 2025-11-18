using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeRoadmap.Core.Entities;
using ChallengeRoadmap.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeRoadmap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoadmapsController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public RoadmapsController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roadmap>>> GetRoadmaps()
        {
            var roadmaps = await _challengeService.GetAllRoadmapsAsync();
            return Ok(roadmaps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Roadmap>> GetRoadmap(int id)
        {
            var roadmap = await _challengeService.GetRoadmapByIdAsync(id);
            if (roadmap == null)
                return NotFound();

            return Ok(roadmap);
        }

        [HttpGet("{roadmapId}/challenges")]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetChallenges(int roadmapId)
        {
            var challenges = await _challengeService.GetChallengesByRoadmapAsync(roadmapId);
            return Ok(challenges);
        }
    }
}