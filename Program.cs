using CsvHelper;
using System.Globalization;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Mail;
using CsvHelper.Configuration.Attributes;

namespace SpaceProgramHitachi
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            

            //Here we have the inputs and some while loops if the input is not appropriate
            Console.WriteLine("Choose your option:");
            Console.WriteLine("For basic weather criteria write -> basic");
            Console.WriteLine("For custom weather criteria write -> custom");

            string option = Console.ReadLine();

            while (option != "basic" && option != "custom")
            {
                Console.WriteLine("You have only two options.");
                Console.WriteLine("For basic weather criteria write -> basic");
                Console.WriteLine("For custom weather criteria write -> custom");

                option = Console.ReadLine();

            }
            Console.Write("Csv Path: ");

            string inputFileName = Console.ReadLine();

            if (!inputFileName.EndsWith(".csv"))
            {
                throw new CustomException(1);
                //"The file you are trying to work with is not a CSV file, try again."
            }

            Console.Write("Sender email adderss: ");

            string inputSenderEmailAddress = Console.ReadLine();

            if (!inputSenderEmailAddress.EndsWith("@gmail.com"))
            {
                throw new CustomException(2);
                //"The application works only with Gmail address, try again."
            }

            Console.Write("Sender email password: ");

            string inputPassword = Console.ReadLine();

            Console.Write("Receiver email address: ");

            string inputRecieverEmailAddress = Console.ReadLine();

            if (!inputRecieverEmailAddress.EndsWith("@gmail.com"))
            {
                throw new CustomException(2);
                //"The application works only with Gmail address, try again."
            }


            
            if (option == "custom")
            {
                //If the user chose to have custom criteria, we use the custom criteria class where they will have to give their inputs as the new criteria

                CustomCriteria custom = new CustomCriteria();

                //Then we have the deserialize class, where we will use the properties from custom criteria class and it will extract the data from the CSV file and it will put it in lists
                Deserialize deserializer = new Deserialize(inputFileName, custom.temperatureMinCriteriaCustom, custom.temperatureMaxCriteriaCustom, custom.windMaxCriteriaCustom, custom.humidityMaxCriteriaCustom,
                    custom.precipitationCriteriaCustom, custom.lightningBooleanAnswerCustom, custom.firstCloudParameterCustom, custom.secondCloudParameterCustom);

                //In the serialize class the data that we want as an output is also aggregated and it will be sent to the receiver email adress
                Serialize serialize = new Serialize(inputSenderEmailAddress, inputPassword, inputRecieverEmailAddress, deserializer);
            }
            else
            {
                //Here is the basic criteria, the variables will be used the same way as in the custom criteria option
                int temperatureMinCriteria = 2;
                int temperatureMaxCriteria = 31;
                int windMaxCriteria = 10;
                int humidityMaxCriteria = 60;
                int precipitationCriteria = 0;
                bool lightningBooleanAnswer = false;
                string firstCloudParameter = "Cumulus";
                string secondCloudParameter = "Nimbus";

                Deserialize deserializer = new Deserialize(inputFileName, temperatureMinCriteria, temperatureMaxCriteria, windMaxCriteria, humidityMaxCriteria,
                   precipitationCriteria, lightningBooleanAnswer, firstCloudParameter, secondCloudParameter);

                Serialize serialize = new Serialize(inputSenderEmailAddress, inputPassword, inputRecieverEmailAddress, deserializer);

            }

        }

        
    }
}
