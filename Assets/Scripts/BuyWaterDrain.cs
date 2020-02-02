using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWaterDrain : MonoBehaviour
{
    public void Click()
    {
        Water.Instance.Buy();
    }
}
