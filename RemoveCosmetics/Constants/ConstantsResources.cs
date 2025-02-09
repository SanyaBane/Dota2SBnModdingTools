namespace RemoveCosmetics.Constants;

public class ConstantsResources
{
  public const string EMPTY_PLACEHOLDER_FILE = "Resources/EmptyPlaceholder.vmdl_c";

  private const string VPK_CREATOR_EXE_FILE = "Resources/VPK_Creator/vpk.exe";
  private static readonly string[] VPK_CREATOR_DLL_FILES =
  [
    "Resources/VPK_Creator/filesystem_stdio.dll",
    "Resources/VPK_Creator/tier0.dll",
    "Resources/VPK_Creator/tier0_s.dll",
    "Resources/VPK_Creator/vstdlib.dll",
    "Resources/VPK_Creator/vstdlib_s.dll",
  ];

  public static string[] GetVpkCreatorResources()
  {
    var ret = new List<string> { VPK_CREATOR_EXE_FILE };
    ret.AddRange(VPK_CREATOR_DLL_FILES);
    return ret.ToArray();
  }
}