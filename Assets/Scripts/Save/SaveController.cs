using System.Collections;
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
	/// All levels.
	/// </summary>
	[SerializeField]
	private List<LevelData> _levels;
	public List<LevelData> Levels { get => _levels; }

	/// <summary>
	/// Enemy controller component.
	/// </summary>
	[SerializeField]
	private EnemyController _enemyController;


	/// <summary>
	/// Loaded level.
	/// </summary>
	public Level LoadedLevel { get; set; }

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
	/// Does this component is initialized?
	/// </summary>
	public bool Initialized { get; private set; } = false;

	/// <summary>
	/// A list of blank saves.
	/// </summary>
	public List<LevelSave> BlankSave
	{
		get 
		{
			List<LevelSave> allSaves = new List<LevelSave>();

			for (int i = 0; i < Levels.Count; i++)
				allSaves.Add(new LevelSave(0, i == 0 ? LevelState.UNLOCKED : LevelState.LOCKED));
			return allSaves;
		}
	}


	/// <summary>
	/// Awake method, used for initialization.
	/// </summary>
	private IEnumerator Start()
	{
		yield return new WaitUntil(() => Controller.Instance);

		_enemyController = Controller.Instance.EnemyController;
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

				CheckSaveVersionNumber();
			}
			catch
			{
				ResetData();
			}

			if (SaveFile == null || SaveFile.CurrentSave == null || SaveFile.CurrentSave.Count == 0)
				ResetData();
		}
		else
			CreateSave();

		Initialized = true;
	}


	/// <summary>
	/// Method used to check the version number before loading save.
	/// </summary>
	private void CheckSaveVersionNumber()
	{
		if (string.IsNullOrEmpty(SaveFile.VersionNumber))
			ResetData();
		else
		{
			//Version number is saved in the following format: 0.4.1
			string[] savedVersionNumber = SaveFile.VersionNumber.Split('.');
			string[] currentVersionNumber = Application.version.Split('.');

			//If the save is older
			if (int.Parse(currentVersionNumber[0]) > int.Parse(savedVersionNumber[0]) || int.Parse(currentVersionNumber[1]) > int.Parse(savedVersionNumber[1]))
				UpdateOldSave();
		}
	}


	/// <summary>
	/// Method used to update an old save.
	/// </summary>
	private void UpdateOldSave()
	{
		int missingLevels = Levels.Count - SaveFile.CurrentSave.Count;
		int missingEnemies = _enemyController.Enemies.Count - SaveFile.EnemiesUnlocked.Count;
		int missingSquads = Controller.Instance.SquadController.Squads.Count - SaveFile.SquadsProgression.Count;

		SaveFile.UpdateOldSave(missingLevels, missingEnemies, missingSquads, BlankSave);
	}


	/// <summary>
	/// Method used to create a new save if inexistant.
	/// </summary>
	private void CreateSave()
	{
		SaveFile = new SaveFile(Application.version, BlankSave, 1, 1, _enemyController.Enemies.Count, 1);

		SaveData();
	}


	/// <summary>
	/// Method used to save music level.
	/// </summary>
	/// <param name="newMusicLevel">The new music level</param>
	public void SaveMusicLevel(float newMusicLevel)
	{
		SaveFile.Music = newMusicLevel;
		SaveData();
	}


	/// <summary>
	/// Method used to save sound level.
	/// </summary>
	/// <param name="newSoundLevel">The new sound level</param>
	public void SaveSoundLevel(float newSoundLevel)
	{
		SaveFile.Sound = newSoundLevel;
		SaveData();
	}


	/// <summary>
	/// Method used to save music mute.
	/// </summary>
	/// <param name="musicMuted">Does the music is muted?</param>
	public void SaveMusicMute(bool musicMuted)
	{
		SaveFile.MusicMuted = musicMuted;
		SaveData();
	}


	/// <summary>
	/// Method used to save sound mute.
	/// </summary>
	/// <param name="soundMuted">Does the sound is muted?</param>
	public void SaveSoundMute(bool soundMuted)
	{
		SaveFile.SoundMuted = soundMuted;
		SaveData();
	}


	/// <summary>
	/// Method used to save a new enemy found.
	/// </summary>
	/// <param name="enemyIndex">Enemy index</param>
	public void SaveNewEnemyFound(int enemyIndex)
	{
		SaveFile.EnemiesUnlocked[enemyIndex] = true;
		SaveData();
	}


	/// <summary>
	/// Method used to save a new tower level discovered.
	/// </summary>
	/// <param name="newLevel">New tower level</param>
	public void SaveTowerLevel(int newLevel)
	{
		if (newLevel > SaveFile.CurrentSquad.TowerLevelMax)
		{
			SaveFile.CurrentSquad.TowerLevelMax = newLevel;
			SaveData();
		}
	}


	/// <summary>
	/// Method used to save a new tower augmentation level.
	/// </summary>
	/// <param name="index">The augmentation index</param>
	/// <param name="newLevel">New tower augmentation level</param>
	public void SaveTowerAugmentationLevel(int index, int newLevel)
	{
		SaveFile.CurrentSquad.AddNewAugmentation(index, newLevel);
		SaveData();
	}


	/// <summary>
	/// Method used to save a level data.
	/// </summary>
	/// <param name="newLivesLost">The number of lives lost</param>
	public void SaveLevelData(int newLivesLost)
	{
		int levelIndexBuffer = RecoverLevelIndex(LoadedLevel);
		LevelSave buffer = SaveFile.CurrentSave[levelIndexBuffer];

		if (LoadedLevel.Type == LevelType.CLASSIC)
		{
			int gainedSeeds = 0;

			if (newLivesLost <= 3)
				gainedSeeds = 3;
			else if (newLivesLost <= 10)
				gainedSeeds = 2;
			else if (newLivesLost <= 15)
				gainedSeeds = 1;

			if (buffer.State == LevelState.UNLOCKED)
				buffer.State = LevelState.COMPLETED;

			if (buffer.SeedsGained < gainedSeeds)
				buffer.SeedsGained = gainedSeeds;

			if (levelIndexBuffer + 1 < Levels.Count && SaveFile.CurrentSave[levelIndexBuffer + 1].State == LevelState.LOCKED)
				SaveFile.CurrentSave[levelIndexBuffer + 1] = new LevelSave(0, LevelState.UNLOCKED);
		}
		else if (LoadedLevel.Type == LevelType.SIDE && buffer.State == LevelState.COMPLETED)
			buffer.State = LevelState.SIDED;
		else if (LoadedLevel.Type == LevelType.CHALLENGE && buffer.State == LevelState.SIDED)
			buffer.State = LevelState.CHALLENGED;

		SaveData();
	}


	/// <summary>
	/// Method used to recover level index based on level searched.
	/// </summary>
	/// <param name="levelSearched">The searched level</param>
	/// <returns>The level index searched</returns>
	public int RecoverLevelIndex(Level levelSearched)
	{
		int i;
		for (i = 0; i < _levels.Count; i ++)
			if (_levels[i].LevelExists(levelSearched))
				break;

		return i;
	}


	/// <summary>
	/// Method used to recover level save based on level.
	/// </summary>
	/// <param name="levelSearched">The searched level</param>
	/// <returns>The level save wanted</returns>
	public LevelSave RecoverLevelSave(Level levelSearched)
	{
		return SaveFile.CurrentSave[RecoverLevelIndex(levelSearched)];
	}


	/// <summary>
	/// Method used to save data in a file.
	/// </summary>
	private void SaveData()
	{
		try
		{
			Controller.Instance.SquadController.ResetData();
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