namespace CampaignCompanion.Core.Catalog;

public sealed record CompendiumDefinition
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required int Version { get; init; }

    public string Description { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public required IReadOnlyList<Guid> CompendiumDependencies { get; set; } = [];
}