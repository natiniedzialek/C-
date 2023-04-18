namespace lab4;
public class EmployeeTerriotory {
    public string employeeID { get; set; }
    public string terriotoryID { get; set; }

    public EmployeeTerriotory(string employeeID, string terriotoryID) => (this.employeeID, this.terriotoryID) = (employeeID, terriotoryID);

    public override string ToString()
    {
        return "employee ID: " + employeeID + " territoryID: " + terriotoryID;
    }
}