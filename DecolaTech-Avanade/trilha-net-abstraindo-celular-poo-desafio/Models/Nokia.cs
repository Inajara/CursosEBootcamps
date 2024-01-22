namespace DesafioPOO.Models
{
    // TODO: Herdar da classe "Smartphone"
    public class Nokia : Smartphone
    {
        // TODO: Sobrescrever o método "InstalarAplicativo"
        public Nokia (String numero, String modelo, String imei, int memoria) : base (numero, modelo,imei,memoria)
        {

        }
        public override void  InstalarAplicativo(string Instragran)
        {
            Console.WriteLine($"Instalar Aplicativo {Instragran} no Nokia" );
        }
        
        
    

    }
}