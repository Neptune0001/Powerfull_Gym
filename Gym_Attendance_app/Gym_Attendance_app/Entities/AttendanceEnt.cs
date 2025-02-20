using System;

namespace Gym_Attendance_app.Entities
{
    public class AttendanceEnt
    {
        public int AttendanceID { get; set; }
        public int UserID { get; set; }
        public DateTime CheckInTime { get; set; } = DateTime.Now;
        public DateTime? CheckOutTime { get; set; }

        public bool IsValidCheckout()
        {
            return CheckOutTime == null || CheckOutTime >= CheckInTime;
        }
    }
}
