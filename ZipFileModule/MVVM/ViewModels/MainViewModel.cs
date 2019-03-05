using System;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Mvvm;

namespace ZipFileModule.MVVM.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public string ZipFilePath => "sdfjlsdflsdjf"; // get, set  or  
        public DelegateCommand ZipFileCommand { get; set; }

        private List<string> _files;
        public List<string> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }

        public MainViewModel()
        {
            Files = new List<string>();
            ZipFileCommand = new DelegateCommand(ExecuteZipFile);
        }

        private void ExecuteZipFile()
        {

        }
    }
}
