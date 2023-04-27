namespace lab4;
public class Region {
    public string regionID { get; set; }
    public string regionDescription { get; set; }

    public Region(string regionID, string regionDescription) => (this.regionID, this.regionDescription) = (regionID, regionDescription);

    public override string ToString()
    {
        return String.Format("region ID: {0} description: {1}", regionID, regionDescription);
    }
}