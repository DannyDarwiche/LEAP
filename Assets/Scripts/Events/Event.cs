using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField]
    int id;

    protected float activeState, loopActiveState;
    protected bool activated;

    protected void Activated(int id)
    {
        if (id == this.id)
        {
            activeState = 1;
            loopActiveState = 1;
            activated = true;
        }
    }

    protected void Deactivated(int id)
    {
        if (id == this.id)
        {
            activeState = 0;
            activated = false;
        }
    }

    protected void Activated(int id, float percentage)
    {
        if (id == this.id)
        {
            activeState = percentage;
            loopActiveState = percentage;
            if (percentage > 0)
                activated = true;
        }
    }

    protected void Deactivated(int id, float percentage)
    {
        if (id == this.id)
        {
            activeState = percentage;
            loopActiveState = percentage;
            if (percentage == 0)
            {
                activeState = 0;
                activated = false;
            }
        }
    }


}
