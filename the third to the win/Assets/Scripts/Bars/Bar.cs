using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bar : MonoBehaviour
{
    //keep the variables name more general like the script name cause can be also for
    //health bar, mana bar and so on. This script is general
    public const float ANIMATION_SPEED = 8f;
    public const float ANIMATION_THRESHOLD = 0.7f;//the threshold in which the animation stop 

    
    private float max_bar_value;//the maximum bar value
    [SerializeField]
    private Characters character_script;//the character this bar related to, each character is a Characters, contain the current bar value
    [SerializeField]
    private float curr_bar_value;
    [SerializeField]
    private BarValue barValue;

    [SerializeField]
    private RectTransform bottom_bar;
    [SerializeField]
    private RectTransform top_bar;
    private float full_bar_width_size;
    private Coroutine curr_coroutine;//the current coroutine that played

    // Start is called before the first frame update
    void Start()
    {
        full_bar_width_size = top_bar.rect.width;//could use also bottom_bar, we updated only the width of the bars
        max_bar_value = barValue.GetValue(character_script);//the initialized bar value is the maximum bar value
        curr_bar_value = barValue.GetValue(character_script);
    }

    // Update is called once per frame
    void Update()
    {
        if(barValue.GetValue(character_script) != curr_bar_value)//if the bar value wasn't change there isn't a need to update the bar
        {
            ChangeBar(barValue.GetValue(character_script));
        }
    }

    private IEnumerator AdjustBarWidth(float amount)
    {
        //here there also an animation, if increase bar value then the bottom bar update fast and the top bar update slowly
        //if decrease bar value then the top bar update fast and the bottom bar slowly

        RectTransform fastChangeBar, slowChangeBar;
        float desiredWidth, changeVal;
        fastChangeBar = amount >= 0 ? bottom_bar : top_bar;
        slowChangeBar = amount >= 0 ? top_bar : bottom_bar;

        desiredWidth = GetBarUpdatedWidth();//the desired bar width
        SetRectWidth(fastChangeBar, desiredWidth);

        while(Mathf.Abs(desiredWidth - slowChangeBar.rect.width) > ANIMATION_THRESHOLD)
        {//as long as the bar width didn't smaller than the threshold, continue the animation
            changeVal = Mathf.Lerp(slowChangeBar.rect.width, desiredWidth, Time.deltaTime*ANIMATION_SPEED);
            SetRectWidth(slowChangeBar, changeVal);
            yield return null;
        }

        SetRectWidth(slowChangeBar, desiredWidth);
    }

    private void SetRectWidth(RectTransform rec, float width)
    {
        //update RectTransform width 
        rec.sizeDelta = new Vector2(width, rec.rect.height);
    }

    private void ChangeBar(float value)
    {
        //update the current bar to the desired bar
        float prev_bar_value = curr_bar_value;
        curr_bar_value = Mathf.Clamp(value, 0, max_bar_value);
        if(curr_coroutine != null)
        {
            StopCoroutine(curr_coroutine);//if the previous animation didn't finish then finish it
        }
        curr_coroutine = StartCoroutine(AdjustBarWidth(curr_bar_value - prev_bar_value));//positive if heal, negative if damage
    }

    private float GetBarUpdatedWidth()
    {
        //return the new bar width
        return (curr_bar_value / max_bar_value) * full_bar_width_size;//should exist 0 <= (curr_bar_value / max_bar_value) <= 1
    }

    

}//end of class Bar
