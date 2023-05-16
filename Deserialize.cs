using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgramHitachi
{
    public class Deserialize
    {
        //The deserialize class is used to put the data from the csv file into lists which will be used for filtering the data
        
        public List<string> temperature { get; } = new List<string>();
        public List<string> wind { get; } = new List<string>();
        public List<string> humidity { get; } = new List<string>();
        public List<string> precipitation { get; } = new List<string>();
        public List<string> lightning { get; } = new List<string>();
        public List<string> clouds { get; } = new List<string>();
        public List<Day> days { get; } = new List<Day>();
        public List<Day> filteredDays { get; } = new List<Day>();


        public Deserialize(string inputFileName, int temperatureMinCriteria, int temperatureMaxCriteria, int windMaxCriteria, int humidityMaxCriteria,
            int precipitationCriteria, bool lightningBooleanAnswer, string firstCloudParameter, string secondCloudParameter)
        {           
            int countOfColumns;

            using (var streamReader = new StreamReader(string.Format(@"{0}", inputFileName)))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    countOfColumns = streamReader.ReadLine().Split(",").Count();

                    for (int i = 0; i <= 5; i++)
                    {
                        csvReader.Read();

                        if (i == 0)
                        {
                            FillInformation(temperature, csvReader, countOfColumns);
                        }
                        if (i == 1)
                        {
                            FillInformation(wind, csvReader, countOfColumns);
                        }
                        if (i == 2)
                        {
                            FillInformation(humidity, csvReader, countOfColumns);
                        }
                        if (i == 3)
                        {
                            FillInformation(precipitation, csvReader, countOfColumns);
                        }
                        if (i == 4)
                        {
                            FillInformation(lightning, csvReader, countOfColumns);
                        }
                        if (i == 5)
                        {
                            FillInformation(clouds, csvReader, countOfColumns);
                        }
                    }
                }
            }
            //Here we fill a list with objects of the Day class, which will be filled with data from the lists for every parameter we have from the csv file
            for (int i = 0; i < countOfColumns - 1; i++)
            {
                Day day = new Day();

                day.DayNumber = i + 1;

                day.Temperature = int.Parse(this.temperature[i]);

                day.Wind = int.Parse(this.wind[i]);

                day.Humidity = int.Parse(this.humidity[i]);

                day.Precipitation = int.Parse(this.precipitation[i]);

                if (this.lightning[i] == "No")
                {
                    day.Lightning = false;
                }
                else
                {
                    day.Lightning = true;
                }

                day.Clouds = this.clouds[i];

                days.Add(day);

            }
            //We use LINQ for filtering the data, and then ordering it, so that we have the lower parameters for wind and humidity upfront in the list
            this.filteredDays = days
                .Where(d => d.Temperature > temperatureMinCriteria
                         && d.Temperature < temperatureMaxCriteria
                         && d.Wind < windMaxCriteria
                         && d.Humidity < humidityMaxCriteria
                         && d.Precipitation == precipitationCriteria
                         && d.Lightning == lightningBooleanAnswer
                         && d.Clouds != firstCloudParameter
                         && d.Clouds != secondCloudParameter)
                .OrderBy(d => d.Wind)
                .ThenBy(d => d.Humidity)
                .ToList();

            if (this.filteredDays.Count == 0)
            {
                throw new CustomException(3);
                //"With this criteria there is no appropriate date for the space shuttle launch :("
            }
        }
        //We have a method for filling the lists which uses a for loop, and using the csvReader takes one by one every field filled with information and then it puts it into the list
        public static List<string> FillInformation(List<string> list, CsvReader reader, int count)
        {

            for (int i = 1; i < count; i++)
            {
                var tempColumn = reader.GetField(i);

                list.Add(tempColumn);
            }

            return list;

        }
    }
}
