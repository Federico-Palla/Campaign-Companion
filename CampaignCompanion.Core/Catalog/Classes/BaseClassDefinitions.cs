using CampaignCompanion.Core.Catalog.Enums;
using CampaignCompanion.Core.Catalog.Equipment;

namespace CampaignCompanion.Core.Catalog.Classes;

public record ClassDefinition : CatalogEntry
{
    //Core Traits
    public required AbilityDefinition PrimaryAbility { get; set; }
    public required DiceSizeEnum HitPointDie { get; set; }
    public required IReadOnlyList<AbilityDefinition> SavingThrowProficencies { get; set; }
    public required IReadOnlyList<SkillDefinition> SkillProficenciesList { get; set; }
    public required IReadOnlyList<WeaponCategoryDefinition> WeaponProficiencies {  get; set; }
    public required IReadOnlyList<ArmorCategoryDefinition> ArmorTrainingList { get; set; }

    // class features
}