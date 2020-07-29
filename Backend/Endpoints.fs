
// /product
//   GET / - List of products
//   POST / - Add product - required : String name , String groupId, String userId
//   GET /{id} - View product
//   POST /{id} - Update product
//   GET /{id}/images - View product images
//   GET /image/{id}- View image
//   POST /{id}/uploadimage - Upload product image


// /order
//   GET / - List of orders
//   POST / - Add order
//   GET /{id} - View order
//   POST /{id} - Update order

// /cart
//   POST / - Create cart
//   GET /{id} - Get items for card with ID = {id}
//   POST /{id} - Add CartItem to cart with ID {id}
//   DELETE /{id}/{product_id} - Remove product with ID {product_id} from cart with ID {id}
//   POST /{id}/quantity - Updates cart item, i.e. set product quantity
//   POTS /{id}/order - Create order from cart