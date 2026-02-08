namespace FirstOOPProject;

public abstract class PassangerCar(string name, string color, string vin, int year)
    : Car(name, color, vin, year),ICar
{
    public override string Brand => "Passenger Car";

    public override double GetParkingFee() => 150;

    protected override void Display()
    {
        base.Display();
        Console.WriteLine($"Тип: Легковой автомобиль, Стоимость парковки: {GetParkingFee()} руб./час");
    }
}