// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Api.cs" company="">
//   
// </copyright>
// <summary>
//   The api.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility.Example.共享内存
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml;

    /// <summary>
    /// The api.
    /// </summary>
    public static class API
    {
        #region Constants and Fields

        /// <summary>
        ///   The erro r_ alread y_ exists.
        /// </summary>
        public const int ERROR_ALREADY_EXISTS = 183;

        /// <summary>
        ///   The fil e_ ma p_ al l_ access.
        /// </summary>
        public const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;

        /// <summary>
        ///   The fil e_ ma p_ copy.
        /// </summary>
        public const int FILE_MAP_COPY = 0x0001;

        /// <summary>
        ///   The fil e_ ma p_ read.
        /// </summary>
        public const int FILE_MAP_READ = 0x0004;

        /// <summary>
        ///   The fil e_ ma p_ write.
        /// </summary>
        public const int FILE_MAP_WRITE = 0x0002;

        /// <summary>
        ///   The invali d_ handl e_ value.
        /// </summary>
        public const int INVALID_HANDLE_VALUE = -1;

        /// <summary>
        ///   The pag e_ execute.
        /// </summary>
        public const int PAGE_EXECUTE = 0x10;

        /// <summary>
        ///   The pag e_ execut e_ read.
        /// </summary>
        public const int PAGE_EXECUTE_READ = 0x20;

        /// <summary>
        ///   The pag e_ execut e_ readwrite.
        /// </summary>
        public const int PAGE_EXECUTE_READWRITE = 0x40;

        /// <summary>
        ///   The pag e_ readonly.
        /// </summary>
        public const int PAGE_READONLY = 0x02;

        /// <summary>
        ///   The pag e_ readwrite.
        /// </summary>
        public const int PAGE_READWRITE = 0x04;

        /// <summary>
        ///   The pag e_ writecopy.
        /// </summary>
        public const int PAGE_WRITECOPY = 0x08;

        /// <summary>
        ///   The se c_ commit.
        /// </summary>
        public const int SEC_COMMIT = 0x8000000;

        /// <summary>
        ///   The se c_ image.
        /// </summary>
        public const int SEC_IMAGE = 0x1000000;

        /// <summary>
        ///   The se c_ nocache.
        /// </summary>
        public const int SEC_NOCACHE = 0x10000000;

        /// <summary>
        ///   The se c_ reserve.
        /// </summary>
        public const int SEC_RESERVE = 0x4000000;

        #endregion

        #region Public Methods

        /// <summary>
        /// The close handle.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <returns>
        /// The close handle.
        /// </returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// The create file mapping.
        /// </summary>
        /// <param name="hFile">
        /// The h file.
        /// </param>
        /// <param name="lpAttributes">
        /// The lp attributes.
        /// </param>
        /// <param name="flProtect">
        /// The fl protect.
        /// </param>
        /// <param name="dwMaxSizeHi">
        /// The dw max size hi.
        /// </param>
        /// <param name="dwMaxSizeLow">
        /// The dw max size low.
        /// </param>
        /// <param name="lpName">
        /// The lp name.
        /// </param>
        /// <returns>
        /// </returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(
            int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);

        /// <summary>
        /// The get last error.
        /// </summary>
        /// <returns>
        /// The get last error.
        /// </returns>
        [DllImport("kernel32", EntryPoint = "GetLastError")]
        public static extern int GetLastError();

        /// <summary>
        /// The map view of file.
        /// </summary>
        /// <param name="hFileMapping">
        /// The h file mapping.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <param name="dwFileOffsetHigh">
        /// The dw file offset high.
        /// </param>
        /// <param name="dwFileOffsetLow">
        /// The dw file offset low.
        /// </param>
        /// <param name="dwNumberOfBytesToMap">
        /// The dw number of bytes to map.
        /// </param>
        /// <returns>
        /// </returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr MapViewOfFile(
            IntPtr hFileMapping,
            uint dwDesiredAccess,
            uint dwFileOffsetHigh,
            uint dwFileOffsetLow,
            uint dwNumberOfBytesToMap);

        /// <summary>
        /// The open file mapping.
        /// </summary>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <param name="bInheritHandle">
        /// The b inherit handle.
        /// </param>
        /// <param name="lpName">
        /// The lp name.
        /// </param>
        /// <returns>
        /// </returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(
            int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        /// <summary>
        /// The rtl compare memory.
        /// </summary>
        /// <param name="Destination">
        /// The destination.
        /// </param>
        /// <param name="Source">
        /// The source.
        /// </param>
        /// <param name="Length">
        /// The length.
        /// </param>
        /// <returns>
        /// The rtl compare memory.
        /// </returns>
        [DllImport("ntdll.dll")]
        public static extern int RtlCompareMemory(IntPtr Destination, IntPtr Source, int Length);

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="Msg">
        /// The msg.
        /// </param>
        /// <param name="wParam">
        /// The w param.
        /// </param>
        /// <param name="lParam">
        /// The l param.
        /// </param>
        /// <returns>
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        /// <summary>
        /// The unmap view of file.
        /// </summary>
        /// <param name="pvBaseAddress">
        /// The pv base address.
        /// </param>
        /// <returns>
        /// The unmap view of file.
        /// </returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

        /// <summary>
        /// The memcmp.
        /// </summary>
        /// <param name="ptr1">
        /// The ptr 1.
        /// </param>
        /// <param name="ptr2">
        /// The ptr 2.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The memcmp.
        /// </returns>
        [DllImport("msvcrt.dll", SetLastError = true)]
        public static extern int memcmp(IntPtr ptr1, IntPtr ptr2, int count);

        /// <summary>
        /// The memcmp.
        /// </summary>
        /// <param name="ptr1">
        /// The ptr 1.
        /// </param>
        /// <param name="ptr2">
        /// The ptr 2.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The memcmp.
        /// </returns>
        [DllImport("msvcrt.dll", SetLastError = true)]
        public static extern unsafe int memcmp(void* ptr1, void* ptr2, int count);

        #endregion
    }

    /// <summary>
    /// The memory result.
    /// </summary>
    public enum MemoryResult
    {
        /// <summary>
        ///   The success.
        /// </summary>
        Success,

        /// <summary>
        ///   The not initialized.
        /// </summary>
        NotInitialized,

        /// <summary>
        ///   The no change.
        /// </summary>
        NoChange,

        /// <summary>
        ///   The failed.
        /// </summary>
        Failed
    }

    /// <summary>
    /// The share memory.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ShareMemory<T> : IDisposable
        where T : class
    {
        #region Constants and Fields

        /// <summary>
        ///   The m_ mem size.
        /// </summary>
        private long m_MemSize = 0;

        /// <summary>
        ///   The m_b already exist.
        /// </summary>
        private bool m_bAlreadyExist = false;

        /// <summary>
        ///   The m_b init.
        /// </summary>
        private bool m_bInit = false;

        /// <summary>
        ///   The m_h shared memory file.
        /// </summary>
        private IntPtr m_hSharedMemoryFile = IntPtr.Zero;

        /// <summary>
        ///   The m_last data.
        /// </summary>
        private byte[] m_lastData;

        /// <summary>
        ///   The m_pw data.
        /// </summary>
        private IntPtr m_pwData = IntPtr.Zero;

        /// <summary>
        ///   The m_size.
        /// </summary>
        private int m_size;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ShareMemory{T}" /> class.
        /// </summary>
        public ShareMemory()
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ShareMemory{T}"/> class.
        /// </summary>
        ~ShareMemory()
        {
            this.Close();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// /// 关闭共享内存     ///
        /// </summary>
        public void Close()
        {
            if (this.m_bInit)
            {
                API.UnmapViewOfFile(this.m_pwData);
                API.CloseHandle(this.m_hSharedMemoryFile);
            }
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }

        /// <summary>
        /// /// 初始化共享内存     ///
        /// </summary>
        /// <param name="strName">
        /// The str Name.
        /// </param>
        public MemoryResult Init(string strName)
        {
            this.m_size = 10240;

            // 固定10K        
            var lngSize = this.m_size;
            if (lngSize <= 0 || lngSize > 0x00800000)
            {
                lngSize = 0x00800000;
            }

            this.m_MemSize = lngSize;
            if (strName.Length > 0)
            {
                // 创建内存共享体 (INVALID_HANDLE_VALUE)         
                this.m_hSharedMemoryFile = API.CreateFileMapping(
                    API.INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)API.PAGE_READWRITE, 0, (uint)lngSize, strName);
                if (this.m_hSharedMemoryFile == IntPtr.Zero)
                {
                    this.m_bAlreadyExist = false;
                    this.m_bInit = false;
                    return MemoryResult.Failed; // 创建共享体失败            
                }
                else
                {
                    if (API.GetLastError() == API.ERROR_ALREADY_EXISTS)
                    {
                        // 已经创建               
                        this.m_bAlreadyExist = true;
                    }
                    else
                    {
                        // 新创建              
                        this.m_bAlreadyExist = false;
                    }
                }

                // ---------------------------------------             //创建内存映射           
                this.m_pwData = API.MapViewOfFile(this.m_hSharedMemoryFile, API.FILE_MAP_WRITE, 0, 0, (uint)lngSize);
                if (this.m_pwData == IntPtr.Zero)
                {
                    this.m_bInit = false;
                    API.CloseHandle(this.m_hSharedMemoryFile);
                    return MemoryResult.Failed; // 创建内存映射失败            
                }
                else
                {
                    this.m_bInit = true;
                    if (this.m_bAlreadyExist == false)
                    {
                        // 初始化           
                    }
                }

                // ----------------------------------------       
            }
            else
            {
                return MemoryResult.Failed;

                // 参数错误             
            }

            return MemoryResult.Success;

            // 创建成功    
        }

        /// <summary>
        /// /// 读数据     ///
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1631:DocumentationMustMeetCharacterPercentage",
            Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented",
            Justification = "Reviewed. Suppression is OK here.")]
        public unsafe MemoryResult Read(out T obj)
        {
            obj = default(T);
            byte[] bytData = new byte[this.m_size];
            if (this.m_bInit)
            {
                Marshal.Copy(this.m_pwData, bytData, 0, this.m_size);
                if (this.m_lastData != null)
                {
                    fixed (byte* p1 = this.m_lastData)
                    {
                        fixed (byte* p2 = bytData)
                        {
                            if (API.memcmp(p1, p2, this.m_size) == 0)
                            {
                                return MemoryResult.NoChange;
                            }
                        }
                    }
                }

                this.m_lastData = bytData;
                var fmt = new BinaryFormatter();
                using (var ms = new MemoryStream(bytData))
                {
                    try
                    {
                        obj = (T)fmt.Deserialize(ms);
                    }
                    catch (SerializationException)
                    {
                        return MemoryResult.Failed;
                    }
                }
            }
            else
            {
                return MemoryResult.NotInitialized;

                // 共享内存未初始化         
            }

            return MemoryResult.Success;

            // 读成功    
        }

        /// <summary>
        /// /// 写数据     ///
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        public MemoryResult Write(T obj)
        {
            var fmt = new BinaryFormatter();
            byte[] bytData;
            if (obj is byte[])
            {
                bytData = obj as byte[];
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    fmt.Serialize(ms, obj);
                    bytData = ms.ToArray();
                }
            }

            if (this.m_bInit)
            {
                Marshal.Copy(bytData, 0, this.m_pwData, bytData.Length);
            }
            else
            {
                return MemoryResult.NotInitialized;
            }

            // 共享内存未初始化         
            return MemoryResult.Success;

            // 写成功    
        }

        #endregion
    }

 
}