using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;

namespace ZipFileModule.MVVM.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public string ZipFilePath => @"C:\Users\super\Desktop\api_data_test"; // get, set  or  lamda??
        public string ExtractFilePath => @"C:\Users\super\Desktop\api_data_test_unzip";
        private List<string> _files;
        public List<string> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }

        public DelegateCommand ZipFileCommand { get; set; }

        public MainViewModel()
        {
            Files = new List<string>();
            ZipFileCommand = new DelegateCommand(ExecuteZipFile);
        }

        private void ExecuteZipFile()
        {
            var encParsing = EncParsing.Create(ZipFilePath, ExtractFilePath);
            var parsing = encParsing.Run();

            foreach (KeyValuePair<string, string> record in parsing)
            {
                var aaa = record.Key.ToString();
                var commentParsingInputDictionary = encParsing.GetCommentParsing(record.Key, "[INPUT]");
                var commentParsingInputDictionary2 = encParsing.GetCommentParsing(record.Key, "[OUTPUT]");
                var commentParsingInputDictionary3 = encParsing.GetDatFileParsing(record.Key, "[INPUT]");
                var commentParsingInputDictionary4 = encParsing.GetDatFileParsing(record.Key, "[OUTPUT]");

                var bbb = commentParsingInputDictionary3.Keys.First();
            }
        }
    }
}