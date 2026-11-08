namespace Calculadora_IMC;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnIrIMCClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CalculadoraIMC());
    }

    private async void OnIrDivisasClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ConversorMoneda());
    }
}