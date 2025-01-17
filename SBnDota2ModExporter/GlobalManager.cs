﻿using System.IO;
using System.Windows;
using CommonLib;
using CSharpFunctionalExtensions;
using Microsoft.Win32;
using SBnDota2ModExporter.Configs.Main;

namespace SBnDota2ModExporter;

public class GlobalManager
{
  #region Singleton

  private static GlobalManager? _instance;

  public static GlobalManager Instance => _instance ??= new GlobalManager();

  #endregion // Singleton

  #region Fields


  #endregion // Fields

  #region Properties

  public GlobalSettings GlobalSettings { get; } = new();
  
  public SBnModExporterGlobalConfig ModExporterGlobalConfig { get; private set; }

  public Dota2GameMainInfo Dota2GameMainInfo { get; private set; }

  #endregion // Properties

  #region Public Methods

  public Result UpdateDota2GameMainInfo()
  {
    var resultCreateDota2GameMainInfo = Dota2GameMainInfo.CreateDota2GameMainInfo(GlobalSettings.Dota2ExeFullPath);
    if (resultCreateDota2GameMainInfo.IsFailure)
      return resultCreateDota2GameMainInfo;

    Dota2GameMainInfo = resultCreateDota2GameMainInfo.Value;
    return Result.Success();
  }

  public SBnModExporterGlobalConfig LoadOrCreateConfigFile()
  {
    var modExporterGlobalConfig = LoadConfigFile();
    if (modExporterGlobalConfig != null)
    {
      ModExporterGlobalConfig = modExporterGlobalConfig;
      return ModExporterGlobalConfig;
    }

    ModExporterGlobalConfig = new SBnModExporterGlobalConfig();

    var resultSaveConfigFile = TrySaveConfigFile();
    if (resultSaveConfigFile.IsFailure)
    {
      MessageBox.Show(resultSaveConfigFile.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    return ModExporterGlobalConfig;
  }

  public Result TrySaveConfigFile()
  {
    return ModExporterGlobalConfig.TrySaveConfigFile();
  }

  public Result<string> CallDialogSetDota2ExePath()
  {
    var openFileDialog = new OpenFileDialog
    {
      Title = "Select 'dota2.exe' file",
      Filter = "dota2.exe|dota2.exe"
    };

    if (openFileDialog.ShowDialog() == true)
    {
      var dota2Exe = new FileInfo(openFileDialog.FileName);
      if (dota2Exe.Exists is false)
      {
        return Result.Failure<string>($"Following file is not found:{Environment.NewLine}" + 
                                  $"{dota2Exe.FullName}");
      }

      return Result.Success(openFileDialog.FileName);
    }

    return Result.Failure<string>(null);
  }

  #endregion // Public Methods

  #region Private Methods

  private static string GetFullPathToExecutableDirectory()
  {
    var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
    var entryAssemblyLocation = new FileInfo(entryAssembly.Location);

    var executableDirectory = entryAssemblyLocation.Directory.FullName;
    return executableDirectory;
  }

  public string GetFullPathToConfigFile()
  {
    var executableDirectory = GetFullPathToExecutableDirectory();
    var configFileFullPath = Path.Combine(executableDirectory, Constants.CONFIG_FILE_NAME);
    return configFileFullPath;
  }

  private SBnModExporterGlobalConfig? LoadConfigFile()
  {
    var configFileFullPath = GetFullPathToConfigFile();
    if (File.Exists(configFileFullPath) is false)
      return null;

    try
    {
      var modExporterGlobalConfig = XmlSerializerService.DeserilazeFromXml<SBnModExporterGlobalConfig>(configFileFullPath);
      return modExporterGlobalConfig;
    }
    catch (Exception ex)
    {
      MessageBox.Show($"Was not able to load config file:{Environment.NewLine}" +
                      $"{ex.Message}{Environment.NewLine}" +
                      $"{Environment.NewLine}" +
                      $"Config file will be recreated.",
        "Error", MessageBoxButton.OK, MessageBoxImage.Error);

      return null;
    }
  }

  #endregion // Private Methods
}