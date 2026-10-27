using CampaignCompanion.Core;
using CampaignCompanion.Core.Catalog;
using CampaignCompanion.Core.Catalog.Enums;
using CampaignCompanion.Core.Catalog.Equipment;

Guid compendiumId = Guid.NewGuid();
AbilityDefinition wisdom = new() { Name = "Wisdom", Description = "Describes the character's wisdom", CompendiumId = compendiumId };
SkillDefinition perception = new() { Name = "Perception", Description = "Ability to perceive your surroundings", CompendiumId = compendiumId, Attribute = wisdom};
ItemTypeDefinition weaponType = new() { Name = "Weapon", Description = "Item used to deal some bonks", CompendiumId = compendiumId };
WeaponCategoryDefinition wc = new() { Name = "Martial", Description = "", CompendiumId = compendiumId };
WeaponPropertyDefinition prop = new() { Name = "Versatile", Description = "Versatile", CompendiumId = compendiumId };
WeaponPropertyUsageDefinition longProp = new() { Property = prop, AlternateRoll = new DiceRoll(1, DiceSizeEnum.D10)};
WeaponDefinition longsword = new()
{
    Name = "Longsword",
    Description = "Sharp weapon indeed",
    CompendiumId = compendiumId,
    Cost = 50,
    Weight = 1,
    Type = weaponType,
    Category = wc,
    WeaponProperties = [longProp],
    Damage = new DiceRoll(1, DiceSizeEnum.D8)
}; 

CompendiumDefinition compendium = new(compendiumId, "Test CompendiumId", [wisdom, perception, weaponType, longsword]);


Console.WriteLine($"Using {compendium.Name} as a source. Current content:");
foreach (var entry in compendium.Entries.Where(e => e.CompendiumId == compendiumId))
{
    Console.WriteLine($"\t- {entry.Name}: {entry.Description}");
}

Console.WriteLine($"Using {compendium.Name} as a source. Current weapons are:");
foreach (var entry in compendium.Of<WeaponDefinition>())
{
    Console.WriteLine($"\t- {entry.Name}: {entry.Description}");
}