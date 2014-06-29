Homebank
========

A simple web based accounting tool to administrate your income and expenses.

There are numerous tools to administrate your income and expenses but most of them are desktop applications or just too heavyweight for standard users. This tool comes as a website and is therefore available wherever you go. Insert your expenses directly after your shopping tour and never forget what you've bought!

As it doesn't support double entry accounting it's suitable for standard users who are only interested in seeing how much they spent for something and how much is left.

Installation
------------

- Download and publish the project
- Create an empty database (currently only SQL Server 2012 is supported)
- Configure IIS to point to the published files
- Modify the connection string in the web.config file
- Open the web page in your browser (database migration is automatically done on the first request)


Todo
----
- Mobile views
