using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausemenu : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool ispasued;
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(ispasued)
            {
                Resumegame();
            }
            else
            {
                Pausegame();
                
                
            }
        }
    }
    public void Pausegame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        ispasued = true;
        Debug.Log("works");
    }
    public void Resumegame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        ispasued = false;
        Debug.Log("works");
    }
}
