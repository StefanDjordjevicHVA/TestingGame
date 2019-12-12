using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public int lives = 3;
    public Text totalScore;

    public RawImage live1, live2, live3;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        if (lives == 2) live3.gameObject.SetActive(false);
        if (lives == 1) live2.gameObject.SetActive(false);

        if (lives == 0)
        {
            live1.gameObject.SetActive(false);
            SceneManager.LoadScene("SampleScene");
        }
    }
}
