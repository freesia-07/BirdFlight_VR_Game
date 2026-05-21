using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startPanel;
    public TextMeshProUGUI startText;
    public GameManager gameManager;

    private bool gameStarted = false;
    private bool waitingForInfoDismiss = false;

    void Start()
    {
        if (startPanel != null) startPanel.SetActive(true);

        if (startText != null)
            startText.text =
                "Red Kite\nNest Builder\n\n" +
                "<size=75%>You are a Red Kite — one of the\n" +
                "most spectacular birds of prey.\n\n" +
                "Collect 5 twigs scattered across the forest\n" +
                "and build your nest high in the trees.\n\n" +
                "<i>Red Kites are known for their forked tail,\n" +
                "reddish-brown plumage, and impressive\n" +
                "2 metre wingspan.</i></size>\n\n" +
                "Press ENTER to begin";

        Time.timeScale = 0f;
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        if (!kb.enterKey.wasPressedThisFrame && !kb.spaceKey.wasPressedThisFrame)
            return;

        // Phase 1 — dismiss start screen and begin game
        if (!gameStarted)
        {
            gameStarted = true;
            Time.timeScale = 1f;

            if (startPanel != null) startPanel.SetActive(false);
            if (gameManager != null) gameManager.StartGame();
            return;
        }

        // Phase 2 — dismiss info panel after collecting all twigs
        if (gameManager != null &&
            gameManager.infoPanel != null &&
            gameManager.infoPanel.activeSelf)
        {
            gameManager.DismissInfoPanel();
        }
    }
}