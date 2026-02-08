namespace FirstOOPProject;

public class ParkingSpot(string spotType, int spotNumber) // класс отвечает за парковочное место
{
    public string SpotType{get;} = spotType.ToUpper(); //A,B,VIP,TRUCK
    public int SpotNumber{get;} = spotNumber;
    public bool IsOccupied=>Car!=null;
    private Car? Car { get; set; }
    public DateTime? OccupiedSince { get; private set; }
    
    public bool AddCar(Car car)
    {
        if (IsOccupied || !ParkingRules.CanParkInZone(car,SpotType)) return false;
        Car = car;
        OccupiedSince = DateTime.Now;
        return true;
    }
    public Car? RemoveCar()
    {
        if (!IsOccupied) return null;
        
        var car = Car;
        Car = null;
        OccupiedSince = null;
        return car;
    }
    public double CalculateParkingFee()
    {
        if (!IsOccupied || !OccupiedSince.HasValue) return 0;
        
        var hours = (DateTime.Now - OccupiedSince.Value).TotalHours;
        var baseRate = Car?.GetParkingFee() ?? 0;
        if (SpotType == "VIP") baseRate *= 1.5;
        
        return baseRate * Math.Ceiling(hours);
    }

    public override string ToString()
    {
        return $"Место {SpotNumber} ({SpotType}) - " + (IsOccupied ? $"Занято: {Car!.Brand} {Car.Name}" : "Свободно");
    }
}