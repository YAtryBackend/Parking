namespace FirstOOPProject;

public class LuxuryCars : PassangerCar
{
    protected override string Brand => "Luxury";

    protected LuxuryCars(string name, string color, string vin, int year) 
        : base(name, color, vin, year)
    {
    }
    
    public override double GetParkingFee() => 200;
    
    public override void Display()
    {
        base.Display();
        Console.WriteLine($"Класс: Люкс. Стоимость парковки: {GetParkingFee()}");
    }
}