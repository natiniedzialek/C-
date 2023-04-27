// LINQ

namespace lab4;

public class Program {
    private static List<Employee> employees = new();
    private static List<EmployeeTerriotory> employeeTerriotories = new();
    private static List<Region> regions = new();
    private static List<Territory> territories = new();
    private static List<Order> orders = new();
    private static List<OrderDetails> orderDetails = new();

    public static void Main() {
        employees = Reader<Employee>.Read("./data/employees.csv",
                                    x => new Employee(x[0],x[1],x[2],x[3],x[4],x[5],x[6],x[7],x[8],x[9],x[10],x[11],x[12],x[13],x[14],x[15],x[16],x[17]));
        employeeTerriotories = Reader<EmployeeTerriotory>.Read("./data/employee_territories.csv",
                                    x => new EmployeeTerriotory(x[0],x[1]));
        regions = Reader<Region>.Read("./data/regions.csv",
                                    x => new Region(x[0],x[1]));  
        territories = Reader<Territory>.Read("./data/territories.csv",
                                    x => new Territory(x[0],x[1],x[2]));  
        orders = Reader<Order>.Read("./data/orders.csv",
                                    x => new Order(x[0],x[1],x[2],x[3],x[4],x[5],x[6],x[7],x[8],x[9],x[10],x[11],x[12],x[13]));
        orderDetails = Reader<OrderDetails>.Read("./data/orders_details.csv",
                                    x => new OrderDetails(x[0],x[1],x[2], x[3], x[4]));                   

        Task2();
        Task3();
        Task4();
        Task5();
        Task6();
    }

    // 2. wybierz nazwiska wszystkich pracowników.
    public static void Task2() {
        var results = from e in employees
            select e.lastName;
        
        foreach(var r in results) {
            Console.WriteLine(r);
        }
    }

    // 3. wypisz nazwiska pracowników oraz dla każdego z nich nazwę regionu i terytorium gdzie pracuje. Rezultatem kwerendy LINQ będzie "płaska" lista, więc nazwiska mogą się powtarzać (ale każdy rekord będzie unikalny).
    public static void Task3() {
        var results = from e in employees 
            join et in employeeTerriotories on e.employeeID equals et.employeeID
            join t in territories on et.terriotoryID equals t.terriotoryID
            join r in regions on t.regionID equals r.regionID
            select new {lastname = e.lastName,
                        territory = t.territoryDescription,
                        region = r.regionDescription};
            
        foreach(var r in results.ToList()) {
            Console.WriteLine(r.lastname + " " + r.region + " " + r.territory);
        }
    }

    // 4. wypisz nazwy regionów oraz nazwiska pracowników, którzy pracują w tych regionach, pracownicy mają być zagregowani po regionach, rezultatem ma być lista regionów z podlistą pracowników (odpowiednik groupjoin).
    public static void Task4() {
        var results = from r in regions 
            join t in territories on r.regionID equals t.regionID
            join et in employeeTerriotories on t.terriotoryID equals et.terriotoryID
            join e in employees on et.employeeID equals e.employeeID
            group e.lastName by r.regionDescription into group_
            select new {region = group_.Key, employees = group_.ToList().Distinct()};

        foreach(var r in results) {
            Console.WriteLine("region: " + r.region + " employees: ");
            foreach(var e in r.employees) {
                Console.WriteLine(e);
            }
        }
    }

    // 5.  wypisz nazwy regionów oraz liczbę pracowników w tych regionach.
    public static void Task5() {
        var results = from r in regions 
            join t in territories on r.regionID equals t.regionID
            join et in employeeTerriotories on t.terriotoryID equals et.terriotoryID
            join e in employees on et.employeeID equals e.employeeID
            group e.lastName by r.regionDescription into group_
            select new {region = group_.Key, employeesCount = group_.ToList().Distinct().Count()};

        foreach(var r in results) {
            Console.WriteLine("region: " + r.region + " employee count: " + r.employeesCount);
        }
    }

    // 6. Wczytaj do odpowiednich struktur dane z plików orders.csv oraz orders_details.csv. Następnie dla każdego pracownika wypisz liczbę dokonanych przez niego zamówień, średnią wartość zamówienia oraz maksymalną wartość zamówienia.
    public static void Task6() {
        var results = from e in employees 
            join o in (from o2 in orders
            join od in orderDetails on o2.orderID equals od.orderID
            group od by o2 into g1
            select new {employeeID = g1.Key.employeeID, price = g1.Sum(x => x.unitPrice*x.quantity*(1-x.discount))})
            on e.employeeID equals o.employeeID
            group o by e.lastName into group_
            select new {lastname = group_.Key,
                count = group_.Count(),
                avgPrice = group_.Average(x => (x.price)),
                maxPrice = group_.Max(x => x.price)};
            

            foreach(var r in results) {
                Console.WriteLine("lastname: " + r.lastname + " count: " + r.count + " average price: " + r.avgPrice + " max price: " + r.maxPrice);
            }
    }
}
