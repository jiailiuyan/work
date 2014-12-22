using System;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using Jisons;

namespace JI
{
    /// <summary>
    /// ClassSerializers 的摘要说明。
    /// </summary>
    public class Helper_Serializers
    {
        private static Helper_Serializers instance;
        public static Helper_Serializers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Helper_Serializers();
                }
                return instance;
            }
        }

        protected Helper_Serializers()
        {

        }

        /// <summary>
        /// 将对象流转换成二进制流
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public System.IO.MemoryStream SerializeBinary<T>(T request) where T : class
        {
            return XmlClassData.WriteDataToStream<T>(request) as MemoryStream;
        }

        /// <summary>
        /// 将二进制流转换成对象
        /// </summary>
        /// <param name="memStream"></param>
        /// <returns></returns>
        public object DeSerializeBinary<T>(System.IO.Stream memStream) where T : class
        {
            memStream.Position = 0;
            return XmlClassData.ReadDataFromXml<T>(memStream);
        }
    }
}
