using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Conversation_Editor
{
    /// <summary>
    /// Serialize and deserialize objects
    /// </summary>
    static class Serializer
    {
        public static void SerializeObject(string filename, object objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public static object DeSerializeObject(string filename)
        {
            object ob;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            ob = bFormatter.Deserialize(stream);
            stream.Close();
            return ob;
        }

        public static void SerializeObjectAsXML(string filename, Type objectType,
            object objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            XmlSerializer xFormatter = new XmlSerializer(objectType);
            xFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }
    }
}
