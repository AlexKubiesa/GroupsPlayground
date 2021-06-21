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

        public CayleyTableViewModel(CayleyTable cayleyTable)
        {
            this.CayleyTable = cayleyTable ?? throw new ArgumentNullException(nameof(cayleyTable));

            GroupElements = cayleyTable.GroupElements.Select(element => new CayleyTableGroupElementViewModel(element)).ToList();

            Products = cayleyTable.Products
                .Select((row, rowIndex) =>
                    row.Select((product, columnIndex) =>
                        new CayleyTableProductViewModel(cayleyTable, rowIndex, columnIndex))
                    .ToList())
                .ToList();

            Products.ForEach(row =>
                row.ForEach(product =>
                    product.PropertyChanged += (sender, args) => Message = string.Empty));

            CheckClosureCommand = new Command(CheckClosure);
            CheckAssociativityCommand = new Command(CheckAssociativity);
            CheckIdentityElementCommand = new Command(CheckIdentityElement);
            CheckInversesCommand = new Command(CheckInverses);
            FinishCommand = new Command(Finish);
        }

        public event EventHandler Finished;

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

        public List<CayleyTableGroupElementViewModel> GroupElements { get; }
        public List<List<CayleyTableProductViewModel>> Products { get; }
        public ICommand CheckClosureCommand { get; }
        public ICommand CheckAssociativityCommand { get; }
        public ICommand CheckIdentityElementCommand { get; }
        public ICommand CheckInversesCommand { get; }
        public ICommand FinishCommand { get; }

        private void CheckClosure()
        {
            var operation = CayleyTable.CreatePartialBinaryOperation();

            if (!operation.IsFullyDefined())
            {
                Message = Messages.NotFullyDefined;
                return;
            }

            bool success = operation.IsClosed();
            Message = success ? Messages.Closed : Messages.NotClosed;
        }

        private void CheckAssociativity()
        {
            var operation = CayleyTable.CreatePartialBinaryOperation();

            if (!operation.IsFullyDefined())
            {
                Message = Messages.NotFullyDefined;
                return;
            }

            if (!operation.IsClosed())
            {
                Message = Messages.NotClosed;
                return;
            }

            bool success = operation.IsAssociative();
            Message = success ? Messages.Associative : Messages.NotAssociative;
        }

        private void CheckIdentityElement()
        {
            var operation = CayleyTable.CreatePartialBinaryOperation();

            if (!operation.IsFullyDefined())
            {
                Message = Messages.NotFullyDefined;
                return;
            }

            if (!operation.IsClosed())
            {
                Message = Messages.NotClosed;
                return;
            }

            bool success = operation.HasIdentityElement();
            Message = success ? Messages.IdentityElement : Messages.NoIdentityElement;
        }

        private void CheckInverses()
        {
            var operation = CayleyTable.CreatePartialBinaryOperation();

            if (!operation.IsFullyDefined())
            {
                Message = Messages.NotFullyDefined;
                return;
            }

            if (!operation.IsClosed())
            {
                Message = Messages.NotClosed;
                return;
            }

            if (!operation.HasIdentityElement())
            {
                Message = Messages.NoIdentityElement;
                return;
            }

            bool success = operation.HasInverses();
            Message = success ? Messages.Inverses : Messages.MissingInverses;
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
        }
    }
}
