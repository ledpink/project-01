using System.Collections.Generic;
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
            var datFilesParsing = encParsing.Run();


            foreach (var datFileParsing in datFilesParsing)
            {
                _files.Add(datFileParsing.Key);
            }

            Files = _files;
        }
    }
}