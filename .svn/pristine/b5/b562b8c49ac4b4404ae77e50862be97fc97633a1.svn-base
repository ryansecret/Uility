using System;
using System.Configuration;
namespace MarvellousWorks.PracticalPattern.Concept.Configurating
{
    // 整个配置节组的对象, 包括 <delegating> 和 <generics> 两个配置节
    //  <sectionGroup name="marvellousWorks.practicalPattern.concept"/>
    public class ChapterConfigurationSectionGroup : ConfigurationSectionGroup
    {
        private const string DelegatingItem = "delegating";
        private const string GenericsItem = "generics";

        public ChapterConfigurationSectionGroup() : base() { }


        [ConfigurationProperty(DelegatingItem, IsRequired=true)]
        public virtual DelegatingParagramConfigurationSection Delegating
        {
            get{return base.Sections[DelegatingItem] 
                as DelegatingParagramConfigurationSection;}
        }

        [ConfigurationProperty(GenericsItem, IsRequired=true)]
        public virtual GenericsParagramConfigurationSection Generics
        {
            get { return base.Sections[GenericsItem] 
                as GenericsParagramConfigurationSection; }
        }
    }
}
