using CampaignCompanion.Core.Catalog.Enums;

namespace CampaignCompanion.Core.Catalog;

/// <summary>
/// Defines a Character's specific Ability, like for example Strength or Intelligence
/// </summary>
public sealed record AbilityDefinition : CatalogEntry;

/// <summary>
/// Defines a Character's specific Skill, like for example Perception or Stealth. It requires
/// a link to an already defined Ability
/// </summary>
public sealed record SkillDefinition : CatalogEntry
{
    public required Guid Attribute { get; init; }
}

/// <summary>
/// Defines a type of Damage that can be dealt, i.e Slashing or Fire Damage
/// </summary>
public sealed record DamageTypeDefinition : CatalogEntry;

/// <summary>
/// A bonus is defined by a flat value + rolling a number of a certain dice (optional). 
/// This dedinition covers all of cases, from adding 1d6 + 3 (1, D6, 3) to your damage to a flat +7 Hp (0,None,7).
/// </summary>
public sealed record BonusDefinition
{
    public required int Bonus { get; set; }
    public DiceRoll? DiceRoll { get; set; }
}

public sealed record DiceRoll(int DiceCount, DiceSizeEnum DiceSize);

public sealed record RangeInterval(int Range)
{
    public int LongRange { get; set; }
}