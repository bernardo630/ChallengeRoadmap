using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChallengeRoadmap.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }
        
        // Navigation properties
        public virtual ICollection<UserProgress> Progresses { get; set; } = new List<UserProgress>();
        public virtual ICollection<UserAchievement> Achievements { get; set; } = new List<UserAchievement>();
    }
}