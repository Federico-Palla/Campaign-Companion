using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignCompanion.Core.Catalog.Classes
{
    public class ClassFeatureDefinition: CatalogEntry
    {
        public required int Level { get; init; }
        public IReadOnlyList<string> Grants { get; init; } = [];
        public IReadOnlyList<string> Rules { get; init; } = [];
    }

    public class ChoiceOptionFeatureDefinition : ClassFeatureDefinition
    {
        public required string PoolTag { get; init; }
        public IReadOnlyList<string> RequiredFeatures { get; init; } = [];
        public string PrerequisiteText { get; init; } = string.Empty;
    }

    public class ClassFeatureRuleDefinitnion : CatalogEntry;

}
