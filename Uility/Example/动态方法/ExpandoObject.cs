using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uility.Example.动态方法
{
    using System.ComponentModel;
    using System.Dynamic;
    using System.Xml.Linq;

    public  class ExpandoObjectExample
    {
        XElement contactXML =
        new XElement("Contact",
        new XElement("Name", "Patrick Hines"),
        new XElement("Phone", "206-555-0144"),
        new XElement("Address",
            new XElement("Street1", "123 Main St"),
            new XElement("City", "Mercer Island"),
            new XElement("State", "WA"),
            new XElement("Postal", "68042")
        )
       );
       
      public void DynamicObject()
      {
          dynamic contact = new ExpandoObject(); 
          contact.Name = "Patrick Hines";
          
          contact.Phone = "206-555-0144";
          contact.Address = new ExpandoObject();
          contact.Address.Street = "123 Main St";
          contact.Address.City = "Mercer Island";
          contact.Address.State = "WA";
          contact.Address.Postal = "68402";

          dynamic contacts = new List<dynamic>();
          contacts.Add(contact );
          var phones = from c in (contacts as List<dynamic>)
                       where c.Name == "Patrick Hines"
                       select c.Phone;

          foreach (var person in contacts)
              ((IDictionary<String, Object>)person).Remove("Phone");


          dynamic employee = new ExpandoObject();
          ((INotifyPropertyChanged)employee).PropertyChanged +=
              new PropertyChangedEventHandler(HandlePropertyChanges);

          employee.save = (Action)(() => Console.WriteLine("ss"));
          employee.save();
          employee.Name = "John Smith";

      }
       
        private void HandlePropertyChanges(object sender, PropertyChangedEventArgs e)
        {
             
        }

        private static XElement expandoToXML(dynamic node, String nodeName)
      {
          XElement xmlNode = new XElement(nodeName);

          foreach (var property in (IDictionary<String, Object>)node)
          {

              if (property.Value.GetType() == typeof(ExpandoObject))
                  xmlNode.Add(expandoToXML(property.Value, property.Key));

              else
                  if (property.Value.GetType() == typeof(List<dynamic>))
                      foreach (var element in (List<dynamic>)property.Value)
                          xmlNode.Add(expandoToXML(element, property.Key));
                  else
                      xmlNode.Add(new XElement(property.Key, property.Value));
          }
          return xmlNode;
      }
    }
}
