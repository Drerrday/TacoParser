using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");
            var lines = File.ReadAllLines(csvPath);
            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();
            var locations = lines.Select(parser.Parse).ToArray();

            var cordA = new GeoCoordinate();
            var cordB = new GeoCoordinate();

            ITrackable locA = null;
            ITrackable locB = null;
            double distance = 0.00;

            for (int i = 0; i < locations.Length; i++)
            {
                cordA.Latitude = locations[i].Location.Latitude;
                cordA.Longitude = locations[i].Location.Longitude;
                logger.LogInfo($"Location A Cordinates: {cordA}");
                for (int j = 0; j < locations.Length; j++)
                {
                    cordB.Latitude = locations[j].Location.Latitude;
                    cordB.Longitude = locations[j].Location.Longitude;
                    var newDistance = cordA.GetDistanceTo(cordB);
                    if (newDistance > distance)
                    {
                        logger.LogInfo($"Checking NEW DIST: {newDistance} Checking OLD DIST: {distance}");
                        locA = locations[i];
                        locB = locations[j];
                        distance = newDistance;
                    }
                }
                logger.LogInfo($"Location ADDED TO locB: {cordB}");
            }
            Console.WriteLine($"The tacobells farthest away from each other are: {locA.Name} && {locB.Name}");
        }
    }
}