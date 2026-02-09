namespace FirstOOPProject;

public abstract class Car
{
    public abstract string Brand { get; }
    
    protected Car(string name, string color, string vin, int year)
    {
        Name = name;
        Color = color;
        _vin = vin;
        Vin = vin ?? throw new ArgumentNullException(nameof(vin));
        Year = year;
        
    }


    public string Name { get; }
    public string Color { get; }
    private readonly string _vin;
    private readonly int _year;

    protected virtual void Display()
    {
        Console.WriteLine($"Машина: {Brand} {Name}, Цвет: {Color}, Год: {Year}, VIN: {Vin}");
    }

    public string Vin
    {
        get=> _vin;
        private init
        {
            if (value.Contains(' ')|| string.IsNullOrEmpty(value))
            {
                throw new ArgumentException();
            }
            _vin = value.Length == 5 ? value : value[..5];
        }
    }

    public int Year
    {
        get => _year;
        private init
        {
            _year = value switch
            {
                < 1885 or > 2026 => throw new ArgumentOutOfRangeException(),
                _ => value
            };
        }
    }

    public abstract double GetParkingFee();
}