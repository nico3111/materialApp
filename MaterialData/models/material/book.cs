namespace MaterialData.models
{
    public class book : Material
    {
        public string isbn { get; set; }
        public string title { get; set; }
        public int? location_id { get; set; }
        public int? person_id { get; set; }
        public int? quantity { get; set; }
        public classroom classroom { get; set; }
        public person person { get; set; }
    }
}