using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceButton : MonoBehaviour
{
    [SerializeField] GameObject[] ballPrefabs;

    public void LaunchBall()
    {
        GameObject selectedBall = ballPrefabs[Random.Range(0, ballPrefabs.Length)];

        Instantiate(selectedBall, new Vector3(transform.position.x + 0.2f, transform.position.y - 0.5f, 0), transform.rotation);
    }
}
