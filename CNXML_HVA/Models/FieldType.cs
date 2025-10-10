using System;

namespace CNXML_HVA.Models
{
    public class FieldType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SizeDisplay { get; set; }
        public int PlayersPerTeam { get; set; }
        public int TotalCapacity { get; set; }
        public string SurfaceType { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
