using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] List<GameObject> levelList = new List<GameObject>();

    [SerializeField] private int currentLevel;

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
        currentLevel = 1;

        SetActiveLevel(currentLevel);
    }

    private void SetActiveLevel(int level)
    {
        if (level > levelList.Count) return;

        currentLevel = level;

        for(int i = 0; i < levelList.Count; i++)
        {
            if(i == level - 1)
            {
                levelList[i].SetActive(true);
            }
            else
            {
                levelList[i].SetActive(false);
            }
        }
    }

    public void NextLevel()
    {
        if (currentLevel >= levelList.Count)
        {
            currentLevel = 1;
        }
        else
        {
            currentLevel++;
        }

        SetActiveLevel(currentLevel);
    }

    public int GetCurrentLevel() => currentLevel;
}
