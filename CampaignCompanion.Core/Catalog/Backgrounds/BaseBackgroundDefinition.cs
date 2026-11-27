using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignCompanion.Core.Catalog.Backgrounds
{
    public class BaseBackgroundDefinition : CatalogEntry
    {
        public new const string SectionName = "Backgrounds";

        public required string BackgroundType { get; set; }
        public required IReadOnlyList<string> AbilityScores { get; set; }
        public int MaxSpendablePointsInAbilities { get; set; } = 2;
        public required IReadOnlyList<string> SkillProficienciesList { get; set; }
        public required IReadOnlyList<string> ToolProficienciesList { get; set; }
        public required IReadOnlyList<string> Equipment { get; set; }           // es. "core:equipment:artisan-tools:smiths-tools" but need to ben revised in order to accomodate the choice between gold or a set of items
        public required string Feat { get; set; }
    }
}
