using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialData.models
{
    [Table("maus_keyboard")]
    public class mouseKeyboard : IMaterial
    {
        public int id { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int location_id { get; set; }
        public int quantity { get; set; }
        public classroom classroom { get; set; }
    }
}
