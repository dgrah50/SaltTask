namespace Cart

open Product

[<CLIMutable>]
type CartItem =
    { _id: string
      cartquantity: int
      product: Product }

type CartSave = CartItem -> CartItem

type CartCriteria = | All

type CartFind = CartCriteria -> CartItem []

type CartDelete = string -> bool
