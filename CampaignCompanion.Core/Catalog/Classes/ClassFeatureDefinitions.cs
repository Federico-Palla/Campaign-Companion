using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignCompanion.Core.Catalog.Classes
{

    public enum FeatureProgressionEnum
    {
        Fixed,
        Dice,
        ClassLevel
    }

    public class BaseClassFeatureDefinintion: CatalogEntry
    {
        public required int Level { get; set; }
        public IReadOnlyList<string> Grants { get; set; } = [];
        public IReadOnlyList<string> Rules { get; set; } = [];

    }

    public class ClassFeatureRuleDefinitnion : CatalogEntry;


    #region Grants
    public class ClassFeatureGrantDefinition : CatalogEntry;

    public class ClassFeatureResourceGrantDefinition: ClassFeatureGrantDefinition
    {
        public IReadOnlyList<ClassFeatureResourceGrantLevelStep> MaxUsages { get; set; } = [];
        public string RechargeRules { get; set; } = string.Empty;
        public ClassFeatureActivationCost? ActivationCost { get; set; }         // va spostato: qui definisco solo la rage-resource, il quanto costa usarla dovrebbe stare fuori
    }

    public sealed class ClassFeatureResourceGrantLevelStep
    {
        public int FromLevel { get; set; }
        public int Value { get; set; }
    }

    public class ClassFeatureActivationCost
    {
        public IReadOnlyList<ClassFeatureResourceGrantLevelStep> Amount { get; set; } = [];
    }
    #endregion
}
