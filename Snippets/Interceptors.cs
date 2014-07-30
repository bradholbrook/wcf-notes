// Define a query interceptor for Urder entities
[QueryInterceptor("Orders")]
public Expression<Func<Order, bool>> OnQueryOrders()
{
    // Filter the returned orders to only orders  
    // that belong to a customer that is the current user. 
    return o => o.Customer.ContactName ==
        HttpContext.Current.User.Identity.Name;
}

// Define a change interceptor for the Products entity set.
[ChangeInterceptor("Products")]
public void OnChangeProducts(Product product, UpdateOperations operations)
{
    if (operations == UpdateOperations.Change)
    {
        System.Data.Objects.ObjectStateEntry entry;

        if (this.CurrentDataSource.ObjectStateManager
            .TryGetObjectStateEntry(product, out entry))
        {
            // Reject changes to a discontinued Product. 
            // Because the update is already made to the entity by the time the  
            // change interceptor in invoked, check the original value of the Discontinued 
            // property in the state entry and reject the change if 'true'. 
            if ((bool)entry.OriginalValues["Discontinued"])
            {
                throw new DataServiceException(400, string.Format(
                            "A discontinued {0} cannot be modified.", product.ToString()));
            }
        }
        else
        {
            throw new DataServiceException(string.Format(
                "The requested {0} could not be found in the data source.", product.ToString()));
        }
    }
    else if (operations == UpdateOperations.Delete)
    {
        // Block the delete and instead set the Discontinued flag. 
        throw new DataServiceException(400,
            "Products cannot be deleted; instead set the Discontinued flag to 'true'");
    }
}