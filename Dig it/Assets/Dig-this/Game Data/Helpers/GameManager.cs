using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int balls_per_stamp = 10, balls_needed = 20, stamps_count = 4, obstacles_count = 4;
    public string intro_text, final_text;
    public GameObject ball_prefab, ballText, introText, finalText;
    int balls_in = 0;

    protected override void Awake()
    {
        base.Awake();

        InitializeGame();
    }

    private void Start()
    {

    }

    void InitializeGame()
    {
        //Customisation
        introText.SetActive(true);
        finalText.SetActive(false);
        introText.GetComponent<Text>().text = intro_text;
        finalText.GetComponent<Text>().text = final_text;

        if (balls_per_stamp == 0)
            balls_per_stamp = 10;
        if (balls_needed == 0 || balls_needed > balls_per_stamp)
            balls_needed = balls_per_stamp;
        displayCount();

        //Désactivation des obstacles
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
            obstacle.SetActive(false);
        //Activation des obstacles demandés
        int count = 0;
        if (obstacles_count > 0)
        {
            obstacles_count = Mathf.Clamp(obstacles_count, 0, 4);
            foreach (GameObject obstacle in obstacles)
            {
                obstacle.SetActive(true);
                if (count == obstacles_count - 1)
                    break;
                count++;
            }
        }

        //Désactivation des Start Stamp
        GameObject[] start_stamps = GameObject.FindGameObjectsWithTag("Start Stamp");
        foreach (GameObject stamp in start_stamps)
            stamp.SetActive(false);
        //Activation des Start Stamp demandés
        count = 0;
        stamps_count = Mathf.Clamp(stamps_count, 1, 4);
        foreach (GameObject start_stamp in start_stamps)
        {
            start_stamp.SetActive(true);
            //Certainement inutile
            foreach (Transform child in start_stamp.transform)
                Destroy(child.gameObject);

            Vector3 pos = start_stamp.transform.position;
            for (int i = 0; i < balls_per_stamp; i++)
            {
                GameObject new_ball = GameObject.Instantiate(ball_prefab, start_stamp.transform);
                pos.x = Random.Range(-0.4f, 0.4f);
                pos.y = Random.Range(-0.4f, 0.4f);
                new_ball.transform.localPosition = pos;
            }

            if (count == stamps_count - 1)
                break;
            count++;
        }
    }

    public void displayCount()
    {
        ballText.GetComponent<Text>().text = balls_in.ToString() + "/" + balls_needed.ToString();
    }

    public void ballIn()
    {
        balls_in += 1;
        displayCount();

        //Win
        if (balls_in >= balls_needed)
        {
            introText.SetActive(false);
            finalText.SetActive(true);
        }
    }

}
