using CampaignCompanion.Core.Catalog;
using System.Text;

namespace CampaignCompanion.Core;

public sealed class Compendium(CompendiumDefinition definition)
{
    private readonly List<CatalogEntry> _entries = [];

    // Info
    public Guid Id => definition.Id;
    public string Name => definition.Name;
    public string Tag => definition.Tag;
    public string Version => definition.Version;
    public string Description => definition.Description;
    public IReadOnlyList<CatalogDependency> Dependencies => [.. definition.Dependencies.Select(x => new CatalogDependency() { Tag = x.Key, VersionRange = x.Value })];
    public DateTime CreationDate => definition.CreationDate;
    public DateTime LastUpdatedAt => definition.LastUpdatedAt;

    public CompendiumDefinition Definition => definition;

    // Entries
    public List<CatalogEntry> All() => _entries;
    public CatalogEntry? FindById(Guid id) => _entries.FirstOrDefault(e => e.Id == id);
    public CatalogEntry? FindByName(string name) => _entries.FirstOrDefault(e => e.Name == name);
    public CatalogEntry? FindByTag(string tag) => _entries.FirstOrDefault(e => e.Tag == tag);
    
    public void Add(CatalogEntry entry) => _entries.Add(entry);
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

                if(!entry.Validate(out string errorMessage))
                    throw new InvalidOperationException($"Invalid Entry Found: '{entry.Tag}': {errorMessage}");

                foreach(var dependency in entry.Dependencies)
                {
                    if (!_entries.Select(x => x.Tag).Any(d => d == dependency.Key))
                        throw new InvalidOperationException($"Invalid Dependency Found: '{entry.Tag}' depends on '{dependency.Key}' which is not listed as a dependency of the Compendium.");

                    //for now, we will not validate the version range of the dependency, but we could add that in the future if needed
                }

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

        sb.AppendLine($"=== Compendium: {Name} (v.{Version}) ===");
        sb.AppendLine($"ID           : {Id}");
        sb.AppendLine($"Tag          : {(string.IsNullOrEmpty(Tag) ? "(None)" : Tag)}");
        sb.AppendLine($"Description  : {(string.IsNullOrEmpty(Description) ? "(No description)" : Description)}");
        sb.AppendLine($"Created      : {CreationDate:yyyy-MM-dd HH:mm:ss UTC}");
        sb.AppendLine($"Last Updated : {LastUpdatedAt:yyyy-MM-dd HH:mm:ss UTC}");
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