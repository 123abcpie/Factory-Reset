using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayHealthMulti : NetworkBehaviour
{
    private TextMeshProUGUI dispHealth;
    private TextMeshProUGUI dispGameOver;
    private GameObject restartButton;
    private MultiPlayerObject playerObject;

    void Start()
    {

        // Only run this for the local player
        if (!IsOwner) return;
        dispHealth = GameObject.Find("Healthbar").GetComponent<TextMeshProUGUI>();
        dispGameOver = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();
        restartButton = GameObject.Find("Restart");

        // Get the PlayerObject attached to this player
        playerObject = GetComponent<MultiPlayerObject>();
        if (playerObject == null)
        {
            Debug.LogError("PlayerObject component not found on local player!");
        }
    }

    void Update()
    {
        if (!IsOwner || playerObject == null) return;

        // Update health display
        dispHealth.text = $"Health: {playerObject.health.Value.ToString()}";

        // Show Game Over if health drops below 1
        if (playerObject.health.Value < 1)
        {
            dispGameOver.enabled = true;
            restartButton.SetActive(true);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
