using EnsureThat;

namespace CINotifier.Logic.Projects
{
    public class Developer
    {
        public string Name { get; private set; }

        public Developer(string name)
        {
            Ensure.That(name).IsNotNullOrEmpty();

            Name = name;
        }
    }
}
