using System;

namespace WindowsFormsApp1.Enum {
    public enum PlcDataAddress {
        [PlcAddress("DB50.DBD20")] X,

        [PlcAddress("DB50.DBD24")] Y,

        [PlcAddress("DB50.DBD28")] Z,

        [PlcAddress("DB50.DBD32")] R,
        [PlcAddress("DB50.DBD8")] NineCaliNum,
        [PlcAddress("DB51.DBD24")] NineCaliNumCheck,
        [PlcAddress("DB51.DBD16")] CenterCaliNumCheck,
        [PlcAddress("DB50.DBD12")] CenterCaliNum,
        [PlcAddress("DB51.DBD4")] OffsetX,
        [PlcAddress("DB51.DBD8")] OffsetY,
        [PlcAddress("DB51.DBD0")] OffsetR,
        [PlcAddress("DB51.DBD12")] MeasureNumCheck,
        [PlcAddress("DB50.DBD4")] MeasureNum,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class PlcAddressAttribute : Attribute {
        public string Address{ get; }

        public PlcAddressAttribute(string address) {
            Address = address;
        }
    }

    public static class PlcTagExtensions {
        public static string GetAddress(this PlcDataAddress tag) {
            var memberInfo = typeof(PlcDataAddress).GetMember(tag.ToString())[0];

            var attr = (PlcAddressAttribute)Attribute.GetCustomAttribute(
                memberInfo, typeof(PlcAddressAttribute));

            return attr?.Address ?? throw new InvalidOperationException($"枚举 {tag} 没有绑定地址");
        }
    }
}