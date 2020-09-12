using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveController : MonoBehaviour
{
	[SerializeField]
	private int _numberOfLevel;


	private SaveFile _saveFile;

	private BinaryFormatter _binaryFormatter;
	private string _gameSavePath;



	private void Start()
    {
		_gameSavePath = Application.persistentDataPath + "/player.dat";

		DontDestroyOnLoad(gameObject);

		_binaryFormatter = new BinaryFormatter();

		RecoverSave();
	}


	private void CreateSave()
	{
		List<LevelSave> allSaves = new List<LevelSave>();

		for (int i = 0; i < _numberOfLevel; i ++)
		{
			if (i == 0)
				allSaves.Add(new LevelSave(0, false, false, false, true));
			else
				allSaves.Add(new LevelSave(0, false, false, false, false));
		}

		_saveFile = new SaveFile(allSaves, 1, 1);

		SaveData();
	}


	private void SaveMusicLevel(float newMusicLevel, float newSoundLevel)
	{
		_saveFile.UpdateSoundAndMusicSave(newMusicLevel, newSoundLevel);

		SaveData();
	}


	public void SaveLevelData(int levelIndex, int newLivesLost, bool isCompleted, bool sideLevel, bool challengeLevel, bool isUnlocked)
	{
		_saveFile.UpdateLevelSave(levelIndex, new LevelSave(newLivesLost, isCompleted, sideLevel, challengeLevel, isUnlocked));

		SaveData();
	}


	private void SaveData()
	{
		FileStream file = File.Create(_gameSavePath);

		_binaryFormatter.Serialize(file, _saveFile);
		file.Close();
	}


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


	private void ResetData()
	{
		if (File.Exists(_gameSavePath))
		{
			File.Delete(_gameSavePath);
			CreateSave();
		}
		else
			CreateSave();
	}
}