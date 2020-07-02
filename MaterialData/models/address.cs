using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.models
{
    public class address
    {
        public int id { get; set; }
        public string street { get; set; }
        public string place { get; set; }
        public int zip { get; set; }
        public string country { get; set; }

        //public addressLocation addressloc { get; set; }
    }
}
