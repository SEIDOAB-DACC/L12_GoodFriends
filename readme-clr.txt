To create the L12_GoodFriends

1. With Terminal in folder .scripts 
   macOs run: ./database-rebuild-all.sh
   Windows run: .\database-rebuild-all.ps1
   Ensure no errors from build, migration or database update

2. From Azure Data Studio you can now connect to the database
   Use connection string from user secrets:
   connection string corresponding to key
   "SQLServer-goodfriendsefc-docker-sysadmin"

   Verify application execution
   The only controller implemented is Health->Heartbeat

3. Run AppGoodFriendsWebApi with or without debugger
   Without debugger: Opeb a Terminal in folder AppGoodFriendsWebApi run: 
   dotnet run -lp https 

4. Use Azure Data Studio to execute SQL script DbContext/SqlScripts/initDatabase.sql

5. Use endpoint Admin->SeedUser to seed the database with users

6. Use endpoint Guest->LoginUser
{
  "userNameOrEmail": "superuser1",
  "password": "superuser1"
}

7. Copy and paste the (Copy and paste the string corresponding to ... below)
   "encryptedToken": "...."
   into Swagger Authorize.  

8. Use endpoint Admin->Seed to seed the database


