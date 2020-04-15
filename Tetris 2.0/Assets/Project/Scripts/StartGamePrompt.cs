using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGamePrompt : MonoBehaviour
{
    void Start()
    {
        enabled = true;
    }

  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }
}
