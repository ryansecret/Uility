using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Uility.Linq.表达式树
{

    public class ClassA
    {
       public  int A1 { get; set; }
       public string A2 { get; set; }
    }

    public class HaveTry
    {
        public void Try()
        {
            List<ClassA> list = new List<ClassA>();
            list.Add(new ClassA() { A1 = 1, A2 = "2" });
            list.Add(new ClassA() { A1 = 11, A2 = "22" });
            list.Add(new ClassA() { A1 = 122, A2 = "122" });

            ParameterExpression parameter = Expression.Parameter(typeof(ClassA), "p");


            //获取对象的相关属性
            var left = Expression.PropertyOrField(parameter, "A1");

            var right = Expression.Constant(1);
            ExpressionType op = ExpressionType.GreaterThan;

            var result = Expression.MakeBinary(op, left, right);
           

            right = Expression.Constant(100);
            op = ExpressionType.LessThan;

            var result2 = Expression.MakeBinary(op, left, right);

            ExpressionType et = ExpressionType.And;
            var newResult = Expression.MakeBinary(et, result, result2);

            var expression = Expression.Lambda(newResult, parameter) as Expression<Func<ClassA, bool>>;

            var newList = list.Where(expression.Compile());

            ParameterExpression param = Expression.Parameter(typeof(int), "x");
            Expression<Func<int>> negateExpr =
                Expression.Lambda<Func<int >>(
                    Expression.Negate(param)
                     );
            Expression<Func<int>> todo = () => 1;
           
            Func<int> negateFunc = negateExpr.Compile();   
        }

        public List<T> ExpressionTree<T>(List<T> collection, object propertyName, string propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

            ParameterExpression value = Expression.Parameter(typeof(string), "propertyValue");

            MethodInfo setter = typeof(T).GetMethod("set_" + propertyName);

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
    public static class GeneralEventHandling
    {
        static object GeneralHandler(params object[] args)
        {
            Console.WriteLine("您的事件发生了说");
            return null;
        }

        public static void AttachGeneralHandler(object target, EventInfo targetEvent)
        {
            //获得事件响应程序的委托类型
            var delegateType = targetEvent.EventHandlerType;

            //这个委托的Invoke方法有我们所需的签名信息
            MethodInfo invokeMethod = delegateType.GetMethod("Invoke");

            //按照这个委托制作所需要的参数
            ParameterInfo[] parameters = invokeMethod.GetParameters();
            ParameterExpression[] paramsExp = new ParameterExpression[parameters.Length];
            Expression[] argsArrayExp = new Expression[parameters.Length];

            //参数一个个转成object类型。有些本身即是object，管他呢……
            for (int i = 0; i < parameters.Length; i++)
            {
                paramsExp[i] = Expression.Parameter(parameters[i].ParameterType, parameters[i].Name);
                argsArrayExp[i] = Expression.Convert(paramsExp[i], typeof(Object));
            }

            //调用我们的GeneralHandler
            MethodInfo executeMethod = typeof(GeneralEventHandling).GetMethod(
                "GeneralHandler", BindingFlags.Static | BindingFlags.NonPublic);

            Expression lambdaBodyExp =
                Expression.Call(null, executeMethod, Expression.NewArrayInit(typeof(Object), argsArrayExp));

             
            //如果有返回值，那么将返回值转换成委托要求的类型
            //如果没有返回值就这样搁那里就成了
            if (!invokeMethod.ReturnType.Equals(typeof(void)))
            {
                //这是有返回值的情况
                lambdaBodyExp = Expression.Convert(lambdaBodyExp, invokeMethod.ReturnType);
            }

            //组装到一起
            LambdaExpression dynamicDelegateExp = Expression.Lambda(delegateType, lambdaBodyExp, paramsExp);

            //我们创建的Expression是这样的一个函数：
            //(委托的参数们) => GeneralHandler(new object[] { 委托的参数们 })

            //编译
            Delegate dynamiceDelegate = dynamicDelegateExp.Compile();

            //完成!
            targetEvent.AddEventHandler(target, dynamiceDelegate);
        }
    }

    public  class ExpresionTree
    {

        public class OperationsVisitor : ExpressionVisitor
        {
            public Expression Modify(Expression expression)
            {
                return Visit(expression);
            }
            protected override Expression VisitBinary(BinaryExpression b)
            {
                if (b.NodeType == ExpressionType.Add)
                {
                    Expression left = this.Visit(b.Left);
                    Expression right = this.Visit(b.Right);
                    return Expression.Subtract(left, right);
                }
                return base.VisitBinary(b);
            }


        }

        static void Main(string[] args)
        {
            Expression<Func<int, int, int>> lambda = (a, b) => a + b * 2;
            var operationsVisitor = new OperationsVisitor();
            Expression modifyExpression = operationsVisitor.Modify(lambda);
            Console.WriteLine(modifyExpression.ToString());



        }
    }

    public class QueryableData<TData> : IQueryable<TData>
    {
        public QueryableData()
        {
            Provider = new TerryQueryProvider();
            Expression = Expression.Constant(this);
        }
        public QueryableData(TerryQueryProvider provider,
            Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            if (!typeof(IQueryable<TData>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }
            Provider = provider;
            Expression = expression;
        }
        public IQueryProvider Provider { get; private set; }
        public Expression Expression { get; private set; }
        public Type ElementType
        {
            get { return typeof(TData); }
        }
        public IEnumerator<TData> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<TData>>(Expression)).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }
    }
    public class TerryQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = Type.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(
                    typeof(QueryableData<>).MakeGenericType(elementType),
                    new object[] { this, expression });
            }
            catch
            {
                throw new Exception();
            }
        }
        public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
      
            return new QueryableData<TResult>(this, expression);
        }
        public object Execute(Expression expression)
        {
            // ......
        }
        public TResult Execute<TResult>(Expression expression)
        {
            // ......
        }
    }


    internal static class TypeSystem
    {

        internal static Type GetElementType(Type seqType)
        {

            Type ienum = FindIEnumerable(seqType);

            if (ienum == null) return seqType;

            return ienum.GetGenericArguments()[0];

        }

        private static Type FindIEnumerable(Type seqType)
        {

            if (seqType == null || seqType == typeof(string))

                return null;

            if (seqType.IsArray)

                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());

            if (seqType.IsGenericType)
            {

                foreach (Type arg in seqType.GetGenericArguments())
                {

                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);

                    if (ienum.IsAssignableFrom(seqType))
                    {

                        return ienum;

                    }

                }

            }

            Type[] ifaces = seqType.GetInterfaces();

            if (ifaces != null && ifaces.Length > 0)
            {

                foreach (Type iface in ifaces)
                {

                    Type ienum = FindIEnumerable(iface);

                    if (ienum != null) return ienum;

                }

            }

            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {

                return FindIEnumerable(seqType.BaseType);

            }

            return null;

        }

        static void ExpressionTest()
        {
            ParameterExpression penum_1 = Expression.Parameter(typeof(double), "num_1");
            ParameterExpression penum_2 = Expression.Parameter(typeof(double),
"num_2");
            BinaryExpression _be = Expression.Add(penum_1, penum_2);
            BinaryExpression _be2 = Expression.Power(_be, penum_2);
            Expression<Func<double, double, double>> ef = Expression.Lambda<Func
<double, double, double>>(_be2, new ParameterExpression[] { 

penum_1, penum_2 });
            Func<double, double, double> cf = ef.Compile();
            
        }

    }


    public class DynamicPropertyAccessor
    {
        private Func<object, object> m_getter;

        public DynamicPropertyAccessor(Type type, string propertyName)
            : this(type.GetProperty(propertyName))
        { }

        public DynamicPropertyAccessor(PropertyInfo propertyInfo)
        {
            // target: (object)((({TargetType})instance).{Property})

            // preparing parameter, object type
            ParameterExpression instance = Expression.Parameter(
                typeof(object), "instance");

            // ({TargetType})instance
            Expression instanceCast = Expression.Convert(
                instance, propertyInfo.ReflectedType);

            // (({TargetType})instance).{Property}
            Expression propertyAccess = Expression.Property(
                instanceCast, propertyInfo);

            // (object)((({TargetType})instance).{Property})
            UnaryExpression castPropertyValue = Expression.Convert(
                propertyAccess, typeof(object));

            // Lambda expression
            Expression<Func<object, object>> lambda =
                Expression.Lambda<Func<object, object>>(
                    castPropertyValue, instance);
           
            this.m_getter = lambda.Compile();
        }

        public object GetValue(object o)
        {
            return this.m_getter(o);
        }
    }

}
