using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.models
{
    public class display : Material
    {
        public string serial_number { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int location_id { get; set; }
        public classroom classroom { get; set; }
    }
}
