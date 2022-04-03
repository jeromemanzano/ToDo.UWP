using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;

namespace ToDo.Behaviors
{
    public class KeyToCommandBehavior : Behavior<Control>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command),
                typeof(ICommand),
                typeof(KeyToCommandBehavior),
                new PropertyMetadata(null));
        
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter),
                typeof(object),
                typeof(KeyToCommandBehavior),
                new PropertyMetadata(null));
        
        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Input.ICommand" that will be invoked when <see cref="Key" is pressed/>/> 
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the CommandParameter of <see cref="Command"/>
        /// </summary>
        public object CommandParameter
        {
            get => (ICommand)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="VirtualKey" that will be used for triggering the Command
        /// </summary>
        public VirtualKey Key { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.KeyDown += OnKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.KeyDown -= OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Key)
            {
                Command?.Execute(CommandParameter);
            }
        }
    }
}
