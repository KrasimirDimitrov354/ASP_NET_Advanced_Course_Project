﻿namespace SithAcademy.Web.ViewModels.Trial;

public class TrialDetailsOverviewModel : TrialOverviewViewModel
{
    public string Description { get; set; } = null!;

    public bool IsLocked { get; set; }
}
