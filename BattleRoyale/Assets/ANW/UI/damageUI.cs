using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageUI : MonoBehaviour {

    public Image hitMarker;
    public Image[] damageIndicator;

    private void Start()
    {
        var setAlphaZero = hitMarker.color;
        setAlphaZero.a = 0;
        hitMarker.color = setAlphaZero;

        for (int i = 0; i < damageIndicator.Length; i++)
        {
            damageIndicator[i].color = setAlphaZero;
        }
    }

    void Update()
    {
        if (hitMarker.color.a > 0)
        {
            var tempAlpha = hitMarker.color;
            tempAlpha.a -= Time.deltaTime * 128;
            hitMarker.color = tempAlpha;

            if (hitMarker.color.a < 0)
            {
                tempAlpha.a = 0f;

                hitMarker.color = tempAlpha;
            }
        }

        for (int i = 0; i < damageIndicator.Length; i++)
        {
            if (damageIndicator[i].color.a > 0)
            {
                var tempAlpha = damageIndicator[i].color;
                tempAlpha.a -= Time.deltaTime * 32;
                damageIndicator[i].color = tempAlpha;

                if (damageIndicator[i].color.a < 0)
                {
                    tempAlpha.a = 0f;

                    damageIndicator[i].color = tempAlpha;
                }
            }
        }
    }

}
