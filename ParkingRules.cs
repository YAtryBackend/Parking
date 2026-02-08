namespace FirstOOPProject;

public static class ParkingRules
{
    public static bool CanParkInZone(Car car, string spotType)
    {
        return (spotType, car) switch
        {
            ("Truck", Truck) => true,
            ("Truck", PassangerCar) => false,
            ("B", Truck) => false,
            ("VIP", LuxuryCar) => true,
            ("VIP", _) => false,
            
            _ => true //бычные зоны
        };
    }
    public static string GetPreferredZone(Car car)
    {
        return car switch
        {
            LuxuryCar => "VIP",
            Truck => "Truck",
            _ => "A"
        };
    }
}