# GroceryCo
Data is stored in json and txt files in the console application. Files can be edited to add ore remove items. 

*products.json*: Has product information. 
*promotions.json*: Has promotion infomation
*items.txt*: Basket of items to checkout

# Project structure
- GroceryCo.Console: Has the console application. Presentaion layer
- GroceryCo.Data: Data and repository layer
    * Models: All domain entities.
    * Repositories: Used to access model storage.
    * FileReaders: Reading text files.
    * Serialization: For deserializing strings to domain objects.
- GroceryCo.Service: Service layer
    * Exceptions: Custom exceptions.
    * Models: Service layer models.
    * Promotions: Applying promotions to sales

# Business Logic & Design choice
- Repository pattern is used to access data. Concreate repository classes can be reimplemented to change data store from file system to database.
- Service layer acts as a bridge between data and presentation layer. Business logic is implemented here.
- Pricing strategy is used to determine whether to chose the lowest or highest prices when there are multiple active promotions.

# Assumption & Limitation
- Name is used as the key to the products and promotions data. Names are not case sensitive. 
- Each product can have multiple active promotions and the pricing stategy will be used to determine whether to pick the lowest or highest price.
- Data is not validated and it is assumed that:
    * Product and promotion names will be unique.
    * Promotion data is correctly entered and promo prices are greater than zero and less than actual prices of products.

# NuGet
- Newtonsoft.Json: Parsing json
- Moq: Unit test mocking 
