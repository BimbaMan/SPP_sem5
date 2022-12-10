namespace String_Formatter;

public class Product
{
    public int productId { get; }
    public string productName { get; }
    public double price { get; }

    public Product(int productId, string productName, double price)
    {
        this.productId = productId;
        this.productName = productName;
        this.price = price;
    }

    public string getProductInfo()
    {
        return StringFormatter.Shared.Format("Product {" + "id : {{productId}} , " + 
                                             "name : {{productName}}, " + 
                                             "price : {{price}}" + "}", this);
    }
}