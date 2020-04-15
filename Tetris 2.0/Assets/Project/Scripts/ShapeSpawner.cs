using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    public GameObject[] shapes;
    public GameObject[] nextshapes;
    GameObject nextShape;
    public Transform nextShapePosition;

    private int nextIndex;
    private int currentIndex;
  
    void Start()
    {
        
        StartCoroutine(StartSpawning());
    }

    // Spawn a random shape from the shapes array
    public void SpawnShape()
    {
        currentIndex = nextIndex;
        Destroy(nextShape);

        nextIndex = Random.Range(0, 7);
        nextShape = Instantiate(nextshapes[nextIndex], nextShapePosition.position, Quaternion.identity);
        nextShape.transform.SetParent(nextShapePosition);

        Instantiate(shapes[currentIndex], transform.position, Quaternion.identity);
        
    }

    IEnumerator StartSpawning()
    {
        nextIndex = Random.Range(0, 7);
        nextShape = Instantiate(nextshapes[nextIndex], nextShapePosition.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        SpawnShape();
    }

}
