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

    public Button[] attackButtons; // Array of attack buttons
    public Button[] healButtons; // Array of heal buttons

    public BattleState state;

    private int currentPlayerIndex = 0; // Index of the current player

    // Names of players
    public string[] playerNames;

    // Damage range for random attacks
    public int minDamage = 5;
    public int maxDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
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

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(int playerIndex)
    {
        int damage = Random.Range(minDamage, maxDamage + 1); // Generate random damage
        bool isDead = enemyUnit.TakeDamage(damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerNames[playerIndex] + " attacks for " + damage + " damage!";

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
        dialogueText.text = enemyUnit.unitName + " attacks " + playerNames[currentPlayerIndex] + "!";

        yield return new WaitForSeconds(1f);

        int damage = Random.Range(minDamage, maxDamage + 1); // Generate random damage
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
            currentPlayerIndex = (currentPlayerIndex + 1) % playerUnits.Length; // Rotate to the next player
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = playerNames[currentPlayerIndex] + ", choose an action:";
        ActivateButtons(); // Activate buttons for the current player
    }

    void ActivateButtons()
    {
        // Enable attack and heal buttons for the current player
        attackButtons[currentPlayerIndex].interactable = true;
        healButtons[currentPlayerIndex].interactable = true;
    }

    void DeactivateButtons()
    {
        // Disable all attack and heal buttons
        foreach (Button button in attackButtons)
            button.interactable = false;

        foreach (Button button in healButtons)
            button.interactable = false;
    }

    IEnumerator PlayerHeal(int playerIndex)
    {
        playerUnits[playerIndex].Heal(5);

        playerHUDs[playerIndex].SetHP(playerUnits[playerIndex].currentHP);
        dialogueText.text = playerNames[playerIndex] + " feels renewed strength!";

        yield return new WaitForSeconds(2f);

        currentPlayerIndex = (currentPlayerIndex + 1) % playerUnits.Length; // Rotate to the next player
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton(int playerIndex)
    {
        if (state != BattleState.PLAYERTURN || playerIndex != currentPlayerIndex)
            return;

        DeactivateButtons(); // Deactivate buttons after pressing
        StartCoroutine(PlayerAttack(playerIndex));
    }

    public void OnHealButton(int playerIndex)
    {
        if (state != BattleState.PLAYERTURN || playerIndex != currentPlayerIndex)
            return;

        DeactivateButtons(); // Deactivate buttons after pressing
        StartCoroutine(PlayerHeal(playerIndex));
    }
}
