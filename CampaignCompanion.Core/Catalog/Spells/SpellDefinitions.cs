using CampaignCompanion.Core.Catalog.Enums;

namespace CampaignCompanion.Core.Catalog.Spells;

public class SpellcastingSchoolDefinition : CatalogEntry;

public sealed class SpellcastingComponentDefinition : CatalogEntry;

public class SpellDefinition : CatalogEntry
{
    /*
    + classes: string[]
    + savingThrow: SavingThrowType
    + savingThrowAbility: Ability
    + additionalEffects: ConditionEffect[]
     */
    public required string SpellSchool {  get; set; }
    public required int Level { get; set; }
    public required ActionTypeEnum CastingTime { get; set; }
    public required RangeInterval Range { get; set; }
    public required int Duration { get; set; }
    public bool HasConcentration { get; set; } = false;
    public bool Ritual { get; set; } = false;
    public string SpellUpgradeLabel { get; set; } = string.Empty;
    public string SpellUpgradeDescription { get; set; } = string.Empty;

    public override bool Validate()
    {
        return Dependencies.ContainsKey(SpellSchool);
    }
}
