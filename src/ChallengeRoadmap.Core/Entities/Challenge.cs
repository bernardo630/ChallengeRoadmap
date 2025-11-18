using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChallengeRoadmap.Core.Entities
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string Instructions { get; set; } = string.Empty;
        
        [Required]
        public string StarterCode { get; set; } = string.Empty;
        
        [Required]
        public string Solution { get; set; } = string.Empty;
        
        public int Points { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Foreign key
        public int RoadmapId { get; set; }
        
        // Navigation properties
        public virtual Roadmap Roadmap { get; set; } = null!;
        public virtual ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
    }
}