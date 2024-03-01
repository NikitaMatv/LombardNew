using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPP
{
    public partial class User
    {
        public override string ToString()
        {
            return login + " " + password; 
        }
    }

    public partial class Dogovor
    {
        public override string ToString()
        {
            return Convert.ToDateTime(date_dogovor).ToShortDateString() + " -> " + Convert.ToDateTime(date_srokzaloga).ToShortDateString() + procent;
        }
    }

    public partial class Client
    {
        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }

    public partial class Sotrudnik
    {
        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }

    public partial class Tovar
    {
        public override string ToString()
        {
            return Name_Tovar + " " + price + " " + status;
        }
    }

    public partial class Category
    {
        public override string ToString()
        {
            return Name_category;
        }
    }
}
