using System.Net.Mime;
using System.IO;
using System;
namespace Vacation_Rental
{
    public class Condo //Condo Class to store all requirements
    {
        private int listingID;
        private string address;
        private DateTime listingEndDate = DateTime.Now.Date; //use date time variable to make accurate comparisions
        private decimal listPrice;
        public string ownerEmail;
        private static int count;

        private string avaliable;

        public Condo(int listingID, string address, DateTime listingEndDate, decimal listPrice, string ownerEmail, string availiable)
        {
            this.listingID =listingID;
            this.address = address;
            this.listingEndDate = listingEndDate;
            this.listPrice =listPrice;
            this.ownerEmail =ownerEmail;
            this.avaliable = availiable;
        }
        public void SetAvaliable(string avaliable)
        {
            this.avaliable = avaliable;
        }
        public string GetAvaliable()
        {
            return avaliable;
        }
        public static void SetCount(int count)
        {
            Condo.count = count;
        }
        public static int GetCount()
        {
            return count;
        }
        public static void IncCount()
        {
            count++;
        }

        public void SetListingID(int listingID)
        {
            this.listingID = listingID;
        }
        public int GetListingID()
        {
            return listingID;
        }
        public void SetAddress(string address)
        {
            this.address = address;
        }
        public string GetAddress()
        {
            return address;
        }
        public void SetListingEndDate(DateTime listingEndDate)
        {
            this.listingEndDate = listingEndDate;
        }
        public DateTime GetListingEndDate()
        {
            return listingEndDate;
        }
        public void SetListPrice(decimal listPrice)
        {
            this.listPrice = listPrice;
        }
        public decimal GetListPrice()
        {
            return listPrice;
        }
        public void SetOwnerEmail(string ownerEmail)
        {
            this.ownerEmail = ownerEmail;
        }
        public string GetOwnerEmail()
        {
            return ownerEmail;
        }
        public override string ToString()
        {
            return "Listing ID: " + listingID + "\nAddress: " + address +"\nListing End Date: " + listingEndDate + "\nList Price: $" + listPrice + "\nOwner Email: " + ownerEmail +"\nAvaliable: " + avaliable + "\n";
        }
        public int CompareTo(Condo compareCondo)
        {
            return this.listingID.CompareTo(compareCondo.GetListingID());
        }
        
    }
    
}