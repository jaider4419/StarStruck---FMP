using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public string winScene;
    public GameObject[] playerPrefabs; // Array of player prefabs
    public GameObject enemyPrefab;
    public string DeathScreen;

    public Animator anim;
    public string Hit;
    public string Idle;

    public Transform[] playerBattleStations; // Array of player battle stations
    public Transform enemyBattleStation;

    Unit[] playerUnits; // Array of player units
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI enemyDialogue;

    public BattleHUD[] playerHUDs; // Array of player HUDs
    public BattleHUD enemyHUD;

    public Button[] player1AttackButtons; // Attack buttons for Player 1
    public Button[] player2AttackButtons; // Attack buttons for Player 2
    public Button[] player3AttackButtons; // Attack buttons for Player 3

    public Button[] player1ReplenishButton;
    public Button[] player2ReplenishButton;
    public Button[] player3ReplenishButton;

    public AudioSource Testing;

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

        dialogueText.text = enemyUnit.unitName + " WANTS YOU TO PAY FOR YOUR CRIMES!";
        enemyDialogue.text = "COUNT YOUR DAYS!";

        yield return new WaitForSeconds(2f);

        enemyDialogue.text = null;

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        // Disable glow effect for all players' HUD frames
        foreach (BattleHUD hud in playerHUDs)
        {
            hud.ToggleGlow(false);
        }

        // Enable glow effect for the current player's HUD frame
        playerHUDs[currentPlayerIndex].ToggleGlow(true);

        anim.Play(Idle);

        // Check if the current player's energy is above 0 before allowing their turn
        if (playerUnits[currentPlayerIndex].currentEnergy > 0)
        {
            // Enable attack and heal buttons for the current player
            switch (currentPlayerIndex)
            {
                case 0:
                    EnableButtons(player1AttackButtons);
                    break;
                case 1:
                    EnableButtons(player2AttackButtons);
                    break;
                case 2:
                    EnableButtons(player3AttackButtons);
                    break;
            }

            dialogueText.text = "WHAT WILL " + playerNames[currentPlayerIndex] + " DO?";
        }
        else
        {
            // Skip the turn if energy is 0
            currentPlayerIndex = (currentPlayerIndex + 1) % playerUnits.Length;
            PlayerTurn();
        }
    }

    void EnableButtons(Button[] buttons)
    {
        foreach (Button button in buttons)
        {
            button.interactable = true; // Enable buttons
        }
        foreach (Button button in healButtons)
        {
            button.interactable = true; // Enable heal buttons
        }
    }

    IEnumerator PlayerAttack(int playerIndex, int damage, int energyCost)
    {
        // Disable attack and heal buttons during the player's turn
        DisableAllButtons();

        // Deduct energy from the player
        playerUnits[playerIndex].DecreaseEnergy(energyCost);

        bool isDead = enemyUnit.TakeDamage(damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerNames[playerIndex] + " ATTACKS " + enemyUnit.unitName;

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

        // Enable attack and heal buttons after player's action is completed
        EnableAllButtons();
    }

    IEnumerator PlayerHeal(int playerIndex)
    {
        // Disable attack and heal buttons during the player's turn
        DisableAllButtons();

        playerUnits[playerIndex].Heal(10);

        playerHUDs[playerIndex].SetHP(playerUnits[playerIndex].currentHP);
        dialogueText.text = playerNames[playerIndex] + " FEELS RENEWED STRENGTH!";

        yield return new WaitForSeconds(2f);

        // Trigger enemy's turn after player heals
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurnAndProceed(playerIndex));
    }

    IEnumerator PlayerReplenish(int playerIndex)
    {
        // Disable attack and heal buttons during the player's turn
        DisableAllButtons();

        playerUnits[playerIndex].Replenish(20); // Replenish with 20 energy

        playerHUDs[playerIndex].SetEnergy(playerUnits[playerIndex].currentEnergy);
        dialogueText.text = playerNames[playerIndex] + " HAS REPLENISHED THEIR ENERGY!";

        yield return new WaitForSeconds(2f);

        // Trigger enemy's turn after player replenishes
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurnAndProceed(playerIndex));
    }

    // Updated EnemyTurnAndProceed Coroutine to accept the playerIndex parameter
    IEnumerator EnemyTurnAndProceed(int playerIndex)
    {
        anim.Play(Idle);
        dialogueText.text = enemyUnit.unitName + " ATTACKS " + playerNames[playerIndex] + "!";

        yield return new WaitForSeconds(1f);

        int baseDamage = Random.Range(minDamage, maxDamage + 1); // Generate base random damage
        int damage = baseDamage;

        int fatigue = 10;

        bool isDead = playerUnits[playerIndex].TakeDamage(damage);
        bool isSleeping = playerUnits[playerIndex].TakeFatigue(fatigue);

        playerHUDs[playerIndex].SetHP(playerUnits[playerIndex].currentHP);
        playerHUDs[playerIndex].SetEnergy(playerUnits[playerIndex].currentEnergy);

        if (isDead)
        {
            dialogueText.text = playerNames[playerIndex] + " HAS NO HEALTH!";
            playerUnits[playerIndex].gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
        }

        if (isSleeping)
        {
            dialogueText.text = playerNames[playerIndex] + " IS LOW ON ENERGY AND IS SLEEPING!";
            playerUnits[playerIndex].gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
        }

        // Check if all players are defeated
        bool allPlayersDefeated = true;
        foreach (Unit playerUnit in playerUnits)
        {
            if (playerUnit.isActiveAndEnabled)
            {
                allPlayersDefeated = false;
                break;
            }
        }

        if (allPlayersDefeated)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            // Proceed to the next player's turn
            currentPlayerIndex = (currentPlayerIndex + 1) % playerUnits.Length;

            // Skip dead players
            while (!playerUnits[currentPlayerIndex].isActiveAndEnabled)
            {
                currentPlayerIndex = (currentPlayerIndex + 1) % playerUnits.Length;
            }

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }




    IEnumerator EnemyTurn()
    {
        anim.Play(Idle);
        dialogueText.text = enemyUnit.unitName + " ATTACKS " + playerNames[currentPlayerIndex] + "!";

        yield return new WaitForSeconds(1f);

        int baseDamage = Random.Range(minDamage, maxDamage + 1); // Generate base random damage
        int damage = baseDamage;

        int fatigue = 10;

        bool isDead = playerUnits[currentPlayerIndex].TakeDamage(damage);
        bool isSleeping = playerUnits[currentPlayerIndex].TakeFatigue(fatigue);

        playerHUDs[currentPlayerIndex].SetHP(playerUnits[currentPlayerIndex].currentHP);
        playerHUDs[currentPlayerIndex].SetEnergy(playerUnits[currentPlayerIndex].currentEnergy);

        if (isDead)
        {
            dialogueText.text = playerNames[currentPlayerIndex] + " IS FRIED!";
            playerUnits[currentPlayerIndex].gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
        }

        if (isSleeping)
        {
            dialogueText.text = playerNames[currentPlayerIndex] + " IS FRIED!";
            playerUnits[currentPlayerIndex].gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
        }

        // Check if all players are defeated
        bool allPlayersDefeated = true;
        foreach (Unit playerUnit in playerUnits)
        {
            if (playerUnit.isActiveAndEnabled)
            {
                allPlayersDefeated = false;
                break;
            }
        }

        if (allPlayersDefeated)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            // Set the next player to be attacked by the boss
            state = BattleState.PLAYERTURN;
            currentPlayerIndex = (currentPlayerIndex + 1) % playerUnits.Length;
            PlayerTurn();
        }
    }



    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "YOU WON THE BATTLE!";
            ReturnToCity();
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "YOU HAVE LOST THE BATTLE.";
            SceneManager.LoadScene(DeathScreen);
        }
    }

    public void ReturnToCity()
    {
        GameObject gameManager = GameObject.Find("GameManager2");
        if (gameManager != null)
        {
            gameManager.GetComponent<GameManager2>().LoadScene(winScene);
        }
    }

    void DisableAllButtons()
    {
        // Disable all attack, heal, and replenish buttons during player's action
        foreach (Button[] buttons in new Button[][] { player1AttackButtons, player2AttackButtons, player3AttackButtons, healButtons, player1ReplenishButton, player2ReplenishButton, player3ReplenishButton })
        {
            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
        }
    }

    void EnableAllButtons()
    {
        // Enable all attack, heal, and replenish buttons after player's action
        foreach (Button[] buttons in new Button[][] { player1AttackButtons, player2AttackButtons, player3AttackButtons, healButtons, player1ReplenishButton, player2ReplenishButton, player3ReplenishButton })
        {
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
    }

    // Button click events for player attacks
    public void OnPlayer1AttackButton1() // Player 1 Attack 1
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 0)
        {
            anim.Play(Hit);
            StartCoroutine(PlayerAttack(0, 10, 5)); // Player 1, Attack 1
        }
    }

    public void OnPlayer1AttackButton2() // Player 1 Attack 2
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 0)
        {
            anim.Play(Hit);
            StartCoroutine(PlayerAttack(0, 15, 10)); // Player 1, Attack 2
        }
    }

    public void OnPlayer2AttackButton1() // Player 2 Attack 1
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 1)
        {
            anim.Play(Hit);
            StartCoroutine(PlayerAttack(1, 10, 5)); // Player 2, Attack 1
        }
    }

    public void OnPlayer2AttackButton2() // Player 2 Attack 2
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 1)
        {
            anim.Play(Hit);
            StartCoroutine(PlayerAttack(1, 15, 10)); // Player 2, Attack 2
        }
    }

    public void OnPlayer3AttackButton1() // Player 3 Attack 1
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 2)
        {
            anim.Play(Hit);
            StartCoroutine(PlayerAttack(2, 15, 5)); // Player 3, Attack 1
        }
    }

    public void OnPlayer3AttackButton2() // Player 3 Attack 2
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 2)
        {
            anim.Play(Hit);
            StartCoroutine(PlayerAttack(2, 12, 10)); // Player 3, Attack 2
        }
    }

    // Button click event for heal
    public void OnHealButton(int playerIndex)
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == playerIndex)
            StartCoroutine(PlayerHeal(playerIndex));
    }

    // Button click event for replenish
    public void OnPlayer1ReplenishButton()
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 0)
            StartCoroutine(PlayerReplenish(0)); // Replenish for Player 1
    }

    public void OnPlayer2ReplenishButton()
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 1)
            StartCoroutine(PlayerReplenish(1)); // Replenish for Player 2
    }

    public void OnPlayer3ReplenishButton()
    {
        if (state == BattleState.PLAYERTURN && currentPlayerIndex == 2)
            StartCoroutine(PlayerReplenish(2)); // Replenish for Player 3
    }

}
