// /* global angular */
// function HomeShowcase(showcaseModel) {
//         var _readModel = showcaseModel;
//         var _showcaseProducts = [];
//         angular.forEach(_readModel.showcaseStockItemIds, function(value, key){
//             _showcaseProducts.push({stockItemId: value});
//         });

//         var _headlineProduct = {
//             stockItemId: _readModel.headlineStockItemId,
//         };

//         Object.defineProperty(this, 'dataType', {
//             get: function () {
//                 return 'home-showcase';
//             }
//         });

//         Object.defineProperty(this, 'showcaseProducts', {
//             get: function () {
//                 return _showcaseProducts;
//             }
//         });

//         Object.defineProperty(this, 'headlineProduct', {
//             get: function () {
//                 return _headlineProduct;
//             }
//         });
//     };