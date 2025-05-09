using System;
using System.IO;
using System.Threading.Tasks;
using Cognex.VisionPro;

namespace WindowsFormsApp1.Common.CogTools {
    public class CameraControl {
        private static readonly Lazy<CameraControl> _instance = new Lazy<CameraControl>(() => new CameraControl());
        private CogFrameGrabbers _cogFrameGrabbers;
        private ICogFrameGrabber _cogFrameGrabber;
        private ICogAcqFifo _cogAcqFifo;
        private int _currentAcqTicket;
        
        public static CameraControl Instance => _instance.Value;

        public bool IsShooting{ get; set; }

        public ICogAcqFifo Acq => _cogAcqFifo;

        private CameraControl() { }

        // 初始化相机
        /// <summary>
        /// 初始化相机。
        /// 该方法通过异步任务初始化相机设备，尝试连接可用的相机并创建帧捕获队列。
        /// 如果未检测到相机设备或初始化失败，则返回错误信息。
        /// </summary>
        /// <returns>
        /// 返回一个表示初始化结果的字符串。
        /// 如果初始化成功，返回 null；
        /// 如果初始化失败，返回异常消息。
        /// </returns>
        public async Task<string> Initialize() {
            return await Task.Run(() => {
                try {
                    _cogFrameGrabbers = new CogFrameGrabbers();

                    if (_cogFrameGrabbers.Count > 0) {
                        _cogFrameGrabber = _cogFrameGrabbers[0];
                        _cogAcqFifo = _cogFrameGrabber.CreateAcqFifo("Generic GigEVision (RGB8)",
                            CogAcqFifoPixelFormatConstants.Format3Plane, 0, true);
                    }
                    else {
                        throw new Exception("无相机连接");
                    }

                    return null;
                }
                catch (Exception exception) {
                    return exception.Message;
                }
            });
        }

        // 拍照
        /// <summary>
        /// 触发相机进行拍照操作。
        /// 该方法尝试启动相机的帧捕获过程，并在成功时返回 null。
        /// 如果在拍照过程中发生异常，将捕获异常并返回错误信息。
        /// </summary>
        /// <returns>
        /// 返回一个表示拍照结果的字符串。
        /// 如果拍照成功，返回 null；
        /// 如果拍照失败，返回包含异常信息的错误消息。
        /// </returns>
        public string TakePhotoGraph() {
            try {
                IsShooting = true;
                _currentAcqTicket = _cogAcqFifo.StartAcquire();
                return null;
            }
            catch (Exception exception) {
                IsShooting = false;
                return $"触发拍照失败：{exception.Message}";
            }
        }

        // 获取拍照后的图像
        /// <summary>
        /// 获取拍照后的图像。
        /// 该方法从帧捕获队列中检查是否有可用的图像。如果队列中有准备好的图像，则获取并返回该图像。
        /// 如果队列中没有可用图像，则抛出异常，提示拍照失败。
        /// </summary>
        /// <returns>
        /// 返回一个包含图像数据的对象，该对象实现了 ICogImage 接口。
        /// 如果没有可用图像，将抛出 IOException 异常。
        /// </returns>
        public ICogImage GetGraphic() {
            _cogAcqFifo.GetFifoState(out int numPending, out int numReady, out bool busy);
            if (numReady > 0) {
                ICogImage image = _cogAcqFifo.CompleteAcquire(_currentAcqTicket, out int ticket,
                    out int triggerNumber);
                return image;
            }

            throw new IOException("拍照失败");
        }

        // 销毁实例
        public void DisposeCamera() {
            _cogFrameGrabber?.Disconnect(true);
        }
    }
}