namespace IIoT.Station.Pages;
public partial class Counter
{
    int currentCount = 0;
    void IncrementCount()
    {
        var ll = Turbo;
        var hh = Hosta;
        currentCount++;
    }
}