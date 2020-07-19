# breachapi
The API is based on https://haveibeenpwned.com/
It supports request for checking, if an email address is present in the breached address database. 

The solution consists of 3 projects: 
- BreachApi: The main application 
- BreachApi.IntegrationTest: The integration tests for API (sends Http requests and validates the results)
- BreachApi.Test: Contains the unit tests for Api controller. 

The BreachApi has three endpoints: 
- Post: Used to insert emails into the database (Returns Created or Conflict, if the email already exists)
- Get: Checks, if an email is in the database (returns Ok if email is in database or NotFound, it it's not)
- Delete: Removes an email address from the database. Returns Ok if it executes successfuly. 


To set up the API, you have to follow these steps: 
- Provide an Sql Server with an emtpy database
- Use the DbInit.sql script to create the required database schema
- Adjust the appsettings.json file, that is located in the BreachApi project. Set connection string for the database according to your server configuration
- Publish the solution
- Deploy the application to an IIS server. 

Use cases: 
query for breached emails:
-  HTTP GET /brechedemails/user@geneplanet.com Expected responses: NotFound or OK
- add a breached email HTTP POST /brechedemails/ Expected responses: Created or Conflict
- remove the breached email: HTTP DELETE /brechedemails/user@geneplanet.com Expected responses: Ok
