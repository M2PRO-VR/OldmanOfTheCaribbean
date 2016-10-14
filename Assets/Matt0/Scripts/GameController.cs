using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{

    public static int numenemies = 30;
    public UnityEngine.UI.Text ScoreLabel;

    public void Update(){
       //never used: int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
      //  ScoreLabel.text = count.ToString();

        ScoreLabel.text = numenemies.ToString();

        if(numenemies == 0)
            SceneManager.LoadScene("Win");
    }
}