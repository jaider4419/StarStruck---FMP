using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject[] playerPrefabs; // Array of player prefabs
    public GameObject enemyPrefab;

    public Transform[] playerBattleStations; // Array of player battle stations
    public Transform enemyBattleStation;

    Unit[] playerUnits; // Array of player units
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD[] playerHUDs; // Array of player HUDs
    public BattleHUD enemyHUD;

    public BattleState state;

    private int currentPlayerIndex = 0; // Index of the current player

    // Names of players
    public string[] playerNames;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator SetupBattle()
    {
        playerUnits = new Unit[playerPrefabs.Length]; // Initialize playerUnits array

        // Instantiate player units and set up battle stations
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            GameObject playerGO = Instantiate(playerPrefabs[i], playerBattleStations[i]);
            playerUnits[i] = playerGO.GetComponent<Unit>();
            playerHUDs[i].SetHUD(playerUnits[i]);
        }

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();


        enemyHUD.SetHUD(enemyUnit);

        dialogueText.text = enemyUnit.unitName + " wants you to pay for your crimes!";

        yield return new WaitForSeconds(3);

        dialogueText.text = "now you must attack him.";


        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(int playerIndex)
    {
        int damage = playerUnits[playerIndex].GetRandomDamage(5, 10); // Adjust the damage range as needed
        bool isDead = enemyUnit.TakeDamage(damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerNames[playerIndex] + "'s attack deals " + damage + " damage!";

        yield return new WaitForSeconds(2f);

        if (enemyUnit.currentHP <= 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        int damage = enemyUnit.GetRandomDamage(8, 12); // Adjust the damage range as needed
        dialogueText.text = enemyUnit.unitName + " attacks " + playerNames[currentPlayerIndex];

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnits[currentPlayerIndex].TakeDamage(damage);
        playerHUDs[currentPlayerIndex].SetHP(playerUnits[currentPlayerIndex].currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % playerUnits.Length;
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        bool allPlayersDefeated = true;

        // Check if all players are defeated
        foreach (Unit playerUnit in playerUnits)
        {
            if (playerUnit.currentHP > 0)
            {
                allPlayersDefeated = false;
                break;
            }
        }

        if (allPlayersDefeated)
        {
            state = BattleState.LOST;
            dialogueText.text = "You were defeated.";
            return;
        }

        if (enemyUnit.currentHP <= 0)
        {
            state = BattleState.WON;
            dialogueText.text = "You won the battle!";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = playerNames[currentPlayerIndex] + ", choose an action:";
    }

    IEnumerator PlayerHeal(int playerIndex)
    {
        playerUnits[playerIndex].Heal(5);

        playerHUDs[playerIndex].SetHP(playerUnits[playerIndex].currentHP);
        dialogueText.text = playerNames[playerIndex] + " has regained some energy!";

        yield return new WaitForSeconds(2f);

        currentPlayerIndex = (currentPlayerIndex + 1) % playerUnits.Length; // Rotate to the next player
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton(int playerIndex)
    {
        if (state != BattleState.PLAYERTURN || playerIndex != currentPlayerIndex)
            return;

        StartCoroutine(PlayerAttack(playerIndex));
    }

    public void OnHealButton(int playerIndex)
    {
        if (state != BattleState.PLAYERTURN || playerIndex != currentPlayerIndex)
            return;

        StartCoroutine(PlayerHeal(playerIndex));
    }

}
