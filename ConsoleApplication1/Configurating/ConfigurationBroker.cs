using System;
using System.Configuration;
namespace MarvellousWorks.PracticalPattern.Concept.Configurating
{
    // 用于调度 App.Config 相关 Configuration 的 Broker 类型
    public static class ConfigurationBroker
    {
        private static ChapterConfigurationSectionGroup group;

        static ConfigurationBroker()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            group = (ChapterConfigurationSectionGroup)config.GetSectionGroup
                    ("marvellousWorks.practicalPattern.concept");
        }

        public static DelegatingParagramConfigurationSection Delegating
        { get { return group.Delegating; } }
        public static GenericsParagramConfigurationSection Generics
        { get { return group.Generics; } }
    }
}
