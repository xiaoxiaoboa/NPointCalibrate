using System;

namespace WindowsFormsApp1.Enum {
    public enum PlcDataAddress {
        [PlcAddress("MD104")]
        Press,

        [PlcAddress("DB1.DBD4")]
        Release,

        [PlcAddress("MD100")]
        OnRed,

        [PlcAddress("DB1.DBD0")]
        OnGreen
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class PlcAddressAttribute : Attribute
    {
        public string Address { get; }

        public PlcAddressAttribute(string address)
        {
            Address = address;
        }
        
    }
    public static class PlcTagExtensions
    {
        public static string GetAddress(this PlcDataAddress tag)
        {
            var memberInfo = typeof(PlcDataAddress).GetMember(tag.ToString())[0];

            var attr = (PlcAddressAttribute)Attribute.GetCustomAttribute(
                memberInfo, typeof(PlcAddressAttribute));

            return attr?.Address ?? throw new InvalidOperationException($"枚举 {tag} 没有绑定地址");
        }
    }

}