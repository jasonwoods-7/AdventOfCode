namespace AoC.Y2023.Day05;

public record Parsed(
    IReadOnlyList<long> Seeds,
    ElfMaps SeedToSoilMaps,
    ElfMaps SoilToFertilizerMaps,
    ElfMaps FertilizerToWaterMaps,
    ElfMaps WaterToLightMaps,
    ElfMaps LightToTemperatureMaps,
    ElfMaps TemperatureToHumidityMaps,
    ElfMaps HumidityToLocationMaps
)
{
    public long SeedToLocation(long seed) =>
        seed.Apply(SeedToSoilMaps.FindDestination)
            .Apply(SoilToFertilizerMaps.FindDestination)
            .Apply(FertilizerToWaterMaps.FindDestination)
            .Apply(WaterToLightMaps.FindDestination)
            .Apply(LightToTemperatureMaps.FindDestination)
            .Apply(TemperatureToHumidityMaps.FindDestination)
            .Apply(HumidityToLocationMaps.FindDestination);

    public long LocationToSeed(long location) =>
        location
            .Apply(HumidityToLocationMaps.FindSource)
            .Apply(TemperatureToHumidityMaps.FindSource)
            .Apply(LightToTemperatureMaps.FindSource)
            .Apply(WaterToLightMaps.FindSource)
            .Apply(FertilizerToWaterMaps.FindSource)
            .Apply(SoilToFertilizerMaps.FindSource)
            .Apply(SeedToSoilMaps.FindSource);
}
