using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialData.models
{
    [Table("adresslocation")]
    public class addressLocation
    {
        public int id { get; set; }
        public int adressId { get; set; }

        public int locationId { get; set; }

        public classroom classroom { get; set; }
        public address address { get; set; }
    }
}