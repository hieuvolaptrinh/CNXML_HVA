using System;

namespace CNXML_HVA.Models
{
    public class Field
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FieldTypeId { get; set; }
        public string BranchId { get; set; }
        public Address Address { get; set; }
        public decimal PricePerHour { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Facilities { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastMaintenance { get; set; }

        // Display properties
        public string TypeName { get; set; }
        public string NextBooking { get; set; }

        public Field()
        {
            Address = new Address();
            CreatedDate = DateTime.Now;
            LastMaintenance = DateTime.Now;
        }
    }

    public class Address
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

        public override string ToString()
        {
            return $"{HouseNumber} {Street}, {District}, {City}";
        }
    }
}
