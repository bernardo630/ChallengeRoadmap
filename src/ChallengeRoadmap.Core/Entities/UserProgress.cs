using System;
using System.ComponentModel.DataAnnotations;

namespace ChallengeRoadmap.Core.Entities
{
    public class UserProgress
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        
        public ProgressStatus Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? UserSolution { get; set; }
        public int Attempts { get; set; }
        
        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Challenge Challenge { get; set; } = null!;
    }

    public enum ProgressStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2
    }
}