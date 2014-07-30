###Overview
WCF Uses OData over HTTP to expose and manipulate data through URIs following REST-like semantics. It can be acessed from any client that can utilize HTTP protocols.

WCF does not handle anything other than data transfer. It relies on separate facilities for caching, storage, authentication, hosting, etc.

Whatever the underlying data representation, WCF passes its identification URI on to a WCF Data Service provider which translates the URI into a data-source-specific format and executes the data request.

###Creating a WCF Data Service
The Data Service creation is as simple as deriving a class from DataService<T> where T is the entity set of the data you want to present through the service, and placing it in a .svc file. That file contains configuration XML markup and code that will help define the service.

 When you initialize the service, you have the option of setting all access policies and version information required by the data set.

 See the snippet called Service for an example.
 
###Executing and Testing
A WCF Data Service can interact with an HTTP client - aka a web browser. Starting the service server and navigating to the service url allows access to all service data.

A client application can also be built for more extensive testing. The client functionality is wrapped up in Microsoft.Data.Services.Client. In short, initialize your data context with the service URI, and start creating queries. 

See the snippet marked Client for an example.

###Data Provider Options
WCF has two main data provider options. Entity Framework, and Custom.
 * Entity Framework, of course, uses a relational database.
 * Making a custom data provider requires using existing, non-relational, static classes defined at runtime. The types are used to implement interfaces to reveal metadata about them, allow querying and updating, as wellas to perform other service operations. The service allows access to the class through sets of IQueryable<T> and IUpdateable<T> entities.

See the snippet marked Reflection for an example.

###Interceptors
Interceptors are used to add custom logic to a request. Query interceptors return whether or not each object of the queried set should be returned by the query results. Change interceptors do not return anything, but instead define a set of update operations to be performed when an entity is changed.

Interceptors are defined within the service class.

See the snippet marked Interceptors for an example.