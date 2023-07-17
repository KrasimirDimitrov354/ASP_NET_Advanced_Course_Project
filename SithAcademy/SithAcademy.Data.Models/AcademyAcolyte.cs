namespace SithAcademy.Data.Models;

public class AcademyAcolyte
{
    public int AcademyId { get; set; }
    public virtual Academy Academy { get; set; } = null!;

    public Guid AcolyteId { get; set; }
    public virtual AcademyUser Acolyte { get; set; } = null!;

    public bool IsAssigned { get; set; }
}
