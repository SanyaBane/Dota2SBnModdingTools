using System.Xml.Serialization;

namespace CommonLib;

// https://medium.com/@mpcodes/how-to-serialization-and-deserialization-xml-using-c-987e3b137aec#:~:text=XML%20serialization%20is%20the%20process,structured%20data%20in%20C%23%20applications.
public static class XmlSerializerService
{
  public static void SerializeToXml<T>(string filePath, T data)
  {
    XmlSerializer serializer= new XmlSerializer(typeof(T));
    using(StreamWriter writer= new StreamWriter(filePath))
    {
      serializer.Serialize(writer, data);
    }
  }

  public static T DeserilazeFromXml<T>(string filePath)
  {
    XmlSerializer serializer = new XmlSerializer(typeof(T));
    using(StreamReader reader= new StreamReader(filePath))
    {
      return (T)serializer.Deserialize(reader);
    }
  }
}