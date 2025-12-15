using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    bool tutorialActive = true;

    void Start()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!tutorialActive)
            return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            CloseTutorial();
        }
    }

    void CloseTutorial()
    {
        tutorialActive = false;
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
