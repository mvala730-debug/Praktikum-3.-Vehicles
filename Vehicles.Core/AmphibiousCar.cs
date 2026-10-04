namespace Vehicles.Core;

public class AmphibiousCar : Car
{
    public AmphibiousCar(string name, double mileage, double fuelLevel)
        : base(name, mileage, fuelLevel)
    {
    }

    public override string TypeName => "AmphibiousCar";
    public override double FuelConsumptionPer100Km => 10.0;

    public override string Move(double distance)
    {
        ValidateDistance(distance);
        double requiredFuel = distance * FuelConsumptionPer100Km / 100.0;

        if (FuelLevel < requiredFuel)
            throw new InvalidOperationException("Amfiibautol ei ole liikumiseks piisavalt kütust.");

        Mileage += distance;
        FuelLevel -= requiredFuel;
        return $"{Name} liikus maal või vees {distance:N1} km. Uus läbisõit: {Mileage:N1} km.";
    }
}
