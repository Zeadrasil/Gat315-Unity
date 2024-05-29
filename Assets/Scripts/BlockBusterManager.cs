using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockBusterManager : MonoBehaviour
{
    private int score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text lastScoreText;
    [SerializeField] IntEvent scoreEvent;
    [SerializeField] VoidEvent steelFireEvent;
    [SerializeField] Image steelReloadImage;
    [SerializeField] VoidEvent woodFireEvent;
    [SerializeField] Image woodReloadImage;
    [SerializeField] VoidEvent rubberFireEvent;
    [SerializeField] Image rubberReloadImage;
    [SerializeField] AudioSource pointAudio;
    private float scoredTimer = 0f;
    private float woodFireTimer = 3f;
    private float steelFireTimer = 3f;
    private float rubberFireTimer = 3f;
    void Start()
    {
        scoreEvent.Subscribe(updateScore);
        rubberFireEvent.Subscribe(rubberFired);
        steelFireEvent.Subscribe(steelFired);
        woodFireEvent.Subscribe(woodFired);
    }

    // Update is called once per frame
    void Update()
    {
        woodFireTimer = Mathf.Min(woodFireTimer + Time.deltaTime, 3);
        steelFireTimer = Mathf.Min(steelFireTimer + Time.deltaTime, 3);
        rubberFireTimer = Mathf.Min(rubberFireTimer + Time.deltaTime, 3);
        scoredTimer = Mathf.Max(scoredTimer - Time.deltaTime, 0);
        steelReloadImage.color = new Color(1 - (steelFireTimer / 3), steelFireTimer / 3, 0);
        woodReloadImage.color = new Color(1 - (woodFireTimer / 3), woodFireTimer / 3, 0);
        rubberReloadImage.color = new Color(1 - (rubberFireTimer / 3), rubberFireTimer / 3, 0);
        lastScoreText.color = new Color(1, 1, 1, scoredTimer / 5);
    }
    void updateScore(int newPoints)
    {
        int streak = newPoints + (scoredTimer > 0 ? int.Parse(lastScoreText.text.Substring(9)) : 0);
        lastScoreText.text = "Streak: +" + streak.ToString();
        if(newPoints > 0)
        {
            scoredTimer = 5;
            pointAudio?.Play();
        }
        score += newPoints;
        scoreText.text = score.ToString();
    }
    void woodFired()
    {
        scoreEvent.RaiseEvent(-17);
        woodFireTimer = 0;
    }
    void steelFired()
    {
        scoreEvent.RaiseEvent(-20);
        steelFireTimer = 0;
    }
    void rubberFired()
    {
        scoreEvent.RaiseEvent(-14);
        rubberFireTimer = 0;
    }
}
