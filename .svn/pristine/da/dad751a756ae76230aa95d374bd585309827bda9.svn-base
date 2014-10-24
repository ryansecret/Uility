using System;
using System.Configuration;
namespace MarvellousWorks.PracticalPattern.Concept.Configurating
{
    // 定义包括 NamedConfigurationElementBase 的 ConfiugrationElementCollection
    [ConfigurationCollection(typeof(NamedConfigurationElementBase),
    CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public abstract class NamedConfigurationElementCollectionBase<T>
        : ConfigurationElementCollection
        where T : NamedConfigurationElementBase, new()
    {
        // 外部通过 index 获取集合中特定 configurationelement 
        public T this[int index] { get { return (T)base.BaseGet(index); } }
        public new T this[string name] { get { return (T)base.BaseGet(name); } }


        // 创建一个新的 NamedConfiugrationElement 实例
        protected override ConfigurationElement CreateNewElement() { return new T(); }

        // 获取集合中某个特定 NamedConfiugrationElement 的 key (Name属性)
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as T).Name; 
        }
    }

    public class ExampleConfigurationElementCollection :
        NamedConfigurationElementCollectionBase<ExampleConfigurationElement> { }
    public class DiagramConfigurationElementCollection :
        NamedConfigurationElementCollectionBase<DiagramConfigurationElement> { }
    public class PictureConfigurationElementCollection :
        NamedConfigurationElementCollectionBase<PictureConfigurationElement> { }
}
