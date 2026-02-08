namespace FirstOOPProject;

public class Lada(string name,string color,string vin,int year):PassangerCar(name,color,vin,year)
{
    private new const string Brand = "Lada";
    public override double GetParkingFee() => 0;
    public override void Display()
    {
        Console.WriteLine($"Вы добавили машину {Brand}:\"{Name}\",{Color} цвета, {Year} года!");
    }
}