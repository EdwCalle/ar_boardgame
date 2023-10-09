using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Models
{


    public class CountdownTimer : MonoBehaviour
    {
        public float totalTime = 60.0f; // Total time for the countdown in seconds
        private float currentTime;
        public TextMeshProUGUI timerText; // Reference to a UI Text component to display the timer
        public bool isCountingDown = false; // Start the countdown when true
        public GameObject countdownFinishedObject; // GameObject to activate when the countdown finishes

        public bool showResults; //encapusule this method

        TextMeshPro timerText2;
        private void Start()
        {
            currentTime = totalTime;

        }

        private void Update()
        {
            if (isCountingDown)
            {
                currentTime -= Time.deltaTime;

                // Update the UI text to display the countdown
                if (timerText != null)
                {
                    timerText.text = currentTime.ToString("0");
                }

                // Check if the countdown has finished
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    isCountingDown = false;
                    showResults = true;

                    // Trigger an event or do something when the countdown finishes
                    if (countdownFinishedObject != null)
                    {
                        countdownFinishedObject.SetActive(true);
                    }
                }
            }
        }

        // Start the countdown
        public void StartCountdown()
        {
            isCountingDown = true;
        }
        public void GoToMinigame(string codeMinigame)
        {
            Debug.Log("go to minigame:" + codeMinigame);
            SceneManager.LoadScene(codeMinigame);
        }
    }
}