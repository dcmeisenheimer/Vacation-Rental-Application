using System.Xml;
using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;
namespace Vacation_Rental
{
    public class CondoReport //report class for condo class
    {
        private Condo [] myCondo;

        public CondoReport(Condo [] myCondo)
        {
            this.myCondo = myCondo;
        }
        public void GetCondos() //populate the object array with the listing information
        {
            Condo.SetCount(0);

            StreamReader inFile = new StreamReader("listings.txt");

            string line = inFile.ReadLine();

            while (line != null)
            {
                string [] data = line.Split('#');
                
                myCondo[Condo.GetCount()] = new Condo(int.Parse(data[0]), data[1], DateTime.Parse(data[2]), decimal.Parse(data[3]), data[4], (data[5])); //this method will be called to bypass the object parameter restrictions.

                Condo.IncCount();

                line = inFile.ReadLine();
            }
            inFile.Close();
        }
        
        public void PrintAvalCondos() //prints all aval condos by identifing if its true or false
        {
            List<string> avalLease = new List<string>();
            string filePath = @"avalCondo.txt";
            System.Console.WriteLine("Here is a list of all avaliable condos");
            for (int i = 0; i < Condo.GetCount(); i++) //I have a list that stores all the avaliable condos by searching if they are true
            {
                if (myCondo[i].GetAvaliable().ToLower() == "true")
                {
                    System.Console.WriteLine(myCondo[i].ToString());
                    avalLease.Add(myCondo[i].GetListingID() + "#" + myCondo[i].GetAddress() + "#" + myCondo[i].GetListingEndDate() + "#" + myCondo[i].GetListPrice() + "#" + myCondo[i].GetOwnerEmail() + "#" + myCondo[i].GetAvaliable());
                }
            }
            if (!File.Exists(filePath)) //if file doesnt exist it will create it
            {
                using(StreamWriter output = File.CreateText(filePath))
                {
                    for (int i = 0; i < avalLease.Count; i++) //determines if the last line should be write or writelines to prevent extra space and the end of txt file
                    {
                        if (i == avalLease.Count -1)
                        {
                            output.Write(avalLease[i]);
                        }
                        else
                        {
                            output.WriteLine(avalLease[i]);
                        }
                    }
                    output.Close();
                }
            }
            else
            {
                using(StreamWriter output = new StreamWriter(filePath)) //if file exist it simply writes to file checking the same issues
                {
                    for (int i = 0; i < avalLease.Count; i++)
                    {
                        if (i == avalLease.Count -1)
                        {
                            output.Write(avalLease[i]);
                        }
                        else
                        {
                            output.WriteLine(avalLease[i]);
                        }
                    }
                    output.Close();
                }
            }
            
        }
        public void SetAvalCondo() //Method to update each condo if they have been rented to false
        {
            if (File.Exists("idChange.txt"))
            {
                List<string> avalLease = new List<string>();
                StreamReader inFile = new StreamReader("idChange.txt");
                string line = inFile.ReadLine();
                int data = 0;
                while (line != null)//stores the number inside the text file
                {
                    data = int.Parse(line);

                    line = inFile.ReadLine();
                }
                inFile.Close();

                for (var i = 0; i < Condo.GetCount(); i++) //checks to if the number matches
                {
                    if (data == myCondo[i].GetListingID()) //if number matches the listing it updates the listing to false.
                    {
                        myCondo[i].SetAvaliable("false"); 
                    }
                    avalLease.Add(myCondo[i].GetListingID() + "#" + myCondo[i].GetAddress() + "#" + myCondo[i].GetListingEndDate() + "#" + myCondo[i].GetListPrice() + "#" + myCondo[i].GetOwnerEmail() + "#" + myCondo[i].GetAvaliable()); //stores the info to write back into listing
                }
                File.Delete("idChange.txt"); //delete the file after information has been gathered.

                using(StreamWriter output = File.CreateText("listings.txt")) //writes all the information back into the listings with updating the true or false
                {
                    for (int i = 0; i < avalLease.Count; i++)
                    {
                        if (i == avalLease.Count -1) //checks to make sure there is no empty space on the last line
                        {
                            output.Write(avalLease[i]);
                        }
                        else
                        {
                            output.WriteLine(avalLease[i]);
                        }
                    }
                    output.Close();
                }
            }

        }

        public void PrintAllCondos() //print all condos for owners.
        {
            for (int i = 0; i < Condo.GetCount(); i++)
            {
                System.Console.WriteLine(myCondo[i].ToString());
            }
        }
        public void DeleteCondo()// delete a condo for user
        {
            int count = 1;
            List<string> delete = new List<string>();
            System.Console.WriteLine("What is the listing ID of the condo you wish to delete?");
            int id = int.Parse(Console.ReadLine());

            for (int i = 0; i < Condo.GetCount(); i++) //takes all the condos that doesnt have the same listing ID and stores it into a list
            {
                if (id != myCondo[i].GetListingID())
                {
                    myCondo[i].SetListingID(count);
                    delete.Add(myCondo[i].GetListingID() + "#" + myCondo[i].GetAddress() + "#" + myCondo[i].GetListingEndDate() + "#" + myCondo[i].GetListPrice() + "#" + myCondo[i].GetOwnerEmail() + "#" + myCondo[i].GetAvaliable());
                    count++;
                }
            }
            using(StreamWriter output = new StreamWriter("listings.txt")) //writes the list into listings.txt file without the deleted listing
            {
                for (var i = 0; i < delete.Count; i++) //checks for empty space in file
                {
                    if (i == delete.Count -1)
                    {
                        output.Write(delete[i]);
                    }
                    else
                    {
                        output.WriteLine(delete[i]);
                    }
                }
                output.Close();
            }
            System.Console.WriteLine("File updated successfully. Press any key to continue...");
            Console.ReadKey();
        }
        public void EditCondo() //edits a condo a user has
        {
            DateTime date = DateTime.Now.Date;   
            System.Console.WriteLine("What is the listing ID of the condo you wish to change?");
            int id = int.Parse(Console.ReadLine());

            for (int i = 0; i < Condo.GetCount(); i++) //checks if the ID selected matches all condos stored
            {
                if (id == myCondo[i].GetListingID()) 
                {
                    myCondo[i].SetListingID(id);
                    System.Console.WriteLine("1: to change all fields" + "\n2: to change specific field");
                    string userChoice = Console.ReadLine();
                    
                    if (userChoice == "1") //changes all fields of a condo
                    {
                        try
                        {
                            System.Console.WriteLine($"Your current address is {myCondo[i].GetAddress()}");
                            System.Console.WriteLine("What is the address you would like to change to?");
                            string address = Console.ReadLine();

                            System.Console.WriteLine($"Your current Listing End Date is {myCondo[i].GetListingEndDate()}");
                            System.Console.WriteLine("What is the listing end date you would like to change to? Format: 00/00/00");
                            date = DateTime.Parse(Console.ReadLine());

                            System.Console.WriteLine($"Your current List Price is {myCondo[i].GetListPrice()}");
                            System.Console.WriteLine("What is the list price you would like to change to?");
                            decimal price = decimal.Parse(Console.ReadLine());

                            System.Console.WriteLine($"Your current email is {myCondo[i].GetOwnerEmail()}");
                            System.Console.WriteLine("What is the email you would like to change to?");
                            string email = Console.ReadLine();

                            System.Console.WriteLine($"Your Condo avaliability is {myCondo[i].GetAvaliable()}");
                            System.Console.WriteLine("What is the avaliability you would like to change to? true or false");
                            string aval = Console.ReadLine();

                            myCondo[i].SetAvaliable(userChoice);
                            myCondo[i].SetAddress(address);
                            myCondo[i].SetListingEndDate(date);
                            myCondo[i].SetListPrice(price);
                            myCondo[i].SetOwnerEmail(email);
                            myCondo[i].SetAvaliable(aval);
                            System.Console.WriteLine("Successfully updated!");
                            System.Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();

                        }                        
                        catch (System.Exception e)
                        {
                            System.Console.WriteLine(e.Message);
                            System.Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                        }
                    
                    }
                    else if (userChoice == "2") //changes a specific field of a condo
                    {
                        System.Console.WriteLine("1: for address" + "\n2: Listing end date" + "\n3: List Price" + "\n4: Owner Email" + "\n5: Avalability");
                        userChoice = Console.ReadLine();
                        if (userChoice == "1")
                        {
                            try
                            {
                                System.Console.WriteLine($"Your current address is {myCondo[i].GetAddress()}");
                                System.Console.WriteLine("What is the address you would like to change to?");
                                userChoice = Console.ReadLine();
                                myCondo[i].SetAddress(userChoice);
                                System.Console.WriteLine("Successfully updated!");
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            catch (System.Exception e)
                            {
                                System.Console.WriteLine(e.Message);
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else if(userChoice == "2")
                        {
                            try
                            {
                                System.Console.WriteLine($"Your current Listing End Date is {myCondo[i].GetListingEndDate()}");
                                System.Console.WriteLine("What is the listing end date you would like to change to? Format: 00/00/00");
                                date = DateTime.Parse(Console.ReadLine());
                                myCondo[i].SetListingEndDate(date);
                                System.Console.WriteLine("Successfully updated!");
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            catch (System.Exception e)
                            {
                                System.Console.WriteLine(e.Message);
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else if(userChoice == "3")
                        {
                            try
                            {
                                System.Console.WriteLine($"Your current List Price is {myCondo[i].GetListPrice()}");
                                System.Console.WriteLine("What is the list price you would like to change to?");
                                decimal price = decimal.Parse(Console.ReadLine());
                                myCondo[i].SetListPrice(price);
                                System.Console.WriteLine("Successfully updated!");
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            catch (System.Exception e)
                            {
                                System.Console.WriteLine(e.Message);
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else if(userChoice == "4")
                        {
                            try
                            {
                                System.Console.WriteLine($"Your current email is {myCondo[i].GetOwnerEmail()}");
                                System.Console.WriteLine("What is the email you would like to change to?");
                                userChoice = Console.ReadLine();
                                myCondo[i].SetOwnerEmail(userChoice);
                                System.Console.WriteLine("Successfully updated!");
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            catch (System.Exception e)
                            {
                                System.Console.WriteLine(e.Message);
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else if(userChoice == "5")
                        {
                            try
                            {
                                System.Console.WriteLine($"Your Condo avaliability is {myCondo[i].GetAvaliable()}");
                                System.Console.WriteLine("What is the avaliability you would like to change to? true or false");
                                userChoice = Console.ReadLine();
                                myCondo[i].SetAvaliable(userChoice);
                                System.Console.WriteLine("Successfully updated!");
                                System.Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                            catch (System.Exception e)
                            {
                               System.Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("1 - 5 was not selected returning to menu. Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                    }
                    else
                    {
                        System.Console.WriteLine("1 or 2 was not entered returning to menu. Press any key to continue...");
                        Console.ReadKey();
                        break;
                    }

                }
            }
            using(StreamWriter output = new StreamWriter("listings.txt")) //writes the changes of the condo to the txt file
            {
                for (var i = 0; i < Condo.GetCount(); i++)
                {
                    if (i == Condo.GetCount() - 1) //determines if it needs to leave spaces or not
                    {
                        output.Write(myCondo[i].GetListingID() + "#" + myCondo[i].GetAddress() + "#" + myCondo[i].GetListingEndDate() + "#" + myCondo[i].GetListPrice() + "#" + myCondo[i].GetOwnerEmail() + "#" + myCondo[i].GetAvaliable());
                    }
                    else
                    {
                        output.WriteLine(myCondo[i].GetListingID() + "#" + myCondo[i].GetAddress() + "#" + myCondo[i].GetListingEndDate() + "#" + myCondo[i].GetListPrice() + "#" + myCondo[i].GetOwnerEmail() + "#" + myCondo[i].GetAvaliable());
                    }
                    
                }
                output.Close();
            }
        }
        public void SortByID() //sort condos by ID
        {
            for (int i = 0; i < Condo.GetCount() - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < Condo.GetCount(); j++) //checks ID number to make sure they are in order
                {
                    if (myCondo[j].CompareTo(myCondo[min]) < 0)
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    Swap(min, i);
                }
            }
        }
        public void Swap(int x, int y) //if they are not in order it swaps them until they are
        {
            Condo temp = myCondo[x];
            myCondo[x] = myCondo[y];
            myCondo[y] = temp;
        }
        
    }
}