namespace Vehicles.Core;

public class Car : Vehicle, IFuelConsumer
{
    public Car(string name, double mileage, double fuelLevel)
        : base(name, mileage)
    {
        if (fuelLevel < 0)
            throw new ArgumentOutOfRangeException(nameof(fuelLevel), "Kütusekogus ei tohi olla negatiivne.");

        FuelLevel = fuelLevel;
    }

    public override string TypeName => "Car";
    public double FuelLevel { get; protected set; }
    public virtual double FuelConsumptionPer100Km => 7.0;

    public override string Move(double distance)
    {
        ValidateDistance(distance);
        double requiredFuel = distance * FuelConsumptionPer100Km / 100.0;

        if (FuelLevel < requiredFuel)
            throw new InvalidOperationException("Autol ei ole liikumiseks piisavalt kütust.");

        Mileage += distance;
        FuelLevel -= requiredFuel;
        return $"{Name} sõitis {distance:N1} km. Uus läbisõit: {Mileage:N1} km.";
    }
}
