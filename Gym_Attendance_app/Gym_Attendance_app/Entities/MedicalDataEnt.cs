using System.Text.RegularExpressions;
using System;

namespace Gym_Attendance_app.Entities
{
    public class MedicalDataEnt
    {
        public int MedicalDataID { get; set; }
        public int UserID { get; set; }
        public string Conditions { get; set; }
        public string Injuries { get; set; }
        public string EmergencyContactName { get; set; }

        private string _emergencyContactPhone;
        public string EmergencyContactPhone
        {
            get => _emergencyContactPhone;
            set
            {
                if (!Regex.IsMatch(value, @"^\d+$"))
                    throw new ArgumentException("Emergency Contact Phone must contain only numbers.");
                _emergencyContactPhone = value;
            }
        }
    }
}
