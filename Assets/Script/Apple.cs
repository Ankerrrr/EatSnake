using UnityEngine;

public class Apple : MonoBehaviour
{
    public Collider2D appleArea;

    void Start()
    {

    }

    void Update()
    {

    }


    public void GenerateApple()
    {
        Vector3 newPoisition = new Vector3((int)Random.Range(appleArea.bounds.min.x, appleArea.bounds.max.x),
             (int)Random.Range(appleArea.bounds.min.y, appleArea.bounds.max.y),
             0);
        transform.position = newPoisition;
    }
}
