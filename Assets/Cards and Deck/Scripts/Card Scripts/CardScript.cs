using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An interface from which CardScripts will call ActivateCard() when their effects are activated.

public abstract class CardScript : ScriptableObject
{
    public abstract void ActivateCard();
}
