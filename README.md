# AspTutorial

This is a fully functional E-commerce website that was created to learn the basics of Asp.Net

Added functions;

-Sign up and login for users
-User.aspx page to edit user info
-Shop.aspx page to take data from db to show products
-Filtering, paging and searching term for shop.aspx page with url queries
-Filters for shop.aspx page
-Sponsored products for shop.aspx page
  This feature takes a value from db called SponsorValue then sorts the products depending on that value
  in that way if there is a sponsor for a certain product that product will show up on top
-Product.aspx page that takes data from db and show it with image carousel and specifications of the product
-Comments and commentlikes functions for product.aspx for users to write comments and/or like-dislike a comment
-ProductFavorite function that works with both db and cookies if there is no user signed in it is for users to add products to their favorites list
-ShoppingCart functions that works with both db and cookies if there is no user signed in to add products to cart
-CheckOut.aspx page for users to see their shopping cart products and to place an order
  Place order button reduces the stock of the specific product with the amount that the user wants
-Orders.aspx page to track the given orders to that time from the signed-in user such as product name, quantity, total price, date-time etc.
