using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgramHitachi
{
    public class CustomCriteria
    {
        //The custom criteria class is used for storing all inputs from the custom option, the properties will be used in deserialize class, and also we have some while loops if the input is not appropriate
        public int temperatureMinCriteriaCustom { get; set; }
        public int temperatureMaxCriteriaCustom { get; set; }
        public int windMaxCriteriaCustom { get; set; }
        public int humidityMaxCriteriaCustom { get; set; }
        public int precipitationCriteriaCustom { get; set; }
        public bool lightningBooleanAnswerCustom { get; set; }
        public string firstCloudParameterCustom { get; set; }
        public string secondCloudParameterCustom { get; set; }
        public CustomCriteria()
        {
            Console.WriteLine("Write your weather criteria!");
            Console.WriteLine("Write the lowest temperature parameter you want to allow: ");

            this.temperatureMinCriteriaCustom = int.Parse(Console.ReadLine());

            Console.WriteLine("Write the highest temperature parameter you want to allow: ");

            this.temperatureMaxCriteriaCustom = int.Parse(Console.ReadLine());

            while (this.temperatureMinCriteriaCustom >= this.temperatureMaxCriteriaCustom)
            {
                Console.WriteLine("The lowest temperature parameter shouldn't be bigger than the highest temperature parameter");
                Console.WriteLine("Write the lowest temperature parameter you want to allow: ");

                this.temperatureMinCriteriaCustom = int.Parse(Console.ReadLine());

                Console.WriteLine("Write the highest temperature parameter you want to allow: ");

                this.temperatureMaxCriteriaCustom = int.Parse(Console.ReadLine());

            }

            Console.WriteLine("Write the highest wind parameter you want to allow: ");

            this.windMaxCriteriaCustom = int.Parse(Console.ReadLine());

            Console.WriteLine("Write the highest humidity parameter you want to allow: ");

            this.humidityMaxCriteriaCustom = int.Parse(Console.ReadLine());

            Console.WriteLine("Write the exact precipitation parameter you want to allow: ");

            this.precipitationCriteriaCustom = int.Parse(Console.ReadLine());

            Console.WriteLine("Type Yes or No if you allow lighting: ");

            string lightningAnswer = Console.ReadLine();

            if (lightningAnswer == "Yes")
            {
                this.lightningBooleanAnswerCustom = true;
            }
            while (lightningAnswer != "Yes" && lightningAnswer != "No")
            {
                Console.WriteLine("The answers can only be Yes or No, try again.");
                Console.WriteLine("Write your decision: ");

                lightningAnswer = Console.ReadLine();

                if (lightningAnswer == "Yes")
                {
                    this.lightningBooleanAnswerCustom = true;
                    break;
                }
            }
            Console.WriteLine("Now choose two different cloud parameters that you don't allow: ");
            Console.WriteLine("These are your options: Cumulus, Stratus, Nimbus, Cirrus.");
            Console.WriteLine("First cloud parameter:");

            this.firstCloudParameterCustom = Console.ReadLine();

            Console.WriteLine("Second cloud parameter:");

            this.secondCloudParameterCustom = Console.ReadLine();

            while (this.firstCloudParameterCustom != "Cumulus"
                && this.firstCloudParameterCustom != "Stratus"
                && this.firstCloudParameterCustom != "Nimbus"
                && this.firstCloudParameterCustom != "Cirrus"
                && this.secondCloudParameterCustom != "Cumulus"
                && this.secondCloudParameterCustom != "Stratus"
                && this.secondCloudParameterCustom != "Nimbus"
                && this.secondCloudParameterCustom != "Cirrus"
                && this.firstCloudParameterCustom == this.secondCloudParameterCustom)
            {

                Console.WriteLine("You may have wrote it wrong, try again.");
                Console.WriteLine("Don't forget they should be different also.");
                Console.WriteLine("These are your options: Cumulus, Stratus, Nimbus, Cirrus.");
                Console.WriteLine("First cloud parameter:");

                this.firstCloudParameterCustom = Console.ReadLine();

                Console.WriteLine("Second cloud parameter:");

                this.secondCloudParameterCustom = Console.ReadLine();

            }
        }
    }
}
