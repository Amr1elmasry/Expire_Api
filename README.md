# Expire_Api

It's API APP that helps markets owners to manage their inventories and reminds them about the expiry date of products that will come soon


# Currant Endpoints 

## Auth
```http
POST /api/Auth/Register
POST /api/Auth/Login
POST /api/Auth/AddNewRole
 ```

## Product
```http
GET /api/Product/GetProductsInMarket
GET /api/Product/GetProductsInMarketWithData
POST /api/Product/AddProductToMarket
```

## Category
```http
GET /api/Category/GetAll
GET /api/Category/GetAllWithData
GET /api/Category/FindById
GET /api/Category/FindByIdWithData
GET /api/Category/GetCategoriesOfMarket
GET /api/Category/GetCategoriesOfMarketWithData
POST /api/Category/AddCategoryToMarket
PUT /api/Category/UpdateCategory
DELETE /api/Category/DeleteCategory
```

## Market
```http
GET /api/Market/GetAll
GET /api/Market/GetAllWithData
GET /api/Market/FindById 
GET /api/Market/FindByIdWithData
GET /api/Market/GetMarketsOfSeller
GET /api/Market/GetMarketsOfSellerWithData
POST /api/Market/AddMarket
PUT /api/Market/UpdateMarket
DELETE /api/Market/DeleteMarket
```

## Seller
```http
GET /api/Seller/GetSellerById
GET /api/Seller/GetSellerByIdWithData
GET /api/Seller/GetAllSellers
GET /api/Seller/GetAllSellersWithData
```
