using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;



namespace Workspace
{
    class Programa
    {

        static void CheckJson()
        {
            //Reconocer el JSON//
            var JsonInicial = File.ReadAllText(@"storage.json");
            var Objeto = JObject.Parse(JsonInicial);
            var JsonToOutput = JsonConvert.SerializeObject(Objeto, Formatting.Indented);
            Console.Clear();
            Console.WriteLine(JsonToOutput);
            Main();
        }

        static void ManagerJson(bool R,string Nombre, int Numero)
        {

            //Reconocer el JSON//
            var JsonInicial = File.ReadAllText(@"storage.json");
            var Objeto = JObject.Parse(JsonInicial);
            JObject ParamObjeto = (JObject)Objeto.SelectToken("Personas[0]");

            //Ver Respuesta//
            if (R == true){


                var itemToAdd = new JObject();
                itemToAdd["ID"] = Numero;
                try {
                    ParamObjeto.Add(Nombre, itemToAdd);
                } catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ese nombre ya esta en uso.");
                    Console.ResetColor();
                    Main();
                }


            } else if (R == false)


            {
                try {
                    ParamObjeto.Property(Nombre).Remove();
                }catch{
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No se reconoce ese nombre.");
                    Console.ResetColor();
                    Main();
                }
                
            }

            //Hacer cambios y sobreescribir el archivo//
            var JsonToOutput = JsonConvert.SerializeObject(Objeto, Formatting.Indented);
            File.WriteAllText(@"storage.json", JsonToOutput);
            Console.Clear();
            Main();
        }

        static void Main()
        {
            //Main//
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("////////////Json Manager//////////////");
            Console.ResetColor();
            Console.Write("¿Desea Agregar(A), Remover(R), Ver JSON(V) o Salir(E)? : ");
            string Respuesta = Console.ReadLine();

            //Ver Respuesta//
            if (Respuesta == "A" || Respuesta == "a")
            {

                Console.Write("Nombre : ");
                string Nombre = Console.ReadLine();
                int i;
                bool Valido = false;
                do {
                    Console.Write("Numero : ");
                    string input = Console.ReadLine();
                    Valido = int.TryParse(input, out i);
                } while(! Valido);
                ManagerJson(true, Nombre, i);

            } else if (Respuesta == "R" || Respuesta == "r") {

                Console.Write("Nombre de la Persona a remover : ");
                string Respuesta2 = Console.ReadLine();
                ManagerJson(false, Respuesta2, 0);         

            } else if (Respuesta == "V" || Respuesta == "v")
            {

                CheckJson();

            } else {

                Console.Clear();
                System.Environment.Exit(0);

            }

        }

    }
}
