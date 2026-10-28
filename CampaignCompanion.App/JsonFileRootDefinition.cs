using CampaignCompanion.Core.Catalog;

namespace CampaignCompanion.App
{
    public sealed record CompendiumFileRoot
    {
        public required CompendiumDefinition Compendium { get; init; }
    }
}
