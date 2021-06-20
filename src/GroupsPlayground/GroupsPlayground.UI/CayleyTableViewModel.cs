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

            Products = cayleyTable.Products.Rows
                .Select((row, rowIndex) =>
                    row.Select((element, columnIndex) =>
                        new CayleyTableProductViewModel(cayleyTable, rowIndex, columnIndex) { GroupElementSymbol = element?.Symbol })
                    .ToList())
                .ToList();

            CheckClosureCommand = new Command(CheckClosure);
            CheckAssociativityCommand = new Command(CheckAssociativity);
            CheckIdentityElementCommand = new Command(CheckIdentityElement);
            CheckInversesCommand = new Command(CheckInverses);
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
        public ICommand CheckAssociativityCommand { get; }
        public ICommand CheckIdentityElementCommand { get; }
        public ICommand CheckInversesCommand { get; }

        private void CheckClosure()
        {
            bool success = cayleyTable.CheckClosure();
            Message = success ? "The group operation is closed." : "The group operation is not closed!";
        }

        private void CheckAssociativity()
        {
            bool success = cayleyTable.CheckAssociativity();
            Message = success ? "The group operation is associative." : "The group operation is not associative!";
        }

        private void CheckIdentityElement()
        {
            bool success = cayleyTable.CheckIdentityElement();
            Message = success ? "The group operation has an identity element." : "The group operation does not have an identity element!";
        }

        private void CheckInverses()
        {
            bool success = cayleyTable.CheckInverses();
            Message = success ? "All elements have inverses." : "Some elements do not have inverses!";
        }
    }
}
