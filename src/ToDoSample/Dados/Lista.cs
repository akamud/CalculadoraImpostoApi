using System.Collections.Generic;

namespace ToDoSample.Dados
{
    public class Lista
    {
        public int ListaId { get; set; }
        public string Titulo { get; set; }

        public ICollection<ToDo> ToDos { get; set; } = new HashSet<ToDo>();
    }
}