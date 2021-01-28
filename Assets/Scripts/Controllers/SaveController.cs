using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Class used to handles saves.
/// </summary>
public class SaveController : MonoBehaviour
{
	[SerializeField]
	/// <summary>
	/// All levels.
	/// </summary>
	private List<LevelData> _levels;
	public List<LevelData> Levels { get => _levels; }


	/// <summary>
	/// Loaded level index.
	/// </summary>
	public int LevelIndex { get; set; }

	/// <summary>
	/// Type of loaded level.
	/// </summary>
	public LevelType LoadedLevel { get; set; }

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
		Application.targetFrameRate = 60;

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

		for (int i = 0; i < Levels.Count; i ++)
			allSaves.Add(new LevelSave(0, i == 0 ? LevelState.UNLOCKED : LevelState.LOCKED));

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
	/// <param name="newLivesLost">The number of lives lost</param>
	public void SaveLevelData(int newLivesLost)
	{
		LevelSave buffer = SaveFile.Saves[LevelIndex];

		if (LoadedLevel == LevelType.CLASSIC)
		{
			int gainedSeeds = 0;

			if (newLivesLost <= 3)
				gainedSeeds = 3;
			else if (newLivesLost <= 10)
				gainedSeeds = 2;
			else if (newLivesLost <= 15)
				gainedSeeds = 1;

			buffer.State = LevelState.COMPLETED;

			if (buffer.SeedsGained < gainedSeeds)
				buffer.SeedsGained = gainedSeeds;

			if (LevelIndex + 1 < Levels.Count && SaveFile.Saves[LevelIndex + 1].State == LevelState.LOCKED)
				SaveFile.Saves[LevelIndex + 1] = new LevelSave(0, LevelState.UNLOCKED);
		}
		else if (LoadedLevel == LevelType.SIDE)
			buffer.State = LevelState.SIDED;
		else if (LoadedLevel == LevelType.CHALLENGE)
			buffer.State = LevelState.CHALLENGED;

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