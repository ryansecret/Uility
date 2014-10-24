using System;
using System.Windows.Input;
using System.Diagnostics;

namespace Uility.WPF
{
    internal static class RoutedCommandHelper
    {
        #region RoutedCommand Helper Methods

        /// <summary>
        /// Combines a list of input gestures into an InputGestureCollection.
        /// </summary>
        /// <param name="gestures">Variable-length parameter list of input gestures.</param>
        internal static InputGestureCollection MakeGestureCollection(params InputGesture[] gestures)
        {
            return new InputGestureCollection(gestures);
        }

        /// <summary>
        /// Registers a routed command.
        /// </summary>
        /// <param name="command">The routed command to be registered.</param>
        /// <param name="type">The type of the owner class for the command.</param>
        /// <param name="executed">The Executed handler for the command.</param>
        private static void RegisterClassCommand(RoutedCommand command, Type type,
                                                 ExecutedRoutedEventHandler executed)
        {
            RegisterClassCommand(command, type, executed, (CanExecuteRoutedEventHandler)null, null);
        }

        /// <summary>
        /// Registers a routed command.
        /// </summary>
        /// <param name="command">The routed command to be registered.</param>
        /// <param name="type">The type of the owner class for the command.</param>
        /// <param name="executed">The Executed handler for the command.</param>
        /// <param name="canExecute">The CanExecute handler for the command.</param>
        internal static void RegisterClassCommand(RoutedCommand command, Type type,
                                                  ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute)
        {
            RegisterClassCommand(command, type, executed, canExecute, null);
        }

        /// <summary>
        /// Registers a routed command along with an optional list of input gestures 
        /// for the command.
        /// </summary>
        /// <param name="command">The routed command to be registered.</param>
        /// <param name="type">The type of the owner class for the command.</param>
        /// <param name="executed">The Executed handler for the command.</param>
        /// <param name="inputGestures">A list of input gestures for the command</param>
        internal static void RegisterClassCommand(RoutedCommand command, Type type,
                                                  ExecutedRoutedEventHandler executed, params InputGesture[] inputGestures)
        {
            RegisterClassCommand(command, type, executed, (CanExecuteRoutedEventHandler)null, inputGestures);
        }

        /// <summary>
        /// Registers a routed command along with an optional list of input gestures 
        /// for the command.
        /// </summary>
        /// <param name="command">The routed command to be registered.</param>
        /// <param name="type">The type of the owner class for the command.</param>
        /// <param name="executed">The Executed handler for the command.</param>
        /// <param name="canExecute">The CanExecute handler for the command.</param>
        /// <param name="inputGestures">A list of input gestures for the command</param>
        internal static void RegisterClassCommand(RoutedCommand command, Type type,
                                                  ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute,
                                                  params InputGesture[] inputGestures)
        {
            Debug.Assert(command != null);
            Debug.Assert(type != null);
            Debug.Assert(executed != null);

            CommandManager.RegisterClassCommandBinding(type, new CommandBinding(command, executed, canExecute));
            if (inputGestures != null)
            {
                foreach (InputGesture gesture in inputGestures)
                {
                    CommandManager.RegisterClassInputBinding(type, new InputBinding(command, gesture));
                }
            }
        }

        #endregion
    }
}