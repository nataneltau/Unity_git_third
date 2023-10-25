using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    public GameObject textcredtics;
    public GameObject Mainmenu;
    public GameObject Creditss;
    public GameObject BackCredits;
    public bool quitcredit;
   

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (quitcredit)
            {
                quittextcredits();

            }
            

        }
        
        
    }

    public void quittextcredits()
    {
        textcredtics.SetActive(false);
        Mainmenu.SetActive(true);
        Creditss.SetActive(true);
        BackCredits.SetActive(false);

    }
    public void Changequitcreditcs(bool value)
    {
        quitcredit = value;
    }
}
