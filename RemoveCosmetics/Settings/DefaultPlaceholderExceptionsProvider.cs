using System.Text.RegularExpressions;
using RemoveCosmetics.Settings.DefaultData;
using RemoveCosmetics.Settings.Types;

namespace RemoveCosmetics.Settings;

public static class DefaultPlaceholderExceptionsProvider
{
  public static PlaceholderDirectoryException[] GetDefaultPlaceholderDirectoryExceptions()
  {
    var ret = new List<PlaceholderDirectoryException>();

    var defaultDirectoryExceptions = new PlaceholderDirectoryException[]
    {
      new() { Value = "models/items/beastmaster/boar", IsRegexPattern = false },
      new() { Value = "models/items/beastmaster/hawk", IsRegexPattern = false },
      new() { Value = "models/items/beastmaster/spittinglizard", IsRegexPattern = false },
      new() { Value = "models/items/broodmother/spiderling", IsRegexPattern = false },
      new() { Value = "models/items/death_prophet/exorcism", IsRegexPattern = false },
      new() { Value = "models/items/enigma/eidolon", IsRegexPattern = false },
      new() { Value = "models/items/furion/treant", IsRegexPattern = false },
      new() { Value = "models/items/gyrocopter/skyhigh_bomb_missle", IsRegexPattern = false },
      new() { Value = "models/items/gyrocopter/skyhigh_bomb_missle_alt", IsRegexPattern = false },
      new() { Value = "models/items/invoker/forge_spirit", IsRegexPattern = false },
      new() { Value = "models/items/juggernaut/ward", IsRegexPattern = false },
      new() { Value = "models/items/kunkka/viking_ship", IsRegexPattern = false },
      new() { Value = "models/items/lone_druid/bear", IsRegexPattern = false },
      new() { Value = "models/items/lone_druid/bear_trap", IsRegexPattern = false },
      new() { Value = "models/items/lone_druid/viciouskraitpanda", IsRegexPattern = false },
      new() { Value = "models/items/lycan/ultimate", IsRegexPattern = false },
      new() { Value = "models/items/lycan/wolves", IsRegexPattern = false },
      new() { Value = "models/items/phoenix/ultimate", IsRegexPattern = false },
      new() { Value = "models/items/pugna/ward", IsRegexPattern = false },
      new() { Value = "models/items/queenofpain/queenofpain_arcana/debut", IsRegexPattern = false, Comment = "Too long file names. Skip it, since 'debut' items should not affect gameplay anyway." },
      new() { Value = "models/items/tuskarr/sigil", IsRegexPattern = false },
      new() { Value = "models/items/undying/flesh_golem", IsRegexPattern = false },
      new() { Value = "models/items/venomancer/ward", IsRegexPattern = false },
      new() { Value = "models/items/warlock/golem", IsRegexPattern = false },
      new() { Value = "models/items/witchdoctor/wd_ward", IsRegexPattern = false },
      new() { Value = "models/items/wraith_king/wk_ti8_creep", IsRegexPattern = false },
    };

    ret.AddRange(defaultDirectoryExceptions);

    return ret.ToArray();
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileExceptions()
  {
    var ret = new List<PlaceholderFileException>();

    // var defaultFileExceptionPatterns = new PlaceholderFileException[]
    // {
    //   // new PlaceholderException { Value = @".*arcana.*", IsRegexPattern = true },
    // };
    //
    // ret.AddRange(defaultFileExceptionPatterns);

    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_abaddon());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_abyssal_underlord());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_alchemist());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_ancient_apparition());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_antimage());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_antimage_female());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_arc_warden());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_axe());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_bane());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_batrider());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_beastmaster());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_blood_seeker());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_bounty_hunter());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_brewmaster());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_bristleback());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_broodmother());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_centaur());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_chaos_knight());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_chen());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_clinkz());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_crystal_maiden());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_crystal_maiden_persona());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_dark_seer());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_dark_willow());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_dawnbreaker());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_dazzle());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_death_prophet());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_disruptor());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_doom());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_dragon_knight());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_dragon_knight_persona());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_drow());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_earthspirit());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_earthshaker());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_elder_titan());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_ember_spirit());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_enchantress());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_enigma());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_faceless_void());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_furion());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_grimstroke());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_gyro());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_hoodwink());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_huskar());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_invoker());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_invoker_kid());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_jakiro());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_juggernaut());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_keeper_of_the_light());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_kez());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_kunkka());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_lanaya());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_legion_commander());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_leshrac());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_lich());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_life_stealer());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_lina());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_lion());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_lone_druid());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_luna());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_lycan());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_magnataur());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_marci());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_mars());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_medusa());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_meepo());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_mirana());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_mirana_persona());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_monkey_king());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_morphling());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_muerta());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_necrolyte());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_nerubian_assassin());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_nevermore());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_nightstalker());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_obsidian_destroyer());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_ogre_magi());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_omniknight());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_oracle());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_pangolier());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_phantom_assassin());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_phantom_assassin_persona());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_phantom_lancer());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_phoenix());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_primal_beast());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_puck());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_pudge());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_pudge_cute());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_pugna());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_queenofpain());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_rattletrap());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_razor());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_rikimaru());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_ringmaster());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_rubick());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_sand_king());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_shadow_demon());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_shadow_fiend());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_shadowshaman());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_shredder());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_silencer());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_siren());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_skywrath_mage());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_slardar());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_slark());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_snapfire());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_sniper());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_spectre());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_spirit_breaker());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_storm_spirit());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_sven());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_techies());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_terrorblade());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_tidehunter());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_tinker());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_tiny());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_treant_protector());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_troll_warlord());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_tuskarr());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_twin_headed_dragon());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_undying());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_ursa());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_vengeful());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_venomancer());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_viper());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_visage());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_void_spirit());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_warlock());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_weaver());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_windrunner());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_winterwyvern());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_wisp());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_witchdoctor());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_wraith_king());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_zeus());
    ret.AddRange(DefaultPlaceholderExceptions.GetDefaultPlaceholderFileException_zuus());

    return ret.ToArray();
  }
}