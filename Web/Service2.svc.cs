using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Transactions;

namespace Web
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service2”。
 
    
    public class Service2 : IService2
    {  
        [TransactionFlow(TransactionFlowOption.Mandatory)]
   
        public void DoWork()
        {
            
        }
    }

    [MessageContract]
    public class FileWrapper
    {
        /// <summary>
        /// 指定数据成员为 SOAP 消息头
        /// </summary>
        [MessageHeader]
        public string FilePath;

        /// <summary>
        /// 指定将成员序列化为 SOAP 正文中的元素
        /// </summary>
        [MessageBodyMember]
        public Stream FileData;

        
    }

   

}
