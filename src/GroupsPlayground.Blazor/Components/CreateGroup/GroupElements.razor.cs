using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupsPlayground.Domain;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Blazor.Components.CreateGroup
{
    public class GroupElementsModel
    {
        private readonly List<GroupElementsElementModel> elements = new();

        public int Count
        {
            get => elements.Count;
            set
            {
                while (elements.Count < value)
                {
                    elements.Add(new GroupElementsElementModel());
                }

                while (elements.Count > value)
                {
                    elements.RemoveAt(elements.Count - 1);
                }
            }
        }

        public IReadOnlyList<GroupElementsElementModel> Elements => elements;

        public string ValidationMessage { get; set; }

        public GroupElementSymbols Result { get; private set; }

        public bool Validate()
        {
            ValidationMessage = null;
            Result = null;

            try
            {
                var symbols = Elements
                    .Select(x => x.Symbol)
                    .Select(Symbol.Create)
                    .ToValueList();
                Result = new GroupElementSymbols(symbols);
                return true;
            }
            catch (ValidationError e)
            {
                ValidationMessage = e.Message;
                return false;
            }
        }
    }

    public class GroupElementsElementModel
    {
        public string Symbol { get; set; }
    }
}
