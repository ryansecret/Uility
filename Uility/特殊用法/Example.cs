using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uility.特殊用法
{
    public interface IRegionBehaviorCollection : IEnumerable<KeyValuePair<string, IRegionBehavior>>
    {

        /// <summary>
        /// Adds a <see cref="IRegionBehavior"/> to the collection, using the specified key as an indexer. 
        /// </summary>
        /// <param name="key">
        /// The key that specifies the type of <see cref="IRegionBehavior"/> that's added. 
        /// </param>
        /// <param name="regionBehavior">The <see cref="IRegionBehavior"/> to add.</param>
        void Add(string key, IRegionBehavior regionBehavior);

        /// <summary>
        /// Checks if a <see cref="IRegionBehavior"/> with the specified key is already present. 
        /// </summary>
        /// <param name="key">The key to use to find a particular <see cref="IRegionBehavior"/>.</param>
        /// <returns></returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Gets the <see cref="IRegionBehavior"/> with the specified key.
        /// </summary>
        /// <value>The registered <see cref="IRegionBehavior"/></value>
        IRegionBehavior this[string key] { get; }
    }
        public class ViewRegisteredEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes the ViewRegisteredEventArgs class.
        /// </summary>
        /// <param name="regionName">The region name to which the content was registered.</param>
        /// <param name="getViewDelegate">The content which was registered.</param>
        public ViewRegisteredEventArgs(string regionName, Func<object> getViewDelegate)
        {
            this.GetView = getViewDelegate;
            this.RegionName = regionName;
        }

        /// <summary>
        /// Gets the region name to which the content was registered.
        /// </summary>
        public string RegionName { get; private set; }

        /// <summary>
        /// Gets the content which was registered.
        /// </summary>
        public Func<object> GetView { get; private set; }
    }

    public class test
    {
          public IEnumerable<TValue> FindAllValuesByKey(Predicate<TKey> keyFilter)
          {
              List<string> list = new List<string>();
              list.RemoveAll(v => string.IsNullOrWhiteSpace(v));
             
            foreach (KeyValuePair<TKey, IList<TValue>> pair in this)
            {
                if (keyFilter(pair.Key))
                {
                    foreach (TValue value in pair.Value)
                    {
                        yield return value;
                    }
                }
            }

        private readonly ListDictionary<string, Func<object>> registeredContent = new ListDictionary<string, Func<object>>();
    }

     private class RegionItemComparer : Comparer<object>
        {
            private readonly Comparison<object> comparer;

            public RegionItemComparer(Comparison<object> comparer)
            {
                this.comparer = comparer;
            }

            public override int Compare(object x, object y)
            {
                if (this.comparer == null)
                {
                    return 0;
                }

                return this.comparer(x, y);
            }
        }


     public class UnityServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        /// Initializes a new instance of <see cref="UnityServiceLocatorAdapter"/>.
        /// </summary>
        /// <param name="unityContainer">The <seealso cref="IUnityContainer"/> that will be used
        /// by the <see cref="DoGetInstance"/> and <see cref="DoGetAllInstances"/> methods.</param>
     
        public UnityServiceLocatorAdapter(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        /// <summary>
        /// Resolves the instance of the requested service.
        /// </summary>
        /// <param name="serviceType">Type of instance requested.</param>
        /// <param name="key">Name of registered service you want. May be null.</param>
        /// <returns>The requested service instance.</returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _unityContainer.Resolve(serviceType, key);
        }

        /// <summary>
        /// Resolves all the instances of the requested service.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>Sequence of service instance objects.</returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _unityContainer.ResolveAll(serviceType);
        }
    }
}
