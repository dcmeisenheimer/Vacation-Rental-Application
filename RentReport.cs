using System.Reflection.Emit;
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;
using System.Net.Mime;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using System.Collections;
namespace Vacation_Rental
{
    public class RentReport //report class for condoRenting class 
    {
        private CondoRenting [] myRent;

        public RentReport(CondoRenting [] myRent)
        {
            this.myRent = myRent;
        }
        public void PrintRentCondo() //prints all condos for owners
        {
            for (int i = 0; i < CondoRenting.GetRentCount(); i++)
            {
                System.Console.WriteLine(myRent[i].ToString());
            }
        }
        public void GetCondos() //updates the class object with transaction.txt items
        {
            CondoRenting.SetRentCount(0);

            StreamReader inFile = new StreamReader("transactions.txt");

            string line = inFile.ReadLine();

            while (line != null)
            {
                string [] data = line.Split('#');

                myRent[CondoRenting.GetRentCount()] = new CondoRenting(int.Parse(data[0]), data[1], data [2], DateTime.Parse(data[3]) , decimal.Parse(data[4]), DateTime.Parse(data[5]), data[6]);

                CondoRenting.IncRentCount();

                line = inFile.ReadLine();
            }
            inFile.Close();

        }
        public void RentCondo() //rent condo method 
        {
            string myFile = @"avalCondo.txt"; //temp file to store data
            StreamReader inFile = new StreamReader(myFile); //reading temp file

            string line = inFile.ReadLine(); 
            int count = 0;
            int [] id = new int[500];
            decimal [] price = new decimal[500];
            string [] email = new string[500];
            DateTime [] endDate = new DateTime[500];

            while (line != null) //store data to transfer over to transaction file for customer
            {
                string [] data = line.Split('#');
                id[count] = int.Parse(data[0]);
                endDate[count] = DateTime.Parse(data[2]);
                price[count] = decimal.Parse(data[3]);
                email[count] = data[4];
                line = inFile.ReadLine();
                count++;
            }
            inFile.Close();
            StreamReader trans = new StreamReader("transactions.txt");
            line = trans.ReadLine();
            trans.Close();
            bool newLine = true;
            if (line!= null) //if transactions has information streamwriter appends
            {
                newLine = true;
            }
            else if(line == null) //else if transaction has no information streamwriter doesnt append or add spaces
            {
                newLine = false;
            }
            count = 0;
            System.Console.WriteLine("What is the listing ID you wish to rent?");
            int ID = int.Parse(Console.ReadLine());
            for (var i = 0; i < id.Length; i++) //loop to find what listing they want
            {
                count = i;
                if (ID == id[i])
                {
                    int place = 0;
                    for (int j = 0; j < CondoRenting.GetRentCount(); j++) //used to find the ID number 
                    {
                        place = j;
                    }
                    DateTime date = DateTime.Now.Date; 
                    DateTime checkDate = DateTime.Now.Date;
                    string name = "", rentEmail = "";
                    try
                    {
                        System.Console.WriteLine("What is the rentee name?");
                        name = Console.ReadLine();

                        System.Console.WriteLine("What is the rentee email?");
                        rentEmail = Console.ReadLine();

                        System.Console.WriteLine($"The current listing end date is {endDate[count]}");
                        System.Console.WriteLine("What is the rent date? Format: 00/00/00");
                        date = DateTime.Parse(Console.ReadLine());
                        if (date > endDate[count]) //checks to see if user date to rent is past the listing end date
                        {
                            System.Console.WriteLine("Invalid response. Date entered is past listing end date... Please try again.");
                            System.Console.WriteLine($"The current listing end date is {endDate[count]}");
                            System.Console.WriteLine("What is the rent date? Format: 00/00/00");
                            date = DateTime.Parse(Console.ReadLine());
                        }

                        System.Console.WriteLine($"Current price listed is {price[count]} and has been stored");
                        
                        System.Console.WriteLine("What is the checkout date? Format: 00/00/00");
                        checkDate = DateTime.Parse(Console.ReadLine());

                        System.Console.WriteLine($"Current owner email is {email[count]} and has been stored");
                        using(StreamWriter output = new StreamWriter("transactions.txt", append: newLine)) //checks to see if transaction file is empty or not
                        {
                            if (newLine == true) //if it does append then it increases count by 2
                            {
                                output.WriteLine();
                                place += 2;
                            }
                            else if(newLine == false) //if there is no info in file it sets the ID to 1
                            {
                                place += 1;
                            }
                            output.Write(place + "#" + name + "#" + rentEmail + "#" + date + "#" + price[count] + "#" + checkDate + "#" + email[count]);
                            output.Close();   
                            string otherFile = @"idChange.txt";
                            StreamWriter select = new StreamWriter(otherFile); //writes the listing ID to a file so we can change avaliability
                            select.Write(ID);
                            select.Close();
                            System.Console.WriteLine("Successfully added new transaction. Press any key to continue...");
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
            File.Delete(myFile); //deletes the file containing all avaliable condos
        }
        public void IndCustomerRent() //gathers all info on email user selected
        {
            var report = new List<string>();
            string listing = "";
            int i = 0;
            System.Console.WriteLine("What is the email address of the customer");
            string userChoice = Console.ReadLine();
            
            for (i = 0; i < CondoRenting.GetRentCount(); i++)
            {
                
                if (myRent[i].GetRentEmail().ToLower() == userChoice.ToLower()) //if emails match the user choice then it adds the reports to a list
                {
                    listing = myRent[i].ToString();
                    report.Add(listing);
                    System.Console.WriteLine(myRent[i].ToString());
                }
                
            }
            bool isEmpty = !report.Any();
            if (isEmpty)
            {
                System.Console.WriteLine("No email exist returning to menu. Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                SaveFile(ref report); //save file method
            }
        }
        public void HistCustomerRent() //sort by customer and then by date method
        {
            int min = 0;
            string custOne = "";
            string custTwo = "";
            for (int i = 0; i < CondoRenting.GetRentCount() -1; i++) 
            {
                min = i;
                for (int j = i + 1; j < CondoRenting.GetRentCount(); j++) //used to compare the first and second position 
                {
                    custOne = myRent[min].GetRentName() + myRent[min].GetRentDate(); //store date and rent of each customer for compare to method
                    custTwo = myRent[j].GetRentName() + myRent[j].GetRentDate();

                    if(custTwo.CompareTo(custOne)< 0 ) //compares the names of both customers and if they are the same 
                    {
                        min = j;
                    }
                }
                if (min != i) //if both names are not the same it swaps 
                {
                    Swap(min , i);
                }
            }
            var result = new List<string>();
            
            for (int i = 0; i < CondoRenting.GetRentCount(); i++) //for loop to add the results of the sorted customers to a list
            {
                result.Add(myRent[i].ToString());
                System.Console.WriteLine(result[i]);
            }
            AvgbyUser(ref result); //getting the average method
            
        }
        
        public void Swap(int x, int y) //swap method
        {
            CondoRenting temp = myRent[x];
            myRent[x] = myRent[y];
            myRent[y] = temp;
        }
        public void AvgbyUser(ref List<string> result) //checks the average per user method
        {
            decimal sum = myRent[0].GetRentAmt();
            int count = 1;
            string curr = myRent[0].GetRentName();

            for (int i = 1; i < CondoRenting.GetRentCount(); i++) //if the name is the same it gets the amount 
            {
                if (curr == myRent[i].GetRentName())
                {
                    sum += myRent[i].GetRentAmt();
                    count++;

                }
                else
                {
                    ProcessBreak(ref curr, ref sum, ref count, i, ref result); 
                }
            }
            ProcessBreak(ref curr, ref sum, ref count, 0, ref result);
            SaveFile(ref result); //save the file for user
        }
        
        public void ProcessBreak(ref string curr, ref decimal sum, ref int count , int i, ref List<string> result)
        {
            int avg = (int)sum / count;

            Console.WriteLine($"{curr} average amount spent is {avg} and has rented {count} condos"); //tells user the average amount per user and how many condos they had
            result.Add($"{curr} average amount spent is {avg} and has rented {count} condos");
            
            sum = myRent[i].GetRentAmt(); //resets data

            count = 1;

            curr = myRent[i].GetRentName();
        }
        public void SaveFile(ref List<string> result) //save file method
        {
            System.Console.WriteLine("Would you like to save this report to a file? Y or N");
            string userInput = Console.ReadLine();

            if (userInput.ToUpper() == "Y")
            {
                System.Console.WriteLine("Please enter a file name to save the date. Ex: listings.txt ");
                string fileName = Console.ReadLine();

                if (File.Exists(fileName))
                {
                    using(StreamWriter output = new StreamWriter(fileName))
                    {
                        for (int i = 0; i < result.Count; i++) //saves the report to a file a user selects
                        {
                            if (result[i] != null)
                            {
                                output.WriteLine(result[i]);
                            }
                            
                        }
                        System.Console.WriteLine("File Saved Successfully");
                        output.Close();
                        System.Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    
                }
                else
                {
                    System.Console.WriteLine("File does not exist returning to menu...");
                    System.Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
        
        public void HistRevenueReport() //tell the revenue by month and year
        {
            IDictionary<DateTime, decimal> monthList = new Dictionary<DateTime, decimal>(); //variable to store rent date and price

            for (int i = 0; i < CondoRenting.GetRentCount(); i++) //adds the values to dictionary
            {
                monthList.Add(myRent[i].GetRentDate(), myRent[i].GetRentAmt());
            }
            var sorted = monthList.OrderBy(item => item.Key).ToList(); //sorts the dates from least to greatest
            
            DateTime date = DateTime.Now; 

            int month = 0;
            decimal revenue = 0;
            decimal [] revenueMonth = new decimal[12];
            int count = 0;

            for (var i = 1; i < 13; i++) //for loop to get revenue of every month 
            {
                for (var j = 0; j < sorted.Count; j++)
                {
                    date = sorted[j].Key;
                    month = date.Month;
                    if (month == i) //adds the revenue if the months match
                    {
                        revenue = sorted[j].Value;
                        revenueMonth[count] = revenueMonth[count] + revenue;
                    }
                }
                count++;
            }
            List<string> storeAll = new List<string>(); //adds all monthly revenue to a list
            storeAll.Add("Here are the results of the historical revenue analysis by month for all records: ");
			storeAll.Add("January: " + revenueMonth[0]);
			storeAll.Add("February: " + revenueMonth[1]);
			storeAll.Add("March: " + revenueMonth[2]);
			storeAll.Add("April: " + revenueMonth[3]);
			storeAll.Add("May: " + revenueMonth[4]);
			storeAll.Add("June: " + revenueMonth[5]);
			storeAll.Add("July: " + revenueMonth[6]);
			storeAll.Add("August: " + revenueMonth[7]);
			storeAll.Add("September: " + revenueMonth[8]);
			storeAll.Add("October: " + revenueMonth[9]);
			storeAll.Add("November: " + revenueMonth[10]);
			storeAll.Add("December: " + revenueMonth[11]);

            DateTime one = DateTime.Now;
            int year = 0;
            decimal revenueOne = 0;
            decimal revenueTwo = 0;
            for (var i = 0; i < sorted.Count; i++) //loop to check if the years are the same
            {
                one = sorted[i].Key;
                year = one.Year;

                if (year == DateTime.Now.Year) //if year is the same it adds the revenue of that year
                {
                    revenueOne += sorted[i].Value;
                    
                }
                else if(year != DateTime.Now.Year) //if year is not current year it adds the value to next year
                {
                    revenueTwo  += sorted[i].Value;
                }
            }
            storeAll.Add("");
            storeAll.Add("Here is the Yearly revenue reports");
            storeAll.Add("Year 2021: " + revenueOne);
            storeAll.Add("Year 2022: " + revenueTwo); //add the yearly revenue to list

            foreach (var item in storeAll) //prints the list to user
            {
                System.Console.WriteLine(item);
            }
            SaveFile(ref storeAll); //save report to file
        }
        
    }
}