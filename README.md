### Useful commands

To create migration:
dotnet ef -v migrations add --project src/DAL/Fawn.DAL.EFCore/ --startup-project src/DAL/Fawn.DAL.EFCore/ <MIGRATION_NAME>

To update database to the last migration:
dotnet ef -v database update --project src/DAL/Fawn.DAL.EFCore/ --startup-project src/DAL/Fawn.DAL.EFCore/

To run all application in development mode:
docker-compose up

### Useful links

(https://runnable.com/docker/advanced-docker-compose-configuration)[Advanced Docker compose configuration]
