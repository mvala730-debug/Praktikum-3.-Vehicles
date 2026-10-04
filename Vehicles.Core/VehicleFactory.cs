namespace Vehicles.Core;

public static class VehicleFactory
{
    public static bool TryCreate(
        string type,
        string name,
        string mileageText,
        string fuelText,
        out Vehicle? vehicle,
        out string errorMessage)
    {
        vehicle = null;
        errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 2 || name.Trim().Length > 30)
        {
            errorMessage = "Nimi peab olema 2-30 märki.";
            return false;
        }

        if (!double.TryParse(mileageText, out double mileage) || mileage < 0)
        {
            errorMessage = "Läbisõit peab olema arv ja vähemalt 0 km.";
            return false;
        }

        if (!double.TryParse(fuelText, out double fuel) || fuel < 0 || fuel > 1000)
        {
            errorMessage = "Kütusekogus peab olema 0-1000 liitrit.";
            return false;
        }

        try
        {
            vehicle = type switch
            {
                "Car" => new Car(name, mileage, fuel),
                "Boat" => new Boat(name, mileage, fuel),
                "AmphibiousCar" => new AmphibiousCar(name, mileage, fuel),
                _ => null
            };
        }
        catch (ArgumentException exception)
        {
            errorMessage = exception.Message;
            return false;
        }

        if (vehicle is null)
        {
            errorMessage = "Vali sõiduki tüüp.";
            return false;
        }

        return true;
    }
}
