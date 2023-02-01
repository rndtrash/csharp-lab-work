using ReactiveUI;

namespace Laba16.Models;

public class MainWindowModel : ReactiveObject
{
    private double a;
    private double b;

    public double A
    {
        get => a;
        set => this.RaiseAndSetIfChanged(ref a, value);
    }
    
    public double B
    {
        get => b;
        set => this.RaiseAndSetIfChanged(ref b, value);
    }
}