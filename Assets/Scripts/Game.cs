using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    float gameSpeed = 0.1f;
    int increaseSpeedEverySeconds = 8;
    float nextTimeToIncreaseSpeed = 0;
    float gameSpeedIncrease = 1.3f;
    public GameObject cat;
    public GameObject[] templateBubbles;
    public Text scoreText;
    public Text missedText;
    public GameObject gameOverText;
    public int score = 0;
    public int missed = 0;
    public int missThreshold = 8;
    public float restartAt = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextTimeToIncreaseSpeed = Time.realtimeSinceStartup + increaseSpeedEverySeconds;

        Application.targetFrameRate = 60;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (missed < missThreshold)
        {
            generateBubbles();
            moveBubbles();
            updateTexts();
        } else
        {
            if (restartAt == 0)
            {
                restartAt = Time.realtimeSinceStartup + 6;
            } 
            else
            {
                if (restartAt > Time.realtimeSinceStartup)
                {
                    gameOverText.SetActive(true);
                    gameOverText.GetComponent<Text>().text = "Game Over. Restarting in " + ((int)(restartAt - Time.realtimeSinceStartup));
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
    private void updateTexts()
    {
        scoreText.text = "Score: " + score;
        missedText.text = "Missed: " + missed + "/" + missThreshold;
    }

    private void generateBubbles()
    {
        float maxHeight = 0;
        foreach (GameObject bubble in GameObject.FindGameObjectsWithTag("bubble"))
        {
            if (maxHeight < bubble.transform.position.y)
            {
                maxHeight = bubble.transform.position.y;
            }
        }

        if (maxHeight < 30)
        {
            for (int i = 1; i <= 12; i++)
            {
                if (Random.Range(0, 100) < 1)
                {
                    var bubble = Instantiate(templateBubbles[Random.Range(0, 5)]);
                    var currentScale = bubble.transform.localScale;
                    float randomScaleIncrease = Random.Range(50, 150) / 100f;
                    bubble.transform.localScale = new Vector3(currentScale.x * randomScaleIncrease, currentScale.y * randomScaleIncrease, currentScale.z * randomScaleIncrease);
                    bubble.tag = "bubble";
                    var currentPos = bubble.transform.position;
                    bubble.transform.position = new Vector3(currentPos.x + (6 * i), currentPos.y, currentPos.z);
                }
            }
        }
    }

    private void moveBubbles()
    {
        if (Time.realtimeSinceStartup > nextTimeToIncreaseSpeed)
        {
            gameSpeed = gameSpeed * gameSpeedIncrease;
            nextTimeToIncreaseSpeed += increaseSpeedEverySeconds;
        }

        foreach (GameObject bubble in GameObject.FindGameObjectsWithTag("bubble"))
        {
            Vector3 current = bubble.transform.position;
            bubble.transform.position = new Vector3(current.x, current.y - gameSpeed, current.z);

            if (current.y < -20)
            {
                missed += 1;
                GameObject.DestroyImmediate(bubble);
            }
        }
    }
}
