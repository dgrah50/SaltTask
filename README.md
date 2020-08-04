# SaltTask

## Backend

### Prerequirements

export MONGO_URL=mongodb://localhost:27017/

mongoimport --type tsv  --db products -c products --ignoreBlanks --fieldFile=fieldtypes.txt --columnsHaveTypes --drop openfoodfacts.csv

openfoodfacts.csv has had the 'code' column changed to _id 