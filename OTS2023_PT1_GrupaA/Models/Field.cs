

namespace OTS2026_PT1_GrupaA.Models
{

    public enum Zone
    {
        Land,
        Pond,
        Invalid
    }

    public enum FieldContent
    {
        Empty,
        Bait,
        Fish,
        Boat
    }

    public class Field
    {
        public FieldContent Content { get; set; }
        public Zone Zone { get; set; }

        public Field(Zone zone)
        {
            Zone = zone;
            Content = FieldContent.Empty;
        }

        public Field(Zone zone, FieldContent content)
        {
            Zone = zone;
            Content = content;
        }
    }        
}
