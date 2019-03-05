using System;
using System.Collections.Generic;
using System.IO.Compression;
using Prism.Commands;
using Prism.Mvvm;

namespace ZipFileModule.MVVM.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private Dictionary<string, string> _tempSaveZipFiles { get; set; } = new Dictionary<string, string>();

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
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = ZipFilePath;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach(string saveFile in openFileDialog.FileNames)
                {
                    ZipFile.ExtractToDirectory(saveFile, ExtractFilePath);
                }
            }
        }
    }
}
