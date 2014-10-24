using System;
using System.Configuration;
namespace MarvellousWorks.PracticalPattern.Concept.Configurating
{
    // 文章段落的配置节类型 包括：
    // 1、<examples>的ConfigurationElementCollection (optional)
    // 1、<diagrams>的ConfigurationElementCollection (optional)
    public abstract class ParagraphConfigurationSectionBase : ConfigurationSection
    {
        private const string ExamplesItem = "examples";
        private const string DiagramsItem = "diagrams";

        [ConfigurationProperty(ExamplesItem, IsRequired=false)]
        public virtual ExampleConfigurationElementCollection Examples
        {
            get 
            {
                return base[ExamplesItem] as ExampleConfigurationElementCollection; 
            }
        }

        [ConfigurationProperty(DiagramsItem, IsRequired=false)]
        public virtual DiagramConfigurationElementCollection Diagrams
        {
            get 
            {
                return base[DiagramsItem] as DiagramConfigurationElementCollection; 
            }
        }
    }

    public class DelegatingParagramConfigurationSection
        : ParagraphConfigurationSectionBase
    {
        private const string PicturesItem = "pictures";

        [ConfigurationProperty(PicturesItem, IsRequired = false)]
        public virtual PictureConfigurationElementCollection Pictures
        {
            get
            {
                return base[PicturesItem] as PictureConfigurationElementCollection;
            }
        }
    }
    public class GenericsParagramConfigurationSection 
        : ParagraphConfigurationSectionBase { }
}
