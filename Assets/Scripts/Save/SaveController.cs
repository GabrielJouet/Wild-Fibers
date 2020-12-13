using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Class used to handles saves.
/// </summary>
public class SaveController : MonoBehaviour
{
	/// <summary>
	/// Number of levels in the game.
	/// </summary>
	[SerializeField]
	private int _numberOfLevel;

	/// <summary>
	/// Loaded save file.
	/// </summary>
	public SaveFile SaveFile { get; private set; }

	/// <summary>
	/// Binary formatter used.
	/// </summary>
	private BinaryFormatter _binaryFormatter;

	/// <summary>
	/// Game save path.
	/// </summary>
	private string _gameSavePath;



	/// <summary>
	/// Awake method, used for initialization.
	/// </summary>
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		_gameSavePath = Application.persistentDataPath + "/player.dat";
		_binaryFormatter = new BinaryFormatter();

		RecoverSave();
	}


	/// <summary>
	/// Method used to recover save.
	/// </summary>
	private void RecoverSave()
	{
		if (File.Exists(_gameSavePath))
		{
			try
			{
				FileStream file = File.OpenRead(_gameSavePath);
				SaveFile = (SaveFile)_binaryFormatter.Deserialize(file);
				file.Close();
			}
			catch
			{
				ResetData();
			}

			if (SaveFile == null || SaveFile.Saves == null || SaveFile.Saves.Count == 0)
				ResetData();
		}
		else
			CreateSave();
	}


	/// <summary>
	/// Method used to create a new save if inexistant.
	/// </summary>
	private void CreateSave()
	{
		List<LevelSave> allSaves = new List<LevelSave>();

		for (int i = 0; i < _numberOfLevel; i ++)
		{
			//First level always unlocked
			if (i == 0)
				allSaves.Add(new LevelSave(20, LevelState.UNLOCKED));
			else
				allSaves.Add(new LevelSave(20, LevelState.LOCKED));
		}

		SaveFile = new SaveFile(allSaves, 1, 1);

		SaveData();
	}


	/// <summary>
	/// Method used to save music and sound level.
	/// </summary>
	/// <param name="newMusicLevel">The new music level</param>
	/// <param name="newSoundLevel">The new sound level</param>
	public void SaveMusicLevel(float newMusicLevel, float newSoundLevel)
	{
		SaveFile.Music = newMusicLevel;
		SaveFile.Sound = newSoundLevel;

		SaveData();
	}


	/// <summary>
	/// Method used to save a level data.
	/// </summary>
	/// <param name="levelIndex">The level index related</param>
	/// <param name="newLivesLost">The number of lives lost</param>
	/// <param name="newState">The new level state</param>
	public void SaveLevelData(int levelIndex, int newLivesLost, LevelState newState)
	{
		if(SaveFile.Saves[levelIndex].LivesLost > newLivesLost)
			SaveFile.Saves[levelIndex] = new LevelSave(newLivesLost, newState);

		if(levelIndex + 1 < _numberOfLevel)
			SaveFile.Saves[levelIndex+1] = new LevelSave(0, LevelState.UNLOCKED);

		SaveData();
	}


	/// <summary>
	/// Method used to save data in a file.
	/// </summary>
	private void SaveData()
	{
		try
		{
			FileStream file = File.OpenWrite(_gameSavePath);

			_binaryFormatter.Serialize(file, SaveFile);
			file.Close();
		}
		catch
		{
			//Display error
		}
	}


	/// <summary>
	/// Method used to reset data if needed.
	/// </summary>
	public void ResetData()
	{
		if (File.Exists(_gameSavePath))
			File.Delete(_gameSavePath);

		CreateSave();
	}
}