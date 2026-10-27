using CampaignCompanion.Core.Catalog.Enums;

namespace CampaignCompanion.Core.Catalog.Equipment;

public record MagicalItemDefinition : ItemDefinition
{
    public required ItemRarityDefinition Rarity { get; init; }
    public required bool Attunement { get; init; }
    public string AttunementRequirement { get; init; } = string.Empty;
    public required bool Consumable { get; init; }
    public int Charges { get; init; } = 0;
    public string RechargeRule { get; init; } = string.Empty;
    public MundaneItemDefinition? BaseItem { get; init; }
    public List<MagicalPropertyDefinition> MagicalProperties { get; set; } = [];
}

public record MagicalPropertyDefinition : CatalogEntry
{
    public List<MagicalEffectDefinition> MagicalEffects { get; set; } = [];
}

#region Effects
public abstract record MagicalEffectDefinition
{
    public string Condition { get; init; } = string.Empty;
}

public record BonusEffect : MagicalEffectDefinition
{
    public required BonusDefinition Bonus { get; set; }
}

public record AttackBonusEffect : BonusEffect;
public record ArmorClassBonusEffect : BonusEffect;

public record SavingThrowBonusEffect : BonusEffect
{
    public AbilityDefinition? Ability { get; init; } 
}

public record DamageBonusEffect : BonusEffect
{
    public required DamageTypeDefinition DamageType { get; set; }
}

public record AbilityScoreEffect : BonusEffect
{
    public required AbilityDefinition AbilityScore { get; init; }
    public required AbilityScoreMode Mode { get; init; }
}

public record DamageResistanceEffect: MagicalEffectDefinition
{
    public required DamageTypeDefinition DamageType { get; init; }
    public required DamageModification Kind { get;init;  } 
}
#endregion