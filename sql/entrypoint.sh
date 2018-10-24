#wait for the SQL Server to come up
sleep 10

# run the init script to create the DB and the tables in /table
/opt/mssql-tools/bin/sqlcmd -U sa -P "KDH4aqZCT3ZBdrf7" -i ./init.sql