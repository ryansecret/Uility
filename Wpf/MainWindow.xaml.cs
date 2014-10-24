using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.ComponentModel;
using Brushes = System.Windows.Media.Brushes;
using Pen = System.Windows.Media.Pen;
using Point = System.Windows.Point;

namespace Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged, Selector 
    {
        public MainWindow()
        {
            InitializeComponent();
            var assembly = typeof (MainWindow).Assembly;
          
            Stream resource = assembly.GetManifestResourceStream(resourceName)

            DrawingContext context;

            Keyboard.AddKeyDownHandler(this,  );

            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);

              var selectorItem = ItemContainerGenerator.ContainerFromItem( item )  ;
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
             
           var isModal=  ComponentDispatcher.IsThreadModal;

             Window window = Window.GetWindow( this );

            ManualResetEvent eEvent = new ManualResetEvent(false);
            eEvent.WaitOne();
              
            eEvent.Set();
            Task.WaitAll();
            ParameterizedThreadStart thread = delegate { };

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
           var task= Task.Factory.StartNew(() => 3, CancellationToken.None, TaskCreationOptions.None, scheduler);

            ThreadPool.RegisterWaitForSingleObject();
        
          
            RoutedCommand  dCommand = new RoutedUICommand();
            CommandBinding commandBinding = new CommandBinding();
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), commandBinding);
            this.CommandBindings.Add(new CommandBinding(dCommand, Execute,CanExecute));
        
            dCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Shift));
            
        }
        void Execute(object sender,EventArgs e)
        {}
        void  CanExecute(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            LinkedList<String> list = new LinkedList<string>();
            var firstNode = new LinkedListNode<string>("dd");
            XmlDocument xmlDocument = new XmlDocument();


            List<string> listser = new List<string>();
            Binding binding = new Binding();
 
            canExecuteRoutedEventArgs.CanExecute = false;
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                Pen drawingPen = new Pen(Brushes.Black, 3);
                dc.DrawLine(drawingPen, new Point(0, 50), new Point(50, 0));
                dc.DrawLine(drawingPen, new Point(50, 0), new Point(100, 50));
                dc.DrawLine(drawingPen, new Point(0, 50), new Point(100, 50));
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
         
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class PropertyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultnDataTemplate { get; set; }
        public DataTemplate BooleanDataTemplate { get; set; }
        public DataTemplate EnumDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return DefaultnDataTemplate;
        }
    }

    public class RelayCommand<T>:ICommand
    {
        
        private Action<T> action;
        private Predicate<T> canExecute;

        public RelayCommand(Action<T> action ):this(action,null)
        {
          
        }

        public RelayCommand(Action<T> action,Predicate<T> canExecute  )
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public void Execute(T parameter)
        {
            if (action!=null)
            {
                action(parameter);
            }
        }

        public bool CanExecute(T parameter)
        {
            if (canExecute!=null)
            {
                return canExecute(parameter);
            }
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter is T ? (T) parameter : default(T));
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T) parameter);
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
         
       
    }

    public class TaskListDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
               
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is Task)
            {
                Task taskitem = item as Task;

                if (taskitem.Priority == 1)
                    return
                        element.FindResource("importantTaskTemplate") as DataTemplate;
                else
                    return
                        element.FindResource("myTaskTemplate") as DataTemplate;
            }
            
            return null;
        }
    }
    public class FutureDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date;
            try
            {
                date = DateTime.Parse(value.ToString());
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Value is not a valid date.");
            }
            if (DateTime.Now.Date > date)
            {
                return new ValidationResult(false, "Please enter a date in the future.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
