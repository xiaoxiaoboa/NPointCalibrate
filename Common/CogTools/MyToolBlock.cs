using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using WindowsFormsApp1.Enum;

namespace WindowsFormsApp1.Common.CogTools {
    public class MyToolBlock {
        private static readonly Lazy<MyToolBlock> _instance = new Lazy<MyToolBlock>(() => new MyToolBlock());


        private CogToolBlock _calibrateToolBlock;
        private CogToolBlock _identificationToolBlock;

        public CogToolBlock CalibrateToolBlock => _calibrateToolBlock;
        public CogToolBlock IdentificationToolBlock => _identificationToolBlock;

        public static MyToolBlock Instance => _instance.Value;

        private MyToolBlock() { }

        // toolblock vpp
        public readonly string CalibrateVppPath = Path.Combine(Application.StartupPath, "vpp/calibrate.vpp");

        public readonly string IdentificationVppPath =
            Path.Combine(Application.StartupPath, "vpp/calibNPointToNPoint.vpp");

        /// <summary>
        /// 设置工具块对象到指定的工具块类型。
        /// 根据传入的加载类型，将工具块实例分配给对应的私有字段。
        /// 如果传入的加载类型不在枚举范围内，则抛出异常。
        /// </summary>
        /// <param name="cogToolBlock">要设置的 CogToolBlock 对象。</param>
        /// <param name="loadToolBlock">指定的加载类型，用于确定工具块的用途。</param>
        private void SetToolBLock(CogToolBlock cogToolBlock, LoadToolBlock loadToolBlock) {
            switch (loadToolBlock) {
                case LoadToolBlock.Calibrate:
                    _calibrateToolBlock = cogToolBlock;
                    break;
                case LoadToolBlock.Identification:
                    _identificationToolBlock = cogToolBlock;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(loadToolBlock), loadToolBlock, null);
            }
        }

        /// <summary>
        /// 将指定的工具块对象保存到指定的文件路径。
        /// 使用 CogSerializer 将工具块序列化并保存为二进制格式文件。
        /// 如果目标路径不存在，调用方需确保目录已创建。
        /// </summary>
        /// <param name="toolBlock">要保存的 CogToolBlock 对象。</param>
        /// <param name="vppPath">保存的目标文件路径。</param>
        public void SaveVpp(CogToolBlock toolBlock, string vppPath) {
            CogSerializer.SaveObjectToFile(toolBlock, vppPath,
                typeof(BinaryFormatter),
                CogSerializationOptionsConstants.Minimum);
        }

        // 加载toolblock vpp
        /// <summary>
        /// 异步加载指定路径的工具块文件（vpp），并根据加载类型设置对应的工具块实例。
        /// 如果文件不存在，则创建一个新的 CogToolBlock 实例并设置到指定的加载类型。
        /// 如果加载过程中发生异常，返回错误信息及日志级别。
        /// </summary>
        /// <param name="toolBlockVppPath">工具块文件的完整路径。</param>
        /// <param name="loadToolBlock">指定的加载类型，用于确定工具块的用途。</param>
        /// <returns>一个元组，包含操作结果的消息字符串和对应的日志级别。</returns>
        public async Task<(string, LogLevel)> LoadToolBlockVpp(string toolBlockVppPath, LoadToolBlock loadToolBlock) {
            if (!File.Exists(toolBlockVppPath)) {
                SetToolBLock(new CogToolBlock(), loadToolBlock);
                return ("未发现ToolBlock vpp文件，new了一个", LogLevel.Info);
            }

            return await Task.Run(() => {
                try {
                    var tb = CogSerializer.LoadObjectFromFile(toolBlockVppPath) as CogToolBlock;
                    SetToolBLock(tb, loadToolBlock);
                    return ("发现vpp文件，加载成功", LogLevel.Info);
                }
                catch (Exception exception) {
                    return ($"加载ToolBlock文件发生未知错误：{exception.Message}", LogLevel.Error);
                }
            });
        }

        // 获取标定结果
        /// <summary>
        /// 获取工具块的输出结果。
        /// 根据传入的工具块对象和指定的键集合，提取工具块输出终端中对应的值。
        /// 如果工具块为空或运行状态不为接受，则返回失败结果。
        /// 如果任意键不存在于输出终端或其值为空，则返回失败结果。
        /// </summary>
        /// <param name="toolBlock">要提取输出结果的 CogToolBlock 对象。</param>
        /// <param name="keys">需要提取的输出键集合。</param>
        /// <returns>一个元组，包含布尔值表示操作是否成功，以及包含提取结果的字典。</returns>
        public (bool, Dictionary<string, object>) GetToolBlockOutputsResults(CogToolBlock toolBlock,
            IEnumerable<string> keys) {
            var values = new Dictionary<string, object>();

            if (toolBlock == null) return (false, values);
            if (toolBlock.RunStatus.Result != CogToolResultConstants.Accept) return (false, values);

            foreach (var key in keys) {
                if (toolBlock.Outputs.Contains(key)) {
                    var terminal = toolBlock.Outputs[key];

                    if (terminal?.Value == null) {
                        return (false, values);
                    }

                    values[key] = terminal.Value;
                }
            }

            return (true, values);
        }


        /// <summary>
        /// 根据指定的名称从工具块中获取特定类型的工具对象。
        /// 如果工具块为空或其工具集合不包含指定名称的工具，则返回 null。
        /// 此方法支持泛型约束，确保返回的工具对象是指定的类型。
        /// </summary>
        /// <typeparam name="T">要获取的工具对象的类型，必须继承自 CogToolBase。</typeparam>
        /// <param name="toolBlock">包含工具集合的 CogToolBlock 对象。</param>
        /// <param name="name">要获取的工具的名称。</param>
        /// <returns>指定名称和类型的工具对象。如果未找到，则返回 null。</returns>
        public T GetTool<T>(CogToolBlock toolBlock, string name) where T : CogToolBase {
            if (toolBlock?.Tools == null)
                return null;

            if (toolBlock.Tools.Contains(name)) {
                return toolBlock.Tools[name] as T;
            }

            return null;
        }

        // 获取record
        /// <summary>
        /// 获取指定工具块运行后的记录对象。
        /// 如果工具块为空或运行结果未被接受，则返回 null。
        /// 根据指定的名称从工具块的最后一次运行记录中查找并返回对应的子记录。
        /// </summary>
        /// <param name="toolBlock">要获取记录的 CogToolBlock 对象。</param>
        /// <param name="name">要查找的记录名称。</param>
        /// <returns>如果找到对应名称的记录，则返回该记录对象；否则返回 null。</returns>
        public ICogRecord GetRecord(CogToolBlock toolBlock, string name) {
            if (toolBlock == null) return null;
            if (toolBlock.RunStatus.Result != CogToolResultConstants.Accept) return null;

            var records = toolBlock.CreateLastRunRecord().SubRecords;

            return records.ContainsKey(name) ? records[name] : null;
        }
    }
}