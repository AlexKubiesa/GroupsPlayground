using GroupsPlayground.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GroupsPlayground.UI
{
    public class CayleyTableViewModel : ViewModel
    {
        private readonly CayleyTable cayleyTable;

        private string message = string.Empty;

        public CayleyTableViewModel(CayleyTable cayleyTable)
        {
            this.cayleyTable = cayleyTable ?? throw new ArgumentNullException(nameof(cayleyTable));

            GroupElements = cayleyTable.GroupElements.Select(element => new GroupElementViewModel(element)).ToList();

            Products = cayleyTable.Products
                .Select((first, firstIndex) =>
                    first.Select((second, secondIndex) =>
                        new CayleyTableProductViewModel(cayleyTable, firstIndex, secondIndex) { GroupElementSymbol = second?.Symbol })
                    .ToList())
                .ToList();

            CheckClosureCommand = new Command(CheckClosure);
        }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                Notify();
            }
        }

        public List<GroupElementViewModel> GroupElements { get; }
        public List<List<CayleyTableProductViewModel>> Products { get; }
        public ICommand CheckClosureCommand { get; }
        

        private void CheckClosure()
        {
            bool success = cayleyTable.CheckClosure();
            Message = success ? "The group operation is closed." : "The group operation is not closed!";
        }
    }
}
