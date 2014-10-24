using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uility.Example.动态加载dll
{
    using System.Runtime.InteropServices;

    class AnotherDynamicLoad
    {
        delegate int Add(int a, int b);


        public void Process()
        {
            int hModule = NativeMethod.LoadLibrary(@"c:\CppDemo.dll");
            if (hModule == 0) return;

            //2. 读取函数指针
            IntPtr intPtr = NativeMethod.GetProcAddress(hModule, "Add");

            //3. 将函数指针封装成委托
            Add addFunction = (Add)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(Add));

            //4. 测试
            Console.WriteLine(addFunction(1, 2));

        }
    }
    public static class NativeMethod
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern int LoadLibrary(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        public static extern IntPtr GetProcAddress(int hModule,
            [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        public static extern bool FreeLibrary(int hModule);
    }

}
