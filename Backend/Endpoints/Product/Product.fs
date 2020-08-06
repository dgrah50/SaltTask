namespace Product

[<CLIMutable>]
type Product =
    { _id: string
      url: string
      creator: string
      created_t: string
      last_modified_t: string
      product_name: string
      generic_name: string
      serving_size: string
      image_url: string
      countries: string
      pnns_groups_1: string
      brands: string
      ingredients_text: string
      categories_tags: string
      labels: string
      quantity: string 
      energy_100g: string
      fat_100g: string
      carbohydrates_100g: string
      sugars_100g: string
      salt_100g: string
      proteins_100g: string
      }

type ProductSave = Product -> Product

type ProductCriteria =
    { query: string
      field: string
      page: int }

type ProductFind = ProductCriteria -> Product []

type ProductDelete = string -> bool
