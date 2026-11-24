namespace CampaignCompanion.Core.Catalog.Feats;

public sealed class FeatCategoryDefinition : CatalogEntry;

public class FeatDefinition : CatalogEntry
{
    /// <summary>
    /// The Category of the feat, for example this can be an Origin Feat or a Class Feature
    /// </summary>
    public required string Category { get; set; }

    /// <summary>
    /// A feat can have prerequisites, for example Dragonmarks
    /// </summary>
    public IReadOnlyList<string> PrerequisiteFeatures { get; init; } = [];
    public string PrerequisiteText { get; init; } = string.Empty;

    /// <summary>
    /// A feat usually grants you something, a resource to spend, one or more spells, etc
    /// </summary>
    public IReadOnlyList<string> Grants { get; set; } = [];

    /// <summary>
    /// A feat comes with a set of written rules, here managed as just text
    /// </summary>
    public IReadOnlyList<string> Rules { get; set; } = [];
}

public class ClassFeatDefinition : FeatDefinition
{
    public required int Level { get; init; }
}

/// <summary>
/// Like for example eldritch invocations: an invocation is still a feature belonging to a pool from where a warlock can choose some, each invocation carries it's own grants and rules
/// </summary>
public class ChoiceOptionFeatDefinition : ClassFeatDefinition
{
    public required string PoolTag { get; init; }
}

public class RuleDefinition : CatalogEntry;