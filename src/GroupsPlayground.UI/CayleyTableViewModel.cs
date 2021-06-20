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

            GroupElements = cayleyTable.Symbols.Select(element => new CayleyTableSymbolViewModel(element)).ToList();

            Products = cayleyTable.Products
                .Select((row, rowIndex) =>
                    row.Select((product, columnIndex) =>
                        new CayleyTableProductViewModel(cayleyTable, rowIndex, columnIndex) { Symbol = product })
                    .ToList())
                .ToList();

            Products.ForEach(row =>
                row.ForEach(product =>
                    product.PropertyChanged += (sender, args) => Message = string.Empty));

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

        public List<CayleyTableSymbolViewModel> GroupElements { get; }
        public List<List<CayleyTableProductViewModel>> Products { get; }
        public ICommand CheckClosureCommand { get; }
        public ICommand CheckAssociativityCommand { get; }
        public ICommand CheckIdentityElementCommand { get; }
        public ICommand CheckInversesCommand { get; }

        private void CheckClosure()
        {
            var operation = cayleyTable.CreatePartialBinaryOperation();
            bool success = operation.IsClosed();
            Message = success ? "The group operation is closed." : "The group operation is not closed!";
        }

        private void CheckAssociativity()
        {
            var operation = cayleyTable.CreatePartialBinaryOperation();
            bool success = operation.IsAssociative();
            Message = success ? "The group operation is associative." : "The group operation is not associative!";
        }

        private void CheckIdentityElement()
        {
            var operation = cayleyTable.CreatePartialBinaryOperation();
            bool success = operation.HasIdentityElement();
            Message = success ? "The group operation has an identity element." : "The group operation does not have an identity element!";
        }

        private void CheckInverses()
        {
            var operation = cayleyTable.CreatePartialBinaryOperation();
            bool success = operation.HasInverses();
            Message = success ? "All elements have inverses." : "Some elements do not have inverses!";
        }
    }
}
