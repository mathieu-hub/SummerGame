using UnityEngine;

public static class IntExtensions
{
    /// <summary>
    /// Returns true if the value is between the two boundaries
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lower"></param>
    /// <param name="greater"></param>
    /// <returns></returns>
    public static bool isBetween(this int value, int lower, bool equal1, int greater, bool equal2)
    {
        bool result = false;

        if (!equal1 && !equal2)
        {
            if (value > lower && value < greater)
            {
                result = true;
            }
        }
        else if (equal1 && equal2)
        {
            if (value >= lower && value <= greater)
            {
                result = true;
            }
        }
        else if (!equal1 && equal2)
        {
            if (value > lower && value <= greater)
            {
                result = true;
            }
        }
        else if (equal1 && !equal2)
        {
            if (value >= lower && value < greater)
            {
                result = true;
            }
        }
        else
        {
            Debug.Log("Error, equal value not assigned");
        }

        return result;
    }
}