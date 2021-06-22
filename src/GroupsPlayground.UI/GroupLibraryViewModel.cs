using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GroupsPlayground.Domain;
using GroupsPlayground.Persistence;

namespace GroupsPlayground.UI
{
    public class GroupLibraryViewModel : ViewModel
    {
        public GroupLibraryViewModel()
        {
            using var session = new Session();
            Groups = session.GroupRepository.GetAllGroups().ToList();
            CreateGroupCommand = new Command(CreateGroup);
        }

        public event EventHandler CreateGroupClicked;

        public List<Group> Groups { get; }
        public ICommand CreateGroupCommand { get; }

        private void CreateGroup() => CreateGroupClicked?.Invoke(this, EventArgs.Empty);
    }
}