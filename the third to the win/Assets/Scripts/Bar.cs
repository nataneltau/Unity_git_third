using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bar : MonoBehaviour
{
    //keep the variables name more general like the script name
    //cause can be also for health bar, mana bar and so on 

    private float max_bar_value;
    private GameObject character;
    private Characters character_script;
    [SerializeField]
    private float curr_bar_value;

    [SerializeField]
    private RectTransform bottom_bar;
    [SerializeField]
    private RectTransform top_bar;
    private float full_bar_width_size;

    private void Awake()
    {
        character = transform.parent.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        full_bar_width_size = top_bar.rect.width;//could use also bottom_bar, we updated only the width of the bars
        if (character.TryGetComponent(out Characters charac))
        {
            character_script = charac;
            max_bar_value = character_script.Health;
            curr_bar_value = character_script.Health;    
        }
        else
        {
            Debug.Log("It's not a character!!!!!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(character_script.Health != curr_bar_value)
        {
            ChangeBar(character_script.Health);
        }
    }

    private void ChangeBar(float value)
    {
        curr_bar_value = Mathf.Clamp(value, 0, max_bar_value);
    }

    private float GetBarUpdatedWidth()
    {
        return (curr_bar_value / max_bar_value) * full_bar_width_size;//should exist 0 <= (curr_bar_value / max_bar_value) <= 1
    }

}//end of class Bar
