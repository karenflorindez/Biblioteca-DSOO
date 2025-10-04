using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAentregar1
{
    internal class Libro
    {
        private string titulo, autor, editorial;

        public Libro(string titulo, string autor, string editorial)
        {
            this.titulo = titulo;
            this.autor = autor;
            this.editorial = editorial;
        }

        public string getTitulo() { return titulo; }

        public string getAutor() { return autor; }
        public string getEditorial() { return editorial; }

        public override string ToString()
        {
            return "Título: " + titulo + ". Autor: " + autor + ". Editorial: " + editorial + ".";
        }
    }
}
