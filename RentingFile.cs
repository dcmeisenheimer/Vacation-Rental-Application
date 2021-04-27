using System;
using System.IO;
namespace Vacation_Rental
{
    public class RentingFile //file class for condoRenting class
    {
        private string fileName;

        public RentingFile(string fileName)
        {
            this.fileName = fileName;
        }

        public CondoRenting[] GetCondoRentings() //used to populate the array with data inside transaction file
        {
            CondoRenting [] myRent = new CondoRenting[500];

            CondoRenting.SetRentCount(0);

            StreamReader inFile = new StreamReader(fileName);

            string line = inFile.ReadLine();

            while (line != null) //stores each data point inside the object
            {
                string [] data = line.Split('#'); 

                myRent[CondoRenting.GetRentCount()] = new CondoRenting(int.Parse(data[0]), data[1], data [2], DateTime.Parse(data[3]) , decimal.Parse(data[4]), DateTime.Parse(data[5]), data[6]);

                CondoRenting.IncRentCount();

                line = inFile.ReadLine();
            }
            inFile.Close();

            return myRent; //returns the object array
        }
        
    }
}