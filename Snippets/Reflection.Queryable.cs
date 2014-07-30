//Define the key of the entity
[DataServiceKeyAttribute("Name")]
public class Item
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}
//Extensible entity to act as the data source
public partial class ItemData
{
    static IList<Item> _items;
    static ItemData()
    {
        _items = new Item[]{
          new Item(){ Name="Chai", Quantity=10 },
          new Item(){ Name="Chang", Quantity=25 },
          new Item(){ Name="Aniseed Syrup", Quantity = 5 },
          new Item(){ Name="Chef Anton's Cajun Seasoning", Quantity=30}};
    }
    //Accessible members must implement IQueryable interface
    public IQueryable<Item> Items
    {
        get { return _items.AsQueryable<Item>(); }
    }
}
//Initialize the service
public class Items : DataService<ItemData>
{
    // Configure access policies
    public static void InitializeService(IDataServiceConfiguration
                                         config)
    {
        config.SetEntitySetAccessRule("Items", EntitySetRights.All);
    }
}