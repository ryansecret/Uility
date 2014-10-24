// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicXMLNode .cs" company="">
//   
// </copyright>
// <summary>
//   The dynamic xml node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility.Example.动态方法
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Reflection;
    using System.Xml.Linq;

    /// <summary>
    /// The dynamic xml node.
    /// </summary>
    public class DynamicXMLNode : DynamicObject
    {
        #region Constants and Fields

        /// <summary>
        /// The node.
        /// </summary>
        private XElement node;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicXMLNode"/> class.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        public DynamicXMLNode(XElement node)
        {
            this.node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicXMLNode"/> class.
        /// </summary>
        public DynamicXMLNode()
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicXMLNode"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public DynamicXMLNode(string name)
        {
            this.node = new XElement(name);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The try get member.
        /// </summary>
        /// <param name="binder">
        /// The binder.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The try get member.
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            XElement getNode = this.node.Element(binder.Name);
            if (getNode != null)
            {
                result = new DynamicXMLNode(getNode);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// The try set member.
        /// </summary>
        /// <param name="binder">
        /// The binder.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The try set member.
        /// </returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            XElement setNode = this.node.Element(binder.Name);
            if (setNode != null)
            {
                setNode.SetValue(value);
            }
            else
            {
                if (value.GetType() == typeof(DynamicXMLNode))
                {
                    this.node.Add(new XElement(binder.Name));
                }
                else
                {
                    this.node.Add(new XElement(binder.Name, value));
                }
            }

            return true;
        }
        public override bool TryConvert(
    ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof(String))
            {
                result = node.Value;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TryInvokeMember(
    InvokeMemberBinder binder, object[] args, out object result)
        {
            Type xmlType = typeof(XElement);
            try
            {
                result = xmlType.InvokeMember(
                          binder.Name,
                          BindingFlags.InvokeMethod |
                          BindingFlags.Public |
                          BindingFlags.Instance,
                          null, node, args);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
            DynamicDictionary dic = new DynamicDictionary();
           
        }
        #endregion
       
    }
//    dynamic contact = new DynamicXMLNode("Contacts");  
//contact.Name = "Patrick Hines";  
//contact.Phone = "206-555-0144";  
//contact.Address = new DynamicXMLNode();  
//contact.Address.Street = "123 Main St";  
//contact.Address.City = "Mercer Island";  
//contact.Address.State = "WA";  
//contact.Address.Postal = "68402";  

    public class DynamicDictionary : DynamicObject
    {
        // The inner dictionary.
        Dictionary<string, object> dictionary
            = new Dictionary<string, object>();

        // This property returns the number of elements
        // in the inner dictionary.
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name.ToLower();

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return dictionary.TryGetValue(name, out result);
        }
      
        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            dictionary[binder.Name.ToLower()] = value;

            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }
    }


}