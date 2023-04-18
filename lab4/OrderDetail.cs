namespace lab4;

public class OrderDetails {
    public string orderID;
    public string productID;
    public double unitPrice;
    public double quantity;
    public double discount;

    public OrderDetails(string orderID, string productID, string unitPrice, string quantity, string discount) {
        this.orderID = orderID;
        this.productID = productID;
        this.unitPrice = Convert.ToDouble(unitPrice.Replace('.', ','));
        this.quantity = Convert.ToDouble(quantity.Replace('.', ','));
        this.discount = Convert.ToDouble(discount.Replace('.', ','));
    }
}