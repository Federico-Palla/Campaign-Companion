using CampaignCompanion.App.Core;
using CampaignCompanion.App.Core.Catalog;

Guid compendiumId = Guid.NewGuid();
AbilityDefinition strength = new() { Name = "Strength", Description = "Describes the character's might", CompendiumId = compendiumId };
SkillDefinition perception = new() { Name = "Perception", Description = "Ability to perceive your surroundings", CompendiumId = compendiumId };
ItemType weaponType = new() { Name = "Weapon", Description = "Item used to deal some bonks", CompendiumId = compendiumId };
ItemDefinition longsword = new() { Name = "Longsword", Description = "Sharp weapon indeed", CompendiumId = compendiumId, Cost = 50, Weight = 1, Type = weaponType };

CompendiumDefinition compendium = new(compendiumId, "Test CompendiumId", [strength, perception, weaponType, longsword]);


Console.WriteLine($"Using {compendium.Name} as a source. Current content:");
foreach (var entry in compendium.Entries.Where(e => e.CompendiumId == compendiumId))
{
    Console.WriteLine($"\t- {entry.Name}: {entry.Description}");
}