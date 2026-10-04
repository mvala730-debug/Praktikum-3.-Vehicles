namespace Vehicles.Core;

public abstract class Vehicle : IMovable
{
    protected Vehicle(string name, double mileage)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nimi ei tohi olla tühi.", nameof(name));

        if (mileage < 0)
            throw new ArgumentOutOfRangeException(nameof(mileage), "Läbisõit ei tohi olla negatiivne.");

        Name = name.Trim();
        Mileage = mileage;
    }

    public string Name { get; }
    public double Mileage { get; protected set; }
    public abstract string TypeName { get; }

    public abstract string Move(double distance);

    protected static void ValidateDistance(double distance)
    {
        if (distance <= 0)
            throw new ArgumentOutOfRangeException(nameof(distance), "Vahemaa peab olema suurem kui 0.");
    }
}
