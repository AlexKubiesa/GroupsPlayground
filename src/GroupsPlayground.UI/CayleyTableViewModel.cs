using GroupsPlayground.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GroupsPlayground.UI
{
    public class CayleyTableViewModel : ViewModel
    {
        private string message = string.Empty;

        public CayleyTableViewModel(string groupName, CayleyTable cayleyTable)
        {
            GroupName = groupName;
            CayleyTable = cayleyTable ?? throw new ArgumentNullException(nameof(cayleyTable));

            GroupElements = cayleyTable.Symbols.Select(element => new CayleyTableSymbolViewModel(element)).ToList();

            Products = cayleyTable.Products
                .Select((row, rowIndex) =>
                    row.Select((product, columnIndex) =>
                        new CayleyTableProductViewModel(cayleyTable, rowIndex, columnIndex))
                    .ToList())
                .ToList();

            Products.ForEach(row =>
                row.ForEach(product =>
                    product.PropertyChanged += (sender, args) => Message = string.Empty));

            CheckGroupAxiomsCommand = new Command(CheckGroupAxioms);
            FinishCommand = new Command(Finish);
        }

        public event EventHandler Finished;

        public string GroupName { get; }
        public CayleyTable CayleyTable { get; }

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
        public ICommand CheckGroupAxiomsCommand { get; }
        public ICommand FinishCommand { get; }

        private void CheckGroupAxioms()
        {
            var operation = CayleyTable.GetBinaryOperation();

            if (!operation.IsFullyDefined())
            {
                Message = Messages.NotFullyDefined;
                return;
            }

            var compliance = GroupAxioms.CheckCompliance(operation);

            if (!compliance.IsClosed)
            {
                Message = Messages.NotClosed;
                return;
            }

            if (!compliance.IsAssociative)
            {
                Message = Messages.NotAssociative;
                return;
            }

            if (!compliance.HasIdentity)
            {
                Message = Messages.NoIdentityElement;
                return;
            }

            if (!compliance.HasInverses)
            {
                Message = Messages.MissingInverses;
                return;
            }

            Message = Messages.Success;
        }

        private void Finish() => Finished?.Invoke(this, EventArgs.Empty);

        private static class Messages
        {
            public const string NotFullyDefined = "The group operation is not fully defined!";
            public const string Closed = "The group operation is closed.";
            public const string NotClosed = "The group operation is not closed!";
            public const string Associative = "The group operation is associative.";
            public const string NotAssociative = "The group operation is not associative!";
            public const string IdentityElement = "There is an identity element.";
            public const string NoIdentityElement = "There is no identity element!";
            public const string Inverses = "All elements have inverses.";
            public const string MissingInverses = "Some elements do not have inverses!";
            public const string Success = "Success!";
        }
    }
}
