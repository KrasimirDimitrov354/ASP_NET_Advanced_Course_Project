namespace SithAcademy.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityFieldValidation.Homework;
using static SithAcademy.Common.EntityColumnInformation.Homework;

public class Homework
{
    public Homework()
    {
        Id = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
    }

    [Key]
    [Comment(IdComment)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(ContentMaxLength)]
    [Comment(ContentComment)]
    public string Content { get; set; } = null!;

    [Comment(ScoreComment)]
    [Column(TypeName = ScoreTypeName)]
    public decimal Score { get; set; }

    [Comment(CreatedOnComment)]
    public DateTime CreatedOn { get; set; }

    [Comment(ReviewerNameComment)]
    [MaxLength(ReviewerNameMaxLength)]
    public string? ReviewerName { get; set; }

    [Comment(ReviewerFeedbackComment)]
    [MaxLength(FeedbackMaxLength)]
    public string? ReviewerFeedback { get; set; }

    [Required]
    [Comment(TrialIdComment)]
    public Guid TrialId { get; set; }
    public virtual Trial Trial { get; set; } = null!;

    [Required]
    [Comment(AcolyteIdComment)]
    public Guid AcolyteId { get; set; }
    public virtual AcademyUser Acolyte { get; set; } = null!;
}
