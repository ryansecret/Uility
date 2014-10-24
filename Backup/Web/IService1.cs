using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;
using System.Data.SqlClient;
namespace Web
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    
    [ServiceContract(CallbackContract=typeof (ShowProgress))]
     
    [DeliveryRequirements(QueuedDeliveryRequirements = QueuedDeliveryRequirementsMode.Required)]
    public interface IService1
    {
        [OperationContract(IsOneWay =true)]
         [MethodImpl(MethodImplOptions.Synchronized)]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DoWork();
    }

    public interface ShowProgress
    {
        [OperationContract(IsOneWay = true)]
        void Show();
    }
    [XmlRoot]
    [XmlSerializerFormat]
    [MessageContract]
   
    public class Test
    {
        [XmlElement]
        public string title { get; set; }
        [OnSerializing]
        void Serializing(StreamingContext context)
        {
            SqlConnection sqlConnection = new SqlConnection("");
            SqlCommand command = new SqlCommand("", sqlConnection);
            sqlConnection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
             
        }

     
    }

    [Serializable]
    [XmlSerializerFormat]
    public class SoapFormatterOjbect
    {
        [XmlAttribute]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Time { get; set; }
    }
}
