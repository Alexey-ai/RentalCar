using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class DriverModel
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Passport { get; set; }
        [Required]
        public string DriveLisence { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthdayDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime RentalJoinDate { get; set; }

        public string DriverPicturePath { get; set; }
        public int DistanceTraveled { get; set; }

        public ICollection<OrderModel> Orders { get; set; }

        public string FullName
        {
            get
            {
                return ID + "--" + FirstName + "--" + LastName;
            }
        }
        public int Age
        {
            get
            {
                return (DateTime.Now - BirthdayDate).Days / 365;
            }
        }
    }
}
