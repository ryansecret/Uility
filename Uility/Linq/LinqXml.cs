﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Uility.Linq
{
    public class LinqXml
    {
        public void AddNewElement()
        {
            var Students = new XElement("Students",
                                        new XElement("Student",
                                                     new XElement("Name", "张三"),
                                                     new XElement("Sex", "男"),
                                                     new XElement("Age", new XAttribute("Year", "1989/8/22"), 20)),
                                        new XElement("Student",
                                                     new XElement("Name", "李四"),
                                                     new XElement("Sex", "女"),
                                                     new XElement("Age", new XAttribute("Year", "1990/8/22"), 20))
                );

            Students = new XElement("Students",
                                    new XElement("Student",
                                                 new XElement("Name", "张三"),
                                                 new XElement("Sex", "男"),
                                                 new XElement("Age", new XAttribute("Year", "1989/8/22"), 20))
                );
          
            //设置属性的值、添加属性或移除属性。为null删除该属性
            Students.Element("Student").SetAttributeValue("dd", "dddd");

            Console.WriteLine(Students);

//更新XML属性
            
            
            Students.Element("Student").Element("Age").ReplaceAttributes(new XAttribute("Year", "dd"));

            Students.Element("Student").Element("Age").SetAttributeValue("Year", "dddd");

//删除XML属性

            Students.Element("Student").Element("Age").Attribute("Year").Remove();

            Students.Element("Student").Element("Age").RemoveAttributes();

//遍历XML属性

            IEnumerable<XAttribute> Attr = from att in Students.Element("Student").Element("Age").Attributes()
                                           select att;

            foreach (XAttribute att in Attr)

            {
                Console.WriteLine(att);
            }
        }

        public void GetDescendants()
        {
            var Students = new XElement("Students",
                                        new XElement("Student",
                                                     new XElement("Name", "张三"),
                                                     new XElement("Sex", "男"),
                                                     new XElement("Age", new XAttribute("Year", "1989/8/22"), 20)),
                                        new XElement("Student",
                                                     new XElement("Name", "李四"),
                                                     new XElement("Sex", "女"),
                                                     new XElement("Age", new XAttribute("Year", "1990/8/22"), 20))
                );

            foreach (XNode v in Students.DescendantNodesAndSelf())
            {
              
                Console.WriteLine(v);

                Console.WriteLine("-----------------------");
            }
        }

        /// <summary>
        ///  返回当前节点后的所有节点。（ElementsBeforeSelft：返回当前节点前的所有节点） 
        /// </summary>
        public void GetBeforeOrAfterElements()
        {
            var Students = new XElement("Students",
                                        new XElement("Student",
                                                     new XElement("Name", "张三"),
                                                     new XElement("Sex", "男"),
                                                     new XElement("Age", new XAttribute("Year", "1989/8/22"), 20)),
                                        new XElement("Student",
                                                     new XElement("Name", "李四"),
                                                     new XElement("Sex", "女"),
                                                     new XElement("Age", new XAttribute("Year", "1990/8/22"), 20))
                );

            foreach (XElement v in Students.Element("Student").ElementsAfterSelf())
            {
                Console.WriteLine(v);

                Console.WriteLine("-----------------------");
            }
        }

        public void GetAttributes()
        {
            //检索索性的集合   
            var val = new XElement("Value",
                                   new XAttribute("ID", "1243"),
                                   new XAttribute("Type", "int"),
                                   new XAttribute("ConvertableTo", "double"),
                                   "100");

            IEnumerable<XAttribute> listOfAttributes =
                from att in val.Attributes()
                select att;
            foreach (XAttribute a in listOfAttributes)
                Console.WriteLine(a);
        }

        public void ComplexSearch()
        {
            XElement root = XElement.Load("PurchaseOrders.xml");
            IEnumerable<XElement> purchaseOrders =
                from el in root.Elements("PurchaseOrder")
                where
                    (from add in el.Elements("Address")
                     where
                         (string)add.Attribute("Type") == "Shipping" &&
                         (string)add.Element("State") == "NY"
                     select add)
                    .Any()
                select el;
            foreach (XElement el in purchaseOrders)
                Console.WriteLine((string)el.Attribute("PurchaseOrderNumber"));

            
              root = XElement.Load("Data.xml");
            IEnumerable<decimal> extensions =
                from el in root.Elements("Data")
                let extension = (decimal)el.Element("Quantity") * (decimal)el.Element("Price")
                where extension >= 25
                orderby extension
                select extension;
            foreach (decimal ex in extensions)
                Console.WriteLine(ex);



            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", "CustomersOrders.xsd");

            Console.Write("Attempting to validate, ");
            XDocument custOrdDoc = XDocument.Load("CustomersOrders.xml");

            bool errors = false;
            custOrdDoc.Validate(schemas, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
                errors = true;
            });
            Console.WriteLine("custOrdDoc {0}", errors ? "did not validate" : "validated");

            if (!errors)
            {
                // Join customers and orders, and create a new XML document with   
                // a different shape.   

                // The new document contains orders only for customers with a   
                // CustomerID > 'K'   
                XElement custOrd = custOrdDoc.Element("Root");
                XElement newCustOrd = new XElement("Root",
                    from c in custOrd.Element("Customers").Elements("Customer")
                     join o in custOrd.Element("Orders").Elements("Order")
                               on new {id=(string)c.Attribute("CustomerID"),name=} equals
                                  (string)o.Element("CustomerID")
                    where ((string)c.Attribute("CustomerID")).CompareTo("K") > 0
                    select new XElement("Order",
                        new XElement("CustomerID", (string)c.Attribute("CustomerID")),
                        new XElement("CompanyName", (string)c.Element("CompanyName")),
                        new XElement("ContactName", (string)c.Element("ContactName")),
                        new XElement("EmployeeID", (string)o.Element("EmployeeID")),
                        new XElement("OrderDate", (DateTime)o.Element("OrderDate"))
                    )
                );
                Console.WriteLine(newCustOrd);
            }
        }
        /// <summary>
        /// 排序时要先转换为相应的类型
        /// </summary>
        public void Order()
        {
            XElement co = XElement.Load("CustomersOrders.xml");
            var sortedElements =
                from c in co.Element("Orders").Elements("Order")
                orderby (string)c.Element("ShipInfo").Element("ShipPostalCode"),
                        (DateTime)c.Element("OrderDate")
                select new
                {
                    CustomerID = (string)c.Element("CustomerID"),
                    EmployeeID = (string)c.Element("EmployeeID"),
                    ShipPostalCode = (string)c.Element("ShipInfo").Element("ShipPostalCode"),
                    OrderDate = (DateTime)c.Element("OrderDate")
                };
            foreach (var r in sortedElements)
                Console.WriteLine("CustomerID:{0} EmployeeID:{1} ShipPostalCode:{2} OrderDate:{3:d}",
                    r.CustomerID, r.EmployeeID, r.ShipPostalCode, r.OrderDate);   
        }

        public void ChangeHir()
        {
            
            XElement co = XElement.Load("CustomersOrders.xml");
            XElement newCustOrd =
                new XElement("Root",
                    from cust in co.Element("Customers").Elements("Customer")
                    select new XElement("Customer",
                        cust.Attributes(),
                        cust.Elements(),
                        new XElement("Orders",
                            from ord in co.Element("Orders").Elements("Order")
                            where (string)ord.Element("CustomerID") == (string)cust.Attribute("CustomerID")
                            select new XElement("Order",
                                ord.Attributes(),
                                ord.Element("EmployeeID"),
                                ord.Element("OrderDate"),
                                ord.Element("RequiredDate"),
                                ord.Element("ShipInfo")
                            )
                        )
                    )
                );
             
            Console.WriteLine(newCustOrd);   
        }
        /// <summary>
        /// 必须有公共的无参构造函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void show<T>(T t) where T: new()
        {
         
        }
    }

     
}