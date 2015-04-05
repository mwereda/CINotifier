using CINotifier.Logic;

namespace CINotifier
{
    public class NotifierWrapper
    {
        private static Notifier notifier;

        public static Notifier Instance
        {
            get
            {
                if (notifier == null)
                {
                    notifier = new Notifier(new ProjectStore());
                    notifier.Initialize();
                }

                return notifier;
            }
        }
    }
}