using CampaignCompanion.App;
using CampaignCompanion.Core;
using CampaignCompanion.Core.Catalog;
using System.Text.Json;

CompendiumManager manager = new CompendiumManager();
manager.LoadCompendiaFromLocalJson();

//manager.TryEnableCompendiumById(new Guid("7bb94f11-1234-4b5a-9c7e-8a9b0c1d2e99")); 
manager.TryEnableCompendiumById(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")); 

return;

string basePath = AppContext.BaseDirectory;
string path = Path.Combine(basePath, "JSON", "test_compendium.json"); 
string jsonString = File.ReadAllText(path);

// Deserialize into the wrapper root
CompendiumFileRoot? root = JsonSerializer.Deserialize<CompendiumFileRoot>(jsonString);

// Extract your CompendiumDefinition
CompendiumDefinition? compendiumDefinition = root?.Compendium;
if (compendiumDefinition == null) throw new Exception("SUKKA");

Compendium compendium = new(compendiumDefinition);
Console.WriteLine(compendium.ToString());
