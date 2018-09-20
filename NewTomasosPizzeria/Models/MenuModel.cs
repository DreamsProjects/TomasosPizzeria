using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewTomasosPizzeria.Models
{
    public class MenuModel
    {
        public string DishName { get; set; }
        public List<Matratt> Dishes { get; set; }
        public List<Produkt> Ingredients { get; set; }

        public List<MatrattProdukt> DishConnection { get; set; }

        public List<MatrattTyp> Types { get; set; }
        public List<Bestallning> Orders { get; set; }
        public List<BestallningMatratt> OrderFood { get; set; }
        public List<Kund> CustomerList { get; set; }

        public Kund Customer { get; set; }
    }
}
