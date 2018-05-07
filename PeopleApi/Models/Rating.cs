using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleApi.Models
{
    public class Rating
    {
        public int PersonID { get; set; }
        public string UserID { get; set; }
        public int Rate { get; set; }
    }
}
