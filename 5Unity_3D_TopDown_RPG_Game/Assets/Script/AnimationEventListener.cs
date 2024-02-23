using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    Fighter fighter;

    private void Awake()
    {
        fighter = GameObjectExtention.GetcomponentAroundOrAdd<Fighter>(this.gameObject);
    }

    public void Shoot()
    {
        fighter.Hit();
    }

    public void Hit()
    {
        fighter.Hit();
    }
}
