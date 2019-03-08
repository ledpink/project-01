using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace ZipFileModule
{
    /// <summary>
    /// 압축된 enc파일들을 unzip, 추출된 dat파일들을 파싱 후 
    /// 데이터는 Dictionary로 담아서 값을 돌려준다.
    /// </summary>
    public class EncParsing
    {
        private readonly string _EQUAL_SIGN_ = "=";
        private readonly string _END_ = "@END_";
        private readonly string _OUTPUT_ = "[OUTPUT]";
        private readonly string _START_ = "@START";
        private readonly string _encPath;
        private readonly string _datPath;
        private DirectoryInfo _datFolder;
        private Dictionary<string, string> _saveDatFiles { get; set; } = new Dictionary<string, string>();
        private Dictionary<string, List<string>> _datFilesParsing { get; set; } = new Dictionary<string, List<string>>();

        private EncParsing(string encPath, string datPath)
        {
            _encPath = encPath;
            _datPath = datPath;
        }

        public static EncParsing Create(string encPath, string datPath)
        {
            return new EncParsing(encPath, datPath);
        }

        public Dictionary<string, List<string>> Run()
        {
            UnzipEncFiles();
            ConvertDatFilesToDictionary();
            ParseDatFiles();
            DeleteDatFolder();
            return _datFilesParsing;
        }

        private void UnzipEncFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = _encPath;
            _datFolder = new DirectoryInfo(_datPath);

            if (_datFolder.Exists == false)
            {
                _datFolder.Create();
            }

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string saveFile in openFileDialog.FileNames)
                {
                    ZipFile.ExtractToDirectory(saveFile, _datPath);
                }
            }
        }

        private void ConvertDatFilesToDictionary()
        {
            foreach (FileInfo file in _datFolder.GetFiles())
            {
                var fileName = file.Name.Substring(0, file.Name.Length - 4);
                _saveDatFiles.Add(fileName, file.FullName);
            }
        }

        private void ParseDatFiles()
        {
            foreach (var datfile in _saveDatFiles)
            {
                string line;
                List<string> lines = new List<string>();
                using (StreamReader sr = new StreamReader(datfile.Value, Encoding.Default))
                {
                    bool output = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (output)
                        {
                            if (!line.Contains(_END_))
                            {
                                var extract = line.Where(c => !char.IsWhiteSpace(c)).ToArray();
                                var str = new string(extract);
                                var result = str.Substring(0, str.IndexOf(_EQUAL_SIGN_));

                                lines.Add(result);
                            }
                        }
                        if (line.Contains(_OUTPUT_))
                        {
                            output = true;
                        }
                    }
                    if (lines[0].Contains(_START_))
                    {
                        var temp = lines[0].Substring(_START_.Length + 1);
                        lines[0] = temp;
                    }
                    _datFilesParsing.Add(datfile.Key, lines);
                }
            }
        }

        private void DeleteDatFolder()
        {
            _datFolder.Delete(true);
        }
    }
}
