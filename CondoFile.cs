using System.Threading.Tasks.Dataflow;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System;
using System.IO;
namespace Vacation_Rental
{
    public class CondoFile //file class for condos
    {
        private string fileName;
        public CondoFile(string fileName)
        {
            this.fileName = fileName;
        }
        
        public Condo [] GetAllCondos() //takes all the information from text file and populates the array
        {
            Condo [] myCondo = new Condo[500];

            Condo.SetCount(0);

            StreamReader inFile = new StreamReader(fileName);

            string line = inFile.ReadLine();

            while (line != null)
            {
                string [] data = line.Split('#');
                
                myCondo[Condo.GetCount()] = new Condo(int.Parse(data[0]), data[1], DateTime.Parse(data[2]), decimal.Parse(data[3]), data[4], (data[5])); //used to populate the array by splitting the data

                Condo.IncCount();

                line = inFile.ReadLine();
            }
            inFile.Close();

            return myCondo; //returns the populated array
        }
        public void AddCondo() //Add listing for condo in text file
        {
            string address = "", email = "", avaliable = "";
            decimal price = 0;
            DateTime date = DateTime.Now;
            int id = 0;
            for (int i = 0; i < Condo.GetCount(); i++) //update ID by one so no listing has the same ID 
            {
                id = i;
            }
            id += 2;
            try
            {
                System.Console.WriteLine("What is the address?");
                address = Console.ReadLine();

                System.Console.WriteLine("What is the listing end date? Format: 00/00/00");
                date = DateTime.Parse(Console.ReadLine());

                System.Console.WriteLine("What is the list price?");
                price = decimal.Parse(Console.ReadLine());

                System.Console.WriteLine("What is the owner email?");
                email = Console.ReadLine();

                System.Console.WriteLine("Is the condo avaliable?: True or False");
                avaliable = Console.ReadLine();

                using(StreamWriter output = new StreamWriter("listings.txt" , append: true))
                {
                    output.WriteLine();
                    output.Write(id+"#");
                    output.Write(address + "#");
                    output.Write(date + "#");
                    output.Write(price+ "#");
                    output.Write(email + "#");
                    output.Write(avaliable.ToLower());
                    output.Close();
                    System.Console.WriteLine("Succesfully added new Condo. Press any key to continue...");
                    Console.ReadKey();
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Press any key to return to menu");
                Console.ReadKey();
            }
            
        }
       
    }
}