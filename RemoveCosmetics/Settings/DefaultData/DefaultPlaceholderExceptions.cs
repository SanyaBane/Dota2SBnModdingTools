using System.Text.RegularExpressions;
using RemoveCosmetics.Settings.Types;

namespace RemoveCosmetics.Settings.DefaultData;

public static class DefaultPlaceholderExceptions
{
  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_abaddon()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/abaddon/abaddon.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_abyssal_underlord()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/abyssal_underlord/abyssal_underlord_v2.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/abyssal_underlord/abyssal_underlord_portal_model.vmdl_c", IsRegexPattern = false, Comment = "Portal model" },
      new() { Value = "models/heroes/abyssal_underlord/rock_debris.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_alchemist()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/alchemist/alchemist.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_ancient_apparition()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/ancient_apparition/ancient_apparition.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_antimage()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/antimage/antimage.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_antimage_female()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/antimage_female/antimage_female.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_arc_warden()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/arc_warden/arc_warden.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/arc_warden/mesh/spark_wraith.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/arc_warden/mesh/spark_wraith_collision.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_axe()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/axe/axe.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_bane()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/bane/bane.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/bane/grip.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_batrider()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/batrider/batrider.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_beastmaster()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/beastmaster/beastmaster.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/beastmaster/beastmaster_beast.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/beastmaster/beastmaster_bird.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_blood_seeker()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/blood_seeker/blood_seeker.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_bounty_hunter()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/bounty_hunter/bounty_hunter.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_brewmaster()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/brewmaster/brewmaster.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/brewmaster/brewmaster_earthspirit.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/brewmaster/brewmaster_earthspirit_end.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/brewmaster/brewmaster_firespirit.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/brewmaster/brewmaster_voidspirit.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/brewmaster/brewmaster_windspirit.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/items/brewmaster/spiritual_spirits.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_bristleback()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/bristleback/bristleback.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_broodmother()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/broodmother/broodmother.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/broodmother/broodmother_original.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/broodmother/spiderling.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/broodmother/spidersack.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_centaur()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/centaur/centaur.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/centaur/diretide_centaur.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_chaos_knight()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/chaos_knight/chaos_knight.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_chen()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/chen/chen.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_clinkz()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/clinkz/clinkz.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/clinkz/clinkz_archer.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_crystal_maiden()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/crystal_maiden/crystal_maiden.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/crystal_maiden/crystal_maiden_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      new() { Value = "models/heroes/crystal_maiden/crystal_maiden_ice.vmdl_c", IsRegexPattern = false, Comment = "Used by Tuskarr for Ice Shards" },
      new() { Value = "models/heroes/crystal_maiden/crystal_maiden_ice_debris_01.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/crystal_maiden/crystal_maiden_screeauk_arcana.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/crystal_maiden/pedestal_cm_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_crystal_maiden_persona()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/crystal_maiden_persona/crystal_maiden_persona.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/crystal_maiden_persona/crystal_maiden_persona_ult_model.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_dark_seer()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/dark_seer/dark_seer.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/dark_seer/darkseer_sfm.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_dark_willow()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/dark_willow/dark_willow.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/dark_willow/dark_willow_thorn.vmdl_c", IsRegexPattern = false, Comment = "Base model stuff" },
      new() { Value = "models/heroes/dark_willow/dark_willow_model_sfm.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_dawnbreaker()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/dawnbreaker/dawnbreaker.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/dawnbreaker/dawnbreaker_lookdev.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_dazzle()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/dazzle/dazzle.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_death_prophet()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/death_prophet/death_prophet.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/death_prophet/death_prophet_ghost.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/death_prophet/rubick_arcana_death_prophet_ghost.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_disruptor()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/disruptor/disruptor.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_doom()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/doom/doom.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_dragon_knight()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/dragon_knight/dragon_knight.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/dragon_knight/dragon_knight_dragon.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_dragon_knight_persona()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/dragon_knight_persona/dk_persona_base.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/dragon_knight_persona/dk_persona_dragon.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/dragon_knight_persona/dk_persona_lookdev.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_drow()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/drow/drow_base.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/drow/drow_arcana/drow_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_earthspirit()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/earth_spirit/earth_spirit.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/earth_spirit/stonesummon.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_earthshaker()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/earthshaker/earthshaker.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/earthshaker/deep_magma/deep_magma_rock.vmdl_c", IsRegexPattern = false, Comment = "Fissure" },
      new() { Value = "models/items/earthshaker/egset_particlerocks/egset_particlerocks.vmdl_c", IsRegexPattern = false, Comment = "Fissure" },
      new() { Value = "models/items/earthshaker/earthshaker_arcana/earthshaker_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      new() { Value = "models/items/earthshaker/earthshaker_arcana/earthshaker_arcana_pedestal.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
      new() { Value = "models/items/earthshaker/ti9_immortal/ti9_es_immortal_rock_01.vmdl_c", IsRegexPattern = false, Comment = "Fissure" },
      new() { Value = "models/items/earthshaker/ti9_immortal/ti9_immortal_fissure.vmdl_c", IsRegexPattern = false, Comment = "Fissure" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_elder_titan()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/elder_titan/elder_titan.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/elder_titan/ancestral_spirit.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_ember_spirit()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/ember_spirit/ember_spirit.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ember_spirit/ember_spirit_sfm.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_enchantress()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/enchantress/enchantress.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_enigma()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/enigma/enigma.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/enigma/eidelon.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_faceless_void()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/faceless_void/faceless_void.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/faceless_void/faceless_void_arcana/faceless_void_arcana_base.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_furion()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/furion/furion.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/furion/treant.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_grimstroke()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/grimstroke/grimstroke.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/grimstroke/ink_phantom.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/grimstroke/grimstroke_soul_chain_link.vmdl_c", IsRegexPattern = false, Comment = "Ultimate" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_gyro()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/gyro/gyro.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/gyro/gyro_missile.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = @"models\/items\/gyrocopter\/.*miss[i]{0,1}le.*" + Regex.Escape(".vmdl_c"), IsRegexPattern = true, Comment = "Missiles models" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_hoodwink()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/hoodwink/hoodwink.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/hoodwink/hoodwink_acorn_knot_model.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/hoodwink/hoodwink_acorn_model.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/hoodwink/hoodwink_acorn_rope_model.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/hoodwink/hoodwink_tree_model.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_huskar()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/huskar/huskar.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_invoker()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/invoker/invoker.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/invoker/forge_spirit.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_invoker_kid()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/invoker_kid/invoker_kid.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_jakiro()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/jakiro/jakiro.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_juggernaut()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/juggernaut/juggernaut.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/juggernaut/jugg_healing_ward.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/juggernaut/juggernaut_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      new() { Value = "models/items/juggernaut/ward_foo.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_keeper_of_the_light()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/keeper_of_the_light/keeper_of_the_light.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/keeper_of_the_light/kotl_wisp.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_kez()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/kez/kez_base.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/kez/kez_sfm.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_kunkka()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/kunkka/ghostship.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/kunkka/kunkka.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/kunkka/kunkka_model_cosmetics.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/kunkka/kunkka_sfm.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_lanaya()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/lanaya/lanaya.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/lanaya/lanaya_refract_hit.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/lanaya/lanaya_refraction.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/lanaya/lanaya_trap_crystal.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/lanaya/lanaya_trap_crystal_invis.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_legion_commander()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/legion_commander/legion_commander.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_leshrac()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/leshrac/leshrac.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_lich()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/lich/lich.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/lich/ice_spire.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/lich/ice_spire_01.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_life_stealer()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/life_stealer/life_stealer.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_lina()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/lina/lina.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_lion()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/lion/lion.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_lone_druid()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/lone_druid/lone_druid.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/lone_druid/spirit_bear.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/lone_druid/spirit_bear_rubick.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/lone_druid/true_form.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_luna()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/luna/luna.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_lycan()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/lycan/lycan.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/lycan/lycan_wolf.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/lycan/lycan_wolf_rubick.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/lycan/summon_wolves.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/lycan/summon_wolves_spirited.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_magnataur()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/magnataur/magnataur.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_marci()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/marci/marci_base.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/marci/sidekick_sigil.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_mars()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/mars/mars.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/mars/mars_colosseum_spear.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/mars/mars_colosseum_walls_00.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/mars/mars_colosseum_walls_01.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/mars/mars_colosseum_walls_02.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_medusa()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/medusa/medusa.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_meepo()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/meepo/meepo.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/meepo/megameepo.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_mirana()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/mirana/mirana.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/mirana/mirana_javelin.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_mirana_persona()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/mirana_persona/mirana_persona_base.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_monkey_king()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/monkey_king/monkey_king.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/monkey_king/monkey_king_status_effect_icon.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/monkey_king/transform_invisiblebox.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_morphling()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/morphling/morphling.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_muerta()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/muerta/muerta_base.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/muerta/muerta_summon_model.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/muerta/muerta_ult.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_necrolyte()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/necrolyte/necrolyte.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_nerubian_assassin()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/nerubian_assassin/mound.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/nerubian_assassin/nerubian_assassin.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_nevermore()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/nevermore/nevermore.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_nightstalker()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/nightstalker/nightstalker.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/nightstalker/nightstalker_night.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_obsidian_destroyer()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/obsidian_destroyer/obsidian_destroyer.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_ogre_magi()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/ogre_magi/ogre_magi.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/ogre_magi/ogre_arcana/ogre_magi_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_omniknight()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/omniknight/omniknight.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/omniknight/omniknightwings.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/omniknight/omniknightwings_rubick.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_oracle()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/oracle/oracle.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_pangolier()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/pangolier/pangolier.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/pangolier/pangolier_gyroshell.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/pangolier/pangolier_gyroshell2.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/pangolier/pangolier_gyroshell2_rubick.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_phantom_assassin()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/phantom_assassin/phantom_assassin.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/phantom_assassin/pa_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      new() { Value = "models/heroes/phantom_assassin/arcana_pedestal.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
      new() { Value = "models/heroes/phantom_assassin/arcana_tombstone.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
      new() { Value = "models/heroes/phantom_assassin/arcana_tombstone2.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
      new() { Value = "models/heroes/phantom_assassin/arcana_tombstone3.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
      new() { Value = "models/heroes/phantom_assassin/weapon_fx.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff (spawns on kill enemy hero)" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_phantom_assassin_persona()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/phantom_assassin_persona/phantom_assassin_persona.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_phantom_lancer()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/phantom_lancer/phantom_lancer.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_phoenix()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/phoenix/phoenix.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/phoenix/phoenix_bird.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/phoenix/phoenix_egg.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_primal_beast()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/primal_beast/primal_beast_base.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_puck()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/puck/puck.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_pudge()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/pudge/pudge.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/pudge/righthook.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/pudge/arcana/pudge_arcana_base.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      // new() { Value = @"models\/items\/pudge\/.*hook.*" + Regex.Escape(".vmdl_c"), IsRegexPattern = true, Comment = "Hooks models"},
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_pudge_cute()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/pudge_cute/pudge_cute.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/pudge_cute/pudge_cute_hook.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_pugna()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/pugna/pugna.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/pugna/pugna_ward.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_queenofpain()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/queenofpain/queenofpain.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/queenofpain/arcana/queenofpain_arcana_base_remodel.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      new() { Value = "models/items/queenofpain/queenofpain_arcana/queenofpain_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_rattletrap()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/rattletrap/rattletrap.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/rattletrap/rattletrap_cog.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/rattletrap/rattletrap_cog_rubick.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = @"models\/items\/rattletrap\/.*cog.*" + Regex.Escape(".vmdl_c"), IsRegexPattern = true, Comment = "Cogs models" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_razor()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/razor/razor.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/razor/razor_arcana/razor_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_rikimaru()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/rikimaru/rikimaru.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_ringmaster()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/ringmaster/ringmaster_base.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_box.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_box_loadout.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_box_swords.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_bullwhip.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_wheel_bulbs.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_wheel_decoy.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_wheel_versus.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_whip.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/ringmaster/ringmaster_whip_alt.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      // todo Ulti
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_rubick()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/rubick/rubick.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/rubick/rubick_tree.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/items/rubick/rubick_arcana/rubick_arcana_base.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_sand_king()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/sand_king/sand_king.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_shadow_demon()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/shadow_demon/shadow_demon.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_shadow_fiend()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/shadow_fiend/shadow_fiend.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/shadow_fiend/shadow_fiend_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      new() { Value = "models/heroes/shadow_fiend/shadow_pedestal_sf.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_shadowshaman()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/shadowshaman/shadowshaman.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/shadowshaman/shadowshaman_totem.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/shadowshaman/shadowshaman_totem_rubick.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_shredder()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/shredder/shredder.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/shredder/shredder_chakram.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_silencer()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/silencer/silencer.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_siren()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/siren/siren.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_skywrath_mage()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/skywrath_mage/skywrath_mage.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_slardar()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/slardar/slardar.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_slark()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/slark/slark.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_snapfire()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/snapfire/snapfire.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_sniper()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/sniper/sniper.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_spectre()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/spectre/spectre.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/spectre/spectre_arcana/spectre_arcana_base.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_spirit_breaker()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/spirit_breaker/spirit_breaker.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_storm_spirit()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/storm_spirit/storm_spirit.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_sven()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/sven/sven.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_techies()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/techies/fx_techies_minefield_hammer.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_minefield_pole.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_remote_cart.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_remote_cart_elephant.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_remote_cart_pig.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_remote_cart_rubick.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_remotebomb.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_remotebomb_invis.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_remotebomb_rubick.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techies_remotebomb_underhollow.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techiesfx_mine.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techiesfx_mine_invis.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techiesfx_mines_sign.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techiesfx_stasis.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techiesfx_stasis_invis.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/fx_techiesfx_stasis_sign.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/techies/techies.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_terrorblade()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/terrorblade/terrorblade.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/terrorblade/demon.vmdl_c", IsRegexPattern = false, Comment = "Base model (shared with arcana)" },
      new() { Value = "models/heroes/terrorblade/terrorblade_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_tidehunter()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/tidehunter/tidehunter.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tidehunter/tidehunter_anchor.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tidehunter/tidehunter_anchor_clamp.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tidehunter/tidehunter_chain_link.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tidehunter/tidehuntertentacle.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tidehunter/tidehuntertentacle_rubick.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_tinker()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/tinker/tinker.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tinker/mom.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tinker/momtest.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_tiny()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/tiny/tiny_01/tiny_01.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tiny/tiny_01/tiny_01_empty.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tiny/tiny_01/tiny_01_tree.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tiny/tiny_01/tiny_empty.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tiny/tiny_01/tiny_tree_proj.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tiny/tiny_02/tiny_02.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tiny/tiny_03/tiny_03.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tiny/tiny_04/tiny_04.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tiny/tiny_tree/tiny_tree.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tiny/tiny_tree/tiny_tree_proj.vmdl_c", IsRegexPattern = false, Comment = "Base model" },

      new() { Value = "models/heroes/tiny_01/tiny_01.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tiny_01/tiny_01_tree.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/tiny_01/tiny_tree_proj.vmdl_c", IsRegexPattern = false, Comment = "???" },

      new() { Value = "models/heroes/tiny_02/tiny_02.vmdl_c", IsRegexPattern = false, Comment = "Base model" },

      new() { Value = "models/heroes/tiny_03/tiny_03.vmdl_c", IsRegexPattern = false, Comment = "Base model" },

      new() { Value = "models/heroes/tiny_04/tiny_04.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_treant_protector()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/treant_protector/treant_ground_root.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/treant_protector/treant_growing_vines.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/treant_protector/treant_protector.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_troll_warlord()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/troll_warlord/troll_warlord.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_tuskarr()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/tuskarr/tuskarr.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/tuskarr/tuskarr_sigil.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/items/tuskarr/nexon_glacialshard/nexon_glacialshard.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_twin_headed_dragon()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/twin_headed_dragon/twin_headed_dragon.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_undying()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/undying/undying.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/undying/undying_flesh_golem.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/undying/undying_flesh_golem_rubick.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/undying/undying_minion.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/undying/undying_minion_torso.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/undying/undying_tower.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_ursa()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/ursa/ursa.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_vengeful()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/vengeful/vengeful.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_venomancer()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/venomancer/venomancer.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/venomancer/venomancer_ward.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_viper()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/viper/viper.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_visage()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/visage/visage.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/visage/visage_familiar.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/visage/rubick_visage_familiar.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = @"models\/items\/visage\/.*familiar.*" + Regex.Escape(".vmdl_c"), IsRegexPattern = true, Comment = "Familiars models" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_void_spirit()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/void_spirit/void_spirit.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/void_spirit/void_spirit_remnant.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_warlock()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/warlock/warlock.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/warlock/warlock_demon.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/warlock/warlock_demon_rubick.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_weaver()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/weaver/weaver.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/weaver/weaver_bug.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_windrunner()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/windrunner/windrunner.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/windrunner/wr_galeforce_02_fx.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/windrunner/wr_galeforce_fx.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/windrunner/wr_galeforce_ground_fx.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/windrunner/wr_galeforce_straightforward_fx.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/items/windrunner/windrunner_arcana/wr_arcana_base.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_winterwyvern()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/winterwyvern/winterwyvern.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/winterwyvern/winterwyvern_sfm.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_wisp()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/wisp/wisp.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/wisp/wisp_additive.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/wisp/wisp_effigy.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_witchdoctor()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/witchdoctor/witchdoctor.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/witchdoctor/witchdoctor_ward.vmdl_c", IsRegexPattern = false, Comment = "Base model summon" },
      new() { Value = "models/heroes/witchdoctor/witchdoctor_old.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_wraith_king()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/wraith_king/wraith_frost.vmdl_c", IsRegexPattern = false, Comment = "???" },
      new() { Value = "models/heroes/wraith_king/wraith_king.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/wraith_king/wraith_king_prop.vmdl_c", IsRegexPattern = false, Comment = "???" },

      new() { Value = "models/items/wraith_king/arcana/wraith_king_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      new() { Value = "models/items/wraith_king/arcana/wk_arcana_skeleton.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
      new() { Value = "models/items/wraith_king/arcana/wk_arcana_tombstone.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
      new() { Value = "models/items/wraith_king/arcana/wk_sk_arc_taunt_throne_model.vmdl_c", IsRegexPattern = false, Comment = "Arcana stuff" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_zeus()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/zeus/zeus.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/zeus/zeus_arcana.vmdl_c", IsRegexPattern = false, Comment = "Arcana base model" },
      new() { Value = "models/heroes/zeus/zeus_sigil.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
    };
  }

  public static PlaceholderFileException[] GetDefaultPlaceholderFileException_zuus()
  {
    return new PlaceholderFileException[]
    {
      new() { Value = "models/heroes/zuus/zuus.vmdl_c", IsRegexPattern = false, Comment = "Base model" },
      new() { Value = "models/heroes/zuus/zuus_model.vmdl_c", IsRegexPattern = false, Comment = "???" },
    };
  }
}