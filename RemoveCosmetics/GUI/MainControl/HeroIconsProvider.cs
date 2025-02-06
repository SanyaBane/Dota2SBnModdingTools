using System.IO;
using System.Windows.Media.Imaging;

namespace RemoveCosmetics.GUI.MainControl;

public class HeroIconsProvider
{
  private readonly Dictionary<string, string> _dictionaryPathToIcons = new()
  {
    { "abaddon", "npc_dota_hero_abaddon_png.png" },
    { "abyssal_underlord", "npc_dota_hero_abyssal_underlord_png.png" },
    { "alchemist", "npc_dota_hero_alchemist_png.png" },
    { "ancient_apparition", "npc_dota_hero_ancient_apparition_png.png" },
    { "antimage", "npc_dota_hero_antimage_png.png" },
    { "antimage_female", "npc_dota_hero_antimage_female_png.png" },
    { "arc_warden", "npc_dota_hero_arc_warden_png.png" },
    { "armadillo", "npc_dota_hero_pangolier_png.png" },
    { "axe", "npc_dota_hero_axe_png.png" },
    { "bane", "npc_dota_hero_bane_png.png" },
    { "batrider", "npc_dota_hero_batrider_png.png" },
    { "beastmaster", "npc_dota_hero_beastmaster_png.png" },
    { "blood_seeker", "npc_dota_hero_bloodseeker_png.png" },
    { "bounty_hunter", "npc_dota_hero_bounty_hunter_png.png" },
    { "brewmaster", "npc_dota_hero_brewmaster_png.png" },
    { "bristleback", "npc_dota_hero_bristleback_png.png" },
    { "broodmother", "npc_dota_hero_broodmother_png.png" },
    { "centaur", "npc_dota_hero_centaur_png.png" },
    { "chaos_knight", "npc_dota_hero_chaos_knight_png.png" },
    { "chen", "npc_dota_hero_chen_png.png" },
    { "clinkz", "npc_dota_hero_clinkz_png.png" },
    { "crystal_maiden", "npc_dota_hero_crystal_maiden_png.png" },
    { "crystal_maiden_persona", "npc_dota_hero_crystal_maiden_persona_png.png" },
    { "dark_seer", "npc_dota_hero_dark_seer_png.png" },
    { "dark_willow", "npc_dota_hero_dark_willow_png.png" },
    { "dawnbreaker", "npc_dota_hero_dawnbreaker_png.png" },
    { "dazzle", "npc_dota_hero_dazzle_png.png" },
    { "death_prophet", "npc_dota_hero_death_prophet_png.png" },
    { "disruptor", "npc_dota_hero_disruptor_png.png" },
    { "doom", "npc_dota_hero_doom_bringer_png.png" },
    { "dragon_knight", "npc_dota_hero_dragon_knight_png.png" },
    { "dragon_knight_persona", "npc_dota_hero_dragon_knight_persona_png.png" },
    { "drow", "npc_dota_hero_drow_ranger_png.png" },
    { "earth_spirit", "npc_dota_hero_earth_spirit_png.png" },
    { "earthshaker", "npc_dota_hero_earthshaker_png.png" },
    { "elder_titan", "npc_dota_hero_elder_titan_png.png" },
    { "ember_spirit", "npc_dota_hero_ember_spirit_png.png" },
    { "enchantress", "npc_dota_hero_enchantress_png.png" },
    { "enigma", "npc_dota_hero_enigma_png.png" },
    { "faceless_void", "npc_dota_hero_faceless_void_png.png" },
    { "furion", "npc_dota_hero_furion_png.png" },
    { "grimstroke", "npc_dota_hero_grimstroke_png.png" },
    { "gyro", "npc_dota_hero_gyrocopter_png.png" },
    { "gyrocopter", "npc_dota_hero_gyrocopter_png.png" },
    { "hex", "npc_dota_hero_hex_png.png" },
    { "hoodwink", "npc_dota_hero_hoodwink_png.png" },
    { "huskar", "npc_dota_hero_huskar_png.png" },
    { "invoker", "npc_dota_hero_invoker_png.png" },
    { "invoker_kid", "npc_dota_hero_invoker_kid_png.png" },
    { "io", "npc_dota_hero_wisp_png.png" },
    { "items", "npc_dota_hero_items_png.png" },
    { "jakiro", "npc_dota_hero_jakiro_png.png" },
    { "juggernaut", "npc_dota_hero_juggernaut_png.png" },
    { "keeper_of_the_light", "npc_dota_hero_keeper_of_the_light_png.png" },
    { "kez", "npc_dota_hero_kez_png.png" },
    { "kunkka", "npc_dota_hero_kunkka_png.png" },
    { "lanaya", "npc_dota_hero_templar_assassin_png.png" },
    { "legion_commander", "npc_dota_hero_legion_commander_png.png" },
    { "leshrac", "npc_dota_hero_leshrac_png.png" },
    { "lich", "npc_dota_hero_lich_png.png" },
    { "life_stealer", "npc_dota_hero_life_stealer_png.png" },
    { "lifestealer", "npc_dota_hero_life_stealer_png.png" },
    { "lina", "npc_dota_hero_lina_png.png" },
    { "lion", "npc_dota_hero_lion_png.png" },
    { "lone_druid", "npc_dota_hero_lone_druid_png.png" },
    { "luna", "npc_dota_hero_luna_png.png" },
    { "lycan", "npc_dota_hero_lycan_png.png" },
    { "magnataur", "npc_dota_hero_magnataur_png.png" },
    { "marci", "npc_dota_hero_marci_png.png" },
    { "mars", "npc_dota_hero_mars_png.png" },
    { "medusa", "npc_dota_hero_medusa_png.png" },
    { "meepo", "npc_dota_hero_meepo_png.png" },
    { "mirana", "npc_dota_hero_mirana_png.png" },
    { "mirana_persona", "npc_dota_hero_mirana_persona_png.png" },
    { "monkey_king", "npc_dota_hero_monkey_king_png.png" },
    { "morphling", "npc_dota_hero_morphling_png.png" },
    { "muerta", "npc_dota_hero_muerta_png.png" },
    { "necrolyte", "npc_dota_hero_necrolyte_png.png" },
    { "nerubian_assassin", "npc_dota_hero_nyx_assassin_png.png" },
    { "nevermore", "npc_dota_hero_nevermore_png.png" },
    { "nightstalker", "npc_dota_hero_night_stalker_png.png" },
    { "obsidian_destroyer", "npc_dota_hero_obsidian_destroyer_png.png" },
    { "ogre_magi", "npc_dota_hero_ogre_magi_png.png" },
    { "omniknight", "npc_dota_hero_omniknight_png.png" },
    { "oracle", "npc_dota_hero_oracle_png.png" },
    { "pangolier", "npc_dota_hero_pangolier_png.png" },
    { "phantom_assassin", "npc_dota_hero_phantom_assassin_png.png" },
    { "phantom_assassin_persona", "npc_dota_hero_phantom_assassin_persona_png.png" },
    { "phantom_lancer", "npc_dota_hero_phantom_lancer_png.png" },
    { "phoenix", "npc_dota_hero_phoenix_png.png" },
    { "primal_beast", "npc_dota_hero_primal_beast_png.png" },
    { "puck", "npc_dota_hero_puck_png.png" },
    { "pudge", "npc_dota_hero_pudge_png.png" },
    { "pudge_cute", "npc_dota_hero_pudge_cute_png.png" },
    { "pugna", "npc_dota_hero_pugna_png.png" },
    { "queenofpain", "npc_dota_hero_queenofpain_png.png" },
    { "rattletrap", "npc_dota_hero_rattletrap_png.png" },
    { "razor", "npc_dota_hero_razor_png.png" },
    { "rikimaru", "npc_dota_hero_riki_png.png" },
    { "ringmaster", "npc_dota_hero_ringmaster_png.png" },
    { "rubick", "npc_dota_hero_rubick_png.png" },
    { "sand_king", "npc_dota_hero_sand_king_png.png" },
    { "shadow_demon", "npc_dota_hero_shadow_demon_png.png" },
    { "shadow_fiend", "npc_dota_hero_nevermore_png.png" },
    { "shadowshaman", "npc_dota_hero_shadow_shaman_png.png" },
    { "shredder", "npc_dota_hero_shredder_png.png" },
    { "silencer", "npc_dota_hero_silencer_png.png" },
    { "siren", "npc_dota_hero_naga_siren_png.png" },
    { "skeleton_king", "npc_dota_hero_skeleton_king_png.png" },
    { "skywrath_mage", "npc_dota_hero_skywrath_mage_png.png" },
    { "slardar", "npc_dota_hero_slardar_png.png" },
    { "slark", "npc_dota_hero_slark_png.png" },
    { "snapfire", "npc_dota_hero_snapfire_png.png" },
    { "sniper", "npc_dota_hero_sniper_png.png" },
    { "spectre", "npc_dota_hero_spectre_png.png" },
    { "spirit_breaker", "npc_dota_hero_spirit_breaker_png.png" },
    { "storm_spirit", "npc_dota_hero_storm_spirit_png.png" },
    { "sven", "npc_dota_hero_sven_png.png" },
    { "techies", "npc_dota_hero_techies_png.png" },
    { "templar_assassin", "npc_dota_hero_templar_assassin_png.png" },
    { "terrorblade", "npc_dota_hero_terrorblade_png.png" },
    { "tidehunter", "npc_dota_hero_tidehunter_png.png" },
    { "tinker", "npc_dota_hero_tinker_png.png" },
    { "tiny", "npc_dota_hero_tiny_png.png" },
    { "tiny_01", "npc_dota_hero_tiny_png.png" },
    { "tiny_02", "npc_dota_hero_tiny_png.png" },
    { "tiny_03", "npc_dota_hero_tiny_png.png" },
    { "tiny_04", "npc_dota_hero_tiny_png.png" },
    { "treant", "npc_dota_hero_treant_png.png" },
    { "treant_protector", "npc_dota_hero_treant_png.png" },
    { "troll_warlord", "npc_dota_hero_troll_warlord_png.png" },
    { "tuskarr", "npc_dota_hero_tusk_png.png" },
    { "twin_headed_dragon", "npc_dota_hero_jakiro_png.png" },
    { "underlord", "npc_dota_hero_abyssal_underlord_png.png" },
    { "undying", "npc_dota_hero_undying_png.png" },
    { "ursa", "npc_dota_hero_ursa_png.png" },
    { "vengeful", "npc_dota_hero_vengefulspirit_png.png" },
    { "vengefulspirit", "npc_dota_hero_vengefulspirit_png.png" },
    { "venomancer", "npc_dota_hero_venomancer_png.png" },
    { "viper", "npc_dota_hero_viper_png.png" },
    { "visage", "npc_dota_hero_visage_png.png" },
    { "void_spirit", "npc_dota_hero_void_spirit_png.png" },
    { "warlock", "npc_dota_hero_warlock_png.png" },
    { "weaver", "npc_dota_hero_weaver_png.png" },
    { "windrunner", "npc_dota_hero_windrunner_png.png" },
    { "winter_wyvern", "npc_dota_hero_winter_wyvern_png.png" },
    { "winterwyvern", "npc_dota_hero_winter_wyvern_png.png" },
    { "wisp", "npc_dota_hero_wisp_png.png" },
    { "witchdoctor", "npc_dota_hero_witch_doctor_png.png" },
    { "wraith_king", "npc_dota_hero_skeleton_king_png.png" },
    { "zeus", "npc_dota_hero_zuus_png.png" },
    { "zuus", "npc_dota_hero_zuus_png.png" },
  };


  private readonly Dictionary<string, BitmapImage?> _dictionaryIcons = new();

  public HeroIconsProvider()
  {
    const string defaultIconPath = "npc_dota_hero_default_png.png";
    var imageUri = new Uri($"pack://application:,,,/Images/HeroIcons/{defaultIconPath}");
    var bitmap = new BitmapImage(imageUri);

    // var imagesDirectory = Path.Combine(Environment.CurrentDirectory, "Template_Directories");
    // BitmapImage bitmap = new BitmapImage();
    // bitmap.BeginInit();
    // bitmap.UriSource = new Uri("C:/path/to/your/image.png"); // Replace with your file path
    // bitmap.EndInit();

    DefaultIcon = bitmap;
  }

  public BitmapImage? DefaultIcon { get; }

  public BitmapImage? GetIcon(string heroName)
  {
    if (_dictionaryIcons.TryGetValue(heroName, out var icon))
      return icon;

    var result = DefaultIcon;
    if (_dictionaryPathToIcons.TryGetValue(heroName, out var iconFilePath))
    {
      try
      {
        var imageUri = new Uri($"pack://application:,,,/Images/HeroIcons/{iconFilePath}");
        result = new BitmapImage(imageUri);
      }
      catch (FileNotFoundException)
      {
      }
      catch (IOException)
      {
      }
    }

    _dictionaryIcons.Add(heroName, result);
    return result;
  }
}