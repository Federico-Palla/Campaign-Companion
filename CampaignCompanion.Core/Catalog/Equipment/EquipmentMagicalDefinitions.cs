using CampaignCompanion.Core.Catalog.Enums;

namespace CampaignCompanion.Core.Catalog.Equipment;

public class MagicalItemDefinition : ItemDefinition
{
    public required string Rarity { get; init; }
    public required bool Attunement { get; init; }
    public string AttunementRequirement { get; init; } = string.Empty;
    public required bool Consumable { get; init; }
    public int Charges { get; init; } = 0;
    public string RechargeRule { get; init; } = string.Empty;
    public string? BaseItem { get; init; }
    public List<MagicalPropertyDefinition> MagicalProperties { get; set; } = [];

    public override bool Validate()
    {
        return Dependencies.ContainsKey(Rarity) && (string.IsNullOrEmpty(BaseItem) || Dependencies.ContainsKey(BaseItem));
    }
}

public class MagicalPropertyDefinition : CatalogEntry
{
    public List<MagicalEffectDefinition> MagicalEffects { get; set; } = [];
}

#region Effects
public abstract class MagicalEffectDefinition
{
    public string Condition { get; init; } = string.Empty;
}

public class BonusEffect : MagicalEffectDefinition
{
    public required BonusDefinition Bonus { get; set; }
}

public class AttackBonusEffect : BonusEffect;
public class ArmorClassBonusEffect : BonusEffect;

public class SavingThrowBonusEffect : BonusEffect
{
    public AbilityDefinition? Ability { get; init; } 
}

public class DamageBonusEffect : BonusEffect
{
    public required DamageTypeDefinition DamageType { get; set; }
}

public class AbilityScoreEffect : BonusEffect
{
    public required AbilityDefinition AbilityScore { get; init; }
    public required AbilityScoreMode Mode { get; init; }
}

public class DamageResistanceEffect: MagicalEffectDefinition
{
    public required DamageTypeDefinition DamageType { get; init; }
    public required DamageModification Kind { get;init;  } 
}
#endregion