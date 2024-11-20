using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs.AddonsExporter;

public class AddonExporterCommandConfigWrapper
{
  [XmlElement(nameof(CopyAddonDirectoryCommandConfig), typeof(CopyAddonDirectoryCommandConfig))]
  [XmlElement(nameof(CopyAddonFileCommandConfig), typeof(CopyAddonFileCommandConfig))]
  [XmlElement(nameof(CopyDirectoryCommandConfig), typeof(CopyDirectoryCommandConfig))]
  [XmlElement(nameof(CopyFileCommandConfig), typeof(CopyFileCommandConfig))]
  [XmlElement(nameof(CompileAddonCommandConfig), typeof(CompileAddonCommandConfig))]
  [XmlElement(nameof(ClearOutputDirectoryCommandConfig), typeof(ClearOutputDirectoryCommandConfig))]
  public object XmlCommandConfig { get; set; }

  [XmlIgnore]
  public BaseAddonExporterCommandConfig CommandConfig
  {
    get => (BaseAddonExporterCommandConfig)XmlCommandConfig;
    set => XmlCommandConfig = value;
  }
}