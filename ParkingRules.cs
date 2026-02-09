namespace FirstOOPProject;

public static class ParkingRules
{
    public static bool CanParkInZone(Car car, Zone spotType)
    {
        return (spotType, car) switch
        {
            (Zone.Truck, Truck) => true,
            (Zone.Truck, PassangerCar) => false,
            (Zone.B, Truck) => false,
            (Zone.Vip, LuxuryCar) => true,
            (Zone.Vip, _) => false,
            
            _ => true //бычные зоны
        };
    }
    public static string GetPreferredZone(Car car)
    {
        return car switch
        {
            LuxuryCar => nameof(Zone.Vip),
            Truck => nameof(Zone.Truck),
            _ => nameof(Zone.A)
        };
    }
}