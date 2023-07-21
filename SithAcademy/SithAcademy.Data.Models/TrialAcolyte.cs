namespace SithAcademy.Data.Models;

using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityColumnInformation.TrialAcolyte;

public class TrialAcolyte
{
    [Required]
    [Comment(TrialIdComment)]
    public Guid TrialId { get; set; }
    public virtual Trial Trial { get; set; } = null!;

    [Required]
    [Comment(AcolyteIdComment)]
    public Guid AcolyteId { get; set; }
    public virtual AcademyUser Acolyte { get; set; } = null!;

    [Required]
    [Comment(IsCompletedComment)]
    public bool IsCompleted { get; set; }
}
