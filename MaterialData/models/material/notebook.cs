namespace MaterialData.models
{
    public class notebook : IMaterial
    {
        public int id { get; set; }
        public string serial_number { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int? location_id { get; set; }
        public classroom classroom { get; set; }
        public int? person_id { get; set; }        
        public person person { get; set; }
    }
}
