using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Uility
{
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Xml;

    class DynamicObject
    {
    }
    public partial class MainPage : UserControl
    {

        public void Initialize()
        {
            string XMLStr = @"
            <NewDataSet>
                <Table  TableName='City'  TableShowName='城市' >
                    <Column   Name='CityName' ShowName='城市名称' /> 
                    <Column   Name='CityTel' ShowName='城市区号' /> 
                    <Column   Name='CityCounty' ShowName='城市所属国家' />
                    <Column   Name='CityName' ShowName='df称' /> 
                    <Column   Name='CityTel' ShowName='城df号' /> 
                    <Column   Name='CityCounty' ShowName='城市df所属国家' />
                </Table> 
                <Table  TableName='User'  TableShowName='用户' >
                    <Column   Name='UserName' ShowName='用户名' /> 
                    <Column   Name='UserPwd' ShowName='用户密码' /> 
                    <Column   Name='UserTel' ShowName='用户电话' /> 
                    <Column   Name='UserEmail' ShowName='用户邮箱' /> 
                </Table> 
            </NewDataSet>";
            List<GridClass> gridClassList = new List<GridClass>();
            using (XmlReader xmlRead = XmlReader.Create(new StringReader(XMLStr)))
            {
                xmlRead.Read();
                while (xmlRead.Read())
                {
                    //获取到一个TABLE,然后转化为一个动态的实体类。
                    gridClassList.Clear();
                    xmlRead.ReadToFollowing("Table");
                    string TableShowName = xmlRead.GetAttribute("TableShowName");
                    string TableName = xmlRead.GetAttribute("TableName");
                    try
                    {
                        using (XmlReader xReader2 = xmlRead.ReadSubtree())
                        {
                            while (xReader2.ReadToFollowing("Column"))
                            {
                                string ShowName = xReader2.GetAttribute("ShowName");
                                string Name = xReader2.GetAttribute("Name");
                                GridClass gclass = new GridClass() { ShowName = ShowName, Name = Name };
                                gridClassList.Add(gclass);
                            }
                            List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();

                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            for (int j = 0; j < gridClassList.Count; j++)
                            {
                                dic[gridClassList[j].Name] = "--" + gridClassList[j].Name + "--";
                            }
                            dicList.Add(dic);

                            DataGrid dgrid = new DataGrid();
                            dgrid.HorizontalAlignment = HorizontalAlignment.Left;
                            dgrid.VerticalAlignment = VerticalAlignment.Top;
                            dgrid.Margin = new Thickness(20, 5, 0, 0);
                            dgrid.Width = 960;
                            dgrid.Name = TableName;

                            List<Dictionary<string, object>> test = new List<Dictionary<string, object>>();
                            Dictionary<string, object> add = new Dictionary<string, object>();
                            add.Add("UserName", "用户名");
                            add.Add("UserPwd", "用户密码");
                            add.Add("UserTel", 123);
                            test.Add(add);
                            Dictionary<string, object> addTwo = new Dictionary<string, object>();
                            addTwo.Add("UserName", "chen");
                            addTwo.Add("UserPwd", "777777");
                            addTwo.Add("UserTel", 123456);
                            test.Add(addTwo);
                            dgrid.ItemsSource = GetEnumerableTwo(test).ToDataSource();
                            //  dgrid.ItemsSource = GetEnumerable(dicList).ToDataSource();

                           
                        }
                    }
                    catch (Exception ex)
                    { }
                }
            }
        }

        public void Initial()
        {

            List<GridClass> gridClassList = new List<GridClass>();
            gridClassList.Clear();

            try
            {


                List<Dictionary<string, object>> test = new List<Dictionary<string, object>>();
                Dictionary<string, object> add = new Dictionary<string, object>();
                add.Add("UserName", "用户名");
                add.Add("UserPwd", true);
                add.Add("UserTel", 123);

                test.Add(add);
                Dictionary<string, object> addTwo = new Dictionary<string, object>();
                addTwo.Add("UserName", "chen");
                addTwo.Add("UserPwd", false);
                addTwo.Add("UserTel", 123456);
                test.Add(addTwo);
                 

            }
            catch (Exception ex)
            { }


        }

       
        public IEnumerable<IDictionary> GetEnumerable(List<Dictionary<string, string>> SourceList)
        {
            for (int i = 0; i < SourceList.Count; i++)
            {
                var dict = new Dictionary<string, string>();
                dict = SourceList[i];
                yield return dict;
            }
        }

        public IEnumerable<IDictionary> GetEnumerableTwo(List<Dictionary<string, object>> SourceList)
        {
            for (int i = 0; i < SourceList.Count; i++)
            {
                var dict = new Dictionary<string, object>();
                dict = SourceList[i];
                yield return dict;
            }
        }

       

    }
    public static class DataSourceCreator
    {
        private static readonly Regex PropertNameRegex =
               new Regex(@"^[A-Za-z]+[A-Za-z1-9_]*$", RegexOptions.Singleline);
        public static List<object> ToDataSource(this IEnumerable<IDictionary> list)
        {
            IDictionary firstDict = null;
            bool hasData = false;
            foreach (IDictionary currentDict in list)
            {
                hasData = true;
                firstDict = currentDict;
                break;
            }
            if (!hasData)
            {
                return new List<object> { };
            }
            if (firstDict == null)
            {
                throw new ArgumentException("IDictionary entry cannot be null");
            }
            Type objectType = null;
            TypeBuilder tb = GetTypeBuilder(list.GetHashCode());
            ConstructorBuilder constructor =
                        tb.DefineDefaultConstructor(
                                    MethodAttributes.Public |
                                    MethodAttributes.SpecialName |
                                    MethodAttributes.RTSpecialName);
            foreach (DictionaryEntry pair in firstDict)
            {
                if (PropertNameRegex.IsMatch(Convert.ToString(pair.Key), 0))
                {
                    CreateProperty(tb,
                                    Convert.ToString(pair.Key),
                                    pair.Value == null ?
                                                typeof(object) :
                                                pair.Value.GetType());
                }
                else
                {
                    throw new ArgumentException(
                                @"Each key of IDictionary must be
                                alphanumeric and start with character.");
                }
            }
           
            objectType = tb.CreateType();
            return GenerateArray(objectType, list, firstDict);
        }
        private static List<object> GenerateArray(Type objectType, IEnumerable<IDictionary> list, IDictionary firstDict)
        {
            var itemsSource = new List<object>();
            foreach (var currentDict in list)
            {
                if (currentDict == null)
                {
                    throw new ArgumentException("IDictionary entry cannot be null");
                }
                object row = Activator.CreateInstance(objectType);
                foreach (DictionaryEntry pair in firstDict)
                {
                    if (currentDict.Contains(pair.Key))
                    {
                        PropertyInfo property =
                            objectType.GetProperty(Convert.ToString(pair.Key));
                        property.SetValue(
                            row,
                            Convert.ChangeType(
                                    currentDict[pair.Key],
                                    property.PropertyType,
                                    null),
                            null);
                    }
                }
                itemsSource.Add(row);
            }
            return itemsSource;
        }

        private static TypeBuilder GetTypeBuilder(int code)
        {
            AssemblyName an = new AssemblyName("TempAssembly" + code);
            AssemblyBuilder assemblyBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType("TempType" + code
                                , TypeAttributes.Public |
                                TypeAttributes.Class |
                                TypeAttributes.AutoClass |
                                TypeAttributes.AnsiClass |
                                TypeAttributes.BeforeFieldInit |
                                TypeAttributes.AutoLayout
                                , typeof(object));
            return tb;
        }
        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName,
                                                        propertyType,
                                                        FieldAttributes.Private);

            PropertyBuilder propertyBuilder =
                tb.DefineProperty(
                    propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr =
                tb.DefineMethod("get_" + propertyName,
                    MethodAttributes.Public |
                    MethodAttributes.SpecialName |
                    MethodAttributes.HideBySig,
                    propertyType, Type.EmptyTypes);
            ILGenerator getIL = getPropMthdBldr.GetILGenerator();
            getIL.Emit(OpCodes.Ldarg_0);
            
            getIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getIL.Emit(OpCodes.Ret);
            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new Type[] { propertyType });
            ILGenerator setIL = setPropMthdBldr.GetILGenerator();
            
            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fieldBuilder);
            setIL.Emit(OpCodes.Ret);
            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }
    }
    /// <summary>
    /// 存放动态表格的字段
    /// </summary>
    public class GridClass
    {
        private string _ShowName;
        private string _Name;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName
        {
            get { return _ShowName; }
            set { _ShowName = value; }
        }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}
