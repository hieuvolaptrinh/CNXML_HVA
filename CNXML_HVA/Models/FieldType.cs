using System;

namespace CNXML_HVA.Models
{
    public class FieldType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
        // Dimensions
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public string DimensionUnit { get; set; }
        public string SizeDisplay { get; set; }
        
        // Players
        public int PlayersPerTeam { get; set; }
        public int TotalCapacity { get; set; }
        
        // Goal Size
        public decimal GoalHeight { get; set; }
        public decimal GoalWidth { get; set; }
        public string GoalUnit { get; set; }
        
        // Field Info
        public string SurfaceType { get; set; }
        public decimal BasePrice { get; set; }
        public decimal PeakHourMultiplier { get; set; }
        public decimal WeekendMultiplier { get; set; }
        
        // Details
        public string Description { get; set; }
        public string Features { get; set; }
        public int MinimumBookingHours { get; set; }
        public int MaximumBookingHours { get; set; }
        public string Status { get; set; }

        public FieldType()
        {
            DimensionUnit = "mét";
            GoalUnit = "mét";
            PeakHourMultiplier = 1.5m;
            WeekendMultiplier = 1.3m;
            MinimumBookingHours = 1;
            MaximumBookingHours = 4;
            Status = "Active";
        }

        public override string ToString()
        {
            return Name;
        }

        public string GetFullDimensions()
        {
            return $"{Width}m x {Length}m";
        }

        public string GetGoalSize()
        {
            return $"{GoalHeight}m x {GoalWidth}m";
        }
    }
}
