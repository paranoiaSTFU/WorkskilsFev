using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2501_Kon_Vol.DB
{
    public partial class Content
    {
        public string Tarif_view { get { return $"Тариф: {Tarif1.Tarif_list}"; } }
        public string Operator_view { get { return $"Оператор: {Operator}"; } }
        public string Cena_view
        {
            get { return $"Цена: {Cena}"; }
        }
    }
}
