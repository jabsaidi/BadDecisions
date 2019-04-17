using System.IO;
using System.Linq;

namespace TestAPI.DLA.Repository
{
    public class BaseFileRepository
    {
        private string _fileName;

        public BaseFileRepository(string fileName)
        {
            _fileName = fileName;
        }

        public void InitFile(params string[] columns)
        {
            bool emptyFile = IsFileEmpty();
            if (emptyFile)
            {
                using (var sw = new StreamWriter(_fileName))
                {
                    int i = 1;
                    foreach (var column in columns)
                    {
                        if (i < columns.Count())
                            sw.Write($"{column};");
                        else
                            sw.WriteLine($"{column}");
                        i++;
                    }
                }
            }
        }

        private bool IsFileEmpty()
        {
            using (var sr = new StreamReader(_fileName))
            {
                var text = sr.ReadToEnd();
                return text == string.Empty;
            }
        }
    }
}
