using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnBasedBattle : MonoBehaviour
{
    public Slider[] playerHealthSliders; // Health sliders for players
    public Slider bossHealthSlider; // Health slider for the boss

    public Text turnText; // Text to display current turn (player or boss)

    public int[] playerHealth; // Health values for players
    public int bossHealth; // Health value for the boss

    private int currentPlayerIndex; // Index of the current player
    private bool playerTurn = true; // Flag to indicate player's turn

    public Button[] playerAttackButtons; // Buttons for players to attack

    void Start()
    {
        // Initialize player health values
        playerHealth = new int[] { 100, 100, 100 };

        // Set up UI buttons
        for (int i = 0; i < playerAttackButtons.Length; i++)
        {
            int playerIndex = i; // Capture the current value of i
            playerAttackButtons[i].onClick.AddListener(() => PlayerAttack(playerIndex));
        }

        // Set initial turn
        currentPlayerIndex = 0;
        UpdateTurnText();

        // Update health sliders
        UpdateHealthSliders();

        // Start the battle
        StartBattle();
    }

    void UpdateTurnText()
    {
        if (playerTurn)
        {
            turnText.text = "Player " + (currentPlayerIndex + 1) + "'s Turn";
        }
        else
        {
            turnText.text = "Enemy's Turn";
        }
    }

    void UpdateHealthSliders()
    {
        // Update player health sliders
        for (int i = 0; i < playerHealthSliders.Length; i++)
        {
            playerHealthSliders[i].DOValue(playerHealth[i], 0.5f); // Smoothly update the slider value
        }

        // Update boss health slider
        bossHealthSlider.DOValue(bossHealth, 0.5f); // Smoothly update the slider value
    }

    void PlayerAttack(int playerIndex)
    {
        // Perform player's attack based on index
        int damage = Random.Range(10, 20); // Adjust damage as needed

        // Reduce boss's health
        bossHealth -= damage;

        // Check if boss is defeated
        if (bossHealth <= 0)
        {
            bossHealth = 0;
            Debug.Log("Boss defeated!");
            // Add logic to handle victory
        }

        // Update health sliders
        UpdateHealthSliders();

        // Check if all players have taken their turns
        if (currentPlayerIndex == playerHealth.Length - 1)
        {
            // Start boss's turn
            playerTurn = false;
            UpdateTurnText();
            BossTurn();
        }
        else
        {
            // Move to the next player's turn
            currentPlayerIndex++;
            UpdateTurnText();
        }
    }

    void BossTurn()
    {
        // Boss's attack logic
        int damage = Random.Range(15, 25); // Adjust damage as needed

        // Reduce player's health (choosing a random player for simplicity)
        int playerIndex = Random.Range(0, playerHealth.Length);
        playerHealth[playerIndex] -= damage;

        // Check if any player is defeated
        for (int i = 0; i < playerHealth.Length; i++)
        {
            if (playerHealth[i] <= 0)
            {
                playerHealth[i] = 0;
                Debug.Log("Player " + (i + 1) + " defeated!");
                // Add logic to handle player defeat
            }
        }

        // Update health sliders
        UpdateHealthSliders();

        // Start player's turn after boss's turn
        playerTurn = true;
        currentPlayerIndex = 0;
        UpdateTurnText();
    }

    void StartBattle()
    {
        // Start the battle with player's turn
        playerTurn = true;
        currentPlayerIndex = 0;
        UpdateTurnText();
    }
}
