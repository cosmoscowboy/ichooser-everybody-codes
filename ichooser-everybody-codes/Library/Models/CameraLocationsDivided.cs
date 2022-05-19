namespace Library.Models
{
    public class CameraLocationsDivided
    {
        public CameraLocationsDivided()
        {
            DivisibleByThree = new List<CameraLocation>();
            DivisibleByFive = new List<CameraLocation>();
            DivisibleByThreeAndFive = new List<CameraLocation>();
            DivisibleNotByThreeAndFive = new List<CameraLocation>();
        }

        public CameraLocationsDivided(IEnumerable<CameraLocation> cameraLocations) : this()
        {
            if (cameraLocations is null)
            {
                throw new ArgumentNullException(nameof(cameraLocations));
            }

            // divide the camera locations into the different lists
            // % 3
            DivisibleByThree = 
                cameraLocations
                    .Where(e => IsDivisibleByThree(e.Number))
                    .ToList();

            // % 5
            DivisibleByFive =
                cameraLocations
                    .Where(e => IsDivisibleByFive(e.Number))
                    .ToList();

            // % 3 && % 5
            DivisibleByThreeAndFive =
                cameraLocations
                    .Where(e => IsDivisibleByThreeAndFive(e.Number))
                    .ToList();


            DivisibleNotByThreeAndFive =
                cameraLocations
                    .Where(e => IsNotDivisibleByThreeAndFive(e.Number))
                    .ToList();
        }

        public IList<CameraLocation> DivisibleByThree { get; set; }
        public IList<CameraLocation> DivisibleByFive { get; set; }
        public IList<CameraLocation> DivisibleByThreeAndFive { get; set; }
        public IList<CameraLocation> DivisibleNotByThreeAndFive { get; set; }

        public bool IsDivisibleByThree(int number)
        {
            return number == 0 ? false :
                number % 3 == 0;
        }

        public bool IsDivisibleByFive(int number)
        {
            return number == 0 ? false :
                number % 5 == 0;
        }

        public bool IsDivisibleByThreeAndFive(int number)
        {
            return number == 0 ? false :
                IsDivisibleByThree(number) && IsDivisibleByFive(number);
        }

        public bool IsNotDivisibleByThreeAndFive(int number)
        {
            return number == 0 ? false :
                (!IsDivisibleByThree(number) && !IsDivisibleByFive(number));
        }
    }
}
