namespace Calculadora_IMC;

public partial class ConversorMoneda : ContentPage
{
    public ConversorMoneda()
    {
        InitializeComponent();
    }

    private void onConvertirCliked(object sender, EventArgs e)
    {
        //Validamos: monto numerico que se haya elegido en ambos picker
        //SelectedIndez vale -1 cuando el usuario todavia no ha selecionado nada

        if (!double.TryParse(txtMonto.Text, out double monto) ||
            pckOrigen.SelectedIndex == -1 || pckDestino.SelectedIndex == -1)
        {
            lblResultado.Text = " Seleccione las monedas e ingrese un monto.";
            return;
        }

        //Primer paso: pasamos todo a dolares y luego a ala moneda destino
        //Asi no nesecitamos una tasa para cada moneda
        double montoEnUSD = pckOrigen.SelectedItem?.ToString() switch
        {
            "HNL (Lempiras)" => monto / 24.75, // 24.75 Lempiras = 1 USD
            "EUR (Euros)" => monto * 1.08,  // 1 Euro = 1.08 USD
            "MXN (Pesos)" => monto / 18.22, // 18.22 Pesos = 1 USD
            _ => monto          // Si es USD (Dolares) o cualquier otro
        };

        //Segundo paso: de dolares a moneda destino
        double resultado = pckDestino.SelectedItem?.ToString() switch
        {
            "HNL (Lempiras)" => montoEnUSD * 24.75, // Convertir de USD a Lempiras
            "EUR (Euros)" => montoEnUSD / 1.08,  // Convertir de USD a Euros
            "MXN (Pesos)" => montoEnUSD * 18.22, // Convertir de USD a Pesos
            _ => montoEnUSD          // Si es USD (Dolares)
        };

        lblResultado.Text = $"Resultado: {resultado:F2}"; string simbolo = pckDestino.SelectedIndex.ToString() switch
        {
            "0" => "L",  // Lempira
            "1" => "$",  // Dolar
            "2" => "€",  // Euro
            "3" => "$",  // Peso mexicano
            _ => ""
        };


        //{resultado:F2} = mostar con 2 decimales
        lblResultado.Text = $"Resultado: {simbolo} {resultado:N2}";
    }
}