// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
//   
// </copyright>
// <summary>
//   The example.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Uility.Example.动态方法
{
    /// <summary>
    /// The example.
    /// </summary>
    public class Example
    {
        // The following constructor and private field are used to
        // demonstrate a method bound to an object.
        #region Constants and Fields

        /// <summary>
        /// The test.
        /// </summary>
        private int test;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Example"/> class.
        /// </summary>
        /// <param name="test">
        /// The test.
        /// </param>
        public Example(int test)
        {
            this.test = test;
        }

        #endregion

        // Declare delegates that can be used to execute the completed 
        // SquareIt dynamic method. The OneParameter delegate can be 
        // used to execute any method with one parameter and a return
        // value, or a method with two parameters and a return value
        // if the delegate is bound to an object.
        #region Delegates

        /// <summary>
        /// The one parameter.
        /// </summary>
        /// <param name="p0">
        /// The p 0.
        /// </param>
        /// <typeparam name="TReturn">
        /// </typeparam>
        /// <typeparam name="TParameter0">
        /// </typeparam>
        private delegate TReturn OneParameter<TReturn, TParameter0>(TParameter0 p0);

        /// <summary>
        /// The square it invoker.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        private delegate long SquareItInvoker(int input);

        #endregion

        #region Public Methods

        /// <summary>
        /// The main.
        /// </summary>
        public static void Main()
        {
            // Example 1: A simple dynamic method.
            // Create an array that specifies the parameter types for the
            // dynamic method. In this example the only parameter is an 
            // int, so the array has only one element.
            Type[] methodArgs = { typeof(int) };

            // Create a DynamicMethod. In this example the method is
            // named SquareIt. It is not necessary to give dynamic 
            // methods names. They cannot be invoked by name, and two
            // dynamic methods can have the same name. However, the 
            // name appears in calls stacks and can be useful for
            // debugging. 
            // In this example the return type of the dynamic method
            // is long. The method is associated with the module that 
            // contains the Example class. Any loaded module could be
            // specified. The dynamic method is like a module-level
            // static method.
            DynamicMethod squareIt = new DynamicMethod("SquareIt", typeof(long), methodArgs, typeof(Example).Module);

            // Emit the method body. In this example ILGenerator is used
            // to emit the MSIL. DynamicMethod has an associated type
            // DynamicILInfo that can be used in conjunction with 
            // unmanaged code generators.
            // The MSIL loads the argument, which is an int, onto the 
            // stack, converts the int to a long, duplicates the top
            // item on the stack, and multiplies the top two items on the
            // stack. This leaves the squared number on the stack, and 
            // all the method has to do is return.
            ILGenerator il = squareIt.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Conv_I8);
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Mul);
            il.Emit(OpCodes.Ret);

            // Create a delegate that represents the dynamic method. 
            // Creating the delegate completes the method, and any further 
            // attempts to change the method (for example, by adding more
            // MSIL) are ignored. The following code uses a generic 
            // delegate that can produce delegate types matching any
            // single-parameter method that has a return type.
            OneParameter<long, int> invokeSquareIt =
                (OneParameter<long, int>)squareIt.CreateDelegate(typeof(OneParameter<long, int>));

            Console.WriteLine("123456789 squared = {0}", invokeSquareIt(123456789));

            // Example 2: A dynamic method bound to an instance.
            // Create an array that specifies the parameter types for a
            // dynamic method. If the delegate representing the method
            // is to be bound to an object, the first parameter must 
            // match the type the delegate is bound to. In the following
            // code the bound instance is of the Example class. 
            Type[] methodArgs2 = { typeof(Example), typeof(int) };

            // Create a DynamicMethod. In this example the method has no
            // name. The return type of the method is int. The method 
            // has access to the protected and private data of the 
            // Example class.
            DynamicMethod multiplyHidden = new DynamicMethod(string.Empty, typeof(int), methodArgs2, typeof(Example));

            // Emit the method body. In this example ILGenerator is used
            // to emit the MSIL. DynamicMethod has an associated type
            // DynamicILInfo that can be used in conjunction with 
            // unmanaged code generators.
            // The MSIL loads the first argument, which is an instance of
            // the Example class, and uses it to load the value of a 
            // private instance field of type int. The second argument is
            // loaded, and the two numbers are multiplied. If the result
            // is larger than int, the value is truncated and the most 
            // significant bits are discarded. The method returns, with
            // the return value on the stack.
            ILGenerator ilMH = multiplyHidden.GetILGenerator();
            ilMH.Emit(OpCodes.Ldarg_0);

            FieldInfo testInfo = typeof(Example).GetField("test", BindingFlags.NonPublic | BindingFlags.Instance);

            ilMH.Emit(OpCodes.Ldfld, testInfo);
            ilMH.Emit(OpCodes.Ldarg_1);
            ilMH.Emit(OpCodes.Mul);
            ilMH.Emit(OpCodes.Ret);


             

            // Create a delegate that represents the dynamic method. 
            // Creating the delegate completes the method, and any further 
            // attempts to change the method � for example, by adding more
            // MSIL � are ignored. 
            // The following code binds the method to a new instance
            // of the Example class whose private test field is set to 42.
            // That is, each time the delegate is invoked the instance of
            // Example is passed to the first parameter of the method.
            // The delegate OneParameter is used, because the first
            // parameter of the method receives the instance of Example.
            // When the delegate is invoked, only the second parameter is
            // required. 
            OneParameter<int, int> invoke =
                (OneParameter<int, int>)multiplyHidden.CreateDelegate(typeof(OneParameter<int, int>), new Example(42));

            Console.WriteLine("3 * test = {0}", invoke(3));
        }

        #endregion
    }
}

/* This code example produces the following output:

123456789 squared = 15241578750190521
3 * test = 126
 */