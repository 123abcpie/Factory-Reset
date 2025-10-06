using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayHealth : MonoBehaviour
{
    public TextMeshProUGUI dispHealth;
    public TextMeshProUGUI dispGameOver;
    public GameObject restartButton;
    private PlayerObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        restartButton.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerObject = player.GetComponent<PlayerObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.health < 1)
        {
            dispGameOver.enabled = true;
            restartButton.SetActive(true);
        }
        if (playerObject != null)
        {
            dispHealth.text = $"Health: {playerObject.health}";
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
