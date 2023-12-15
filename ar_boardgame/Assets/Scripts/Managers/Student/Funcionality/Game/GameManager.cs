using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private UIPlayerManager uiManager;

    [SerializeField] private GameObject[] squares;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 20;
    int index;
    void Start()
    {

    }
    IEnumerator MovePlayer()
    {
        while (transform.position != squares[index].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, squares[index].transform.position, Time.deltaTime * speed);
            yield return null;
        }
        if (index == squares.Length - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

    }
    public void StartMoving()
    {
        StartCoroutine(IterateMovement());
    }
    IEnumerator IterateMovement()
    {
        while (index < squares.Length)
        {
            yield return StartCoroutine(MovePlayer());
        }

    }



}