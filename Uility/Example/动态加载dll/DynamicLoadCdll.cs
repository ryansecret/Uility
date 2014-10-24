// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicLoadCdll.cs" company="">
//   
// </copyright>
// <summary>
//   The dynamic load cdll.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility.Example.动态加载dll
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// The dynamic load cdll.
    /// </summary>
    public class DynamicLoadCdll
    {
        #region Constants and Fields

        /// <summary>
        /// The lcma p_ simplifie d_ chinese.
        /// </summary>
        private const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;

        /// <summary>
        /// The lcma p_ traditiona l_ chinese.
        /// </summary>
        private const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

        #endregion

        #region Public Methods

        /// <summary>
        /// The lc map string.
        /// </summary>
        /// <param name="Locale">
        /// The locale.
        /// </param>
        /// <param name="dwMapFlags">
        /// The dw map flags.
        /// </param>
        /// <param name="lpSrcStr">
        /// The lp src str.
        /// </param>
        /// <param name="cchSrc">
        /// The cch src.
        /// </param>
        /// <param name="lpDestStr">
        /// The lp dest str.
        /// </param>
        /// <param name="cchDest">
        /// The cch dest.
        /// </param>
        /// <returns>
        /// The lc map string.
        /// </returns>
        [DllImport("kernel32.dll", EntryPoint = "LCMapStringA")]
        public static extern int LCMapString(
            int Locale, int dwMapFlags, byte[] lpSrcStr, int cchSrc, byte[] lpDestStr, int cchDest);

        /// <summary>
        /// The simple to tadition.
        /// </summary>
        /// <param name="src">
        /// The src.
        /// </param>
        /// <returns>
        /// The simple to tadition.
        /// </returns>
        public string SimpleToTadition(string src)
        {
            byte[] srcByte = Encoding.Default.GetBytes(src);

            byte[] desByte = new byte[srcByte.Length];

            LCMapString(2052, LCMAP_TRADITIONAL_CHINESE, srcByte, -1, desByte, srcByte.Length);

            string des = Encoding.Default.GetString(desByte);
            return des;
        }

        /// <summary>
        /// The tadition to simple.
        /// </summary>
        /// <param name="src">
        /// The src.
        /// </param>
        /// <returns>
        /// The tadition to simple.
        /// </returns>
        public string TaditionToSimple(string src)
        {
            byte[] srcByte = Encoding.Default.GetBytes(src);

            byte[] desByte = new byte[srcByte.Length];

            LCMapString(2052, LCMAP_SIMPLIFIED_CHINESE, srcByte, -1, desByte, srcByte.Length);

            string des = Encoding.Default.GetString(desByte);
            return des;
        }

        #endregion
    }

    /// <summary>
    /// The dld.
    /// </summary>
    public class DLD
    {
        // 声明LoadLibrary、GetProcAddress、FreeLibrary及私有变量hModule和farProc：
        #region Constants and Fields

        /// <summary>
        ///   The far proc.
        /// </summary>
        /// GetProcAddress 返回的函数指针
        public IntPtr farProc = IntPtr.Zero;

        /// <summary>
        ///   The h module.
        /// </summary>
        /// Loadlibrary 返回的函数库模块的句柄
        private IntPtr hModule = IntPtr.Zero;

        #endregion

        #region Public Methods

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="ObjArray_Parameter">
        /// The obj array_ parameter.
        /// </param>
        /// <param name="TypeArray_ParameterType">
        /// The type array_ parameter type.
        /// </param>
        /// <param name="ModePassArray_Parameter">
        /// The mode pass array_ parameter.
        /// </param>
        /// <param name="Type_Return">
        /// The type_ return.
        /// </param>
        /// <returns>
        /// The invoke.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="PlatformNotSupportedException">
        /// </exception>
        public object Invoke(
            object[] ObjArray_Parameter, 
            Type[] TypeArray_ParameterType, 
            ModePass[] ModePassArray_Parameter, 
            Type Type_Return)
        {
            // 下面 3 个 if 是进行安全检查 , 若不能通过 , 则抛出异常
            if (this.hModule == IntPtr.Zero)
            {
                throw new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !");
            }

            if (this.farProc == IntPtr.Zero)
            {
                throw new Exception(" 函数指针为空 , 请确保已进行 LoadFun 操作 !");
            }

            if (ObjArray_Parameter.Length != ModePassArray_Parameter.Length)
            {
                throw new Exception(" 参数个数及其传递方式的个数不匹配 .");
            }

            // 下面是创建 MyAssemblyName 对象并设置其 Name 属性
            AssemblyName MyAssemblyName = new AssemblyName();
            MyAssemblyName.Name = "InvokeFun";

            // 生成单模块配件
            AssemblyBuilder MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                MyAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InvokeDll");

            // 定义要调用的方法 , 方法名为“ MyFun ”，返回类型是“ Type_Return ”
            // 参数类型是“ TypeArray_ParameterType ”
            string methodName = "Process";
            MethodBuilder MyMethodBuilder = MyModuleBuilder.DefineGlobalMethod(
                methodName, MethodAttributes.Public | MethodAttributes.Static, Type_Return, TypeArray_ParameterType);

            // 获取一个 ILGenerator ，用于发送所需的 IL
            ILGenerator IL = MyMethodBuilder.GetILGenerator();

            int i;
            for (i = 0; i < ObjArray_Parameter.Length; i++)
            {
                // 用循环将参数依次压入堆栈
                switch (ModePassArray_Parameter[i])
                {
                    case ModePass.ByValue:
                        IL.Emit(OpCodes.Ldarg, i);
                        break;
                    case ModePass.ByRef:
                        IL.Emit(OpCodes.Ldarga, i);
                        break;
                    default:
                        throw new Exception(" 第 " + (i + 1).ToString() + " 个参数没有给定正确的传递方式 .");
                }
            }

            if (IntPtr.Size == 4)
            {
                // 判断处理器类型
                IL.Emit(OpCodes.Ldc_I4, this.farProc.ToInt32());
            }
            else if (IntPtr.Size == 8)
            {
                IL.Emit(OpCodes.Ldc_I8, this.farProc.ToInt64());
            }
            else
            {
                throw new PlatformNotSupportedException();
            }

            IL.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, Type_Return, TypeArray_ParameterType);
            IL.Emit(OpCodes.Ret); // 返回值
            MyModuleBuilder.CreateGlobalFunctions();

            // 取得方法信息
            MethodInfo MyMethodInfo = MyModuleBuilder.GetMethod(methodName);
            return MyMethodInfo.Invoke(null, ObjArray_Parameter); // 调用方法，并返回其值
        }

        // Invoke方法的第二个版本，它是调用了第一个版本的：
        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="IntPtr_Function">
        /// The int ptr_ function.
        /// </param>
        /// <param name="ObjArray_Parameter">
        /// The obj array_ parameter.
        /// </param>
        /// <param name="TypeArray_ParameterType">
        /// The type array_ parameter type.
        /// </param>
        /// <param name="ModePassArray_Parameter">
        /// The mode pass array_ parameter.
        /// </param>
        /// <param name="Type_Return">
        /// The type_ return.
        /// </param>
        /// <returns>
        /// The invoke.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public object Invoke(
            IntPtr IntPtr_Function, 
            object[] ObjArray_Parameter, 
            Type[] TypeArray_ParameterType, 
            ModePass[] ModePassArray_Parameter, 
            Type Type_Return)
        {
            // 下面 2 个 if 是进行安全检查 , 若不能通过 , 则抛出异常
            if (this.hModule == IntPtr.Zero)
            {
                throw new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !");
            }

            if (IntPtr_Function == IntPtr.Zero)
            {
                throw new Exception(" 函数指针 IntPtr_Function 为空 !");
            }

            this.farProc = IntPtr_Function;
            return this.Invoke(ObjArray_Parameter, TypeArray_ParameterType, ModePassArray_Parameter, Type_Return);
        }

        // 添加LoadDll方法，并为了调用时方便，重载了这个方法：
        /// <summary>
        /// The load dll.
        /// </summary>
        /// <param name="lpFileName">
        /// The lp file name.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void LoadDll(string lpFileName)
        {
            this.hModule = LoadLibrary(lpFileName);
            if (this.hModule == IntPtr.Zero)
            {
                throw new Exception(" 没有找到 :" + lpFileName + ".");
            }
        }

        // 若已有已装载Dll的句柄，可以使用LoadDll方法的第二个版本：
        /// <summary>
        /// The load dll.
        /// </summary>
        /// <param name="HMODULE">
        /// The hmodule.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void LoadDll(IntPtr HMODULE)
        {
            if (HMODULE == IntPtr.Zero)
            {
                throw new Exception(" 所传入的函数库模块的句柄 HMODULE 为空 .");
            }

            this.hModule = HMODULE;
        }

        // 5.       添加LoadFun方法，并为了调用时方便，也重载了这个方法，方法的具体代码及注释如下：
        /// <summary>
        /// The load fun.
        /// </summary>
        /// <param name="lpProcName">
        /// The lp proc name.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public void LoadFun(string lpProcName)
        {
            // 若函数库模块的句柄为空，则抛出异常
            if (this.hModule == IntPtr.Zero)
            {
                throw new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !");
            }

            // 取得函数指针
            this.farProc = GetProcAddress(this.hModule, lpProcName);

            // 若函数指针，则抛出异常
            if (this.farProc == IntPtr.Zero)
            {
                throw new Exception(" 没有找到 : " + lpProcName + " 这个函数的入口点 ");
            }
        }

        /// <summary>
        /// The load fun.
        /// </summary>
        /// <param name="lpFileName">
        /// The lp file name.
        /// </param>
        /// <param name="lpProcName">
        /// The lp proc name.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public void LoadFun(string lpFileName, string lpProcName)
        {
            // 取得函数库模块的句柄
            this.hModule = LoadLibrary(lpFileName);

            // 若函数库模块的句柄为空，则抛出异常
            if (this.hModule == IntPtr.Zero)
            {
                throw new Exception(" 没有找到 :" + lpFileName + ".");
            }

            // 取得函数指针
            this.farProc = GetProcAddress(this.hModule, lpProcName);

            // 若函数指针，则抛出异常
            if (this.farProc == IntPtr.Zero)
            {
                throw new Exception(" 没有找到 :" + lpProcName + " 这个函数的入口点 ");
            }
        }

        // 6.  添加UnLoadDll及Invoke方法，Invoke方法也进行了重载：
        /// <summary>
        /// The un load dll.
        /// </summary>
        /// 卸载 Dll
        public void UnLoadDll()
        {
            FreeLibrary(this.hModule);
            this.hModule = IntPtr.Zero;
            this.farProc = IntPtr.Zero;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The free library.
        /// </summary>
        /// <param name="hModule">
        /// The h module.
        /// </param>
        /// <returns>
        /// The free library.
        /// </returns>
        [DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// The get proc address.
        /// </summary>
        /// <param name="hModule">
        /// The h module.
        /// </param>
        /// <param name="lpProcName">
        /// The lp proc name.
        /// </param>
        /// <returns>
        /// </returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary>
        /// The load library.
        /// </summary>
        /// <param name="lpFileName">
        /// The lp file name.
        /// </param>
        /// <returns>
        /// </returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        #endregion
    }

    /// <summary>
    /// The mode pass.
    /// </summary>
    public enum ModePass
    {
        /// <summary>
        ///   The by value.
        /// </summary>
        ByValue = 0x0001, 

        /// <summary>
        ///   The by ref.
        /// </summary>
        ByRef = 0x0002
    }
}