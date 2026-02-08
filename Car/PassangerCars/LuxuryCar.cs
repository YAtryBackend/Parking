namespace FirstOOPProject;

public abstract class LuxuryCar(string name, string color, string vin, int year)
    : PassangerCar(name, color, vin, year)
{
    public override string Brand => "Luxury";

    public override double GetParkingFee() => 200;

    protected override void Display()
    {
        base.Display();
        Console.WriteLine($"Класс: Люкс. Стоимость парковки: {GetParkingFee()}");
    }
}