using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2.DTO
{
    public class LevelResultDTO
    {
        [ForeignKey("User")]
        public required int UserId { get; set; }
        public required int LevelId { get; set; }
        public required int Score { get; set; }
    }
}
