using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;
    public string intro_text, final_text;
    public GameObject ball_prefab, introText, finalText;

    protected override void Awake()
    {
        base.Awake();

        InitializeGame();
    }

    private void Start()
    {
        instance = this;
    }

    void InitializeGame()
    {
        introText.SetActive(true);
        finalText.SetActive(false);
        introText.GetComponent<Text>().text = intro_text;
        finalText.GetComponent<Text>().text = final_text;
    }

    public void CheckWin()
    {
        GameObject[] bowlcolliders = GameObject.FindGameObjectsWithTag("BowlCollider");

        foreach(GameObject bowlcollider in bowlcolliders)
        {
            if (!bowlcollider.GetComponent<bowl>().isActive)
                return;
        }
        introText.SetActive(false);
        finalText.SetActive(true);
    }


}
