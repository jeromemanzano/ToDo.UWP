using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo.ViewModels;

namespace ToDo.Test.ViewModels
{
    [TestClass]
    public class WithMainViewModel
    {
        private MainViewModel _viewModel;

        [TestInitialize]
        public void Init()
        {
            _viewModel = new MainViewModel();
        }

        [DataRow(null)]
        [DataRow("")]
        [DataRow("a")]
        [DataTestMethod]
        public void AddItemCommand_Should_Not_Execute_When_NewItem_Is_Null_Or_Empty(string newItem)
        {
            _viewModel.NewItem = newItem;
            Assert.AreNotEqual(string.IsNullOrEmpty(newItem), _viewModel.AddItemCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddItemCommand_Should_Add_NewItem_To_ToDoList_And_Clear_NewItem()
        {
            var itemToAdd = "Test item";
            _viewModel.NewItem = itemToAdd;
            
            _viewModel.AddItemCommand.Execute(null);
            
            CollectionAssert.Contains(_viewModel.ToDoList, itemToAdd);
            Assert.AreEqual(string.Empty, _viewModel.NewItem);
        }

        [TestMethod]
        public void When_NewItem_Changed_Should_Call_PropertyChanged_And_NotifyCanExecuteChanged()
        {
            var propertyChangedCounter = 0;
            var notifyCanExecuteChanged = 0;

            _viewModel.PropertyChanged += (sender, args) =>
            {
                propertyChangedCounter++;
            };

            _viewModel.AddItemCommand.CanExecuteChanged += (sender, args) =>
            {
                notifyCanExecuteChanged++;
            };

            _viewModel.NewItem = "Test";
            
            Assert.AreEqual(1, propertyChangedCounter);
            Assert.AreEqual(1, notifyCanExecuteChanged);
        }
    }
}
