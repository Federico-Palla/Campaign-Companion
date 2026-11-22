using CampaignCompanion.Core.Catalog.Enums;

namespace CampaignCompanion.Core.Catalog.Classes;

public class ClassDefinition : CatalogEntry
{
    //Core Traits
    public required string PrimaryAbility { get; set; }
    public required DiceSizeEnum HitPointDie { get; set; }
    public required IReadOnlyList<string> SavingThrowProficencies { get; set; }
    public required IReadOnlyList<string> SkillProficenciesList { get; set; }
    public int SkillProficienciesCount { get; set; } = 2;
    public required IReadOnlyList<string> WeaponProficiencies {  get; set; }
    public required IReadOnlyList<string> ArmorTrainingList { get; set; }
    public required IReadOnlyList<string> ToolProficiencies { get; set; }

    // class features

    public IReadOnlyList<string> Features { get; set; } = [];
}