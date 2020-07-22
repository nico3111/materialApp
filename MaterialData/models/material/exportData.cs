namespace MaterialData.models.material
{
    public class exportData : Material
    {
        public string isbn { get; set; }
        public string title { get; set; }
        public int? location_id { get; set; }
        public int? person_id { get; set; }
        public int? quantity { get; set; }
        public string serial_number { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string type { get; set; }
        public classroom classroom { get; set; }
        public person person { get; set; }
    }
}
