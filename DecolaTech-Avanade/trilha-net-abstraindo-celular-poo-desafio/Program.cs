using DesafioPOO.Models;

// TODO: Realizar os testes com as classes Nokia e Iphone
Console.WriteLine("Smartphone Nokia");
Smartphone nokia = new Nokia (numero: "333333333", modelo:  "01", imei: "11111111111111111122", memoria:128);
nokia.Ligar();
nokia.InstalarAplicativo("Instragran");

Console.WriteLine("Smartfhone Iphone");
Smartphone ifhone = new Iphone (numero: "22222222", modelo: "02", imei: "111113333331111111122", memoria:254 );
ifhone.Ligar();
ifhone.InstalarAplicativo("facebook");

