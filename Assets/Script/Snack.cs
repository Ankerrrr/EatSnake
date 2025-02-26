using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snack : MonoBehaviour
{
    public float gameSpeed;
    public Apple apple;
    public GameSound gameSound;
    public Transform bodyFile;
    public GameUI gameui;
    public GameObject startMenu;

    Vector3 nowDirection;
    Vector3 nextDirection;
    List<Transform> bodies = new List<Transform>();


    void Start()
    {
        Time.timeScale = gameSpeed;
        bodies.Add(transform);
        ResetScence();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W)
                || Input.GetKeyDown(KeyCode.UpArrow))
                && nowDirection != Vector3.down)
        {
            nextDirection = Vector3.up;
        }
        else if ((Input.GetKeyDown(KeyCode.S)
         || Input.GetKeyDown(KeyCode.DownArrow))
        && nowDirection != Vector3.up)
        {
            nextDirection = Vector3.down;
        }
        else if ((Input.GetKeyDown(KeyCode.A)
         || Input.GetKeyDown(KeyCode.LeftArrow))
        && nowDirection != Vector3.right)
        {
            nextDirection = Vector3.left;
        }
        else if ((Input.GetKeyDown(KeyCode.D)
         || Input.GetKeyDown(KeyCode.RightArrow))
        && nowDirection != Vector3.left)
        {
            nextDirection = Vector3.right;
        }

        if (startMenu.activeInHierarchy)
        {
            nowDirection = Vector3.zero;
            nextDirection = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        nowDirection = nextDirection;
        for (int i = bodies.Count - 1; i > 0; i--)
        {
            bodies[i].position = bodies[i - 1].position;
        }

        transform.Translate(nowDirection);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle"))
        {
            CaculatorPage(0);
            loose();
        }
        else if (collision.CompareTag("tail"))
        {
            CaculatorPage(1);
            gameSound.PlaytouchTailSound();
            loose();
        }
        else if (collision.CompareTag("food"))
        {
            gameSound.PlayEatSound();
            apple.GenerateApple();
            addBody();
            gameui.AddScore();

            gameSound.PlayBG();
            gameSound.BGraisePitch();
        }
    }

    void CaculatorPage(int dieWay)
    {
        gameui.ShowCaculatorPage(dieWay);
    }

    public void stopSnack()
    {
        nowDirection = Vector3.zero;
        Time.timeScale = 0;

    }

    public void ResetScence()
    {
        Time.timeScale = gameSpeed;

        nowDirection = Vector3.zero;
        transform.position = Vector3.zero;

        apple.GenerateApple();

        for (int i = 1; i < bodies.Count; i++)
        {
            Destroy(bodies[i].gameObject);
        }
        bodies.Clear();
        bodies.Add(transform);
        gameui.ResetScore();
    }

    void addBody()
    {
        bodies.Add(Instantiate(bodyFile, transform.position, Quaternion.identity));
    }

    void loose()
    {
        gameSound.PlayLooseMusic();
        gameSound.BGMStop();
    }
}
