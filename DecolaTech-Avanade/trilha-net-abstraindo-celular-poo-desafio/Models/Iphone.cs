namespace DesafioPOO.Models
{
    // TODO: Herdar da classe "Smartphone"
    public class Iphone : Smartphone
    {
        // TODO: Sobrescrever o método "InstalarAplicativo"
         public Iphone (String numero, String modelo, String imei, int memoria) : base (numero, modelo,imei,memoria)
        {

        }
        public override void  InstalarAplicativo(string facebook)
        {
            Console.WriteLine($"Instalar Aplicativo {facebook} no Iphone" );
        }
        
    }
}