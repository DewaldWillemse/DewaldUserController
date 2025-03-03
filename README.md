![Head Logo](documents/readmeHeader.png)
# Welcome to this project
## Objectives of this project
Please note the following, this project is the Assessment Test Project for Junior applicants, as you work through this project you will see that most of the working project is here, we remove parts of the application, your objective is to analyze the application and fill in the missing parts.

The database part is not provided because it lives on your dev station, you should be able to analyze the application and design your database accordingly. 

This test will test your skills in the following way:
1. css/sass Knowledge
2. html Knowledge
3. typescript Knowledge
4. .net Knowledge
5. database Knowledge
6. Debugging Knowledge

# The Process
1. You will receive a email, with a git link. 
2. Create a Git Repo (your own) and ensure that the project becomes part of it,
3. Open and Install webapp packages and run application, in browser navigate to given url,
4. Open api restore the nuGet packages, rebuild, once complete Success run application,
5. Navigate to the api/UserControlApi/Scripts, run the queries in the manager to create the components,
6. Investigate and Debug the application, find the missing parts and fill in.
7. Create your own Git Readme.md in git and promote your work,
8. On Completion send us the git repo link of delivery and inspection,
9. If you are late you will be disqualified,


# OR 

You may decide to pack this all on and do your own thing but, your scope here will be to learn our way of doing things so we would not recommend that but, we are doing this to measure your skill level.

Following, is examples of the app, ensure that the app looks the same with functionality.<br>
You will have 4 days to complete it, from the day you received the mail.<br>
You may use any AI that you need to complete this Assessment Test.<br>

Good Luck!!! And have fun!!!

## You will learn the following
### Angular Web-ui
The learner will be exposed to and required to learn the following disciplines:
1. css
2. html
3. typescript
4. http-client
5. modeling

### Web-api .Net
The learner will be exposed to and required to learn the following disciplines:
1. Web-api setup and initiation
2. Controller Principals,
3. Service Principals,
4. DataBase Principals,

### DabaBase MSSQL
The learner will be exposed to and required to learn the following disciplines:
1. Table Structure Principals
2. Stored Procedure Principals

## Your Tasks and Targets
### Angular 
1. Create a new WebApi in your root directory
2. Create a components/Header component
3. Create a pages/home component
3. Create a pages/application component
3. Create a pages/components/userlist component
3. Create a pages/components/userdetails component

## Home Page / User Login
![HomeLoginPage](documents/homeLoginPage.png)

### Focus on the following
1. The login box must always remain in the middle of he page,
2. Ensure framing is done for final visual objects,
3. Use sass for all of the above,
4. All input Boxes to be linked with variables,

## Home Page / User Register
![HomeRegisterPage](documents/homeRegisterPage.png)

### Focus on the following
1. The login box must always remain in the middle of he page,
2. Ensure framing is done for final visual objects,
3. Use sass for all of the above,
4. All input Boxes to be linked with variables,

## Application
![ApplicationBasePage](documents/applicationHomePage.png)
### Focus on the following
1. Build header,
2. Left side buttons navigation,
3. Navigation to be done by changing var values, no rooting,
4. Right side, housing for target page

## UserList Page
![UserListPage](documents/userListPage.png)
### Focus on the following
1. Navigate Admin -> Users -> open UserList Page,
2. Create a Header and a Table object,
3. Create a Previous and Next Indexing Control,
4. Only Collect rows as per visible spacing for rows,
5. You may use mat-icon for icons, the rest must be self designed,

## UserList Page
![UserProfilePage](documents/userDetailsPage.png)
### Focus on the following
1. You need a view icon to go into Profile view, this will take away the update and delete button,
2. In edit profile mode the buttons will be visible,

# Your BackEnd Requirments
# The Controller and Functions
![bckendController](documents/bckendController.png)
### Focus on the following
1. These are the functions that we require,
2. Create a Interface for the services and imbed it into the Controller,
3. Inter Communication to take place with a ServiceModel,

# The IUserServices and Functions
![bckendService](documents/iservicesLayout.png)

# The IDBUserServices and Functions
![bckendDBService](documents/idbServicesLayout.png)

# User Security
You will see that we are creating a AES256 Encrypted JWT, it is a bit lazy as to only use the password, cracking the encryption will then also be easier, in this application we are making use of the whole model of the user when registering, so we create a json object and then encrypt, to be a little bit more cheeky, the system uses a sanitized epoc value as the key and iv for the encryption, this epoc also makes up the userId column, you will see we sanitize the userId, extract the epoc value and feed the iv with it.

As we know the passphrase is a set byte[32] and the iv is a set byte[16], so how this works, we have a key in our config file "appsettings.json" -> "PrimaryLinkSection", we collect this and all the epoc value to the start, we then for passphrase substring to 32 and for the iv substring to 16, so we only have one entry point for a secret value and the user or a person does not know what is looking at, the epoc value in the db is the dynamic missing part that only internal developers knows about.

Have a look, do some research and enjoy.

# Bonus Points
The candidate will succeed if the application works, to give you the proverbial browny points, get the application to deploy in IIS, better yet if you can Linux in Docker. The Api already has a certificate, we can see this all through the application, tip go have a look at the webapp/assets/services/rest.service.ts we have a isDevelopment key when set to true we can have this ready for development and debugging, if set to false it is ready for IIS, your docker is a bit more complicated or is it? With docker I will suggest to do a .net runtime env, nginx webservice and a Docker MSSQL, keep in mind here that the ConnectionString should then talk to the container name and not the ip address. Just some ideas.

maybe to complicated give it a try.

If you are good with debugging techniques and your are willing to try and not give up this should not be that difficult.
