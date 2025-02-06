﻿using System.IO;
using System.Windows;
using CommonLib;
using CSharpFunctionalExtensions;
using Microsoft.Win32;

namespace RemoveCosmetics.Settings;

public class SettingsManager
{
  #region Singleton

  private static SettingsManager? _instance;

  public static SettingsManager Instance => _instance ??= new SettingsManager();

  #endregion // Singleton

  private const string CONFIG_FILE_NAME = "RemoveCosmetics.config";

  #region Properties

  public RemoveCosmeticsConfig RemoveCosmeticsConfig { get; private set; } = null!;

  public Dota2GameMainInfo Dota2GameMainInfo { get; private set; } = null!;

  #endregion // Properties

  #region Public Methods

  public Result UpdateDota2GameMainInfo()
  {
    var resultCreateDota2GameMainInfo = Dota2GameMainInfo.CreateDota2GameMainInfo(RemoveCosmeticsConfig.Dota2ExeFullPath);
    if (resultCreateDota2GameMainInfo.IsFailure)
      return resultCreateDota2GameMainInfo;

    Dota2GameMainInfo = resultCreateDota2GameMainInfo.Value;
    return Result.Success();
  }

  public RemoveCosmeticsConfig EnsureConfigFileExists()
  {
    var configFile = LoadConfigFile();
    if (configFile != null)
    {
      RemoveCosmeticsConfig = new RemoveCosmeticsConfig()
      {
        Dota2ExeFullPath = configFile.Dota2ExeFullPath,
        ExportVpkFileSavedDirectoryPath = configFile.OutputDirectoryFullPath,
        HeroesInRightList = configFile.HeroesInRightList.Select(x => x.Value).ToArray(),
      };
    
      return RemoveCosmeticsConfig;
    }

    RemoveCosmeticsConfig = new RemoveCosmeticsConfig();

    var resultSaveConfigFile = TrySaveConfigFile();
    if (resultSaveConfigFile.IsFailure)
    {
      MessageBox.Show(resultSaveConfigFile.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    return RemoveCosmeticsConfig;
  }

  public Result TrySaveConfigFile()
  {
    try
    {
      var fullPathToConfigFile = GetFullPathToConfigFile();
      var xmlConfig = new RemoveCosmeticsConfigXml()
      {
        Dota2ExeFullPath = RemoveCosmeticsConfig.Dota2ExeFullPath,
        OutputDirectoryFullPath = RemoveCosmeticsConfig.ExportVpkFileSavedDirectoryPath,
        HeroesInRightList = RemoveCosmeticsConfig.HeroesInRightList.Select(x => new RemoveCosmeticsConfigXml.HeroInRightList()
        {
          Value = x
        }).ToArray(),
      };
      
      XmlSerializerService.SerializeToXml(fullPathToConfigFile, xmlConfig);

      if (File.Exists(fullPathToConfigFile) is false)
      {
        return Result.Failure($"Was not able to save config file by following path:{Environment.NewLine}" +
                              $"{fullPathToConfigFile}");
      }

      return Result.Success();
    }
    catch (Exception ex)
    {
      return Result.Failure(ex.Message);
    }
  }

  public Result<string?> CallDialogSetDota2ExePath()
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
        return Result.Failure<string?>($"Following file is not found:{Environment.NewLine}" +
                                       $"{dota2Exe.FullName}");
      }

      return Result.Success<string?>(openFileDialog.FileName);
    }

    return Result.Success<string?>(null);
  }

  private string GetFullPathToConfigFile()
  {
    var executableDirectory = GetFullPathToExecutableDirectory();
    var configFileFullPath = Path.Combine(executableDirectory, CONFIG_FILE_NAME);
    return configFileFullPath;
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

  private RemoveCosmeticsConfigXml? LoadConfigFile()
  {
    var configFileFullPath = GetFullPathToConfigFile();
    if (File.Exists(configFileFullPath) is false)
      return null;

    try
    {
      var modExporterGlobalConfig = XmlSerializerService.DeserilazeFromXml<RemoveCosmeticsConfigXml>(configFileFullPath);
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