namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");
            var cells = line.Split(',');
            if (cells.Length < 3) // If your array.Length is less than 3, something went wrong
            {
                logger.LogError("Error: Something went wrong");
                return null;
            }

            var latitude = double.Parse(cells[0]);
            var longitude = double.Parse(cells[1]);
            var name = cells[2];

            var tacoBellInstance = new TacoBell();
            tacoBellInstance.Name = name;

            Point pointInstance = new Point();
            pointInstance.Latitude = latitude;
            pointInstance.Longitude = longitude;

            tacoBellInstance.Location = pointInstance;
            return tacoBellInstance;
        }
    }
}