using System;
using GroupsPlayground.Domain;

namespace GroupsPlayground.UI
{
    public class MainViewModel : ViewModel
    {
        private ViewModel currentViewModel;

        public MainViewModel()
        {
            var viewModel = new GroupSizeViewModel();
            viewModel.NextClicked += GroupSizeViewModel_NextClicked;
            CurrentViewModel = viewModel;
        }

        private void GroupSizeViewModel_NextClicked(object sender, EventArgs e)
        {
            if (sender is GroupSizeViewModel viewModel)
            {
                var cayleyTable = new CayleyTable(Guid.NewGuid(), viewModel.GroupSize);
                CurrentViewModel = new CayleyTableViewModel(cayleyTable);
            }
        }

        public ViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                Notify();
            }
        }
    }
}