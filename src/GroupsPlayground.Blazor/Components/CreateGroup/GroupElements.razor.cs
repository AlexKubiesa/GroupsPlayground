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
                elements.Clear();
                for (int i = 0; i < value; i++)
                {
                    elements.Add(new GroupElementsElementModel());
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

            var symbols = Elements.Select(x => new Symbol(x.Symbol)).ToValueList();
            string error = GroupElementSymbols.Validate(symbols);
            if (error != null)
            {
                ValidationMessage = error;
                return false;
            }

            Result = new GroupElementSymbols(symbols);
            return true;
        }
    }

    public class GroupElementsElementModel
    {
        public string Symbol { get; set; }
    }
}
