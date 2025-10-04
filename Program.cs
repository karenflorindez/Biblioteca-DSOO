using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAentregar1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE GESTIÓN DE BIBLIOTECA ===");
            Console.WriteLine();

            Biblioteca miBiblioteca = new Biblioteca();
            miBiblioteca.CargarDatosIniciales();

            Console.WriteLine("¿Cómo deseas probar el sistema?");
            Console.WriteLine("1. Demostración automática (con datos de prueba)");
            Console.WriteLine("2. Modo manual (vos elegis que hacer)");
            Console.Write("Elegí una opción (1 o 2): ");

            string opcion = Console.ReadLine();

            if (opcion == "1")
                DemostracionAutomatica(miBiblioteca);
            else
                ModoManual(miBiblioteca);
        }

        static void DemostracionAutomatica(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== DEMOSTRACIÓN AUTOMÁTICA ===");
            Console.WriteLine();

            Console.WriteLine("Datos iniciales cargados.");
            Console.WriteLine();

            Console.WriteLine("LIBROS DISPONIBLES EN LA BIBLIOTECA:");
            biblioteca.listarLibros();
            Console.WriteLine();

            Console.WriteLine("REALIZANDO PRÉSTAMOS:");
            Console.WriteLine("- " + biblioteca.prestarLibro("El Quijote", 12345678));
            Console.WriteLine("- " + biblioteca.prestarLibro("1984", 12345678));
            Console.WriteLine("- " + biblioteca.prestarLibro("Cien Años de Soledad", 87654321));
            Console.WriteLine();

            Console.WriteLine("PROBANDO CASOS DE ERROR:");
            Console.WriteLine("- Lector inexistente: " + biblioteca.prestarLibro("El Principito", 99999999));
            Console.WriteLine("- Libro inexistente: " + biblioteca.prestarLibro("Libro Fantasma", 12345678));
            Console.WriteLine("- Libro ya prestado: " + biblioteca.prestarLibro("El Quijote", 87654321));

            Console.WriteLine("- Tercer préstamo Juan: " + biblioteca.prestarLibro("El Principito", 12345678));

            Console.WriteLine("- Cuarto préstamo Juan: " + biblioteca.prestarLibro("Algún Libro", 12345678));
            Console.WriteLine();

            Console.WriteLine("LIBROS DISPONIBLES DESPUÉS DE LOS PRÉSTAMOS:");
            biblioteca.listarLibros();
            Console.WriteLine();

            Console.WriteLine("=== FIN DE LA DEMOSTRACIÓN AUTOMÁTICA ===");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void ModoManual(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== MODO MANUAL ===");
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("--- MENÚ PRINCIPAL ---");
                Console.WriteLine("1. Agregar libro");
                Console.WriteLine("2. Eliminar libro");
                Console.WriteLine("3. Listar libros disponibles");
                Console.WriteLine("4. Registrar lector");
                Console.WriteLine("5. Prestar libro");
                Console.WriteLine("6. Devolver libro");
                Console.WriteLine("7. Listar libros prestados de un lector");
                Console.WriteLine("8. Salir");
                Console.Write("Elige una opción (1-8): ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarLibro(biblioteca);
                        break;
                    case "2":
                        EliminarLibro(biblioteca);
                        break;
                    case "3":
                        Console.WriteLine(" LIBROS DISPONIBLES:");
                        biblioteca.listarLibros();
                        break;
                    case "4":
                        RegistrarLector(biblioteca);
                        break;
                    case "5":
                        PrestarLibro(biblioteca);
                        break;
                    case "6":
                        DevolverLibro(biblioteca);
                        break;
                    case "7":
                        Console.WriteLine("--- LISTAR LIBROS PRESTADOS ---");
                        Console.Write("DNI del lector: ");
                        if (int.TryParse(Console.ReadLine(), out int dni))
                            biblioteca.listarLibrosPrestadosPorLector(dni);
                        else
                            Console.WriteLine("❌ DNI inválido. Debe ser un número.");
                        break;
                    case "8":
                        continuar = false;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intenta de nuevo.");
                        break;
                }

                // Esta es la parte que corregimos.
                // Ahora se ejecuta para todas las opciones excepto "Salir" (opción 8).
                if (continuar)
                {
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void AgregarLibro(Biblioteca biblioteca)
        {
            Console.WriteLine("\n--- AGREGAR LIBRO ---");
            Console.Write("¿Cuántos libros querés agregar? ");

            if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
            {
                int librosAgregadosExitosamente = 0;

                for (int i = 0; i < cantidad; i++)
                {
                    Console.WriteLine($"Ingresa los datos para el libro #{i + 1}:");
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();
                    Console.Write("Editorial: ");
                    string editorial = Console.ReadLine();

                    if (biblioteca.agregarLibro(titulo, autor, editorial))
                    {
                        Console.WriteLine(" Libro agregado exitosamente!");
                        librosAgregadosExitosamente++;
                    }
                    else
                        Console.WriteLine("El libro ya existe, un dato estaba en blanco o hubo un error.");
                }

                Console.WriteLine($" Proceso completado. Se agregaron {librosAgregadosExitosamente} de {cantidad} libros.");
            }
            else
                Console.WriteLine("Entrada inválida. Debes ingresar un número entero positivo.");
        }

        static void EliminarLibro(Biblioteca biblioteca)
        {
            Console.WriteLine("--- ELIMINAR LIBRO ---");
            Console.Write("Título del libro a eliminar: ");
            string titulo = Console.ReadLine();

            if (biblioteca.eliminarLibro(titulo))
                Console.WriteLine("Libro eliminado exitosamente!");
            else
                Console.WriteLine("Libro no encontrado.");
        }

        static void RegistrarLector(Biblioteca biblioteca)
        {
            Console.WriteLine("--- REGISTRAR LECTOR ---");
            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();
            Console.Write("DNI (solo números): ");

            if (int.TryParse(Console.ReadLine(), out int dni))
            {
                if (biblioteca.altaLector(nombre, dni))
                    Console.WriteLine(" Lector registrado exitosamente!");
                else
                    Console.WriteLine(" El lector ya existe.");
            }
            else
                Console.WriteLine(" DNI inválido. Debe ser un número.");
        }

        static void PrestarLibro(Biblioteca biblioteca)
        {
            Console.WriteLine("--- PRESTAR LIBRO ---");
            Console.Write("Título del libro: ");
            string titulo = Console.ReadLine();
            Console.Write("DNI del lector: ");

            if (int.TryParse(Console.ReadLine(), out int dni))
            {
                string resultado = biblioteca.prestarLibro(titulo, dni);
                Console.WriteLine("📖 " + resultado);
            }
            else
                Console.WriteLine("❌ DNI inválido. Debe ser un número.");
        }

        static void DevolverLibro(Biblioteca biblioteca)
        {
            Console.WriteLine("--- DEVOLVER LIBRO ---");
            Console.Write("Título del libro a devolver: ");
            string titulo = Console.ReadLine();
            Console.Write("DNI del lector: ");

            if (int.TryParse(Console.ReadLine(), out int dni))
            {
                string resultado = biblioteca.devolverLibro(titulo, dni);
                Console.WriteLine("🔄 " + resultado);
            }
            else
                Console.WriteLine("❌ DNI inválido. Debe ser un número.");
        }
    }
}