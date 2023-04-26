using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }
    public static string COIN_KEY = "Coin";

    [SerializeField] GameObject winPanel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] Button nextLevelButton;

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
        nextLevelButton.onClick.AddListener(NextLevel);

        CloseWinPanel();
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        levelText.text = "LEVEL " + LevelManager.Instance.GetCurrentLevel();

        int coinNumber = PlayerPrefs.GetInt(COIN_KEY, 0);
        coinText.text = coinNumber.ToString();
    }

    public void OpenWinPanel()
    {
        winPanel.SetActive(true);

        Player.Instance.IsControllable = false;
    }

    public void CloseWinPanel()
    {
        winPanel.SetActive(false);

        Player.Instance.IsControllable = true;
    }

    private void NextLevel()
    {
        CloseWinPanel();

        Player.Instance.CoinAmount += 50;
        Player.Instance.NextLevel();

        LevelManager.Instance.NextLevel();

        UpdateLevelText();
    }
}
