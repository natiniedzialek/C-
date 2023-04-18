namespace lab4;
public class Employee {
    public string employeeID { get; set; }
    public string lastName { get; set; }
    public string firstName  { get; set; }
    public string title { get; set; }
    public string titleOfCourtesy { get; set; }
    public string birthDate { get; set; }
    public string hireDate { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string region { get; set; }
    public string postalCode { get; set; }
    public string country { get; set; }
    public string homePhone { get; set; }
    public string extension { get; set; }
    public string photo { get; set; }
    public string notes { get; set; }
    public string reportsTo { get; set; }
    public string photoPath { get; set; }


    public Employee(string employeeID, string lastName, string firstName, string title, string titleOfCourtesy, string birthDate, string hireDate, string address, string city, string region, string postalCode, string country, string homePhone, string extension, string photo, string notes, string reportsTo, string photoPath) {
            this.employeeID = employeeID;
            this.lastName = lastName;
            this.firstName = firstName;
            this.title = title;
            this.titleOfCourtesy = titleOfCourtesy;
            this.birthDate = birthDate;
            this.hireDate = hireDate;
            this.address = address;
            this.city = city;
            this.region = region;
            this.postalCode = postalCode;
            this.country = country;
            this.homePhone = homePhone;
            this.extension = extension;
            this.photo = photo;
            this.notes = notes;
            this.reportsTo = reportsTo;
            this.photoPath = photoPath;
        }

        public override String ToString() {
            return firstName + " " + lastName;
        }
    
}