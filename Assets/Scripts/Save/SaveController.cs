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
	private List<Level> _levels;

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
		if (FindObjectsOfType<SaveController>().Length > 1)
			Destroy(gameObject);

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

		for (int i = 0; i < _levels.Count; i ++)
		{
			//First level always unlocked
			if (i == 0)
				allSaves.Add(new LevelSave(0, LevelState.UNLOCKED, _levels[i].Name));
			else
				allSaves.Add(new LevelSave(0, LevelState.LOCKED, _levels[i].Name));
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
		int gainedSeeds = 0;

		if (newLivesLost <= 3)
			gainedSeeds = 3;
		else if (newLivesLost <= 10)
			gainedSeeds = 2;
		else if (newLivesLost <= 15)
			gainedSeeds = 1;

		if (SaveFile.Saves[levelIndex].SeedsGained < gainedSeeds)
			SaveFile.Saves[levelIndex] = new LevelSave(gainedSeeds, newState, _levels[levelIndex].Name);

		if(levelIndex + 1 < _levels.Count)
			SaveFile.Saves[levelIndex + 1] = new LevelSave(0, LevelState.UNLOCKED, _levels[levelIndex + 1].Name);

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


	public int FindLevelSaveWithName(string levelName)
	{
		int result = 0;

		for (int i = 0; i < SaveFile.Saves.Count; i ++)
		{
			if (SaveFile.Saves[i].Name == levelName)
			{
				result = i;
				break;
			}
		}

		return result;
	}
}