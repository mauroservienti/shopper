### User front end, catalog

* incoming request /products/1ir
* go to sales and ask for *published* product 1
* build the dto

```
Product
{
   int Id,
   dynamic ViewModel
}
```

* enumerate all IEnumerable<IProductsVisitor>
* let them do the job

```
Task IProductsVisitor.VisitAsync( IEnumerable<Product> products )
```