using CampaignCompanion.Core.Catalog;
using System.Text;

namespace CampaignCompanion.Core;

public sealed class Compendium(CompendiumDefinition Definition)
{
    private CompendiumDefinition Definition { get; set; } = Definition;
    private readonly List<CatalogEntry> _entries = [];

    public Guid Id => Definition.Id;
    public string Name => Definition.Name;
    public string Tag => Definition.Tag;
    public int Version => Definition.Version;
    public string Description => Definition.Description;
    public IReadOnlyList<Guid> Dependencies => Definition.CompendiumDependencies;

    public List<CatalogEntry> All() => _entries;
    public CatalogEntry? FindById(Guid id) => _entries.FirstOrDefault(e => e.Id == id);
    public CatalogEntry? FindByName(string name) => _entries.FirstOrDefault(e => e.Name == name);
    public void Add(CatalogEntry entry)
    {
        entry.CompendiumId = Id;
        _entries.Add(entry);
    }

    public void Clear() => _entries.Clear();
    public IEnumerable<T> Of<T>() where T : CatalogEntry => _entries.OfType<T>();

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"=== Compendium: {Name} (v{Version}) ===");
        sb.AppendLine($"ID           : {Definition.Id}");
        sb.AppendLine($"Tag          : {(string.IsNullOrEmpty(Tag) ? "(None)" : Tag)}");
        sb.AppendLine($"Description  : {(string.IsNullOrEmpty(Description) ? "(No description)" : Description)}");
        sb.AppendLine($"Created      : {Definition.CreationDate:yyyy-MM-dd HH:mm:ss UTC}");
        sb.AppendLine($"Last Updated : {Definition.LastUpdatedAt:yyyy-MM-dd HH:mm:ss UTC}");
        sb.AppendLine("Dependencies :");

        if (Dependencies.Count == 0)
        {
            sb.AppendLine("  (None)");
        }
        else
        {
            foreach (var dependencyId in Dependencies)
            {
                sb.AppendLine($"  - {dependencyId}");
            }
        }
        sb.AppendLine(new string('=', 50));

        return sb.ToString();
    }
}