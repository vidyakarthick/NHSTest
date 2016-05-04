using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Model.Classes
{
    public class PersonColours
    {
       public Person Person { get; set; }
       public string Colours { get; set; }
    }

    public class PeopleViewModel
    {
        public List<PersonColours> PeopleList { get; set; }
    }

    public class PersonEditViewModel
    {
        public Person Person { get; set; }
        public List<Colour> Colours { get; set; }
    }



}