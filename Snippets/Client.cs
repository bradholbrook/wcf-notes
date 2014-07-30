//Taken from a WPF Application
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Uri serviceURI = new Uri("http://localhost:1234/Northwind.svc");
    private NorthwindEntities context = null;
    private string customerId = "ALFKI";

    public MainWindow() { InitializeComponent(); }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Instantiate the DataServiceContext using the service URI
            // Make sure to add the service as a Service Reference first
            context = new NorthwindEntities(serviceURI);

            context.IgnoreMissingProperties = true;

            // Define a LINQ query that returns Orders and  
            // Order_Details for a specific customer. 
            var ordersQuery = from o in context.Orders.Expand("Order_Details")
                              where o.Customer.CustomerID == customerId
                              select o;

            //Alternatively, you can use query options instead of Lync
            //var ordersQuery = context.Orders
            //    .Expand("Order_Details")
            //    .AddQueryOption("$filter", String.Format("CustomerID eq '{0}'", customerId))
            //    .AddQueryOption("$orderby", "Freight gt 30 desc");

            // Create an DataServiceCollection<T> based on  
            // execution of the LINQ query for Orders.
            DataServiceCollection<Order> customerOrders = new
                DataServiceCollection<Order>(ordersQuery);

            // Load the results into the WPF window Grid
            // Make the DataServiceCollection<T> the binding source for the Grid. 
            this.orderItemsGrid.DataContext = customerOrders;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }
    private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Save changes made to objects tracked by the context.
            context.SaveChanges();
        }
        catch (DataServiceRequestException ex)
        {
            MessageBox.Show(ex.ToString());

        }
    }
    private void buttonClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
  }
}