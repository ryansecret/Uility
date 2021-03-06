﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tools.cs" company="">
//   
// </copyright>
// <summary>
//   工具类定义
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq.Expressions;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace Framework.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Management;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// 工具类定义
    /// </summary>
    public static  class Tools
    {
        #region 序列化和反序列化

        /// <summary>
        /// 二进制反列化
        /// </summary>
        /// <typeparam name="T">
        /// 反序列化的类型
        /// </typeparam>
        /// <param name="filename">
        /// 反序列化的XML文件名
        /// </param>
        /// <returns>
        /// T类型对象
        /// </returns>
        public static T BinaryDeserialize<T>(string filename)
        {
            // 检查文件是否存在
            if (!File.Exists(filename))
            {
                return default(T);
            }

            T obj = default(T);
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();
                try
                {
                    obj = (T)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Trace.WriteLine(e);
                }
            }

            return obj;
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型
        /// </typeparam>
        /// <param name="filename">
        /// 文件名
        /// </param>
        /// <param name="IsEncryption">
        /// 是否已加密
        /// </param>
        /// <returns>
        /// 对象
        /// </returns>
        public static T BinaryDeserialize<T>(string filename, bool IsEncryption)
        {
            // 检查文件是否存在
            if (!File.Exists(filename))
            {
                return default(T);
            }

            T obj = default(T);
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
          
            using (var bw = new BinaryReader(fs))
            {
                var data = new byte[fs.Length];
                
                bw.Read(data, 0, data.Length);

                obj = BinaryDeserialize<T>(data, IsEncryption);
            }

            return obj;
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型
        /// </typeparam>
        /// <param name="serializedData">
        /// 数据
        /// </param>
        /// <returns>
        /// 对象
        /// </returns>
        public static T BinaryDeserialize<T>(byte[] serializedData)
        {
            return BinaryDeserialize<T>(serializedData, false);
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型
        /// </typeparam>
        /// <param name="serializedData">
        /// 数据
        /// </param>
        /// <param name="IsEncryption">
        /// 是否已加密
        /// </param>
        /// <returns>
        /// 对象
        /// </returns>
        public static T BinaryDeserialize<T>(byte[] serializedData, bool IsEncryption)
        {
            byte[] temp = null;
            if (IsEncryption)
            {
                temp = Decrypt(serializedData);
            }
            else
            {
                temp = serializedData;
            }

            var bf = new BinaryFormatter();
            T destObject;
            using (var ms = new MemoryStream())
            {
                ms.Write(temp, 0, temp.Length);
                ms.Seek(0, SeekOrigin.Begin);
                destObject = (T)bf.Deserialize(ms);
            }

            return destObject;
        }

        /// <summary>
        /// 二进制序列化
        /// </summary>
        /// <typeparam name="T">
        /// 序列化的类型
        /// </typeparam>
        /// <param name="obj">
        /// 序列化的对象
        /// </param>
        /// <param name="filename">
        /// 序列化的XML文件名
        /// </param>
        public static void BinarySerializer<T>(T obj, string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, obj);
                }
                catch (SerializationException e)
                {
                    Trace.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// 二进制对象序列化
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型
        /// </typeparam>
        /// <param name="obj">
        /// 对象
        /// </param>
        /// <param name="filename">
        /// 文件名
        /// </param>
        /// <param name="IsEncryption">
        /// 是否加密
        /// </param>
        /// <returns>
        /// 操作是否成功
        /// </returns>
        public static bool BinarySerializer<T>(T obj, string filename, bool IsEncryption)
        {
            try
            {
                var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);

                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(BinarySerializer(obj, IsEncryption));
                }

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            return false;
        }
        public static T Clone<T>(T obj)
        {
            T result = default(T);
            try
            {
                if (!typeof(T).IsSerializable)
                {
                    throw new Exception("此对象不可序列化！");
                }

                var bf = new BinaryFormatter();
                
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    result = (T)bf.Deserialize(ms);

                }
               
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
            return result;

        }
        /// <summary>
        /// 二进制对象序列化
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型
        /// </typeparam>
        /// <param name="destObject">
        /// 对象
        /// </param>
        /// <returns>
        /// 结果数据
        /// </returns>
        public static byte[] BinarySerializer<T>(T destObject)
        {
            return BinarySerializer(destObject, false);
        }

        /// <summary>
        /// 二进制对象序列化
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型
        /// </typeparam>
        /// <param name="destObject">
        /// 对象
        /// </param>
        /// <param name="IsEncryption">
        /// 是否加密
        /// </param>
        /// <returns>
        /// 结果数据
        /// </returns>
        public static byte[] BinarySerializer<T>(T destObject, bool IsEncryption)
        {
            var bf = new BinaryFormatter();
            byte[] destBuf = null;
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, destObject);
                
                destBuf = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(destBuf, 0, destBuf.Length);
            }

            byte[] temp = null;
            if (IsEncryption)
            {
                temp = Encryption(destBuf);
            }
            else
            {
                temp = destBuf;
            }

            return temp;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">
        /// 目录名
        /// </param>
        public static void CreateDir(string path)
        {
             
            string[] dirs = path.Split(char.Parse("\\"));
            string tmp = string.Empty;
            if (!Directory.Exists(path))
            {
                foreach (string item in dirs)
                {
                    tmp += item + "\\";
                    if (!Directory.Exists(tmp))
                    {
                        Directory.CreateDirectory(tmp);
                    }
                }
            }
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="Value">
        /// 待解密数据
        /// </param>
        /// <returns>
        /// 已解密数据
        /// </returns>
        public static byte[] Decrypt(byte[] Value)
        {
            var temp = new byte[Value.Length - 1];
            byte Key = Value[Value.Length - 1];
            int Index = 0;
            foreach (byte item in Value)
            {
                temp[Index] = (byte)(item ^ Key);

                Index++;
                if (Index == Value.Length - 1)
                {
                    break;
                }
            }

            string String64 = Encoding.UTF8.GetString(temp);
            
            byte[] Value64 = Convert.FromBase64String(String64);
            return Value64;
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="Value">
        /// 待加密数据
        /// </param>
        /// <returns>
        /// 已加密数据
        /// </returns>
        public static byte[] Encryption(byte[] Value)
        {
            string String64 = Convert.ToBase64String(Value);
            byte[] Value64 = Encoding.UTF8.GetBytes(String64);
            var temp = new byte[Value64.Length + 1];
            var rm = new Random(DateTime.Now.Millisecond);
            var Key = (byte)rm.Next(2, 254);
            int Index = 0;
            foreach (byte item in Value64)
            {
                temp[Index] = (byte)(item ^ Key);
                Index++;
            }

            temp[Index] = Key;

            return temp;
        }


        /// <summary>
        /// 
        /// 获取cpu序列号   
        ///  </summary>
        /// <returns>
        /// string 
        /// </returns>
        public static string GetCpuInfo()
        {
            string cpuInfo = string.Empty;
            var cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }

            return cpuInfo;
        }


        /// <summary>
        /// 
        /// 获取硬盘ID   
        ///  </summary>
        /// <returns>
        /// string 
        /// </returns>
        public static string GetHDid()
        {
          
            string HDid = string.Empty;
            var cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid = (string)mo.Properties["Model"].Value;
            }

            return HDid;
        }


        /// <summary>
        /// 
        /// 获取网卡硬件地址 
        ///  </summary>
        /// <returns>
        /// string 
        /// </returns>
        public static string GetMoAddress()
        {
            string MoAddress = string.Empty;
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if ((bool)mo["IPEnabled"])
                {
                    MoAddress = mo["MacAddress"].ToString();
                }

                mo.Dispose();
            }

            return MoAddress;
        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T">
        /// 反序列化的类型
        /// </typeparam>
        /// <param name="filename">
        /// 反序列化的XML文件名
        /// </param>
        /// <returns>
        /// T类型对象
        /// </returns>
        public static T XmlDeserialize<T>(string filename)
        {
            // 检查文件是否存在
            if (!File.Exists(filename))
            {
                return default(T);
            }

            T obj = default(T);

            using (var stream = new FileStream(filename, FileMode.Open))
            {
                var xml = new XmlSerializer(typeof(T));
                obj = (T)xml.Deserialize(stream);
            }

            return obj;
        }

        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T">
        /// 序列化的类型
        /// </typeparam>
        /// <param name="obj">
        /// 序列化的对象
        /// </param>
        /// <param name="filename">
        /// 序列化的XML文件名
        /// </param>
        public static void XmlSerializer<T>(T obj, string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                var xml = new XmlSerializer(obj.GetType());
                xml.Serialize(stream, obj);
            }
        }

        /// <summary>
        /// Serialize the object to xml.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="item">Object to serialize.</param>
        /// <returns>XML contents representing the serialized object.</returns>
        public static string XmlSerialize<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter writer = new StringWriter(stringBuilder))
            {
                serializer.Serialize(writer, item);
            }
            return stringBuilder.ToString();
        }

        #endregion

        #region 字符串处理
        public static  string Combine(List<string> words,char word)
        {
            
            return words.Aggregate((current,next)=>current+word+next);
        }

        #endregion

        #region 指定程序集加载资源
        public static string GetInternalFileContent(string assemblyFolderPath, string fileName)
        {
            Assembly current = Assembly.GetExecutingAssembly();

            Stream stream = current.GetManifestResourceStream(assemblyFolderPath + fileName);
            if (stream == null)
            {
                return string.Empty;
            }
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            return content;
        }
        #endregion


        /// Throws an ArgumentNullException if the given data item is null.
        /// </summary>
        /// <param name="data">The item to check for nullity.</param>
        /// <param name="name">The name to use when throwing an exception, if necessary</param>
        public static void ThrowIfNull<T>(this T data, string name) where T : class
        {
            if (data == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if the given data item is null.
        /// No parameter name is specified.
        /// </summary>
        /// <param name="data">The item to check for nullity.</param>
        public static void ThrowIfNull<T>(this T data) where T : class
        {
            if (data == null)
            {
                throw new ArgumentNullException();
            }
        }


        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
               
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
               
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic)
            {
               
            }

            return memberExpression.Member.Name;
        }

        public static void InvokeAction<TPayload>(this Action<TPayload> action, TPayload argument)
        {
            ThreadPool.QueueUserWorkItem((o) => action(argument));
        }
    }
}