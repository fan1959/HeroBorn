using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomExtensions;
using System.Linq;

public class GameBehavior : MonoBehaviour, IManager
{
    // Start is called before the first frame update
    public PlayerBehavior playerBehavior;

    public int MaxItems = 4;
    private string _state;
    public string State
    {
        get { return _state; }
        set
        {
            _state = value;
        }
    }

    public TMP_Text HealthText;
    public TMP_Text ItemsText;
    public TMP_Text ProgressText;

    public Button WinButton;
    public Button LossButton;

    private int _itemsCollected = 0;
    private int _playerHP = 10;

    public Stack<Loot> LootStack = new Stack<Loot>();

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemsText.text = "Items Collected:" + Items;
            if (_itemsCollected > MaxItems)
            {
                UpdateScene("You found all the items! You win!");
                WinButton.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = "Item found,only" + (MaxItems - _itemsCollected) + "more!";
            }
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            HealthText.text = "Player Health:" + HP;
            if (_playerHP <= 0)
            {
                UpdateScene("You want another life with that?");
                LossButton.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = "Ouch...that's got hurt";
            }
            Debug.Log($"Lives:{_playerHP}");
        }
    }

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }
    void Start()
    {
        ItemsText.text += _itemsCollected;
        HealthText.text += _playerHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartScene()
    {
        Utilities.RestartLevel();
    }
    public void Initialize()
    {
        _state = "Game Manager initialized";
        _state.FancyDebug();
        //Debug.Log(_state);
        debug(_state);
        LogWithDelegate(debug);
        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost", 1));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));
        FilterLoot();
        var itemShop = new Shop<Collectable>();
        itemShop.AddItem(new Potion());
        itemShop.AddItem(new Antidote());
        Debug.Log("Items for sale:" + itemShop.inventory.Count);
    }
    public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();
        Debug.LogFormat($"You found a {currentItem.name}! You've got a good chance of finding a {nextItem.name} next! ");
        Debug.LogFormat($"There are {LootStack.Count} random loot items waiting for you!");
    }
    public void FilterLoot()
    {
        var rareLoot = LootStack.Where(item => item.rarity >= 3)
                                .OrderBy(item => item.rarity)
                                .Select(item => new { item.name });
        foreach (var item in rareLoot)
        {
            Debug.LogFormat($"Rare item:{item.name}!");
        }
    }
    public static void Print(string newText)
    {
        Debug.Log(newText);
    }
    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }
    void OnEnable()
    {
        GameObject player = GameObject.Find("Player");
        playerBehavior=player.GetComponent<PlayerBehavior>();
        playerBehavior.playerJump += HandlePlayerJump;
    }
    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }
    private void OnDisable()
    {
       playerBehavior.playerJump -= HandlePlayerJump;
        debug("Jump event unsubscribed");
    }
}
