using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/*
 * Class used to controls every save related object
 */
public class SaveController : MonoBehaviour
{
	//Number of level in game (can be dynamic)
	[SerializeField]
	private int _numberOfLevel;

	//The current save state
	public SaveFile SaveFile { get; private set; }

	//A Binary Formatter for data handling
	private BinaryFormatter _binaryFormatter;

	//Path of save file
	private string _gameSavePath;



	//Start method, called after Awake
	private void Awake()
	{
		_gameSavePath = Application.persistentDataPath + "/player.dat";
		_binaryFormatter = new BinaryFormatter();

		RecoverSave();
	}


	//Method used to read save file
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


	//Method used to create a brand new save if no other were found
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


	//Method used to save music and sound level when changed
	public void SaveMusicLevel(float newMusicLevel, float newSoundLevel)
	{
		SaveFile.Music = newMusicLevel;
		SaveFile.Sound = newSoundLevel;

		SaveData();
	}


	//Method used to save level data when changed
	public void SaveLevelData(int levelIndex, int newLivesLost, LevelState newState)
	{
		SaveFile.Saves[levelIndex] = new LevelSave(newLivesLost, newState);

		if(levelIndex + 1 < _numberOfLevel)
			SaveFile.Saves[levelIndex+1] = new LevelSave(0, LevelState.UNLOCKED);

		SaveData();
	}


	//Method used to save current save data into a file
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


	//Method used to reset data when reset data button is pressed
	public void ResetData()
	{
		if (File.Exists(_gameSavePath))
			File.Delete(_gameSavePath);

		CreateSave();
	}
}