using System;

namespace Gym_Attendance_app.Entities
{
    public class MembershipEnt
    {
        public int MembershipID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private decimal _cost;
        public decimal Cost
        {
            get => _cost;
            set
            {
                if (value < 0) throw new ArgumentException("Cost cannot be negative.");
                _cost = value;
            }
        }

        private int _durationMonths;
        public int DurationMonths
        {
            get => _durationMonths;
            set
            {
                if (value != 1 && value != 2 && value != 3 && value != 12)
                    throw new ArgumentException("Duration must be 1, 2, 3, or 12 months.");
                _durationMonths = value;
            }
        }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
