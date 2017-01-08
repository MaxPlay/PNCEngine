using PNCEngine.Core;

namespace PNCEngine
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            using (Engine engine = new Engine(args))
                engine.Run();
        }

        #endregion Private Methods
    }
}