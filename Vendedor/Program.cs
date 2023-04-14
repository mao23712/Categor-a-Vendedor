using System;
using System.Collections.Generic;
using System.Linq;

class Vendedor
{
    private string nombre;
    private int cantidad;
    private double monto;

    public Vendedor(string nombre, int cantidad, double monto)
    {
        this.Nombre = nombre;
        this.Cantidad = cantidad;
        this.Monto = monto;
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
    public double Monto { get => monto; set => monto = value; }
}

class CategoriaVendedor
{
    public List<Vendedor> vendedores;

    public CategoriaVendedor(List<Vendedor> vendedores)
    {
        this.vendedores = vendedores;
    }
    //parallelfor and parallelfore task hilo
    public double PromedioVentas()
    {
        return vendedores.Average(v => v.Monto); 
    }
    public List<Vendedor> VendedoresCategoriaA()
    {
        double promedio = PromedioVentas();
        double minimo = promedio + 5;
        return vendedores.Where(v => v.Monto > minimo).ToList();
    }

    public List<Vendedor> VendedoresCategoriaB()
    {
        double promedio = PromedioVentas();
        double minimo = promedio - 5;
        return vendedores.Where(v => v.Monto < minimo).ToList();
    }
    public List<Vendedor> VendedoresCategoriaC()
    {
        double promedio = PromedioVentas();
        double minimo = promedio - 5;
        double maximo = promedio + 5;
        return vendedores.Where(v => v.Monto >= minimo && v.Monto <= maximo).ToList();
    }
    public List<Vendedor> TopVendedoresCategoriaA(int cantidad)
    {
        return VendedoresCategoriaA().OrderByDescending(v => v.Monto).Take(cantidad).ToList();
    }

    public List<Vendedor> TopVendedoresCategoriaB(int cantidad)
    {
        return VendedoresCategoriaB().OrderByDescending(v => v.Monto).Take(cantidad).ToList();
    }
}


class Program
{
    static void Main(string[] args)
    {
        List<Vendedor> vendedores = new List<Vendedor>();
        Random random = new Random();
        CrearLista();
        int opcion;
        do
       {
            Console.Clear();
            Console.WriteLine("=========           === MENÚ ===         =========");
            Console.WriteLine("=== 1. Mostrar todos los vendedores            ===");
            Console.WriteLine("=== 2. Mostrar Top vendedores de categoría A   ===");
            Console.WriteLine("=== 3. Mostrar Top vendedores de categoría B   ===");
            Console.WriteLine("=== 4. Mostrar vendedores de categoría A       ===");
            Console.WriteLine("=== 5. Mostrar vendedores de categoría B       ===");
            Console.WriteLine("=== 6. Mostrar vendedores de categoría C       ===");
            Console.WriteLine("=== 0. Salir                                   ===");
            Console.WriteLine("=========                                =========");
            Console.Write("Ingrese una opción: ");
            opcion = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    MostarVendedores();
                    break;
                case 2:
                    TopCategoriaA();
                    break;
                case 3:
                    TopCategoriaB();
                    break;
                case 4:
                    MostrarCatA();
                    break;
                case 5:
                    MostrarCatB();
                    break;
                case 6:
                    MostrarCatC();
                    break;
                case 0:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
            Console.WriteLine();
        } while (opcion != 0);
        void CrearLista()
        {
            string[] nombres = { "Marcos", "Juan", "Ana", "Pedro", "María", "Lucía", "Carlos", "Luisa", "Javier", "Lorena", "Antonio", "Carmen", "Elena", "Miguel", "Sofía", "Jorge", "Claudia", "Roberto", "Laura", "José", "Isabel", "Ricardo", "Patricia", "Fernando", "Diana", "Diego", "Natalia", "Pablo", "Verónica", "Gustavo", "Silvia", "Raúl", "Adriana", "Emilio", "Beatriz", "Rafael", "Julia", "Alberto", "Cristina", "Mario", "Susana", "Sergio", "Gloria", "Esteban", "Alicia", "Alejandro", "Rosa", "Gabriel", "Victoria" };
            int[] cantidadVentas = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 26, 80 };
            double[] monto = { 50.00, 78.00, 67.00, 56.00, 51.00, 101.00, 34.00, 69.00, 45.00 };
            Dictionary<string, bool> nombresUtilizados = new Dictionary<string, bool>();
            for (int i = 0; i < 30; i++)
            {
                string nombre;
                do
                {
                    int x = random.Next(nombres.Length);
                    nombre = nombres[x];
                } while (nombresUtilizados.ContainsKey(nombre));

                nombresUtilizados[nombre] = true;

                int cantidad = cantidadVentas[random.Next(cantidadVentas.Length)];
                double total = monto[random.Next(monto.Length)];
                Vendedor vendedor = new Vendedor(nombre, cantidad, total);
                vendedores.Add(vendedor);
            }
        }
        void MostarVendedores()
        {
            Console.Clear();
            Console.WriteLine("          === Todos los vendedores ===          ");
            Console.WriteLine("{0,-15}{1,-20}{2,-15}", "Nombre", "Cantidad de ventas", "Total vendido");
            Console.WriteLine("".PadLeft(50, '-'));
            foreach (Vendedor vendedor in vendedores)
            {
                int padding = (20 - vendedor.Cantidad.ToString().Length) / 2;
                Console.WriteLine("{0,-15}{1}{2,-15}{3,-10}", vendedor.Nombre, "".PadLeft(padding), vendedor.Cantidad, vendedor.Monto.ToString("F2") + " Bs");
                Console.WriteLine("".PadLeft(50, '-'));
            }
            Console.ReadLine();
        }
        void TopCategoriaA()
        {
            Console.Clear();
            CategoriaVendedor categoriaVendedorA = new CategoriaVendedor(vendedores);
            double promedio = categoriaVendedorA.PromedioVentas();
            Console.WriteLine("          === Vendedores Top Categoría A ===          ");
            Console.WriteLine($"Promedio de Ventas: {promedio:F2}");
            Console.WriteLine("{0,-15}{1,-20}{2,-15}", "Nombre", "Cantidad de ventas", "Total vendido");
            Console.WriteLine("".PadLeft(50, '-'));
            foreach (Vendedor vendedor in categoriaVendedorA.TopVendedoresCategoriaA(3))
            {
                int padding = (20 - vendedor.Cantidad.ToString().Length) / 2;
                Console.WriteLine("{0,-15}{1}{2,-15}{3,-10}", vendedor.Nombre, "".PadLeft(padding), vendedor.Cantidad, vendedor.Monto.ToString("F2") + " Bs");
            }
            Console.ReadLine();
        }
        void TopCategoriaB()
        {
            Console.Clear();
            CategoriaVendedor categoriaVendedorB = new CategoriaVendedor(vendedores);
            double promedio = categoriaVendedorB.PromedioVentas();
            Console.WriteLine("          === Vendedores Top Categoría B ===          ");
            Console.WriteLine($"Promedio de Ventas: {promedio:F2}");
            Console.WriteLine("{0,-15}{1,-20}{2,-15}", "Nombre", "Cantidad de ventas", "Total vendido");
            Console.WriteLine("".PadLeft(50, '-'));
            foreach (Vendedor vendedor in categoriaVendedorB.TopVendedoresCategoriaB(3))
            {
                int padding = (20 - vendedor.Cantidad.ToString().Length) / 2;
                Console.WriteLine("{0,-15}{1}{2,-15}{3,-10}", vendedor.Nombre, "".PadLeft(padding), vendedor.Cantidad, vendedor.Monto.ToString("F2") + " Bs");
            }
            Console.ReadLine();
        }
        void MostrarCatA()
        {
            Console.Clear();
            CategoriaVendedor categoriaA = new CategoriaVendedor(vendedores);
            double promedio = categoriaA.PromedioVentas();
            Console.WriteLine("          === Vendedores Categoria A ===          ");
            Console.WriteLine($"Promedio de Ventas: {promedio:F2}  [Limite Categoria A: {promedio + 5:F2}]");
            Console.WriteLine("{0,-15}{1,-20}{2,-15}", "Nombre", "Cantidad de ventas", "Total vendido");
            Console.WriteLine("".PadLeft(50, '-'));
            foreach (Vendedor vendedor in categoriaA.VendedoresCategoriaA())
            {
                int padding = (20 - vendedor.Cantidad.ToString().Length) / 2;
                Console.WriteLine("{0,-15}{1}{2,-15}{3,-10}", vendedor.Nombre, "".PadLeft(padding), vendedor.Cantidad, vendedor.Monto.ToString("F2") + " Bs");
            }
            Console.ReadLine();
        }
        void MostrarCatB()
        {
            Console.Clear();
            CategoriaVendedor categoriaB = new CategoriaVendedor(vendedores);
            double promedio = categoriaB.PromedioVentas();
            Console.WriteLine("          === Vendedores Categoria B ===          ");
            Console.WriteLine($"Promedio de Ventas: {promedio:F2}  [Limite Categoria B: {promedio - 5:F2}]");
            Console.WriteLine("{0,-15}{1,-20}{2,-15}", "Nombre", "Cantidad de ventas", "Total vendido");
            Console.WriteLine("".PadLeft(50, '-'));
            foreach (Vendedor vendedor in categoriaB.VendedoresCategoriaB())
            {
                int padding = (20 - vendedor.Cantidad.ToString().Length) / 2;
                Console.WriteLine("{0,-15}{1}{2,-15}{3,-10}", vendedor.Nombre, "".PadLeft(padding), vendedor.Cantidad, vendedor.Monto.ToString("F2") + " Bs");
            }
            Console.ReadLine();
        }
        void MostrarCatC()
        {
            Console.Clear();
            CategoriaVendedor categoriaVendedorC = new CategoriaVendedor(vendedores);
            double promedio = categoriaVendedorC.PromedioVentas();
            Console.WriteLine("          === Vendedores Categoría C ===          ");
            Console.WriteLine($"Promedio de Ventas: {promedio:F2}  [ Minimo = {promedio-5:F2} - Maximo = {promedio+5:F2} ]");
            Console.WriteLine("{0,-15}{1,-20}{2,-15}", "Nombre", "Cantidad de ventas", "Total vendido");
            Console.WriteLine("".PadLeft(50, '-'));
            foreach (Vendedor vendedor in categoriaVendedorC.VendedoresCategoriaC())
            {
                int padding = (20 - vendedor.Cantidad.ToString().Length) / 2;
                Console.WriteLine("{0,-15}{1}{2,-15}{3,-10}", vendedor.Nombre, "".PadLeft(padding), vendedor.Cantidad, vendedor.Monto.ToString("F2") + " Bs");
            }
            Console.ReadLine();
        }
    }
}

