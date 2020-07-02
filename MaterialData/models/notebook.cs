using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialData.models
{
    public class notebook
    {
        public int id { get; set; }
        public string serial_number { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int? location_id { get; set; }
        public int? person_id { get; set; }        
        public person people { get; set; }        
    }
}
