// See https://aka.ms/new-console-template for more information

using BridgeIntelligence.iCompass.Shm.Common.SeedMigrator;

await new SeedMigrator().RunAsync(CancellationToken.None);
await new SeedMigrator().ValidateAsync(CancellationToken.None);
Console.WriteLine("Completed");
