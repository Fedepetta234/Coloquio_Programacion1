using System;
using System.Globalization;
using System.IO;

class Tp3
{
    static void MetodoBurbuja(string[] v)
    {
        int n = v.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool ordenado = true;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (string.Compare(v[j], v[j + 1]) > 0)
                {
                    ordenado = false;
                    string temp = v[j];
                    v[j] = v[j + 1];
                    v[j + 1] = temp;
                }
            }
            if (ordenado)
            {
                break;
            }
        }
    }
    static void Main()
    {
        // Abre y guarda el archivo en una matriz.

        string rutaArchivo = @"C:\Users\feder\source\repos\Coloquio_Programacion\TrabajoPractico3\f1_last5years.csv";
        string[] lineas = File.ReadAllLines(rutaArchivo);

        int filas = lineas.Length;
        int cols = lineas[0].Split(',').Length;

        string[,] datos = new string[filas, cols];

        for (int i = 0; i < filas; i++)
        {
            string[] values = lineas[i].Split(',');
            for (int j = 0; j < cols; j++)
            {
                datos[i, j] = values[j];
            }
        }

        int op;

        Console.WriteLine("\t\tBienvenido a la Fromula 1!!!");

        do
        {
            // Menu de acciones.

            Console.WriteLine("");
            Console.WriteLine("¿Que desea hacer?");
            Console.WriteLine("");
            Console.WriteLine("1-Buscar piloto.");
            Console.WriteLine("2-Resultados campeonato.");
            Console.WriteLine("3-Ver mayor remontada.");
            Console.WriteLine("4-Mostrar equipos");
            Console.WriteLine("5-Mostrar datos");
            Console.WriteLine("6-Mostrar promedio de piloto");
            Console.WriteLine("7-Salir.");
            Console.WriteLine("");
            Console.Write("Ingrese una opcion: ");
            op = int.Parse(Console.ReadLine());

            Console.WriteLine("");
            switch (op)
            {
                case 1:
                    BuscarPiloto(datos);
                    break;
                case 2:
                    Campeonato(datos);
                    break;
                case 3:
                    Remontada(datos);
                    break;
                case 4:
                    OrdenEquipo(datos);
                    break;
                case 5:
                    MostrarDatos(datos);
                    break;
                case 6:
                    PromedioPiloto(datos);
                    break;
                case 7:
                    Console.WriteLine("");
                    Console.WriteLine("Gracias. Hasta la proxima");
                    break;
                default:
                    Console.WriteLine("");
                    Console.WriteLine("Ingrese una opcion valida.");
                    break;
            }
        } while (op != 7);

    }
    static void BuscarPiloto(string[,] datos)
    {
        Console.WriteLine("");
        Console.Write("Ingrese el nombre del piloto (nombre y apellido): ");
        string piloto = Console.ReadLine();
        int podios = 0;

        for (int i = 1; i < datos.GetLength(0); i++)
        {
            string nombre = datos[i, 2]; // Guardamos el dato de cada fila de la columna 2
            int posLlegada = int.Parse(datos[i, 5]);

            if (nombre.Contains(piloto, StringComparison.OrdinalIgnoreCase) && // Funcion que ignora las mayusculas y minusculas.
                (posLlegada == 1 || posLlegada == 2 || posLlegada == 3))
            {
                podios++;
            }
        }

        Console.WriteLine("");
        Console.WriteLine($"El piloto {piloto} tuvo {podios} podios.");
        Console.WriteLine("");
    }

    static void Campeonato(string[,] datos)
    {
        Console.WriteLine("");
        Console.Write("Ingrese el año del campeonato: ");
        string anio = Console.ReadLine();
        Console.WriteLine("");
        Console.Write("Ingrese el equipo que desea mostrar: ");
        string equipo = Console.ReadLine();
        double puntosTotales = 0;

        for (int i = 1; i < datos.GetLength(0); i++)
        {
            string year = datos[i, 0];
            string team = datos[i, 1];
            double puntos = double.Parse(datos[i, 6], CultureInfo.InvariantCulture); // Esta funcion la usamos para arreglar los decimales.
            if (year == anio && team.Contains(equipo, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("");
                Console.WriteLine($"Puntos carrera {datos[i, 3]}, piloto {datos[i, 2]}: {puntos.ToString("F1")}");
                puntosTotales += puntos;
            }
        }
        Console.WriteLine("");
        Console.WriteLine($"Puntos totales de la temporada {anio}: {puntosTotales.ToString("F1")}"); // Traduccion a tipo string.
        Console.WriteLine("");
    }

    static void Remontada(string[,] datos)
    {
        Console.WriteLine("");
        int diferencia = 0;
        int mayDiferencia = 0;
        string nombre = "";
        string equipo = "";
        string temporada = "";
        string carrera = "";

        for (int i = 0; i < datos.GetLength(0); i++)
        {

            int posClasi = int.Parse(datos[i, 4]);
            int posFinal = int.Parse(datos[i, 5]);

            diferencia = posClasi - posFinal;

            if (diferencia > 0 && diferencia > mayDiferencia)
            {
                nombre = datos[i, 2];
                equipo = datos[i, 1];
                temporada = datos[i, 0];
                carrera = datos[i, 3];
                mayDiferencia = diferencia;
            }
        }
        Console.WriteLine($"Mayor remontada: {mayDiferencia} - Piloto: {nombre} - Temporada: {temporada} - Carrera: {carrera} - Equipo: {equipo}");
        Console.WriteLine("");
    }

    static void OrdenEquipo(string[,] datos)
    {
        Console.WriteLine("");
        Console.WriteLine("Equipos: ");
        List<String> equipo = new List<String>();
        Console.WriteLine("");

        for (int i = 0; i < datos.GetLength(0); i++)
        {
            string equipos = datos[i, 1];
            if (!equipo.Contains(equipos))
            {
                equipo.Add(equipos);
            }

        }
        string[] equiposArreglo = equipo.ToArray(); // Transformacion y guardado de la lista en un array.
        MetodoBurbuja(equiposArreglo);
        foreach (var nombre in equiposArreglo)
        {
            Console.WriteLine($"-{nombre}");
        }
        Console.WriteLine("");
    }

    static void MostrarDatos(string[,] datos)
    {
        Console.WriteLine("");

        for (int i = 0; i < datos.GetLength(0); i++)
        {
            string temporada = datos[i, 0];
            string equipo = datos[i, 1];
            string nombre = datos[i, 2];
            string carrera = datos[i, 3];
            string posClasi = datos[i, 4];
            string posFinal = datos[i, 5];
            string puntos = datos[i, 6];


            Console.WriteLine($"Temporada: {temporada} -Equipo: {equipo} -Nombre del piloto: {nombre} -Carrera: {carrera} -Clasificacion: {posClasi} -Posicion final: {posFinal} -Puntos: {puntos}");
            Console.WriteLine("");
        }
    }

    // NUEVO INDICADOR - Promedio de posición final por piloto
    static void PromedioPiloto(string[,] datos)
    {
        Console.Write("Ingrese piloto: ");
        string piloto = Console.ReadLine();

        int suma = 0;
        int cantidad = 0;

        for (int i = 1; i < datos.GetLength(0); i++)
        {
            if (datos[i, 2].Contains(piloto, StringComparison.OrdinalIgnoreCase))
            {
                suma += int.Parse(datos[i, 5]);
                cantidad++;
            }
        }

        if (cantidad == 0)
        {
            Console.WriteLine("No se encontraron datos del piloto.");
            return;
        }

        double promedio = (double)suma / cantidad;
        Console.WriteLine($"Promedio de posición final de {piloto}: {promedio:F2}");
    }
}

