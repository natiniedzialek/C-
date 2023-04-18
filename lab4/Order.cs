namespace lab4;

public class Order {
    public string orderID;
    public string customerID;
    public string employeeID;
    public string orderDate;
    public string requiredDate;
    public string shippedDate;
    public string shipVia;
    public string freight;
    public string shipName;
    public string shipAddress;
    public string shipCity;
    public string shipRegion;
    public string shipPostalCode;
    public string shipCountry;

    public Order(string orderID, string customerID, string employeeID, string orderDate, string requiredDate, string shippedDate, string shipVia, string freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry) {
        this.orderID = orderID;
        this.customerID = customerID;
        this.employeeID = employeeID;
        this.orderDate = orderDate;
        this.requiredDate = requiredDate;
        this.shippedDate = shippedDate;
        this.shipVia = shipVia;
        this.freight = freight;
        this.shipName = shipName;
        this.shipAddress = shipAddress;
        this.shipCity = shipCity;
        this.shipRegion = shipRegion;
        this.shipPostalCode = shipPostalCode;
        this.shipCountry = shipCountry;
    }
}