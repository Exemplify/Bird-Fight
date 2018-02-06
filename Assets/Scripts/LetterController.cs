using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{
    public GameObject LetterPrefab;
    public Vector3 StartPosition;
    private GameObject LetterCurrent;

    void Start()
    {
        LetterSpawn();
    }

    public void LetterRespawn()
    {
        LetterCurrent.SetActive(false);
        LetterCurrent.transform.position = StartPosition;
        LetterCurrent.transform.rotation = Quaternion.identity;
        LetterCurrent.SetActive(true);
    }

    public void LetterColour(Color newColour)
    {
        LetterCurrent.GetComponent<Renderer>().material.color = newColour;
    }

    public void LetterSpawn()
    {
        LetterCurrent = Instantiate(LetterPrefab, StartPosition, Quaternion.identity);
        LetterCurrent.GetComponent<Renderer>().material.color = Color.white;
    }
}
