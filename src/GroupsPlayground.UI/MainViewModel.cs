using System;
using GroupsPlayground.Domain;
using GroupsPlayground.Persistence;

namespace GroupsPlayground.UI
{
    public class MainViewModel : ViewModel
    {
        private ViewModel currentViewModel;

        public MainViewModel()
        {
            using (var session = new Session())
            {
                session.CreateDatabase();
            }

            var viewModel = new GroupSizeViewModel();
            viewModel.NextClicked += GroupSizeViewModel_NextClicked;
            CurrentViewModel = viewModel;
        }

        private void GroupSizeViewModel_NextClicked(object sender, EventArgs e)
        {
            if (!(sender is GroupSizeViewModel viewModel)) return;

            var cayleyTable = new CayleyTable(Guid.NewGuid(), viewModel.GroupSize);
            var next = new CayleyTableViewModel(cayleyTable);
            next.Finished += CayleyTableViewModel_Finished;
            CurrentViewModel = next;
        }

        private void CayleyTableViewModel_Finished(object sender, EventArgs e)
        {
            if (!(sender is CayleyTableViewModel viewModel)) return;

            var cayleyTable = viewModel.CayleyTable;
            var group = new Group(Guid.NewGuid(), cayleyTable);

            using var session = new Session();
            session.GroupRepository.AddGroup(group);
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