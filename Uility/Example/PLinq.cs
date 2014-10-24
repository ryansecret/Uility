using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Uility.Example
{
    using System.Threading;
    using System.Threading.Tasks;

    public partial class PLinq
    {
        //note:PLINQ 中的顺序保留,在默认情况下，PLINQ 不会保留源序列的顺序。当查询以并行方式执行时，PLINQ 对源序列分区，以便多个线程可以并发处理通常在不同的线程上的不同部分。
        public void AsOrder()
        {

            //var orderedCities2 = (from city in cities.AsParallel().AsOrdered()
            //                      where city.Population > 10000
            //                      select city)
            //                        .Take(1000);


            //var finalResult = from city in orderedCities2.AsUnordered()
            //                  join p in people.AsParallel() on city.Name equals p.CityName into details
            //                  from c in details
            //                  select new { Name = city.Name, Pop = city.Population, Mayor = c.Mayor };

            //foreach (var city in finalResult) { /*...*/ }

            var source = Enumerable.Range(9, 10000);

            // Source is ordered; let's preserve it.
            var parallelQuery = from num in source.AsParallel().AsOrdered()
                                where num % 3 == 0
                                select num;

            // Use foreach to preserve order at execution time.
            foreach (var v in parallelQuery)
                Console.Write("{0} ", v);

            // Some operators expect an ordered source sequence.
            var lowValues = parallelQuery.Take(10);

      
             
        }

        static void PLINQExceptions_2()
        {


            var customers = GetCustomersAsStrings().ToArray();
            // Using the raw string array here.
            // First, we must simulate some currupt input
            customers[54] = "###";

            // Create a delegate with a lambda expression.
            // Assume that in this app, we expect malformed data
            // occasionally and by design we just report it and continue.
            Func<string[], string, bool> isTrue = (f, c) =>
            {
                try
                {
                    string s = f[3];
                    return s.StartsWith(c);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Malformed cust: {0}", f);
                    return false;
                }
            };

            // Using the raw string array here
            var parallelQuery = from cust in customers.AsParallel()
                                let fields = cust.Split(',')
                                where isTrue(fields, "C") //use a named delegate with a try-catch
                                select new { city = fields[3] };
            try
            {
              
                // We use ForAll although it doesn't really improve performance
                // since all output must be serialized through the Console.
                parallelQuery.ForAll(e => Console.WriteLine(e.city));
            }

            // IndexOutOfRangeException will not bubble up      
            // because we handle it where it is thrown.
            catch (AggregateException e)
            {
                foreach (var ex in e.InnerExceptions)
                    Console.WriteLine(ex.Message);
            }
        }

        public void CanCell()
        {
            int[] source = Enumerable.Range(1, 10000000).ToArray();
            CancellationTokenSource cs = new CancellationTokenSource();

            // Start a new asynchronous task that will cancel the 
            // operation from another thread. Typically you would call
            // Cancel() in response to a button click or some other
            // user interface event.
            Task.Factory.StartNew(() =>
            {
                UserClicksTheCancelButton(cs);
            });

            int[] results = null;
            try
            {
                results = (from num in source.AsParallel().WithCancellation(cs.Token)
                           where num % 3 == 0
                           orderby num descending 
                           select num).ToArray();

            }

            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }

            catch (AggregateException ae)
            {
                if (ae.InnerExceptions != null)
                {
                    foreach (Exception e in ae.InnerExceptions)
                        Console.WriteLine(e.Message);
                }
            }

            if (results != null)
            {
                foreach (var v in results)
                    Console.WriteLine(v);
            }
            Console.WriteLine();
            Console.ReadKey();
            
          

        }

        static void UserClicksTheCancelButton(CancellationTokenSource cs)
        {
            // Wait between 150 and 500 ms, then cancel.
            // Adjust these values if necessary to make
            // cancellation fire while query is still executing.
            Random rand = new Random();
            Thread.Sleep(rand.Next(150, 350));
            cs.Cancel();
        }



        //note:排序在不必要时移除
        // Paste into PLINQDataSample class.
        static void OrderedThenUnordered()
        {

            var orders = GetOrders();
            var orderDetails = GetOrderDetails();

            var q2 = orders.AsParallel()
               .Where(o => o.OrderDate < DateTime.Parse("07/04/1997"))
               .Select(o => o)
               .OrderBy(o => o.CustomerID) // Preserve original ordering for Take operation.
               .Take(20)
               .AsUnordered()  // Remove ordering constraint to make join faster.
               .Join(
                      orderDetails.AsParallel(),
                      ord => ord.OrderID,
                      od => od.OrderID,
                      (ord, od) =>
                      new
                      {
                          ID = ord.OrderID,
                          Customer = ord.CustomerID,
                          Product = od.ProductID
                      }
                     )
               .OrderBy(i => i.Product); // Apply new ordering to final result sequence.

            foreach (var v in q2)
                Console.WriteLine("{0} {1} {2}", v.ID, v.Customer, v.Product);

        }

        public class Order
        {
            private Lazy<OrderDetail[]> _orderDetails;
            public Order()
            {
                _orderDetails = new Lazy<OrderDetail[]>(() => GetOrderDetailsForOrder(OrderID));
            }
            public int OrderID { get; set; }
            public string CustomerID { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime ShippedDate { get; set; }
            public OrderDetail[] OrderDetails { get { return _orderDetails.Value; } }
        }

        public class Customer
        {
            private Lazy<Order[]> _orders;
            public Customer()
            {
                _orders = new Lazy<Order[]>(() => GetOrdersForCustomer(CustomerID));
            }
            public string CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public Order[] Orders
            {
                get
                {
                    return _orders.Value;
                }
            }
        }

        public class Product
        {
            public string ProductName { get; set; }
            public int ProductID { get; set; }
            public double UnitPrice { get; set; }
        }

        public class OrderDetail
        {
            public int OrderID { get; set; }
            public int ProductID { get; set; }
            public double UnitPrice { get; set; }
            public double Quantity { get; set; }
            public double Discount { get; set; }
        }


        public static IEnumerable<string> GetCustomersAsStrings()
        { 
           
            return System.IO.File.ReadAllLines(@"..\..\plinqdata.csv")
                                            .SkipWhile((line) => line.StartsWith("CUSTOMERS") == false)
                                             .Skip(1)
                                            .TakeWhile((line) => line.StartsWith("END CUSTOMERS") == false);
        }

        public static IEnumerable<Customer> GetCustomers()
        {
            var customers = System.IO.File.ReadAllLines(@"..\..\plinqdata.csv")
                                             .SkipWhile((line) => line.StartsWith("CUSTOMERS") == false)
                                             .Skip(1)
                                             .TakeWhile((line) => line.StartsWith("END CUSTOMERS") == false);
            return (from line in customers
                    let fields = line.Split(',')
                    let custID = fields[0].Trim()
                    select new Customer()
                    {
                        CustomerID = custID,
                        CustomerName = fields[1].Trim(),
                        Address = fields[2].Trim(),
                        City = fields[3].Trim(),
                        PostalCode = fields[4].Trim()
                    });
         
        }

        public static Order[] GetOrdersForCustomer(string id)
        {
            // Assumes we copied the file correctly!
            var orders = System.IO.File.ReadAllLines(@"..\..\plinqdata.csv")
                                             .SkipWhile((line) => line.StartsWith("ORDERS") == false)
                                              .Skip(1)
                                            .TakeWhile((line) => line.StartsWith("END ORDERS") == false);
            var orderStrings = from line in orders
                               let fields = line.Split(',')
                               where fields[1].CompareTo(id) == 0
                               select new Order()
                               {
                                   OrderID = Convert.ToInt32(fields[0]),
                                   CustomerID = fields[1].Trim(),
                                   OrderDate = DateTime.Parse(fields[2]),
                                   ShippedDate = DateTime.Parse(fields[3])
                               };
            return orderStrings.ToArray();
        }

        //  "10248, VINET, 7/4/1996 12:00:00 AM, 7/16/1996 12:00:00 AM
        public static IEnumerable<Order> GetOrders()
        {
            // Assumes we copied the file correctly!
            var orders = System.IO.File.ReadAllLines(@"..\..\plinqdata.csv")
                                            .SkipWhile((line) => line.StartsWith("ORDERS") == false)
                                             .Skip(1)
                                            .TakeWhile((line) => line.StartsWith("END ORDERS") == false);
            return from line in orders
                   let fields = line.Split(',')

                   select new Order()
                   {
                       OrderID = Convert.ToInt32(fields[0]),
                       CustomerID = fields[1].Trim(),
                       OrderDate = DateTime.Parse(fields[2]),
                       ShippedDate = DateTime.Parse(fields[3])
                   };
        }

        public static IEnumerable<Product> GetProducts()
        {
            // Assumes we copied the file correctly!
            var products = System.IO.File.ReadAllLines(@"..\..\plinqdata.csv")
                                            .SkipWhile((line) => line.StartsWith("PRODUCTS") == false)
                                             .Skip(1)
                                            .TakeWhile((line) => line.StartsWith("END PRODUCTS") == false);
            return from line in products
                   let fields = line.Split(',')
                   select new Product()
                   {
                       ProductID = Convert.ToInt32(fields[0]),
                       ProductName = fields[1].Trim(),
                       UnitPrice = Convert.ToDouble(fields[2])

                   };
        }

        public static IEnumerable<OrderDetail> GetOrderDetails()
        {
            // Assumes we copied the file correctly!
            var orderDetails = System.IO.File.ReadAllLines(@"..\..\plinqdata.csv")
                                            .SkipWhile((line) => line.StartsWith("ORDER DETAILS") == false)
                                             .Skip(1)
                                            .TakeWhile((line) => line.StartsWith("END ORDER DETAILS") == false);

            return from line in orderDetails
                   let fields = line.Split(',')
                   select new OrderDetail()
                   {
                       OrderID = Convert.ToInt32(fields[0]),
                       ProductID = Convert.ToInt32(fields[1]),
                       UnitPrice = Convert.ToDouble(fields[2]),
                       Quantity = Convert.ToDouble(fields[3]),
                       Discount = Convert.ToDouble(fields[4])
                   };
        }

        public static OrderDetail[] GetOrderDetailsForOrder(int id)
        {
            // Assumes we copied the file correctly!
            var orderDetails = System.IO.File.ReadAllLines(@"..\..\plinqdata.csv")
                                            .SkipWhile((line) => line.StartsWith("ORDER DETAILS") == false)
                                             .Skip(1)
                                            .TakeWhile((line) => line.StartsWith("END ORDER DETAILS") == false);

            var orderDetailStrings = from line in orderDetails
                                     let fields = line.Split(',')
                                     let ordID = Convert.ToInt32(fields[0])
                                     where ordID == id
                                     select new OrderDetail()
                                     {
                                         OrderID = ordID,
                                         ProductID = Convert.ToInt32(fields[1]),
                                         UnitPrice = Convert.ToDouble(fields[2]),
                                         Quantity = Convert.ToDouble(fields[3]),
                                         Discount = Convert.ToDouble(fields[4])
                                     };

            return orderDetailStrings.ToArray();
        }
    }
}
    

