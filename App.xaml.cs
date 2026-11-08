using Microsoft.Extensions.DependencyInjection;

namespace Calculadora_IMC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Envolvemos MainPage en un NavigationPage para habilitar el PushAsync
            return new Window(new NavigationPage(new MainPage()));
        }
    }
}