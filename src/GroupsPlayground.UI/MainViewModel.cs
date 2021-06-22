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
            using var session = new Session();
            session.RegenerateDatabase();

            var first = new GroupLibraryViewModel();
            first.CreateGroupClicked += GroupLibraryViewModel_CreateGroupClicked;
            CurrentViewModel = first;
        }

        private void GroupLibraryViewModel_CreateGroupClicked(object sender, EventArgs e)
        {
            if (!(sender is GroupLibraryViewModel viewModel)) return;

            var next = new GroupPropertiesViewModel();
            next.NextClicked += GroupSizeViewModel_NextClicked;
            CurrentViewModel = next;
        }

        private void GroupSizeViewModel_NextClicked(object sender, EventArgs e)
        {
            if (!(sender is GroupPropertiesViewModel viewModel)) return;

            var cayleyTable = new CayleyTable(Guid.NewGuid(), viewModel.GroupSize);
            var next = new CayleyTableViewModel(viewModel.GroupName, cayleyTable);
            next.Finished += CayleyTableViewModel_Finished;
            CurrentViewModel = next;
        }

        private void CayleyTableViewModel_Finished(object sender, EventArgs e)
        {
            if (!(sender is CayleyTableViewModel viewModel)) return;

            var cayleyTable = viewModel.CayleyTable;
            var group = new Group(Guid.NewGuid(), cayleyTable) { Name = viewModel.GroupName };

            using var session = new Session();
            session.GroupRepository.AddGroup(group);
            session.SaveChanges();

            var next = new GroupLibraryViewModel();
            next.CreateGroupClicked += GroupLibraryViewModel_CreateGroupClicked;
            CurrentViewModel = next;
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