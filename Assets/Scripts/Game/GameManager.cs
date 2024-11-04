using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int money { private set; get; } = 0;
    public int currentDay { private set; get; } = 1;
    public float timeOfDay { private set; get; } = 0.0f;
    public enum GameState { Playing, Paused, GameOver }
    public GameState currentState = GameState.Playing;
    public enum TimeOfDay { Day, Night }
    public TimeOfDay currentTimeOfDay = TimeOfDay.Night;
    public GameObject UITimeOfDay;
    public GameObject audioManager;
    public AudioManager audioScript;
    public GameObject player;
    public PlayerIUiOpener playerIUiOpener;
    public BuyMenuScript buyMenuScript;
    public BankBalanceScript bankBalanceScript;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep SceneManager between scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        audioManager = GameObject.Find("AudioManager");
        audioScript = audioManager.GetComponent<AudioManager>();
        player = GameObject.Find("Player");
        playerIUiOpener = player.GetComponent<PlayerIUiOpener>();
       
    }

    void Start()
    {
        currentState = GameState.Playing;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;
    }

    public void AdvanceDay()
    {
        SceneManager.instance.FadeSceneWithoutLoad(true);
        currentTimeOfDay = TimeOfDay.Night;
        currentDay++;
        UITimeOfDay.GetComponent<Text>().text = currentDay.ToString();
    }

    public void GameOver()
    {
        playerIUiOpener.lobby = true;
        audioScript.StopMusic();
        buyMenuScript.shopReset = true;
        buyMenuScript.money = 0;
        bankBalanceScript.UpdateBank(0);
        currentState = GameState.GameOver;
        currentTimeOfDay = TimeOfDay.Night;
        playerIUiOpener.hasExited = false;
        SceneManager.instance.LoadSceneWithFade("Lobby");
        InventoryManager.Instance.ClearAllSlots();
        UITimeOfDay.gameObject.SetActive(false);
        Player.instance.HideHealth();
        Player.instance.canMove = true;
        StartCoroutine(WaitAndResetPlayer());
       


    }

    public GameState GetGameState()
    {
        return currentState;
    }

    private IEnumerator WaitAndResetPlayer()
    {
        yield return new WaitForSeconds(1.5f); 

        
        if (Player.instance != null)
        {

            currentState = GameState.Playing;
            timeOfDay = 0.0f;
            currentDay = 1;
            UITimeOfDay.gameObject.SetActive(true);
            UITimeOfDay.GetComponent<Text>().text = currentDay.ToString();
            Player.instance.SetHealth(100);
            money = 0;
            
            Player.instance.ShowHealth();
        }
    }
}
