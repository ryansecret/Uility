using System;
using System.Configuration;
namespace MarvellousWorks.PracticalPattern.Concept.Configurating
{
    // 定义具有 name 和 description 属性的配置元素
    // name 属性作为 ConfigurationElementCollection中相应的 key
    public abstract class NamedConfigurationElementBase : ConfigurationElement
    {
        private const string NameItem = "name";
        private const string DescriptionItem = "description";

        [ConfigurationProperty(NameItem, IsKey=true, IsRequired=true)]
        public virtual string Name { get { return base[NameItem] as string; } }

        [ConfigurationProperty(DescriptionItem, IsRequired = false)]
        public virtual string Description { get { return base[DescriptionItem] as string; } }
    }

    public class ExampleConfigurationElement : NamedConfigurationElementBase { }
    public class DiagramConfigurationElement : NamedConfigurationElementBase { }
    public class PictureConfigurationElement : NamedConfigurationElementBase
    {
        private const string ColorizedItem = "colorized";

        [ConfigurationProperty(ColorizedItem, IsRequired = true)]
        public bool Colorized { get { return (bool)base[ColorizedItem]; } }
    }
}   