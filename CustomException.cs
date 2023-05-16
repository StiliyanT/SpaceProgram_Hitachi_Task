using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgramHitachi
{
    public class CustomException : Exception
    {
        public CustomException(int options)
        {
            //We have custom exception class for our application, this way it is easier to work with it
            switch (options)
            {
                case 1:
                    throw new NotImplementedException("The file you are trying to work with is not a CSV file, try again.");
                case 2:
                    throw new NotImplementedException("The application works only with Gmail address, try again.");
                case 3:
                    throw new NotImplementedException("With this criteria there is no appropriate date for the space shuttle launch :(");              
            }
        }

    }
}
