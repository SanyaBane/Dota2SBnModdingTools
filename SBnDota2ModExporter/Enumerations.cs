using System.ComponentModel;

namespace SBnDota2ModExporter;

public static class Enumerations
{
  public static string GetEnumDescription(Enum value)
  {
    var fi = value.GetType().GetField(value.ToString());

    var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

    if (attributes != null && attributes.Any())
    {
      return attributes.First().Description;
    }

    return value.ToString();
  }
}