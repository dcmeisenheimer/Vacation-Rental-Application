## Welcome-------------------------------
My Menu System has 6 options
1. Allows an owner to add a condo to lease
2. Allows an owner to edit any condo
3. Allows an owner to delete any condo
4. Allows a customer to rent a condo
5. Has 3 Options
  - 1: Has the indiviual Report of a customer
    - 2: Has the Historical Customer Report of all customers sorted by name then date
      - 3: Tracks Revenue by month and year
6. Exits the Application

## Formatting---------------------------
- #### **All Listings made by owners are stored in listings.txt**
- Format for Listings Example: *1#400 East Edge Drive#8/9/2021 12:00:00 AM#450#test123@gmail.com#false*
- Format Explained: Each Item is delimited by '#' 
  - 1 = Listing ID number
  - 400 East Edge Drive = Address of Condo
  - 8/9/2021 12:00:00 AM = Listing End Date
  - 450 = Listing Price
  - test123@gmail.com = owner email
  - false = availability status

- #### **All Transactions made by customers are stored in transactions.txt**
- Format for Transactions Example: *3#Richard Ellito#rEllito@hotmail.com#5/15/2021 12:00:00 AM#300#5/21/2021 12:00:00 AM#hotrock@gmail.com*
- Format Explained: Each item is delimited by '#'
  - 3 = Listing ID Number
  - Richard Ellito = Rentee Name
  - rEllito@hotmail.com = Rentee Email
  - 5/15/2021 12:00:00 AM = Check In Date
  - 300 = Price to rent
  - 5/21/2021 12:00:00 AM = Check Out Date
  - hotrock@gmail.com = Owner Email
  
###### All reports can be saved to a text file and text files need to be named exactly the same for transactions and listings
