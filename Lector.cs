using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAentregar1
{
    internal class Lector
    {
        private int dni;
        private string nombre;
        private List<Libro> librosPrestados;


        public Lector(string nombre, int dni)
        {
            this.nombre = nombre;
            this.dni = dni;
            this.librosPrestados = new List<Libro>();
        }

        public int getDni() { return this.dni; }
        public string getNombre() { return this.nombre; }

        public bool puedePedirPrestamo()
        {
            return librosPrestados.Count < 3;
        }

        public void agregarLibroPrestado(Libro libro)
        {
            this.librosPrestados.Add(libro);
        }

        public Libro buscarLibroPrestado(string titulo)
        {
            foreach (Libro libro in librosPrestados)
            {
                if (libro.getTitulo().Equals(titulo))
                    return libro;
            }
            return null;
        }

        public void removerLibroPrestado(Libro libro)
        {
            librosPrestados.Remove(libro);
        }

        public List<Libro> getLibrosPrestados()
        {
            return librosPrestados;
        }
    }
}
