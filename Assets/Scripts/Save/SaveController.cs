using System.Collections.Generic;
using System.IO;
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
	private SaveFile _saveFile;

	//A Binary Formatter for data handling
	private BinaryFormatter _binaryFormatter;

	//Path of save file
	private string _gameSavePath;



	//Start method, called after Awake
	private void Start()
    {
		_gameSavePath = Application.persistentDataPath + "/player.dat";
		_binaryFormatter = new BinaryFormatter();

		DontDestroyOnLoad(gameObject);

		RecoverSave();
	}



	//Method used to create a brand new save if no other were found
	private void CreateSave()
	{
		List<LevelSave> allSaves = new List<LevelSave>();

		for (int i = 0; i < _numberOfLevel; i ++)
		{
			//First level always unlocked
			if (i == 0)
				allSaves.Add(new LevelSave(0, false, false, false, true));
			else
				allSaves.Add(new LevelSave(0, false, false, false, false));
		}

		_saveFile = new SaveFile(allSaves, 1, 1);

		SaveData();
	}


	//Method used to save music and sound level when changed
	public void SaveMusicLevel(float newMusicLevel, float newSoundLevel)
	{
		_saveFile.UpdateSoundAndMusicSave(newMusicLevel, newSoundLevel);

		SaveData();
	}


	//Method used to save level data when changed
	public void SaveLevelData(int levelIndex, int newLivesLost, bool isCompleted, bool sideLevel, bool challengeLevel, bool unlockNext)
	{
		_saveFile.UpdateLevelSave(levelIndex, new LevelSave(newLivesLost, isCompleted, sideLevel, challengeLevel, true));

		if(unlockNext && levelIndex + 1 < _numberOfLevel)
			_saveFile.UpdateLevelSave(levelIndex + 1, new LevelSave(0, false, false, false, true));

		SaveData();
	}


	//Method used to save current save data into a file
	private void SaveData()
	{
		FileStream file = File.Create(_gameSavePath);

		_binaryFormatter.Serialize(file, _saveFile);
		file.Close();
	}


	//Method used to read save file
    private void RecoverSave()
    {
		if (File.Exists(_gameSavePath))
		{
			FileStream file = File.Open(_gameSavePath, FileMode.Open);
			_saveFile = (SaveFile)_binaryFormatter.Deserialize(file);
			file.Close();
		}
		else
			CreateSave();
	}


	//Method used to reset data when reset data button is pressed
	public void ResetData()
	{
		if (File.Exists(_gameSavePath))
		{
			File.Delete(_gameSavePath);
			CreateSave();
		}
		else
			CreateSave();
	}



	//Getter
	public SaveFile GetSaveFile() { return _saveFile; }
}