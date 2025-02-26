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

    Vector3 direction;
    List<Transform> bodies = new List<Transform>();

    bool waitForMoving = false;


    void Start()
    {
        Time.timeScale = gameSpeed;
        bodies.Add(transform);
        ResetScence();
    }

    void Update()
    {
        if (!waitForMoving)
        {
            if ((Input.GetKeyDown(KeyCode.W)
                    || Input.GetKeyDown(KeyCode.UpArrow))
                    && direction != Vector3.down)
            {
                direction = Vector3.up;
                waitForMoving = true;
            }
            else if ((Input.GetKeyDown(KeyCode.S)
             || Input.GetKeyDown(KeyCode.DownArrow))
            && direction != Vector3.up)
            {
                direction = Vector3.down;
                waitForMoving = true;
            }
            else if ((Input.GetKeyDown(KeyCode.A)
             || Input.GetKeyDown(KeyCode.LeftArrow))
            && direction != Vector3.right)
            {
                direction = Vector3.left;
                waitForMoving = true;
            }
            else if ((Input.GetKeyDown(KeyCode.D)
             || Input.GetKeyDown(KeyCode.RightArrow))
            && direction != Vector3.left)
            {
                direction = Vector3.right;
                waitForMoving = true;
            }
        }
        if (startMenu.activeInHierarchy)
        {
            direction = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        for (int i = bodies.Count - 1; i > 0; i--)
        {
            bodies[i].position = bodies[i - 1].position;
        }

        transform.Translate(direction);

        if (waitForMoving)
        {
            waitForMoving = false;
        }
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
        direction = Vector3.zero;
        Time.timeScale = 0;

    }

    public void ResetScence()
    {
        Time.timeScale = gameSpeed;

        direction = Vector3.zero;
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
