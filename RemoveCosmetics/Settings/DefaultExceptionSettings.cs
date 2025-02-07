using RemoveCosmetics.Settings.Types;

namespace RemoveCosmetics.Settings;

public static class DefaultExceptionSettings
{
  private static readonly string[] _defaultPlaceholderDirectoryExceptionPatterns =
  [
    // ".*arcana.*",
  ];

  private static readonly string[] _defaultPlaceholderDirectoryExceptions =
  [
    "models/items/beastmaster/boar",
    "models/items/beastmaster/hawk",
    "models/items/broodmother/spiderling",
    "models/items/death_prophet/exorcism",
    "models/items/enigma/eidolon",
    "models/items/furion/treant",
    "models/items/gyrocopter/skyhigh_bomb_missle",
    "models/items/gyrocopter/skyhigh_bomb_missle_alt",
    "models/items/invoker/forge_spirit",
    "models/items/juggernaut/ward",
    "models/items/kunkka/viking_ship",
    "models/items/lone_druid/bear",
    "models/items/lone_druid/viciouskraitpanda",
    "models/items/lycan/ultimate",
    "models/items/lycan/wolves",
    "models/items/phoenix/ultimate",
    "models/items/pugna/ward",
    "models/items/queenofpain/queenofpain_arcana/debut",
    "models/items/templar_assassin/psi_blades",
    "models/items/tuskarr/sigil",
    "models/items/undying/flesh_golem",
    "models/items/venomancer/ward",
    "models/items/warlock/golem",
    "models/items/wraith_king/wk_ti8_creep"
  ];

  private static readonly string[] _defaultPlaceholderFileExceptionPatterns =
  [
    // ".*arcana.*",
  ];

  private static readonly string[] _defaultPlaceholderFileExceptions =
  [
    "models/heroes/abyssal_underlord/abyssal_underlord_portal_model.vmdl_c",
    "models/heroes/abyssal_underlord/rock_debris.vmdl_c",
    "models/heroes/crystal_maiden/pedestal_cm_arcana.vmdl",
    "models/heroes/invoker/forge_spirit.vmdl_c",
    "models/heroes/keeper_of_the_light/kotl_wisp.vmdl_c",
    "models/heroes/phantom_assassin/weapon_fx.vmdl_c",
    "models/items/earthshaker/ti9_immortal/ti9_immortal_fissure.vmdl_c",
    "models/items/juggernaut/ward_foo.vmdl_c",
  ];

  public static PlaceholderException[] GetDefaultPlaceholderDirectoryExceptions()
  {
    var ret = _defaultPlaceholderDirectoryExceptionPatterns.Select(val => new PlaceholderException { Value = val, IsRegexPattern = true }).ToList();
    ret.AddRange(_defaultPlaceholderDirectoryExceptions.Select(val => new PlaceholderException { Value = val, IsRegexPattern = false }));

    return ret.ToArray();
  }

  public static PlaceholderException[] GetDefaultPlaceholderFileExceptions()
  {
    var ret = _defaultPlaceholderFileExceptionPatterns.Select(val => new PlaceholderException { Value = val, IsRegexPattern = true }).ToList();
    ret.AddRange(_defaultPlaceholderFileExceptions.Select(val => new PlaceholderException { Value = val, IsRegexPattern = false }));

    return ret.ToArray();
  }
}