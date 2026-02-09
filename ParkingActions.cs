using FirstOOPProject.LuxuryCars;
using FirstOOPProject.Trucks;

namespace FirstOOPProject;



public class ParkingActions(Parking parking)
{
    private enum PassangerBrands
    {
        Mersedes,
        Porshe,
        RolsRoyce,
        Lada,
        Renault
    }

    private enum TruckBrands
    {
        Mersedes,
        Man,
        Kamaz
    }
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("   СИСТЕМА УПРАВЛЕНИЯ ПАРКОВКОЙ    ");
            Console.WriteLine($"Парковка: {parking.Name}");
            Console.WriteLine($"Свободных мест: {parking.FreeSpots}/{parking.Capacity}");
            Console.WriteLine("1. Припарковать машину");
            Console.WriteLine("2. Забрать машину");
            Console.WriteLine("3. Найти машину по VIN");
            Console.WriteLine("4. Показать статус парковки");
            Console.WriteLine("5. Найти все машины марки");
            Console.WriteLine("6. Показать свободные места по зоне");
            Console.WriteLine("7. Общая выручка");
            Console.WriteLine("8. Выйти");
            Console.Write("\nВыберите действие: ");
            
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ParkCarAction();
                    break;
                case "2":
                    RemoveCarAction();
                    break;
                case "3":
                    FindCarByVinAction();
                    break;
                case "4":
                    parking.DisplayParkingStatus();
                    break;
                case "5":
                    FindCarsByBrandAction();
                    break;
                case "6":
                    ShowFreeSpotsAction();
                    break;
                case "7":
                    Console.WriteLine($"Общая выручка: {parking.CalculateTotalRevenue():C}");
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
            WaitForContinue();
        }
    }
    
    private void ParkCarAction()
    {
        Console.WriteLine("\n    ПАРКОВКА МАШИНЫ    ");
        
        Console.Write("Тип машины (1-Паcсажирская, 2-Грузовик): ");
        var typeChoice = Console.ReadLine();

        ICar? car = null;
        
        Console.Write("Бренд: ");
        var brand = Console.ReadLine()!;
        Console.Write("Марка: ");
        var name = Console.ReadLine()!;
        Console.Write("Цвет: ");
        var color = Console.ReadLine()!;
        
        Console.Write("VIN (5 символов): ");
        var vin = Console.ReadLine()!;
        
        Console.Write("Год выпуска: ");
        var year = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        
        switch (typeChoice)
        {
            case "1":
                if (Enum.TryParse<PassangerBrands>(brand, true, out var pasBrand))
                {
                    car = CreateCar(pasBrand, name, color, vin, year);
                }
                else
                {
                    Console.WriteLine("Нет такого автомобиля");
                }
                break;
            case "2":
                if (Enum.TryParse<TruckBrands>(brand, true, out var truckBrand))
                {
                    car = CreateCar(truckBrand, name, color, vin, year);
                }
                else
                {
                    Console.WriteLine("Нет такого автомобиля");
                }
                break;
            default:
                Console.WriteLine("Неверный тип");
                return;
        }
        
        var result = parking.ParkCar(car as Car ?? throw new InvalidOperationException());
        Console.WriteLine(result.Message);
    }

    private ICar CreateCar(PassangerBrands type, string name, string color, string vin, int year) => type switch
    {
        PassangerBrands.Lada => new Lada(name, color, vin, year),
        PassangerBrands.Mersedes => new Mersedes(name, color, vin, year),
        PassangerBrands.Porshe => new Porshe(name, color, vin, year),
        PassangerBrands.Renault => new Renault(name, color, vin, year),
        PassangerBrands.RolsRoyce => new RolsRoyce(name, color, vin, year),
        _ => throw new ArgumentException()
    };
    private ICar CreateCar(TruckBrands type, string name, string color, string vin, int year) => type switch
    {
        TruckBrands.Man => new Man(name, color, vin, year),
        TruckBrands.Mersedes => new MersedesBenz(name, color, vin, year),
        TruckBrands.Kamaz => new Kamaz(name, color, vin, year),
        _ => throw new ArgumentException()
    };
    private void RemoveCarAction()
    {
        Console.Write("\nВведите VIN машины для выезда: ");
        var vin = Console.ReadLine();

        if (vin != null)
        {
            var car = parking.RemoveCar(vin);
            if (car != null)
            {
                Console.WriteLine($"Машина {car.Brand} {car.Name} успешно удалена");
            }
        }

    }
    
    private void FindCarByVinAction()
    {
        Console.Write("\nВведите VIN для поиска: ");
        var vin = Console.ReadLine();

        if (vin != null)
        {
            var car = parking.FindCarByVin(vin);
            var spot = parking.FindCarSpot(vin);
        
            if (car != null && spot != null)
            {
                Console.WriteLine($"Найдена машина: {car.Brand} {car.Name}");
                Console.WriteLine($"Цвет: {car.Color}, Год: {car.Year}");
                Console.WriteLine($"Место: {spot.SpotNumber} (зона {spot.SpotType})");
                Console.WriteLine($"Парковался с: {spot.OccupiedSince}");
            }
            else
            {
                Console.WriteLine("Машина не найдена");
            }
        }

    }
    
    private void FindCarsByBrandAction()
    {
        Console.Write("\nВведите марку для поиска: ");
        var brand = Console.ReadLine();

        if (brand != null)
        {
            var cars = parking.FindCarsByBrand(brand);
        
            if (cars.Count != 0)
            {
                Console.WriteLine($"Найдено машин {brand}: {cars.Count}");
                foreach (var car in cars)
                {
                    Console.WriteLine($"  - {car.Name}, {car.Color}, VIN: {car.Vin}");
                }
            }
            else
            {
                Console.WriteLine($"Машин марки {brand} не найдено");
            }
        }

    }
    
    private void ShowFreeSpotsAction()
    {
        Console.Write("\nВведите зону (A, B, VIP, Truck): ");
        var zone = Console.ReadLine();

        if (zone != null)
        {
            var freeSpots = parking.GetFreeSpotsByZone(zone);
        
            if (freeSpots.Any())
            {
                Console.WriteLine($"Свободные места в зоне {zone}:");
                foreach (var spot in freeSpots)
                {
                    Console.WriteLine($"  Место {spot.SpotNumber}-");
                }
            }
            else
            {
                Console.WriteLine($"Нет свободных мест в зоне {zone}");
            }
        }

    }
    
    private static void WaitForContinue()
    {
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}