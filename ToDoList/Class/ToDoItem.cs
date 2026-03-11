using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Class
{
    public class ToDoItem
    {
        public int ID { get; set; }
        public string Categoria { get; set; }
        public string Nota { get; set; }
        public string Stato { get; set; }
        public DateTime DataInserimento { get; set; }
        public DateTime? DataScadenza { get; set; }
        public bool Fatto { get; set; }
    }
}
