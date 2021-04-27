using System.IO;
using System;

namespace Vacation_Rental
{
    class Program
    {
        static void Main(string[] args)
        {
            int userChoice = 0;
            int userReport = 0;

            Condo [] myCondo = new Condo[500];
            CondoFile myFile = new CondoFile("listings.txt");
            myCondo = myFile.GetAllCondos();
            CondoReport myReport = new CondoReport(myCondo);

            CondoRenting [] myRent = new CondoRenting[500];
            RentingFile rentFile = new RentingFile("transactions.txt");
            myRent = rentFile.GetCondoRentings();
            RentReport reportRent = new RentReport(myRent);

            userChoice = MenuDisplay();//calls the method to choose a menu option
            
                while (userChoice != 6) //ends if user enters 6
                {
                    if (userChoice == 1)
                    {
                        myFile.AddCondo();
                        myReport.GetCondos();
                        userChoice = MenuDisplay();
                    }
                    else if(userChoice == 2)
                    {
                        myReport.SortByID();
                        myReport.PrintAllCondos();
                        myReport.EditCondo();
                        myReport.GetCondos();
                        userChoice = MenuDisplay();
                    }
                    else if(userChoice == 3)
                    {
                        myReport.SortByID();
                        myReport.PrintAllCondos();
                        myReport.DeleteCondo();
                        myReport.GetCondos();
                        userChoice = MenuDisplay();
                    }
                    else if(userChoice == 4)
                    {
                        myReport.SortByID();
                        myReport.PrintAvalCondos();
                        reportRent.RentCondo();
                        myReport.SetAvalCondo();
                        reportRent.GetCondos();
                        userChoice = MenuDisplay();

                    }
                    else if(userChoice == 5)
                    {
                        userReport = ReportsMenu();
                        if (userReport == 1)
                        {
                            reportRent.GetCondos();
                            reportRent.IndCustomerRent();
                        }
                        else if(userReport == 2)
                        {
                            reportRent.GetCondos();
                            reportRent.HistCustomerRent();
                        }
                        else if(userReport == 3)
                        {
                            reportRent.GetCondos();
                            reportRent.HistRevenueReport();
                        }
                        else if(userReport != 1 && userReport != 2 && userReport!= 3) //error checking
                        {
                            System.Console.WriteLine("Invalid choice. Press any key to continue");
                            Console.ReadKey();
                            userReport = ReportsMenu();
                        }
                        userChoice = MenuDisplay();
                    }
                    else if(userChoice != 1 && userChoice != 2 && userChoice != 3 && userChoice != 4 && userChoice != 5 && userChoice != 6) //error checking
                    {
                        System.Console.WriteLine("Invalid choice. Press any key to continue");
                        Console.ReadKey();
                        userChoice = MenuDisplay();
                    }
                
            }
        }
        public static int MenuDisplay()
        {
            int userChoice = 0;
            try //makes sure the user inputs a valid input
            {
                Console.Clear();
                System.Console.WriteLine($"1: Add Listing \n2: Edit Listing \n3: Delete Listing \n4: Lease Condo \n5: Run Reports \n6: Exit");
                userChoice = int.Parse(Console.ReadLine());
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return userChoice;
        }
        public static int ReportsMenu()
        {
            int userReport = 0;
            try //catch any invalid input
            {
                Console.WriteLine("Please select the report you wish to view: ");
                Console.WriteLine("Input 1 for Individual Customer Rentals.");
                Console.WriteLine("Input 2 for Historical Customer Rentals.");
                Console.WriteLine("Input 3 for Historical Revenue Rentals.");

                userReport = int.Parse(Console.ReadLine());
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            return userReport;
        }
    }
}
