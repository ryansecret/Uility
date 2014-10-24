using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Uility.Example.共享内存
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            long offset = 0x10000000; // 256 megabytes
            long length = 0x20000000; // 512 megabytes

            // Create the memory-mapped file.
            using (MemoryMappedFile mmf =
                MemoryMappedFile.CreateFromFile(@"c:\ExtremelyLargeImage.data",
                                                FileMode.Open, "ImgA"))
            {
                // Create a random access view, from the 256th megabyte (the offset)
                // to the 768th megabyte (the offset plus length).
                using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(offset, length))
                {
                    int colorSize = Marshal.SizeOf(typeof (MyColor));
                    MyColor color;

                    // Make changes to the view.
                    for (long i = 0; i < length; i += colorSize)
                    {
                        accessor.Read(i, out color);
                        color.Brighten(10);
                        accessor.Write(i, ref color);
                    }
                }
            }
        }

        #region Nested type: MyColor

        public struct MyColor
        {
            public short Alpha;
            public short Blue;
            public short Green;
            public short Red;

            // Make the view brigher.
            public void Brighten(short value)
            {
                Red = (short) Math.Min(short.MaxValue, Red + value);
                Green = (short) Math.Min(short.MaxValue, Green + value);
                Blue = (short) Math.Min(short.MaxValue, Blue + value);
                Alpha = (short) Math.Min(short.MaxValue, Alpha + value);
            }
        }

        #endregion
    }

    public class CompactHeap<T> : IDisposable where T : new()
    {
        #region Fields

        private readonly int _rawSize;

        private readonly T _t;
        private readonly Type _type;
        private IntPtr _currPtr;
        private IntPtr _ptr;

        #endregion

        public CompactHeap(int num)
        {
            _t = new T();

            _type = _t.GetType();


            _rawSize = Marshal.SizeOf(_t);

            _ptr = Marshal.AllocHGlobal(_rawSize*num);

            _currPtr = _ptr;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Marshal.FreeHGlobal(_currPtr);
        }

        #endregion

        public void Add(T t)
        {
            Marshal.StructureToPtr(t, _ptr, false);

            _ptr = new IntPtr((_ptr.ToInt32() + _rawSize));
        }

        public T Get(int index)
        {
            var p = new IntPtr(_currPtr.ToInt32() + _rawSize*index);

            return (T) Marshal.PtrToStructure(p, _type);
        }
    }
}