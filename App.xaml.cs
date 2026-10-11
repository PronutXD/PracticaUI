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
            return new Window(new CalculadoraIMC());
        }
    }
}