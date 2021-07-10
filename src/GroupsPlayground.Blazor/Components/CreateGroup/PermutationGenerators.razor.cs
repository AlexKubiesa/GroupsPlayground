using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupsPlayground.Domain;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Blazor.Components.CreateGroup
{
    public class PermutationGeneratorsModel
    {
        private readonly List<PermutationGeneratorModel> generators = new();

        public int GeneratorCount
        {
            get => generators.Count;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                while (generators.Count < value)
                    generators.Add(new PermutationGeneratorModel());
                while (generators.Count > value)
                    generators.RemoveAt(generators.Count - 1);
            }
        }

        public IReadOnlyList<PermutationGeneratorModel> Generators => generators;

        public bool Validate() => generators.Select(x => x.Validate()).ToList().All(x => x);
    }

    public class PermutationGeneratorModel
    {
        public string Expression { get; set; }

        public string ValidationMessage { get; private set; }

        public bool Validate()
        {
            ValidationMessage = null;
            Result = null;

            try
            {
                Result = Permutation.Parse(Expression);
                return true;
            }
            catch (ValidationError e)
            {
                ValidationMessage = e.Message;
                return false;
            }
        }

        public Permutation Result { get; private set; }
    }
}
