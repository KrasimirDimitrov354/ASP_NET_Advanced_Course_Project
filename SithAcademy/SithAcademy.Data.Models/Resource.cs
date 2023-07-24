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
    [Comment(SourceUrlComment)]
    public string SourceUrl { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment(ImageUrlComment)]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [Comment(IsDeletedComment)]
    public bool IsDeleted { get; set; }

    public Guid TrialId { get; set; }
    public virtual Trial Trial { get; set; } = null!;
}