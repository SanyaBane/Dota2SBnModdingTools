// See https://aka.ms/new-console-template for more information

using CommonLib;
using ReplaceMissingFilesInsideVsndevts;

// Console.WriteLine($"Directory.GetCurrentDirectory(): {Directory.GetCurrentDirectory()}");
// Console.WriteLine($"Environment.CurrentDirectory: {Environment.CurrentDirectory}");
// Console.WriteLine($"System.Reflection.Assembly.GetEntryAssembly().Location;: {System.Reflection.Assembly.GetEntryAssembly().Location}");


// string fullPathToPassedVsndevtsFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_pub_nero_replace_legioncommander\\soundevents\\game_sounds_heroes\\game_sounds_legion_commander.vsndevts";
// string fullPathToPassedVsndevtsFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_pub_nero_replace_legioncommander\\soundevents\\voscripts\\game_sounds_vo_legion_commander.vsndevts";
// string fullPathToPassedVsndevtsFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_pub_riderfuc_replace_pudge\\soundevents\\game_sounds_heroes\\game_sounds_pudge.vsndevts";
// string fullPathToPassedVsndevtsFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_pub_saberfuc_replace_juggernaut\\soundevents\\voscripts\\game_sounds_vo_juggernaut.vsndevts";


if (args.Length != 1)
{
  Console.WriteLine($"Drag&Drop '.{ConstantsCommon.VSNDEVTS_FORMAT}' file (which is located inside dota addon 'content' directory) onto this executable.");
  Console.WriteLine(Constants.TEXT_PRESS_ENTER_TO_EXIT);
  Console.ReadLine();
  return;
}

string fullPathToPassedVsndevtsFile = args[0];

Worker worker =  new Worker();
worker.DoWork(fullPathToPassedVsndevtsFile);