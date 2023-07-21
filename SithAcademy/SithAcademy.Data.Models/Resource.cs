namespace SithAcademy.Data.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityFieldValidation.Resource;
using static SithAcademy.Common.EntityColumnInformation.Resource;

public class Resource
{
    public Resource()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    [Comment(IdComment)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment(NameComment)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment(UrlComment)]
    public string Url { get; set; } = null!;

    public Guid TrialId { get; set; }
    public virtual Trial Trial { get; set; } = null!;
}