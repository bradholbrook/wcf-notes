using System.Data.Services;

// This project includes Northwind.edmx and is attached to a database instance
public class Northwind : DataService<NorthwindEntities>
{
    // This method is called only once to initialize service-wide policies.
    public static void InitializeService(DataServiceConfiguration config)
    {
        // Grant only the rights needed to support the client application.
        config.SetEntitySetAccessRule("Orders", EntitySetRights.AllRead
             | EntitySetRights.WriteMerge
             | EntitySetRights.WriteReplace);
        config.SetEntitySetAccessRule("Order_Details", EntitySetRights.AllRead
            | EntitySetRights.AllWrite);
        config.SetEntitySetAccessRule("Customers", EntitySetRights.AllRead);
        config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
    }

    // Define Interceptors here
}