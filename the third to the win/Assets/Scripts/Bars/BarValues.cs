
public enum BarValue
{
    Health,
    //Mana,
}

static class BarValuesMethods
{
    public static float GetValue(this BarValue val, Characters character_script)
    {
        switch (val)
        {
            case BarValue.Health: 
                return character_script.Health;
            //case BarValues.Mana:
            //  return character_script.Mana;
            default:
                return -1f;

        }
    }
}
