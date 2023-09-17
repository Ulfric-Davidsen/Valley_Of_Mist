using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGiver : MonoBehaviour
{
    private int expAmount = 25;
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            ExperienceManager.Instance.AddExperience(expAmount);
        }
    }
}
