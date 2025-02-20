using System;

namespace Gym_Attendance_app.Entities
{
    public class UserMembershipEnt
    {
        public int UserMembershipID { get; set; }
        public int UserID { get; set; }
        public int MembershipID { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
