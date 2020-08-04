namespace Cart
open Product


type CartSave = Product -> Product


type CartCriteria =
  | All

type CartFind = CartCriteria -> Product[]

type CartDelete = string -> bool