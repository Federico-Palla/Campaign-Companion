namespace CampaignCompanion.App.Core.Catalog
{
    public sealed record ItemType : CatalogEntry;
    public sealed record ItemRarity : CatalogEntry;

    public record ItemDefinition : CatalogEntry
    {
        public required int Cost { get; init; }
        public required int Weight { get; init; }
        public required ItemType Type { get; init; }
    }

    public record MundaneItemDefinition : ItemDefinition;
    public record MagicalItemDefinition : ItemDefinition
    {
        public required ItemRarity Rarity { get; init; }
        public required bool Attunement { get; init; }
        public string AttunementRequirement { get; init; } = string.Empty;
        public required bool Consumable { get; init; }
        public int Charges { get; init; } = 0;
        public string RechargeRule { get; init; } = string.Empty;
        public MundaneItemDefinition? BaseItem { get; init; }
    }

}
