﻿using System;
using System.IO;
using System.Threading;

class Program
{

    static void Main()
    {
        try
        {
            // Initialize a Stream resource to pass 
            // to the DisposableResource class.
            Console.Write("Enter filename and its path: ");
            string fileSpec = Console.ReadLine();
            FileStream fs = File.OpenRead(fileSpec);
            DisposableResource TestObj = new DisposableResource(fs);

            // Use the resource.
            TestObj.DoSomethingWithResource();

            // Dispose the resource.
            TestObj.Dispose();

        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}


// This class shows how to use a disposable resource.
// The resource is first initialized and passed to
// the constructor, but it could also be
// initialized in the constructor.
// The lifetime of the resource does not 
// exceed the lifetime of this instance.
// This type does not need a finalizer because it does not
// directly create a native resource like a file handle
// or memory in the unmanaged heap.

public class DisposableResource : IDisposable
{

    private Stream _resource;
    private bool _disposed;

    // The stream passed to the constructor 
    // must be readable and not null.
    public DisposableResource(Stream stream)
    {
        if (stream == null)
            throw new ArgumentNullException("Stream in null.");
        if (!stream.CanRead)
            throw new ArgumentException("Stream must be readable.");

        _resource = stream;

        _disposed = false;
    }

    // Demonstrates using the resource. 
    // It must not be already disposed.
    public void DoSomethingWithResource()
    {
        if (_disposed)
            throw new ObjectDisposedException("Resource was disposed.");

        // Show the number of bytes.
        int numBytes = (int)_resource.Length;
        Console.WriteLine("Number of bytes: {0}", numBytes.ToString());
    }
    ~DisposableResource()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        
        // Use SupressFinalize in case a subclass
        // of this type implements a finalizer.
        // note:然而，在调用 Dispose 方法后，通常不需要垃圾回收器调用已释放对象的终结器。 为防止自动终止， Dispose 实现可以调用 GC.SuppressFinalize 方法。 

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // If you need thread safety, use a lock around these 
        // operations, as well as in your methods that use the resource.
        if (!_disposed)
        {
            if (disposing)
            {
                if (_resource != null)
                    _resource.Dispose();
                Console.WriteLine("Object disposed.");
            }

            // Indicate that the instance has been disposed.
            _resource = null;
            _disposed = true;
        }
    }
}