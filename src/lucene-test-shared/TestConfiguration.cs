using System.IO;
using System.Reflection;

namespace lucene_test_shared
{
    public static class TestConfiguration
    {
        public static string GetInputPath()
        {
            string inputPath = Path.Combine(
                GetRoot(),
                "testdata");
            return inputPath;
        }

        public static string GetRoot()
        {
            return Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))));
        }

        public static string GetIndexPath()
        {
            string indexPath = Path.Combine(
                GetRoot(),
                "lucene-index");
            return indexPath;
        }
    }
}