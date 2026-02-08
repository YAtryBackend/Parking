namespace FirstOOPProject.Trucks;

public class Камаз(string name,string color,string vin,int year):Truck(name,color,vin,year)
{
    private new const string Brand = "Камаз";

    protected override void Display()
    {
        Console.WriteLine($"Вы добавили машину {Brand}:\"{Name}\",{Color} цвета, {Year} года!");
    }
}