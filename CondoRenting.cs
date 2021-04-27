using System;
using System.Net.Http;
using System.Globalization;
namespace Vacation_Rental
{
    public class CondoRenting //class to store all customer information
    {
        private int listingID;
        private string ownerEmail;
        private string rentName;
        private string rentEmail;
        private DateTime rentDate = DateTime.Now.Date;
        private decimal rentAmt;
        private DateTime checkDate = DateTime.Now.Date;

        public static int rentCount;

        public CondoRenting(int listingID,string rentName, string rentEmail, DateTime rentDate, decimal rentAmt, DateTime checkDate, string ownerEmail)
        {
            this.listingID = listingID;
            this.rentName = rentName;
            this.rentEmail = rentEmail;
            this.rentDate = rentDate;
            this.rentAmt = rentAmt;
            this.checkDate = checkDate;
            this.ownerEmail = ownerEmail;
        }
        public static void SetRentCount(int rentCount)
        {
            CondoRenting.rentCount = rentCount;
        }
        public static int GetRentCount()
        {
            return rentCount;
        }
        public static void IncRentCount()
        {
            rentCount++;
        }
        public void SetRentEmail(string rentEmail)
        {
            this.rentEmail = rentEmail;
        }
        public string GetRentEmail()
        {
            return rentEmail;
        }
        public void SetListingID(int listingID)
        {
            this.listingID = listingID;
        }
        public int GetListingID()
        {
            return listingID;
        }
        public void SetOwnerEmail(string ownerEmail)
        {
            this.ownerEmail = ownerEmail;
        }
        public string GetOwnerEmail()
        {
            return ownerEmail;
        }
        public void SetRentName(string rentName)
        {
            this.rentName = rentName;
        }
        public string GetRentName()
        {
            return rentName;
        }
        public void SetRentDate(DateTime rentDate)
        {
            this.rentDate = rentDate;
        }
        public DateTime GetRentDate()
        {
            return rentDate;
        }
        public void SetRentAmt(decimal rentAmt)
        {
            this.rentAmt = rentAmt;
        }
        public decimal GetRentAmt()
        {
            return rentAmt;
        }
        public void SetCheckDate(DateTime checkDate)
        {
            this.checkDate = checkDate;
        }
        public DateTime GetCheckDate()
        {
            return checkDate;
        }
        public override string ToString() 
        {
            return "Listing ID: " + listingID + "\nRentee Name: " + rentName + "\nRent Email: " + rentEmail + "\nRent Date: "  + rentDate + "\nRent Amount: $" + rentAmt + "\nCheckout Date: " + checkDate + "\nOwner Email: " + ownerEmail + "\n";
        }
        public int CompareTo(CondoRenting compareCondo) //used to compare names then date
        {
            
            return this.rentName.CompareTo(compareCondo.GetRentName()) + this.rentDate.CompareTo(compareCondo.GetRentDate()); 
            
        }
        
    }
}