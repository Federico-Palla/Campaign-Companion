using CampaignCompanion.Core.Catalog.Enums;

namespace CampaignCompanion.Core.Catalog.Classes
{
    public class GrantDefinition : CatalogEntry;

    public class AbilityScoreGrantDefinition : GrantDefinition
    {
        public IReadOnlyList<string> AbilityChoiceLIst { get; set; } = [];
        public int MaxAffectedAbilities { get; set; }
        public int ScoreChange { get; set; }
        public int ScoreMax { get; set; }
    }

    public class ProficiencyGrantDefinition : GrantDefinition
    {
        public IReadOnlyList<string> Skills { get; set; } = [];
    }

    public class ResourceGrantDefinition : GrantDefinition
    {
        public FeatureProgressionEnum Progression { get; set; }
        public IReadOnlyList<Step> MaxUsages { get; set; } = [];

        // RechargeRules come stringa libera va contro la tua regola dei tipi aperti. Sarebbe meglio dei tag che puntano a entry (core:rest:short).
        public string RechargeRules { get; set; } = string.Empty;
    }

    public class MultiSpellChoiceGrantDefinition : GrantDefinition
    {
        public int SpellLevel { get; set; }
        public int SpellCount { get; set; } = 1;
        public IReadOnlyList<string> ClassSpellLists { get; set; } = [];
        public IReadOnlyList<string> SpellcastingAbilityChoice { get; set; } = [];
    }

    public class FeatureChoiceGrantDefinition : GrantDefinition
    {
        public required string PoolTag { get; init; }            // es. "core:pool:eldritch-invocation"
        public IReadOnlyList<Step> Count { get; init; } = [];    // quante scelte, per livello di classe
    }

    public class SpellGrantDefinition : GrantDefinition
    {
        public string AlwaysPreparedSpell { get; set; } = string.Empty;
    }

    #region Utils
    public sealed class Step
    {
        public int FromLevel { get; set; }
        public int Multiplier { get; set; } = 1;            // nel caso in cui si usi dice (1d6 o 2d6 etc)
        public int Value { get; set; }
    }
    #endregion
}
