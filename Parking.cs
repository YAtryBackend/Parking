namespace FirstOOPProject;

public class Parking
{
    private readonly List<ParkingSpot> _parkingSpots;
    private readonly Dictionary<string, Car> _carsByVin;
    private readonly Dictionary<string, ParkingSpot> _spotsByCarVin;
    public string Name { get; }
    public int Capacity => _parkingSpots.Count;
    private int OccupiedSpots => _parkingSpots.Count(s => s.IsOccupied);
    public int FreeSpots => Capacity - OccupiedSpots;

    public Parking(string name,int countSpots)
    {
        Name = name;
        _parkingSpots =  new List<ParkingSpot>(countSpots);
        _carsByVin = new Dictionary<string, Car>(countSpots);
        _spotsByCarVin = new Dictionary<string, ParkingSpot>(countSpots);
        
        if (countSpots <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(countSpots));
        }
        InitializeParkingSports(countSpots);
    }

    private void InitializeParkingSports(int countSpots)
    {
        var vipSpots = countSpots / 10; //10%
        var truckSports = countSpots / 5; //20%
        var bSports = countSpots / 4; //25%
        var aSports = countSpots - vipSpots - truckSports - bSports; //55%

        var numerSpot = 1;
        for (var i = 0; i < vipSpots; i++)
        {
            _parkingSpots.Add(new ParkingSpot("VIP", numerSpot++));
        }

        for (var i = 0; i < truckSports; i++)
        {
            _parkingSpots.Add(new ParkingSpot("TRUCK", numerSpot++));
        }

        for (var i = 0; i < bSports; i++)
        {
            _parkingSpots.Add(new ParkingSpot("B", numerSpot++));
        }

        for (var i = 0; i < aSports; i++)
        {
            _parkingSpots.Add(new ParkingSpot("A", numerSpot++));
        }
    }
    public ParkingResult ParkCar(Car car)
    {
        if (_carsByVin.ContainsKey(car.Vin))
        {
            return new ParkingResult(false, "Машина уже припаркована", null);
        }
        var suitableSpot = FindSuitableSpot(car);

        if (suitableSpot == null || !suitableSpot.AddCar(car))
            return new ParkingResult(false, "Не удалось припарковать машину", null);
        _carsByVin[car.Vin] = car;
        _spotsByCarVin[car.Vin] = suitableSpot;
            
        return new ParkingResult(true, 
            $"Машина припаркована на месте {suitableSpot.SpotNumber} ({suitableSpot.SpotType})", 
            suitableSpot);

    }

    private ParkingSpot? FindSuitableSpot(Car car)
    {
        var preferredZone = ParkingRules.GetPreferredZone(car);
        
        var spot = _parkingSpots.FirstOrDefault(s =>
            !s.IsOccupied &&
            s.SpotType == preferredZone &&
            ParkingRules.CanParkInZone(car, s.SpotType)
        ) ?? _parkingSpots.FirstOrDefault(s =>
            !s.IsOccupied &&
            ParkingRules.CanParkInZone(car, s.SpotType)
        );

        return spot;
    }

    public Car? RemoveCar(string vin)
    {
        if (!_carsByVin.ContainsKey(vin))
        {
            Console.WriteLine("Машина с таким VIN не найдена");
            return null;
        }
        
        var spot = _spotsByCarVin[vin];
        var car = spot.RemoveCar();

        if (car == null) return car;
        _carsByVin.Remove(vin);
        _spotsByCarVin.Remove(vin);
        
        var fee = spot.CalculateParkingFee();
        Console.WriteLine($"Машина {car.Brand} {car.Name} выехала. К оплате: {fee:C}");

        return car;
    }

    public void DisplayParkingStatus()
    {
        Console.WriteLine($"\n    Статус парковки '{Name}'    ");
        Console.WriteLine($"Всего мест: {Capacity}");
        Console.WriteLine($"Занято: {OccupiedSpots}");
        Console.WriteLine($"Свободно: {FreeSpots}");
        Console.WriteLine($"\nДетали по зонам:");
        
        var zones = _parkingSpots.GroupBy(s => s.SpotType)
                         .OrderBy(g => g.Key);
        
        foreach (var zone in zones)
        {
            var occupied = zone.Count(s => s.IsOccupied);
            var total = zone.Count();
            Console.WriteLine($"Зона {zone.Key}: {occupied}/{total} занято");
        }
        
        Console.WriteLine("\nЗанятые места:");
        foreach (var spot in _parkingSpots.Where(s => s.IsOccupied))
        {
            Console.WriteLine($"  {spot}");
        }
    }

    public Car? FindCarByVin(string vin)
    {
        return _carsByVin.GetValueOrDefault(vin);
    }

    public List<Car> FindCarsByBrand(string brand)
    {
        return _carsByVin.Values
            .Where(c => c.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public ParkingSpot? FindCarSpot(string vin)
    {
        return _spotsByCarVin.GetValueOrDefault(vin);
    }

    public decimal CalculateTotalRevenue()
    {
        return (decimal)_parkingSpots.Sum(s => s.CalculateParkingFee());
    }

    public List<ParkingSpot> GetFreeSpotsByZone(string zone)
    {
        return _parkingSpots.Where(s => !s.IsOccupied && s.SpotType == zone).ToList();
    }
}

    public class ParkingResult(bool success, string message, ParkingSpot? spot)
    {
        public bool Success { get; } = success;
        public string Message { get; } = message;
        public ParkingSpot Spot { get; } = spot ?? throw new ArgumentNullException(nameof(spot));
    }