How to install the whole things?

Change all the ex_ file ext to exe due to the security issue of mail attachment.

-Database: In SQLServer folder
	i.   There is a SQL script you can run to create DB/table as well as user to access it. 
	ii.  Since the security consideration, the user created is with a random password and disabled. You need to change the password and status in SSMS by yourself.
	iii. I am using Mixed authentication mode of SQL server and using a uaername/password to access the DB. If need to be just Windows Auth, you need to change setting for WCF service running. This will be mentioned later.
	iv.  Any potential naming confition is not settled. Youn may need to handle it.

-WCF Service: In WCF folder
	i.   Open VS development CMD and go to the WCF service folder.
	ii.  run "installutil WCFServiceHost.exe"
	iii. run "net start VehicleWCFService"
	iv.  You can check the config file for the base url of WCF service and connection string to SQL server for any modification needed.
	v.   If Windows Auth to DB is needed, change the connection string for integrated security and also need to stop the service and change the running user and restart again. By default it is running as Local System.

-WPF: In WPF folder
	i.   Open config file to see if the endpoint for WCF need any modification.
	ii.  Run the exe directly.

-Source code is in the SourceCode folder.