namespace MaterialData.models
{
    public class furniture : IMaterial
    {
        public int id { get; set; }
        public string type { get; set; }
        public int quantity { get; set; }
        public int location_id { get; set; }
        public classroom classroom { get; set; }
    }

}
