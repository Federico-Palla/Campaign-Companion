using CampaignCompanion.Core.Catalog;
using CampaignCompanion.Core.Catalog.Equipment;

namespace CampaignCompanion.Core.Entities
{
    public class BaseEntity(Compendium compendium, CatalogEntry catalogEntry)
    {
        public string Name => catalogEntry.Name;
        public string Tag => catalogEntry.Tag;
        public string Description => catalogEntry.Description;
        public Compendium Compendium { get; init; } = compendium;
    }

    public class ItemEntity(Compendium compendium, ItemDefinition itemDefinition) : BaseEntity(compendium, itemDefinition)
    {
        public int Cost => itemDefinition.Cost;
        public int Weight => itemDefinition.Weight;

    }
}
