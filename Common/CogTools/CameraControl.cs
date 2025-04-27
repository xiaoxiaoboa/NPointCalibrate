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
        public async Task<string> Initialize() {
            return await Task.Run(() => {
                try {
                    _cogFrameGrabbers = new CogFrameGrabbers();

                    if (_cogFrameGrabbers.Count > 0) {
                        _cogFrameGrabber = _cogFrameGrabbers[0];
                        _cogAcqFifo = _cogFrameGrabber.CreateAcqFifo("Generic GigEVision (RGB8)",
                            CogAcqFifoPixelFormatConstants.Format32RGB, 0, true);
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