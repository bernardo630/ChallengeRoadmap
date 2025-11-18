using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChallengeRoadmap.Core.Entities
{
    public class Roadmap
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public DifficultyLevel Difficulty { get; set; }
        
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
    }

    public enum DifficultyLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3,
        Expert = 4
    }
}