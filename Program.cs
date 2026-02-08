using FirstOOPProject.LuxuryCars;
using FirstOOPProject.Trucks;

namespace FirstOOPProject
{
    class Program
    {
        static void Main()
        {
            // Создаем парковку на 50 мест
            Parking parking = new Parking("Central Parking", 50);
        
            // Создаем действия для управления
            ParkingActions actions = new ParkingActions(parking);
        
            // Заполняем парковку тестовыми данными
            SeedTestData(parking);
        
            // Запускаем меню управления
            actions.ShowMenu();
        }

        private static void SeedTestData(Parking parking)
        {
            // Добавляем несколько тестовых машин
            var cars = new Car[]
            {
                new Mersedes("S-Class", "Black", "MB001", 2023),
                new Mersedes("E-Class", "White", "MB002", 2022),
                new MersedesBenz("Actros", "Blue", "MT001", 2021),
                new Lada("Granta", "Red", "LD001", 2020),
                new Renault("Logan", "Silver", "HY001", 2021),
                new RolsRoyce("Phantom", "Black", "BM001", 2023)
            };
        
            foreach (var car in cars)
            {
                parking.ParkCar(car);
            }
        }
    }
}
