using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class AutoModel
    {
        public int ID { get; set; }

        [Required]
        public string CarMake { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Issue { get; set; }

        public int Capacity { get; set; }
        public double FuelConsuption { get; set; }
        public string EngineType { get; set; }

        public double EngineCapacity {get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]")]
        public string TransmissionType { get; set; }
        [Required]
        public int Price { get; set; }

        public bool Aviablity { get; set; }
        public int Mileage { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
        public List<PictureModel> Pictures { get; set; } = new List<PictureModel>();

        public string FullName
        {
            get
            {
                return ID + " - " + CarMake + " - " + Model + " - " + EngineCapacity + " - " + TransmissionType;
            }
        }
    }
}
