namespace MaterialData.models
{
    public class equipment : Material
    {
        public string type { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int? location_id { get; set; }
        public int? person_id { get; set; }
        public int? quantity { get; set; }
        public classroom classroom { get; set; }
        public person person { get; set; }
    }
}