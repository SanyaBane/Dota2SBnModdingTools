// See https://aka.ms/new-console-template for more information

using CommonLib;
using ReplaceMissingFilesInsideVsndevts;
using Serilog;

// Console.WriteLine($"Directory.GetCurrentDirectory(): {Directory.GetCurrentDirectory()}");
// Console.WriteLine($"Environment.CurrentDirectory: {Environment.CurrentDirectory}");
// Console.WriteLine($"System.Reflection.Assembly.GetEntryAssembly().Location;: {System.Reflection.Assembly.GetEntryAssembly().Location}");


// string fullPathToPassedVsndevtsFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_pub_nero_replace_legioncommander\\soundevents\\game_sounds_heroes\\game_sounds_legion_commander.vsndevts";
// string fullPathToPassedVsndevtsFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_pub_nero_replace_legioncommander\\soundevents\\voscripts\\game_sounds_vo_legion_commander.vsndevts";
// string fullPathToPassedVsndevtsFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_pub_riderfuc_replace_pudge\\soundevents\\game_sounds_heroes\\game_sounds_pudge.vsndevts";
// string fullPathToPassedVsndevtsFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_pub_saberfuc_replace_juggernaut\\soundevents\\voscripts\\game_sounds_vo_juggernaut.vsndevts";

const string logDirectory = "logs";

string logFile = Path.Combine(logDirectory, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");

var logger = new LoggerConfiguration()
  .WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
  .CreateLogger();


try
{
// #if DEBUG
//   string pathToTestFile = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\content\\dota_addons\\mod_test_shiro\\soundevents\\voscripts\\game_sounds_vo_medusa.vsndevts";
//   args = new string[] { pathToTestFile };
// #else
  if (args.Length == 0)
  {
    Console.WriteLine($"Drag&Drop '.{ConstantsCommon.VSNDEVTS_FORMAT}' file (which is located inside dota addon 'content' directory) onto this executable.");
    Console.WriteLine(Constants.TEXT_PRESS_ENTER_TO_EXIT);
    Console.ReadLine();
    return;
  }
  
  var worker = new Worker();
  worker.DoWork(args);
// #endif
}
catch (Exception ex)
{
  logger.Fatal(ex, "Unhandled Exception");
  Console.WriteLine(ex);
}