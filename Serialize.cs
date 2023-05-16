using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgramHitachi
{
    public class Serialize
    {
        //The serialize class is taking the object of the deserialize class that has all the data into its properties, and the it uses it to aggregate the data
        public Serialize(string inputSenderEmailAddress, string inputPassword, string inputRecieverEmailAddress, Deserialize d)
        {
            string lightningAllowed = "No";
            if(d.filteredDays.Select(d => d.Lightning).First() == true)            
                lightningAllowed = "Yes";
            var csvPath = Path.Combine(Environment.CurrentDirectory, $"WeatherReport.csv");

            //Using the stream writer class we start to compile our csv file
            using (var streamWriter = new StreamWriter(csvPath))
            {
                streamWriter.WriteLine("Parameters/Aggregated data" + "," + "Average value" + "," + "Max value" + "," + "Min value" + "," + "Median value" + "," + "Most appropriate launch day parameter value");
                streamWriter.WriteLine("Temperature (C)" + "," + d.days.Average(d => d.Temperature) + "," + d.days.Max(d => d.Temperature) + "," + d.days.Min(d => d.Temperature) + "," + GetMedian(d.temperature) + "," + d.filteredDays.Select(d => d.Temperature).First());
                streamWriter.WriteLine("Wind (m/s)" + "," + d.days.Average(d => d.Wind) + "," + d.days.Max(d => d.Wind) + "," + d.days.Min(d => d.Wind) + "," + GetMedian(d.wind) + "," + d.filteredDays.Select(d => d.Wind).First());
                streamWriter.WriteLine("Humidity (%)" + "," + d.days.Average(d => d.Humidity) + "," + d.days.Max(d => d.Humidity) + "," + d.days.Min(d => d.Humidity) + "," + GetMedian(d.humidity) + "," + d.filteredDays.Select(d => d.Humidity).First());
                streamWriter.WriteLine("Precipitation (%)" + "," + d.days.Average(d => d.Precipitation) + "," + d.days.Max(d => d.Precipitation) + "," + d.days.Min(d => d.Precipitation) + "," + GetMedian(d.precipitation) + "," + d.filteredDays.Select(d => d.Precipitation).First());
                streamWriter.WriteLine("Lightning" + "," + "-" + "," + "-" + "," + "-" + "," + "-" + "," + lightningAllowed);
                streamWriter.WriteLine("Clouds" + "," + "-" + "," + "-" + "," + "-" + "," + "-" + "," + d.filteredDays.Select(d => d.Clouds).First());
                streamWriter.WriteLine($"The most appropriate date for the space shuttle launch is {d.filteredDays.Select(d => d.DayNumber).First()}th of July!!!");
            }
            //Here we are establishing connection using STMP, and we send the csv file as an attachment to our mail
            //We are working with gmail
            SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);

            mailServer.EnableSsl = true;

            mailServer.Credentials = new NetworkCredential(inputSenderEmailAddress, inputPassword);

            MailMessage msg = new MailMessage(inputSenderEmailAddress, inputRecieverEmailAddress);

            msg.Subject = "Weather Report!";
            msg.Body = "Here is the weather report for the space lauch!";

            msg.Attachments.Add(new Attachment(csvPath));

            mailServer.Send(msg);
        }
        //Here we have a method for getting the median of a list
        public static double GetMedian(List<string> sourceNumbers)
        {

            double[] numbers = new double[sourceNumbers.Count];

            for (int i = 0; i < sourceNumbers.Count; i++)
            {
                numbers[i] = double.Parse(sourceNumbers[i]);
            }

            double[] sortedPNumbers = (double[])numbers.Clone();

            Array.Sort(sortedPNumbers);

            int size = sortedPNumbers.Length;

            int mid = size / 2;

            double median = (size % 2 != 0) ? (double)sortedPNumbers[mid] : ((double)sortedPNumbers[mid] + (double)sortedPNumbers[mid - 1]) / 2;

            return median;

        }
    }
}
