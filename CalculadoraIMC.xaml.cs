namespace Calculadora_IMC;

public partial class CalculadoraIMC : ContentPage
{
	public CalculadoraIMC()
	{
		InitializeComponent();
	}

	//usamos async por que  DisplayAlert espera a que el usuario toque ok
    private async void Button_Clicked(object sender, EventArgs e)
    {
		//TryParse intenta convertir el texto a numero
		//Tambien validamos que la altura sea mayor que cero
		
		if(!double.TryParse(txtPeso.Text, out double peso) 
			|| !double.TryParse(txtAltura.Text, out double altura) || altura <= 0 || peso<=0)
		{
			await DisplayAlert("Error", "Por favor ingrese valores válidos para peso y altura.", "OK");
            return;
		}

        //Calculamos el IMC
        double imc = peso / (altura * altura);

		//Formato con 1 decimal
		lblValorIMC.Text = imc.ToString("F1");

        //Clasificamos el IMC
		
		if (imc <= 18.5)
		{
			lblDiacnostico.Text = "Bajo peso";
			lblDiacnostico.TextColor = Colors.DarkOrange;

            //Color.FromARGB convierte un codigo hexadecimal a un color
			brResulustado.BackgroundColor = Color.FromArgb("#0B8711");
			brResulustado.Stroke = Colors.Orange;
        }
        else if (imc <= 24.9)
        {
            lblDiacnostico.Text = "Normal";
            lblDiacnostico.TextColor = Colors.Green;
            brResulustado.BackgroundColor = Color.FromArgb("#0B8711");
            brResulustado.Stroke = Colors.Green;
        }else if (imc <= 29.9)
        {
            lblDiacnostico.Text = "Sobrepeso";
            lblDiacnostico.TextColor = Colors.Orange;
            brResulustado.BackgroundColor = Color.FromArgb("#0B8711");
            brResulustado.Stroke = Colors.Orange;
        }
        else
        {
            lblDiacnostico.Text = "Obesidad";
            lblDiacnostico.TextColor = Colors.Red;
            brResulustado.BackgroundColor = Color.FromArgb("#0B8711");
            brResulustado.Stroke = Colors.Red;

        }

        //Por Ultimo muestra la targeta que esta oculta
        brResulustado.IsVisible = true;
    }
}