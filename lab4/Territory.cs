namespace lab4;
public class Territory {
    public string terriotoryID { get; set; }
    public string territoryDescription { get; set; }
    public string regionID { get; set; }

    public Territory(string terriotoryID, string territoryDescription, string regionID) => 
        (this.terriotoryID, this.territoryDescription, this.regionID) = (terriotoryID, territoryDescription, regionID);

    public override string ToString()
    {
        return String.Format("territory ID: {0} description: {1}", regionID, territoryDescription);
    }
}