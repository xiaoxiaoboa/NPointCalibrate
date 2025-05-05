using System;
using System.Windows.Forms;
using IniParser;
using System.IO;
using Cognex.VisionPro.DSCameraSetup.Implementation.Internal;
using IniParser.Model;

namespace WindowsFormsApp1.Common {
    public class IniControl {
        private static readonly IniControl _instance = new IniControl();

        public static IniControl Instance => _instance;

        private readonly FileIniDataParser _parser = new FileIniDataParser();
        private IniData _data;

        private readonly string _filePath = Path.Combine(Application.StartupPath + "\\config.ini");

        private IniControl() {
        }

        // 加载文件
        public void LoadFile() {
            if (File.Exists(_filePath))
                _data = _parser.ReadFile(_filePath);
            else
                Logger.Instance.AddLog("ini文件不存在");
        }

        // 读取ini值
        public string Read(string field, string key) {
            return _data?[field][key];
        }

        // 写入ini值
        public void Write(string field, string key, string value) {
            _data[field][key] = value;
            if (File.Exists(_filePath)) {
                _parser.WriteFile(_filePath, _data);
            }
            else {
                Logger.Instance.AddLog("无法写入，ini文件不存在");
            }
        }
    }
}