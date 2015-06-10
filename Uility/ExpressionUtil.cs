using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Uility
{
    public static class ExpressionUtil
    {
        /// <summary>
        /// Create a function delegate representing a unary operation
        /// </summary>
        /// <typeparam name="TArg1">The parameter type</typeparam>
        /// <typeparam name="TResult">The return type</typeparam>
        /// <param name="body">Body factory</param>
        /// <returns>Compiled function delegate</returns>
        public static Func<TArg1, TResult> CreateExpression<TArg1, TResult>(
            Func<Expression, UnaryExpression> body)
        {
//            ParameterExpression penum_1 = Expression.Parameter(typeof(double), "num_1");
//            ParameterExpression penum_2 = Expression.Parameter(typeof(double),
//"num_2");

//            BinaryExpression _be = Expression.Add(penum_1, penum_2);
//            BinaryExpression _be2 = Expression.Power(_be, penum_2);
//            Expression<Func<double, double, double>> ef = Expression.Lambda<Func
//<double, double, double>>(_be2, new ParameterExpression[] { 

//penum_1, penum_2 });
//            Func<double, double, double> cf = ef.Compile();

//          ParameterExpression param = Expression.Parameter(typeof(int), "x");   
//        Expression<Func<int, int>> negateExpr =   
//            Expression.Lambda<Func<int, int>>(   
//                Expression.Negate(param),   
//                new ParameterExpression[ ] { param } );   
//        Delegate.CreateDelegate()
//        Func<int, int> negateFunc = negateExpr.Compile( );   

//          BinaryExpression valueObj = Expression.ArrayIndex(
//                parametersParameter, Expression.Constant(i));

            //ExpressionType expressionType = ExpressionType.GreaterThan;
            //Expression.MakeBinary(expressionType,)
            //Expression.Constant(1, typeof (int));

            ParameterExpression inp = Expression.Parameter(typeof (TArg1), "inp");
            try
            {
                return Expression.Lambda<Func<TArg1, TResult>>(body(inp), inp).Compile();
            }
            catch (Exception ex)
            {
                string msg = ex.Message; // avoid capture of ex itself
                return delegate { throw new InvalidOperationException(msg); };
            }
        }

        /// <summary>
        /// Create a function delegate representing a binary operation
        /// </summary>
        /// <typeparam name="TArg1">The first parameter type</typeparam>
        /// <typeparam name="TArg2">The second parameter type</typeparam>
        /// <typeparam name="TResult">The return type</typeparam>
        /// <param name="body">Body factory</param>
        /// <returns>Compiled function delegate</returns>
        public static Func<TArg1, TArg2, TResult> CreateExpression<TArg1, TArg2, TResult>(
            Func<Expression, Expression, BinaryExpression> body)
        {
            return CreateExpression<TArg1, TArg2, TResult>(body, false);
        }

        /// <summary>
        /// Create a function delegate representing a binary operation
        /// </summary>
        /// <param name="castArgsToResultOnFailure">
        /// If no matching operation is possible, attempt to convert
        /// TArg1 and TArg2 to TResult for a match? For example, there is no
        /// "decimal operator /(decimal, int)", but by converting TArg2 (int) to
        /// TResult (decimal) a match is found.
        /// </param>
        /// <typeparam name="TArg1">The first parameter type</typeparam>
        /// <typeparam name="TArg2">The second parameter type</typeparam>
        /// <typeparam name="TResult">The return type</typeparam>
        /// <param name="body">Body factory</param>
        /// <returns>Compiled function delegate</returns>
        public static Func<TArg1, TArg2, TResult> CreateExpression<TArg1, TArg2, TResult>(
            Func<Expression, Expression, BinaryExpression> body, bool castArgsToResultOnFailure)
        {
            int i = 102;

            ParameterExpression lhs = Expression.Parameter(typeof (TArg1), "lhs");
            ParameterExpression rhs = Expression.Parameter(typeof (TArg2), "rhs");
            try
            {
                try
                {
                    return Expression.Lambda<Func<TArg1, TArg2, TResult>>(body(lhs, rhs), lhs, rhs).Compile();
                }
                catch (InvalidOperationException)
                {
                    if (castArgsToResultOnFailure &&
                        !( // if we show retry                                                        
                            typeof (TArg1) == typeof (TResult) && // and the args aren't
                            typeof (TArg2) == typeof (TResult)))
                    {
                        // already "TValue, TValue, TValue"...
                        // convert both lhs and rhs to TResult (as appropriate)
                        Expression castLhs = typeof (TArg1) == typeof (TResult)
                            ? (Expression) lhs
                            : (Expression) Expression.Convert(lhs, typeof (TResult));
                        Expression castRhs = typeof (TArg2) == typeof (TResult)
                            ? (Expression) rhs
                            : (Expression) Expression.Convert(rhs, typeof (TResult));

                        return Expression.Lambda<Func<TArg1, TArg2, TResult>>(
                            body(castLhs, castRhs), lhs, rhs).Compile();
                    }
                    else throw;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message; // avoid capture of ex itself
                return delegate { throw new InvalidOperationException(msg); };
            }
        }

        public static List<T> ExpressionTree<T>(List<T> collection, object propertyName, string propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof (T), "x");

            ParameterExpression value = Expression.Parameter(typeof (string), "propertyValue");

            MethodInfo setter = typeof (T).GetMethod("set_" + propertyName);

            MethodCallExpression call = Expression.Call(parameter, setter, value);

            LambdaExpression lambda = Expression.Lambda(call, parameter, value);
            var exp = lambda.Compile();
            for (int i = 0; i < collection.Count; i++)
            {
                exp.DynamicInvoke(collection[i], propertyValue);
            }
            return collection;
        }
    }

    public class DynamicMethodExecutor
    {
        private Func<object, object[], object> m_execute;

        public DynamicMethodExecutor(MethodInfo methodInfo)
        {
            this.m_execute = this.GetExecuteDelegate(methodInfo);
        }

        public object Execute(object instance, object[] parameters)
        {
            return this.m_execute(instance, parameters);
        }

        private Func<object, object[], object> GetExecuteDelegate(MethodInfo methodInfo)
        {
            // parameters to execute
            ParameterExpression instanceParameter =
                Expression.Parameter(typeof (object), "instance");
            ParameterExpression parametersParameter =
                Expression.Parameter(typeof (object[]), "parameters");

            // build parameter list
            List<Expression> parameterExpressions = new List<Expression>();
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                // (Ti)parameters[i]
                BinaryExpression valueObj = Expression.ArrayIndex(
                    parametersParameter, Expression.Constant(i));
                UnaryExpression valueCast = Expression.Convert(
                    valueObj, paramInfos[i].ParameterType);

                parameterExpressions.Add(valueCast);
            }

            // non-instance for static method, or ((TInstance)instance)
            Expression instanceCast = methodInfo.IsStatic
                ? null
                : Expression.Convert(instanceParameter, methodInfo.ReflectedType);

            // static invoke or ((TInstance)instance).Method
            MethodCallExpression methodCall = Expression.Call(
                instanceCast, methodInfo, parameterExpressions);

            // ((TInstance)instance).Method((T0)parameters[0], (T1)parameters[1], ...)
            if (methodCall.Type == typeof (void))
            {
                Expression<Action<object, object[]>> lambda =
                    Expression.Lambda<Action<object, object[]>>(
                        methodCall, instanceParameter, parametersParameter);

                Action<object, object[]> execute = lambda.Compile();
                return (instance, parameters) =>
                {
                    execute(instance, parameters);
                    return null;
                };
            }
            else
            {
                UnaryExpression castMethodCall = Expression.Convert(
                    methodCall, typeof (object));
                Expression<Func<object, object[], object>> lambda =
                    Expression.Lambda<Func<object, object[], object>>(
                        castMethodCall, instanceParameter, parametersParameter);

                return lambda.Compile();
            }
        }
    }
}