# Salt eCommerce Task

This is a task for Salt in which a basic clone of an ecommerce website has been made. One can view products and add/remove them from the cart. One can search for products by name.
![Salt eCommerce Preview](https://i.ibb.co/71D3XCW/Screenshot-2020-08-12-at-18-45-45.png)

---

## Backend

The backend was written with the help of the following technologies:

- F#
- Giraffe
- MongoDB

---

## Frontend

The frontend was written with the help of the following technologies:

- JavaScript
- React
- Redux
- Ant Design

---

### How to run

This project requires the installation of MongoDB, a dotnet environment and a JS package manager such as npm or yarn.

- Clone this repo to your desktop
- Export the MongoDB environment variable
- `export MONGO_URL=mongodb://localhost:27017/`
- Import the CSV file to MongoDB, openfoodfacts.csv has had the 'code' column changed to \_id
- `mongoimport --type tsv --db products -c products --ignoreBlanks --fieldFile=fieldtypes.txt --columnsHaveTypes --drop openfoodfacts.csv`
- cd into the Backend folder and start the F# API
- `dotnet run`
- cd into the Frontend/salt-frontend/ and start the frontend
- First `yarn install`
- Then `yarn start`
