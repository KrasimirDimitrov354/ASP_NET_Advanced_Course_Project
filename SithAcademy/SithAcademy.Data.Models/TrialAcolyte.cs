namespace SithAcademy.Data.Models;

using System;

public class TrialAcolyte
{
    public Guid TrialId { get; set; }
    public virtual Trial Trial { get; set; } = null!;

    public Guid AcolyteId { get; set; }
    public virtual AcademyUser Acolyte { get; set; } = null!; 

    public bool IsCompleted { get; set; }
}
