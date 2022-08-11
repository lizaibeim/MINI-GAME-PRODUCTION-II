using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public float yOffset;
    bool isQuitting;
    // Start is called before the first frame update


    private void OnDisable()
    {
        if (ScoreManager.Instance == null || isQuitting) return;
        ScoreManager.Instance.score += score;
        GameObject GO = Instantiate(ScoreManager.Instance.scoreTextPrefab, transform.position + Vector3.up * yOffset, Quaternion.identity);
        GO.GetComponent<ScoreText>().SetScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
