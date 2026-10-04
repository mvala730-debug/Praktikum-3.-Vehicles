namespace Vehicles.Core;

public class Boat : Vehicle, IFuelConsumer
{
    public Boat(string name, double mileage, double fuelLevel)
        : base(name, mileage)
    {
        if (fuelLevel < 0)
            throw new ArgumentOutOfRangeException(nameof(fuelLevel), "Kütusekogus ei tohi olla negatiivne.");

        FuelLevel = fuelLevel;
    }

    public override string TypeName => "Boat";
    public double FuelLevel { get; protected set; }
    public virtual double FuelConsumptionPer100Km => 12.0;

    public override string Move(double distance)
    {
        ValidateDistance(distance);
        double requiredFuel = distance * FuelConsumptionPer100Km / 100.0;

        if (FuelLevel < requiredFuel)
            throw new InvalidOperationException("Paadil ei ole liikumiseks piisavalt kütust.");

        Mileage += distance;
        FuelLevel -= requiredFuel;
        return $"{Name} sõitis vees {distance:N1} km. Uus läbisõit: {Mileage:N1} km.";
    }
}
