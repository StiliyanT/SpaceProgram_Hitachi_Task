using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SpaceProgramHitachi
{
    public class Day
    {
        //Here is the class Day, which will be used in lists to store the data for every day    
        public int DayNumber { get; set; }
        public int Temperature { get; set; }
        public int Wind { get; set; }
        public int Humidity { get; set; }
        public int Precipitation { get; set; }
        public bool Lightning { get; set; }
        public string Clouds { get; set; } = null!;

    }
}
