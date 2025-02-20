using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Gym_Attendance_app.Entities
{
    public class UserEnt
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (value < 10 || value > 100)
                    throw new ArgumentOutOfRangeException("Age must be between 10 and 100.");
                _age = value;
            }
        }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                if (!Regex.IsMatch(value, @"^\d+$"))
                    throw new ArgumentException("Phone must contain only numbers.");
                _phone = value;
            }
        }

        public string Email { get; set; }
        public byte[] BiometricData { get; set; }
        public byte[] Password { get; set; }
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public string PasswordN { get; set; }

        public void SetPassword(string password)
        {
            Password = HashPasswordWithSalt(password);
        }

        private byte[] HashPasswordWithSalt(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(32);
                    byte[] hashWithSalt = new byte[salt.Length + hash.Length];

                    Array.Copy(salt, 0, hashWithSalt, 0, salt.Length);
                    Array.Copy(hash, 0, hashWithSalt, salt.Length, hash.Length);

                    return hashWithSalt;
                }
            }
        }

        public bool VerifyPassword(string password, byte[] storedHash)
        {
            byte[] salt = new byte[16];
            Array.Copy(storedHash, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                for (int i = 0; i < hash.Length; i++)
                {
                    if (storedHash[i + 16] != hash[i])
                        return false;
                }
            }
            return true;
        }
    }
}
