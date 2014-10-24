using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Threading;

namespace Uility.WPF.DelegateCommand
{
    public static class DelegateCommandExtensions
{
	/// <summary>
	/// Makes DelegateCommnand listen on PropertyChanged events of some object,
	/// so that DelegateCommnand can update its IsEnabled property.
	/// </summary>
	public static DelegateCommand ListenOn<ObservedType, PropertyType>
		(this DelegateCommand delegateCommand, 
		ObservedType observedObject, 
		Expression<Func<ObservedType, PropertyType>> propertyExpression,
		Dispatcher dispatcher)
		where ObservedType : INotifyPropertyChanged
	{
		//string propertyName = observedObject.GetPropertyName(propertyExpression);
		string propertyName = NotifyPropertyChangedBaseExtensions.GetPropertyName(propertyExpression);

		observedObject.PropertyChanged += (sender, e) =>
        {
        	if (e.PropertyName == propertyName)
        	{
				if (dispatcher != null)
				{
					ThreadTools.RunInDispatcher(dispatcher, delegateCommand.RaiseCanExecuteChanged);
				}
				else
				{
					delegateCommand.RaiseCanExecuteChanged();
				}
        	}
        };

		return delegateCommand; //chain calling
	}

    
    public static DelegateCommand<T> ListenOn<T, ObservedType, PropertyType>
        (this DelegateCommand<T> delegateCommand,
        ObservedType observedObject,
        Expression<Func<ObservedType, PropertyType>> propertyExpression,
        Dispatcher dispatcher)
        where ObservedType : INotifyPropertyChanged
    {
        //string propertyName = observedObject.GetPropertyName(propertyExpression);
        string propertyName = NotifyPropertyChangedBaseExtensions.GetPropertyName(propertyExpression);

        observedObject.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
        {
            if (e.PropertyName == propertyName)
            {
                if (dispatcher != null)
                {
                    ThreadTools.RunInDispatcher(dispatcher, delegateCommand.RaiseCanExecuteChanged);
                }
                else
                {
                    delegateCommand.RaiseCanExecuteChanged();
                }
            }
        };

        return delegateCommand; //chain calling
    }
}

    public static class NotifyPropertyChangedBaseExtensions
    {
        /// <summary>
        /// Raises PropertyChanged event.
        /// To use: call the extension method with this: this.OnPropertyChanged(n => n.Title);
        /// </summary>
        /// <typeparam name="T">Property owner</typeparam>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="observableBase"></param>
        /// <param name="expression">Property expression like 'n => n.Property'</param>
        public static void OnPropertyChanged<T, TProperty>(this T observableBase, Expression<Func<T, TProperty>> expression) where T : INotifyPropertyChangedWithRaise
        {
            observableBase.OnPropertyChanged(GetPropertyName<T, TProperty>(expression));
        }

        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression) where T : INotifyPropertyChanged
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            if (memberExpression == null)
                throw new ArgumentException("Please provide a lambda expression like 'n => n.PropertyName'");

            MemberInfo memberInfo = memberExpression.Member;

            if (String.IsNullOrEmpty(memberInfo.Name))
                throw new ArgumentException("'expression' did not provide a property name.");

            return memberInfo.Name;
        }
    }

    public interface INotifyPropertyChangedWithRaise : INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }
    public class ThreadTools
    {
        public static void RunInDispatcher(Dispatcher dispatcher, Action action)
        {
            RunInDispatcher(dispatcher, DispatcherPriority.Normal, action);
        }

        public static void RunInDispatcher(Dispatcher dispatcher, DispatcherPriority priority, Action action)
        {
            if (action == null) { return; }

            if (dispatcher.CheckAccess())
            {
                // we are already on thread associated with the dispatcher -> just call action
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    //Log error here!
                }
            }
            else
            {
                // we are on different thread, invoke action on dispatcher's thread
                dispatcher.BeginInvoke(
                    priority,
                    (Action)(
                    () =>
                    {
                        try
                        {
                            action();
                        }
                        catch (Exception ex)
                        {
                            //Log error here!
                        }
                    })
                );
            }
        }
    }



}
