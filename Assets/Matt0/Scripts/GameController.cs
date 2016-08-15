using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text ScoreLabel;
    public void Update()
    {
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
      //  ScoreLabel.text = count.ToString();
    }
}