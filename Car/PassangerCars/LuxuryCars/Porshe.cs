namespace FirstOOPProject.LuxuryCar;

public class Porshe(string name,string color,string vin, int year):LuxuryCars(name,color,vin,year)
{
    private new const string Brand = "Porshe";

    public override void Display()
    {
        Console.WriteLine($"Вы добавили машину {Brand}:\"{Name}\",{Color} цвета, {Year} года!");
    }
}