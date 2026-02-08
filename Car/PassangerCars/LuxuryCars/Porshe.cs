namespace FirstOOPProject.LuxuryCars;

public class Porshe(string name,string color,string vin, int year):LuxuryCar(name,color,vin,year)
{
    private new const string Brand = "Porshe";

    protected override void Display()
    {
        Console.WriteLine($"Вы добавили машину {Brand}:\"{Name}\",{Color} цвета, {Year} года!");
    }
}