using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    public AudioClip winSound;
    public AudioClip hitSound;

    public int PointValue;

    private MainManager mainManager;
    private AudioSource audio;

    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        audio = GetComponent<AudioSource>();
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 1 :
                block.SetColor("_BaseColor", Color.green);
                break;
            case 2:
                block.SetColor("_BaseColor", Color.yellow);
                break;
            case 5:
                block.SetColor("_BaseColor", Color.blue);
                break;
            default:
                block.SetColor("_BaseColor", Color.red);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {

        //check for win condition
        mainManager.brickCount--;
        if (mainManager.brickCount == 0)
        {
            Destroy(other.gameObject);  //destroy the ball
            //play win sound
            audio.PlayOneShot(winSound);
            Destroy(gameObject, 0.6f);
            mainManager.GameOver();
        }
        else
        {
            //play sound hit sound
            audio.PlayOneShot(hitSound);
            Destroy(gameObject, 0.2f);
        }

        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
    }
}
