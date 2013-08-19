using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using lucene_test_shared;
using Version = Lucene.Net.Util.Version;

namespace build_index
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Building index...");

            var indexPath = TestConfiguration.GetIndexPath();
            Console.WriteLine("Index path: {0}", indexPath);
            if (!System.IO.Directory.Exists(indexPath))
            {
                System.IO.Directory.CreateDirectory(indexPath);
            }

            var directory = FSDirectory.Open(indexPath);
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);

            var inputPath = TestConfiguration.GetInputPath();
            Console.WriteLine("Input path: {0}", inputPath);
            foreach (var inFilename in System.IO.Directory.GetFiles(inputPath, "*.txt"))
            {
                IndexTheFile(inFilename, writer);
            }

            Console.ReadKey();
        }

        private static void IndexTheFile(string inFilename, IndexWriter writer)
        {
            Console.WriteLine("Indexing {0}", inFilename);
            var doc = new Document();
            doc.Add(new Field("id", inFilename, Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("postBody", File.ReadAllText(inFilename), Field.Store.YES, Field.Index.ANALYZED,
                              Field.TermVector.YES));
            writer.AddDocument(doc);
        }

    }
}
