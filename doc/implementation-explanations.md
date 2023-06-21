
*I also left comments in place in the code base to provide specific  explanations* 

# Environment setup
*You can run solution with no extra settings so in memory db will be used*

## Switch to MSSQL server
* In *appsettings.json* change connection string *ConnectionStrings:DefaultConnection* to a MSSQL DB of your choice or leave it by default
* In *appsettings.json* change flag *UseInMemoryDatabase* to *false*
* Make sure the SQL Server of your choice *doesn't* have *Coupon* DB
   * DB and the schema will be created automatically 
   * if you don't change schema or domain model than for the next run you may keep same db and use its advantage 

# Explaining decisions
## Storage 
EF + in memory storage used to make local development/test easy

EF + MSSQL storage used to emulate production (see instructions in the [README](../README.md) how to set it up). I chose SQL server as for me it is the easiest and most familiar storage technology, it is also good approach to support Domain consistency and it is easy to spin on local dev env with Visual Studio.

Speaking of Azure technologies for the initial implementation I would prefer *Azure Table Storage* as it simple, fast, cheep, supports optimistic lock. Three things stoped me of choosing Table Storage: need of indexes, complexity of implementation, possible complexity to run Table Storage locally from emulator. 

## Caching strategy 
I don't have metrics how API is used (GET, PUT ratio and frequency). Also Coupon PUT endpoint/Domain is not reliable for extensive caching as system can accept Usages as plain integer, but requirements forces to increment/decrement by ONE only, so extensive period of caching may introduce significant error rate for PUT. 

I decided to use short expiration  to minimize PUT possible issues.
I decided to use Output Caching strategy as it is easy to implement compared to InMemoryCaching and
I don't expect clients to store/utilize coupons for a long time (Response Caching could be better here)
 but I expect different new clients to fetch coupons

# Furter improvements

## API endpoints
I would split Create/Update endpoint and implement them by REST guidelines.

## Authentication
API key is a nice approach for internal communication but having secrets directly in app config is a bad practice as more people have or could have access to security information the higher chance of the application to be hacked. The least I would do move the API key secret to Azure Key Vault and narrow down access from the whole team to only 'Security team'

## Configuration
For this simple app it might be overkill and I don't know use cases but as a point for **potential** improvement I can recommend *Azure App Configuration*: easy, fast and flexible way to manage service configurations as well it can aggregate configurations for mere than one service.  

## Domain
When domain start complicating I would use DomainEvents, UnitOfWork as those things will enable to keep logic for different Aggregates separate, but on contrary it will complicate over code base.

## Testing
For better coverage I would introduce more tests:
- (might use) mutation testing (e.g. Stryker.NET)
- performance tests with (e.g. k6)
- functional testing (e.g. SpecFlow)

## Build/Deploy piplines
I would add github actions with yaml to introduce automatic builds and deployments.