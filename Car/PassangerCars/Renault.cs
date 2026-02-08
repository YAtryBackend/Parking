namespace FirstOOPProject;

public class Renault(string name,string color,string vin,int year):PassangerCar(name,color,vin,year)
{
    private new const string Brand = "Renault";

    protected override void Display()
    {
        Console.WriteLine($"Вы добавили машину {Brand}:\"{Name}\",{Color} цвета, {Year} года!");
    }
}