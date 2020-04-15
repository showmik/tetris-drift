using UnityEngine;

public class TakeInput : MonoBehaviour
{
    public bool buttonIsUp = true;

    public bool CheckRightDown(float horizontalValue)
    {
        if (horizontalValue == 0)
        {
            buttonIsUp = true;
        }

        if (horizontalValue > 0)
        {
            if (buttonIsUp)
            {
                buttonIsUp = false;
                return true;
            }
            else
            {
                buttonIsUp = true;
                return false;
            }
        }
        return false;

    }
}
