using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class CameraLocation
    {
        public const string FieldSeparator = "|";
        public const string NameSeparator = "-";


        /* making assumptions here
         * - The 'Name' contains the 'Number' of the camera in the last position: abc-ab-123 (= 123)
         */
        public int Number
        {
            get
            {
                if (string.IsNullOrEmpty(Name)) return 0;

                var nameValues = Name.Split(NameSeparator, StringSplitOptions.RemoveEmptyEntries);

                if (nameValues.Length != 3) return 0;

                var numberValue = nameValues[2];

                if (!Int32.TryParse(numberValue, out int number)) return 0;

                return number;
            }
        }

        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public override string ToString()
        {
            return $"{Name} {FieldSeparator} {Location} {FieldSeparator} {Latitude} {FieldSeparator} {Longitude}";
        }
    }
}
