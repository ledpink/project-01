using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;

namespace ZipFileModule.MVVM.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private Dictionary<string, string> _tempSaveZipFiles { get; set; } = new Dictionary<string, string>();
        private Dictionary<string, List<string>> _textParsing { get; set; } = new Dictionary<string, List<string>>();

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
            ConverFilesToList();
        }

        private void ConverFilesToList()
        {
            System.IO.DirectoryInfo unzipFolder = new System.IO.DirectoryInfo(ExtractFilePath);
            foreach (System.IO.FileInfo file in unzipFolder.GetFiles())
            {
                var fileName = file.Name.Substring(0, file.Name.Length - 4);
                _tempSaveZipFiles.Add(fileName, file.FullName);
            }
            ParseTextFiles();
        }

        private void ParseTextFiles()
        {
            string line;
            List<string> lines = new List<string>();            
            foreach (var file in _tempSaveZipFiles)
            {                
                using (StreamReader sr = new StreamReader(file.Value, Encoding.Default))
                {
                    bool output = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (output)
                        {
                            var extract = line.Where(c => !char.IsWhiteSpace(c)).ToArray();
                            var str = new string(extract);
                            var result = str.Substring(0, str.IndexOf("="));

                            lines.Add(result);
                        }
                        if (line.Contains("[OUTPUT]"))
                        {
                            output = true;
                        }
                    }
                    _textParsing.Add(file.Key, lines);
                    line = null;
                    lines.Clear();
                }
            }
        }
    }
}