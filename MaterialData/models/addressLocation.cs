using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.models
{
    [Table("adresslocation")]
    public class addressLocation
    {
        public int id { get; set; }
        public int adressId { get; set; }

        //classroom id
        public int locationId { get; set; }

        public classroom classroom { get; set; }
        public address address { get; set; }
    }
}
