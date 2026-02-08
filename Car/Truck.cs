namespace FirstOOPProject;

public abstract class Truck(string name, string color, string vin, int year)
    : Car(name, color, vin, year),ICar
{
    public override string Brand => "Truck";

    public override double GetParkingFee() => 250;

    protected override void Display()
    {
        base.Display();
        Console.WriteLine($"Тип: Грузовой автомобиль, Стоимость парковки: {GetParkingFee()} руб./час");
    }
}