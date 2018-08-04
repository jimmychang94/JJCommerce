# Jimmy and Judah Commerce 

## Products
We sell furniture of all kinds which are grouped into 9 basic categories:  
1. Chairs  
2. Beds  
3. Couches  
4. Dressers  
5. Shelves  
6. Tables  
7. Cabinets
8. Desks
9. Other  

Other is used for any furniture that does not fit the previous 8 categories.  

## Claims
When someone registers onto our site, we capture 3 claims:  
1. Name  
a. We have a claim for your name in order to personalize the site to you  
2. Email  
a. We have a claim for your email to store it in an easy to access location  
3. Location  
a. We have a claim for your location to give promotions for specific locations where we are located  

## Policies
We are enforcing 2 policies:  
1. Admin Only  
a. We are enforcing this policy because we need the ability to edit our inventory 
but not give access to a regular member  
(This way a random person can't "delete" all of our stock)  
2. Location
a. We are enforcing this policy so we can give promotions to specific locations 
as we our promotions only apply to certain locations  
3. Member Only  
a. We are enforcing this policy so that only members can purchase our products.  

## 3rd Party OAuth  
Microsoft  
Google  

## Product Database Schema/Explanation  
We have a products table to store all the products.  
This stores:  
  -  Product ID  
  -  Product Sku  
  -  Product Name  
  -  Product Price  
  -  Product Image  
  -  Product Description  
  -  Product Category  

We have a basket table to store every basket made by every user in our user database.  
This stores:  
  -  Basket ID  
  -  User Id  
  -  List of Basket Items  

We have a basket item table to store the references to the products and the baskets 
as well as hold the quantity of the product that the user wishes to buy.  
This stores:  
  -  Basket Item ID
  -  Basket ID  
  -  Product  
  -  Product ID
  -  Quantity  

## Our website:  
Please [click here](https://jjcommerce.azurewebsites.net/) to access our site.  
If that doesn't work, go to: 
https://jjcommerce.azurewebsites.net/  
and you will be brought to the same place.  

## Vulnerabilty Report  
Our vulnerability report can be found [here](Vulnerability-Report.md)  

## Contributors
Jimmy Chang  
Judah Hunger  