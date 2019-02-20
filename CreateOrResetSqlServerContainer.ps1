#Variables
$Image= "mcr.microsoft.com/mssql/server:2017-CU9-ubuntu"
$Container = "HomebankDevSqlServer"
$Password = "H0mebankPassw0rd!"
$Database = "Homebank"

#The first item is the header row so check for == 2
if((docker ps -f name=$Container).Count -eq 2){
    docker stop $Container
    docker rm $Container
}

#The first item is the header row so check for == 1
if((docker image ls $Image).Count -eq 1){
    docker pull $Image
}

#Start container
docker run --restart unless-stopped --name $Container -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$Password" -e "MSSQL_PID=Standard" -p 1433:1433 -d $Image

#Create DB
docker exec -it $Container /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $Password -Q "CREATE DATABASE $Database" -l 60