using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Class
{
    public class InfoeEstesa
    {
        public int ID { get; set; }
        public string Categoria { get; set; }
        public string Titolo { get; set; }
        [Browsable (false)]
        public string Testo { get; set; }

   
    }
}
