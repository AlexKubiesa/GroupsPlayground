using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }

    public class GroupElementsElementModel
    {
        public string Symbol { get; set; }
    }
}
