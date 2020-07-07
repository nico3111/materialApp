using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialData.models
{
    [Table("classrooms")]
    public class classroom
    {
        public int id { get; set; }
        public string room { get; set; }

        public addressLocation addressloc { get; set; }
    }
}