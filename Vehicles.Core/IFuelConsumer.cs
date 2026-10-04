namespace Vehicles.Core;

public interface IFuelConsumer
{
    double FuelLevel { get; }
    double FuelConsumptionPer100Km { get; }
}
