using CampaignCompanion.Core.Catalog.Enums;

namespace CampaignCompanion.Core.Catalog.Spells;

public record SpellcastingSchoolDefinition : CatalogEntry
{
    public IReadOnlyList<SpellDefinition> Spells { get; set; } = [];
}

public sealed record SpellcastingComponentDefinition : CatalogEntry;

public record SpellDefinition : CatalogEntry
{
    /*
    + classes: string[]
    + savingThrow: SavingThrowType
    + savingThrowAbility: Ability
    + additionalEffects: ConditionEffect[]
    + school : SpellSchoolDefinition
     */
    public required int Level { get; set; }
    public required ActionTypeEnum CastingTime { get; set; }
    public required RangeInterval Range { get; set; }
    public required int Duration { get; set; }
    public required AbilityDefinition SpellcastingAbility {  get; set; }
    public bool HasConcentration { get; set; } = false;
    public bool Ritual { get; set; } = false;
    public string AtHigherLevelsDescription { get; set; } = string.Empty;
}
