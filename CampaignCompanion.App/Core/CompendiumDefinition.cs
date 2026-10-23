using CampaignCompanion.App.Core.Catalog;

namespace CampaignCompanion.App.Core;

public sealed class CompendiumDefinition
{
    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyList<CatalogEntry> Entries { get; }

    public CompendiumDefinition(Guid id, string name, IEnumerable<CatalogEntry> entries)
    {
        Id = id;
        Name = name;
        Entries = [.. entries];

        var stray = Entries.FirstOrDefault(e => e.CompendiumId != id);
        if (stray is not null)
            throw new ArgumentException($"'{stray.Name}' appartiene a un altro compendium.");
    }

    public CatalogEntry? Find(Guid id) => Entries.FirstOrDefault(e => e.Id == id);
    public IEnumerable<T> Of<T>() where T : CatalogEntry => Entries.OfType<T>();
}