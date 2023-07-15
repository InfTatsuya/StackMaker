using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string LEVEL_KEY = "Level";

    public static LevelManager Instance { get; private set; }

    [SerializeField] List<GameObject> levelList = new List<GameObject>();

    [SerializeField] private int currentLevel;

    private GameObject currentLevelInstance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt(LEVEL_KEY, 0);

        SetActiveLevel(currentLevel);
    }

    private void SetActiveLevel(int level)
    {
        currentLevelInstance = Instantiate(levelList[currentLevel], Vector3.zero, Quaternion.identity);
    }

    public void NextLevel()
    {
        Destroy(currentLevelInstance);

        currentLevel = ++currentLevel % levelList.Count; 
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);

        SetActiveLevel(currentLevel);
    }

    public void ResetLevel()
    {
        if(currentLevelInstance != null)
        {
            Destroy(currentLevelInstance);
        }

        SetActiveLevel(currentLevel);
        Player.Instance.NextLevel();
    }

    public int GetCurrentLevel() => currentLevel;
}
