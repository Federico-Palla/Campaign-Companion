using CampaignCompanion.Core.Catalog;
using System.Text;

namespace CampaignCompanion.Core;

public sealed class Compendium(CompendiumDefinition Definition)
{
    private CompendiumDefinition Definition { get; init; } = Definition;
    private readonly List<CatalogEntry> _entries = [];

    public Guid Id => Definition.Id;
    public string Name => Definition.Name;
    public string Tag => Definition.Tag;
    public string Version => Definition.Version;
    public string Description => Definition.Description;
    public IReadOnlyList<CatalogDependency> Dependencies => [.. Definition.Dependencies.Select(x => new CatalogDependency() { Tag = x.Key, VersionRange = x.Value })];

    public List<CatalogEntry> All() => _entries;
    public CatalogEntry? FindById(Guid id) => _entries.FirstOrDefault(e => e.Id == id);
    public CatalogEntry? FindByName(string name) => _entries.FirstOrDefault(e => e.Name == name);
    public CatalogEntry? FindByTag(string tag) => _entries.FirstOrDefault(e => e.Tag == tag);
    public void Add(CatalogEntry entry)
    {
        entry.CompendiumId = Id;
        _entries.Add(entry);
    }

    public void Clear() => _entries.Clear();
    public IEnumerable<T> Of<T>() where T : CatalogEntry => _entries.OfType<T>();

    public bool Validate()
    {
        var registry = new Dictionary<string, CatalogEntry>();
        try
        {
            foreach (var entry in _entries)
            {
                if (registry.ContainsKey(entry.Tag))
                    throw new InvalidOperationException($"Duplicated Tag Found: '{entry.Tag}'.");

                registry[entry.Tag] = entry;
            }



            return true;
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

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
            foreach (var dependency in Dependencies)
            {
                sb.AppendLine($"  - {dependency.Tag}:{dependency.VersionRange}");
            }
        }
        sb.AppendLine(new string('=', 50));

        return sb.ToString();
    }
}