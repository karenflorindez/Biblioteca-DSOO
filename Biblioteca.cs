using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAentregar1
{
    internal class Biblioteca
    {
        private List<Libro> libros;
        private List<Lector> lectores;

        public Biblioteca()
        {
            this.libros = new List<Libro>();
            this.lectores = new List<Lector>();
        }

        // Método para buscar libros por título (sin cambios, ya que se usa para otras funciones)
        private Libro buscarLibro(string titulo)
        {
            Libro libroBuscado = null;
            int i = 0;
            while ((i < libros.Count) && !(libros[i].getTitulo().Equals(titulo)))
                i++;
            if (i != libros.Count)
                libroBuscado = libros[i];
            return libroBuscado;
        }

        // NUEVO: Método para validar si un libro ya existe,
        // usando la combinación de título, autor y editorial, y convirtiendo todo a mayúsculas.
        private bool existeLibroRepetido(string titulo, string autor, string editorial)
        {
            string nuevaCombinacion = (titulo + autor + editorial).ToUpper();

            foreach (var libroExistente in libros)
            {
                string combinacionExistente = (libroExistente.getTitulo() + libroExistente.getAutor() + libroExistente.getEditorial()).ToUpper();
                if (nuevaCombinacion.Equals(combinacionExistente))
                {
                    return true;
                }
            }
            return false;
        }

        public bool agregarLibro(string titulo, string autor, string editorial)
        {
            // 1. Validar que no haya campos en blanco
            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(autor) || string.IsNullOrWhiteSpace(editorial))
            {
                return false;
            }

            // 2. Validar si el libro ya existe con la lógica de ToUpper()
            if (existeLibroRepetido(titulo, autor, editorial))
            {
                return false;
            }

            Libro nuevoLibro = new Libro(titulo, autor, editorial);
            libros.Add(nuevoLibro);
            return true;
        }

        public void listarLibros()
        {
            foreach (var libro in libros)
                Console.WriteLine(libro);
        }

        public bool eliminarLibro(string titulo)
        {
            Libro libro = buscarLibro(titulo);
            if (libro != null)
            {
                libros.Remove(libro);
                return true;
            }
            return false;
        }

        private Lector buscarLector(int dni)
        {
            foreach (Lector lector in lectores)
            {
                if (lector.getDni() == dni)
                    return lector;
            }
            return null;
        }

        public bool altaLector(String nombre, int dni)
        {
            if (buscarLector(dni) == null)
            {
                Lector nuevoLector = new Lector(nombre, dni);
                lectores.Add(nuevoLector);
                return true;
            }
            return false;
        }

        public string prestarLibro(string titulo, int dniLector)
        {
            Lector lector = buscarLector(dniLector);
            if (lector == null)
                return "LECTOR INEXISTENTE";

            Libro libro = buscarLibro(titulo);
            if (libro == null)
                return "LIBRO INEXISTENTE";

            if (lector.puedePedirPrestamo() == false)
                return "TOPE DE PRESTAMO ALCAZADO";

            libros.Remove(libro);
            lector.agregarLibroPrestado(libro);

            return "PRESTAMO EXITOSO";
        }

        public string devolverLibro(string titulo, int dniLector)
        {
            Lector lector = buscarLector(dniLector);
            if (lector == null)
                return "LECTOR INEXISTENTE";

            Libro libro = lector.buscarLibroPrestado(titulo);
            if (libro == null)
                return "LIBRO NO PRESTADO A ESTE LECTOR";

            lector.removerLibroPrestado(libro);
            libros.Add(libro);
            return "DEVOLUCIÓN EXITOSA";
        }

        public void CargarDatosIniciales()
        {
            this.agregarLibro("El Quijote", "Miguel de Cervantes", "Planeta");
            this.agregarLibro("Cien Años de Soledad", "Gabriel García Márquez", "Sudamericana");
            this.agregarLibro("1984", "George Orwell", "Seix Barral");
            this.agregarLibro("El Principito", "Antoine de Saint-Exupéry", "Salamandra");

            this.altaLector("Juan Pérez", 12345678);
            this.altaLector("María García", 87654321);
            this.altaLector("Carlos López", 11223344);
        }


        public void listarLibrosPrestadosPorLector(int dniLector)
        {
            Lector lector = buscarLector(dniLector);

            if (lector == null)
            {
                Console.WriteLine("❌ Lector no encontrado. Por favor, verifica el DNI.");
                return;
            }

            List<Libro> librosPrestados = lector.getLibrosPrestados();

            if (librosPrestados.Count == 0)
            {
                Console.WriteLine($"📖 El lector {lector.getNombre()} no tiene libros prestados en este momento.");
            }
            else
            {
                Console.WriteLine($"📖 Libros prestados a {lector.getNombre()}:");
                foreach (var libro in librosPrestados)
                {
                    Console.WriteLine(libro);
                }
            }
        }

    }
}
