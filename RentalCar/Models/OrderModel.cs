using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        public int AutoID { get; set; }
        public int DriverID { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? OrderEndDate { get; set; }
        public int? OrderMilleage { get; set; }

        [DataType(DataType.Date)]
        public DateTime? OrderDayCount { get; set; }
        public int TotalPrice { get; set; }

        public AutoModel Auto { get; set; }
        public DriverModel Driver { get; set; }
    }
}
