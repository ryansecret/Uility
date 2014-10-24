using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Persistence;
using System.Text;
using System.Threading;
using Message = System.ServiceModel.Channels.Message;

namespace Web
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,ConcurrencyMode =ConcurrencyMode.Reentrant)]
    [Serializable]
    [DurableService]
    [ServiceContract(SessionMode =SessionMode.Required)]
    public class Service1 : IService1, IServiceBehavior 
    {
        [OperationContract(IsOneWay = true)]  
        [TransactionFlow(TransactionFlowOption.Allowed)]
        public void DoWork()
        {
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.Message);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

           ShowProgress callBackChannel= OperationContext.Current.GetCallbackChannel<ShowProgress>();
           
    
            for (int i = 0; i < 100; i++)
            {
                callBackChannel.Show();
            }
              
        }
        [DurableOperation(CompletesInstance = true)]
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler handler = new FaultErrorHandler();

            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                // 增加错误处理器
                dispatcher.ErrorHandlers.Add(handler);
            }
        }
    }
    public class FaultErrorHandler : IErrorHandler
    {
        /// <summary>
        /// 在异常返回给客户端之后被调用
        /// </summary>
        /// <param name="error">异常</param>
        /// <returns></returns>
        public bool HandleError(System.Exception error)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\WCF_Log.txt", true);
            sw.Write("IErrorHandler - HandleError测试。错误类型：{0}；错误信息：{1}", error.GetType().ToString(), error.Message);
            sw.WriteLine();
            sw.Flush();
            sw.Close();

            // true - 已处理
            return true;
        }

        /// <summary>
        /// 在异常发生后，异常信息返回前被调用
        /// </summary>
        /// <param name="error">异常</param>
        /// <param name="version">SOAP版本</param>
        /// <param name="fault">返回给客户端的错误信息</param>
        public void ProvideFault(System.Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            if (error is System.IO.IOException)
            {
                FaultException ex = new FaultException("IErrorHandler - ProvideFault测试");

                MessageFault mf = ex.CreateMessageFault();

                fault = Message.CreateMessage(version, mf, ex.Action);

                // InvalidOperationException error = new InvalidOperationException("An invalid operation has occurred.");
                // MessageFault mfault = MessageFault.CreateFault(new FaultCode("Server", new FaultCode(String.Format("Server.{0}", error.GetType().Name))), new FaultReason(error.Message), error);
                // FaultException fe = FaultException.CreateFault(mfault, typeof(InvalidOperationException));
            }
        }
    }
}

