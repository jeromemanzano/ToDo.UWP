using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ToDo.Helpers;

namespace ToDo.Behaviors
{
    public class CollectionViewBehavior : Behavior<ListView>
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource),
                typeof(IEnumerable<string>),
                typeof(CollectionViewBehavior),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register(nameof(Filter),
                typeof(string),
                typeof(CollectionViewBehavior),
                new PropertyMetadata(null, OnFilterChanged));

        /// <summary>
        /// Gets or sets the ItemsSource that will be used for updating <see cref="ListView.ItemsSource"/>
        /// </summary>
        public IEnumerable<string> ItemsSource
        {
            get => (IEnumerable<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the string filter that will be used to update <see cref="ListView.ItemsSource"/>
        /// </summary>
        public string Filter
        {
            get => (string)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }
        
        // TODO: Inject this and change documentation
        public IStringMetric StringMetric { get; set; } = new LevenshteinDistance();

        private static void OnItemsSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is not CollectionViewBehavior behavior)
                return;

            if (e.OldValue is INotifyCollectionChanged oldObservableCollection)
                oldObservableCollection.CollectionChanged -= behavior.OnItemsSourceCollectionChanged;

            if (e.NewValue is INotifyCollectionChanged observableCollection)
                observableCollection.CollectionChanged += behavior.OnItemsSourceCollectionChanged;
        }
        
        private static void OnFilterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is CollectionViewBehavior behavior)
                behavior.UpdateList();
        }

        private void OnItemsSourceCollectionChanged(object obj, NotifyCollectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                AssociatedObject.ItemsSource = ItemsSource;
            }
            else
            {
                //TODO: Optimize
                AssociatedObject.ItemsSource = ItemsSource
                 .Select(item => (StringMetric.Compute(item, Filter), item))
                 .OrderBy(item => item.Item1)
                 .Where(item => item.Item1 < item.Item2.Length)
                 .Select(item => item.Item2);
            }
        }
    }
}
