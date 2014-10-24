// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DyMothedBag.cs" company="">
//   
// </copyright>
// <summary>
//   The dy mothed bag.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Uility.Example.动态方法
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The dy mothed bag.
    /// </summary>
    public class DyMothedBag
    {
        #region Public Methods

        /// <summary>
        /// The show.
        /// </summary>
        public void Show()
        {
            // Create an object that contains some new methods:
            var newType = new MethodBag();
            newType.SetMethod("Write", () => Console.WriteLine("Hello World"));
            newType.SetMethod("Display", (string parm) => Console.WriteLine(parm));
            newType.SetMethod("IsValid", () => true);
            newType.SetMethod("Square", (int num) => num * num);
            newType.SetMethod("Sequence", () => from n in Enumerable.Range(1, 100) where n % 5 == 2 select n * n);

            

            dynamic dispatcher = newType;
            
            dispatcher.Write();
            var result = dispatcher.IsValid();
            Console.WriteLine(result);
            dispatcher.Display("This is a message");
            var result2 = dispatcher.Square(5);
            Console.WriteLine(result2);
            var sequence = dispatcher.Sequence();
            foreach (var num in sequence)
            {
                Console.WriteLine(num);
            }
        }

        #endregion
    }

    /// <summary>
    /// The method bag.
    /// </summary>
    public class MethodBag : DynamicObject
    {
        #region Constants and Fields

        /// <summary>
        /// The methods.
        /// </summary>
        private Dictionary<string, MethodDescription> methods = new Dictionary<string, MethodDescription>();

        #endregion

        #region Public Methods

        /// <summary>
        /// The set method.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="lambda">
        /// The lambda.
        /// </param>
        public void SetMethod(string name, Expression<Action> lambda)
        {
            var desc = new ActionDescription { target = lambda };
            this.methods.Add(name, desc);
        }

        /// <summary>
        /// The set method.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="lambda">
        /// The lambda.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public void SetMethod<T>(string name, Expression<Action<T>> lambda)
        {
            var desc = new ActionOfTDescription { target = lambda };
            this.methods.Add(name, desc);
        }

        /// <summary>
        /// The set method.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="lambda">
        /// The lambda.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        public void SetMethod<TResult>(string name, Expression<Func<TResult>> lambda)
        {
            var desc = new FuncDescription { target = lambda };
            this.methods.Add(name, desc);
        }

        /// <summary>
        /// The set method.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="lambda">
        /// The lambda.
        /// </param>
        /// <typeparam name="T1">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        public void SetMethod<T1, TResult>(string name, Expression<Func<T1, TResult>> lambda)
        {
            var desc = new FuncofTDescription { target = lambda };
            this.methods.Add(name, desc);
        }

        /// <summary>
        /// The try invoke member.
        /// </summary>
        /// <param name="binder">
        /// The binder.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The try invoke member.
        /// </returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            if (!this.methods.ContainsKey(binder.Name))
            {
                return false;
            }

            MethodDescription method = this.methods[binder.Name];

            if (method.NumberOfParameters != args.Length)
            {
                return false;
            }

            result = method.Invoke(args);
            return true;
        }

        #endregion

        /// <summary>
        /// The action description.
        /// </summary>
        private class ActionDescription : MethodDescription
        {
            #region Properties

            /// <summary>
            /// Gets NumberOfParameters.
            /// </summary>
            internal override int NumberOfParameters
            {
                get
                {
                    return 0;
                }
            }

            #endregion

            #region Methods

            /// <summary>
            /// The invoke.
            /// </summary>
            /// <param name="parms">
            /// The parms.
            /// </param>
            /// <returns>
            /// The invoke.
            /// </returns>
            internal override object Invoke(object[] parms)
            {
                var target2 = this.target as Expression<Action>;
                target2.Compile().Invoke();
                return null;
            }

            #endregion
        }

        /// <summary>
        /// The action of t description.
        /// </summary>
        private class ActionOfTDescription : MethodDescription
        {
            #region Properties

            /// <summary>
            /// Gets NumberOfParameters.
            /// </summary>
            internal override int NumberOfParameters
            {
                get
                {
                    return 1;
                }
            }

            #endregion

            #region Methods

            /// <summary>
            /// The invoke.
            /// </summary>
            /// <param name="parms">
            /// The parms.
            /// </param>
            /// <returns>
            /// The invoke.
            /// </returns>
            internal override object Invoke(object[] parms)
            {
                dynamic target2 = this.target;
                target2.Compile().Invoke(parms[0]);
                return null;
            }

            #endregion
        }

        /// <summary>
        /// The func description.
        /// </summary>
        private class FuncDescription : MethodDescription
        {
            #region Properties

            /// <summary>
            /// Gets NumberOfParameters.
            /// </summary>
            internal override int NumberOfParameters
            {
                get
                {
                    return 0;
                }
            }

            #endregion

            #region Methods

            /// <summary>
            /// The invoke.
            /// </summary>
            /// <param name="parms">
            /// The parms.
            /// </param>
            /// <returns>
            /// The invoke.
            /// </returns>
            internal override object Invoke(object[] parms)
            {
                dynamic target2 = this.target;
                return target2.Compile().Invoke();
            }

            #endregion
        }

        /// <summary>
        /// The funcof t description.
        /// </summary>
        private class FuncofTDescription : MethodDescription
        {
            #region Properties

            /// <summary>
            /// Gets NumberOfParameters.
            /// </summary>
            internal override int NumberOfParameters
            {
                get
                {
                    return 1;
                }
            }

            #endregion

            #region Methods

            /// <summary>
            /// The invoke.
            /// </summary>
            /// <param name="parms">
            /// The parms.
            /// </param>
            /// <returns>
            /// The invoke.
            /// </returns>
            internal override object Invoke(object[] parms)
            {
                dynamic target2 = this.target;
               
                return target2.Compile().Invoke(parms[0]);
            }

            #endregion
        }

        /// <summary>
        /// The method description.
        /// </summary>
        private abstract class MethodDescription
        {
            #region Properties

            /// <summary>
            /// Gets NumberOfParameters.
            /// </summary>
            internal abstract int NumberOfParameters { get; }

            /// <summary>
            /// Gets or sets target.
            /// </summary>
            internal Expression target { get; set; }

            #endregion

            #region Methods

            /// <summary>
            /// The invoke.
            /// </summary>
            /// <param name="parms">
            /// The parms.
            /// </param>
            /// <returns>
            /// The invoke.
            /// </returns>
            internal abstract object Invoke(object[] parms);

            #endregion
        }
    }
}