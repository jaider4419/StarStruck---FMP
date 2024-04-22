using UnityEngine;
using UnityEngine.UI;

public class TurnBasedBattle : MonoBehaviour
{
    public GameObject[] playerCharacters; // Array of player characters
    public GameObject enemy; // Enemy object
    public Slider[] playerHealthBars; // Health bars for player characters
    public Slider enemyHealthBar; // Health bar for the enemy

    private int currentPlayerIndex = 0; // Index of the currently active player character

    void Start()
    {
        // Initialize health bars
        UpdateHealthBars();
    }

    void UpdateHealthBars()
    {
        // Update player health bars
        for (int i = 0; i < playerCharacters.Length; i++)
        {
            playerHealthBars[i].value = playerCharacters[i].GetComponent<CharacterStats>().health;
        }

        // Update enemy health bar
        enemyHealthBar.value = enemy.GetComponent<EnemyStats>().health;
    }

    public void Attack()
    {
        // Perform attack action
        enemy.GetComponent<EnemyStats>().TakeDamage(playerCharacters[currentPlayerIndex].GetComponent<CharacterStats>().attack);
        UpdateHealthBars();
        EndTurn();
    }

    public void Shield()
    {
        // Perform shield action (reducing damage or increasing defense, for example)
        // For simplicity, let's just switch to the next player's turn
        EndTurn();
    }

    void EndTurn()
    {
        // Switch to the next player's turn
        currentPlayerIndex = (currentPlayerIndex + 1) % playerCharacters.Length;
    }
}
