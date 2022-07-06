using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBlink : MonoBehaviour
{
    List<Color> originalColors;

    public void SetOriginalColors(List<Color> colors)
    {
        originalColors = colors;
    }

    public void Blink()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(Execute());
        }
    }

    private void SetAlpha(float alpha)
    {
        if (gameObject != null)
        {
            MeshRenderer[] children = gameObject.GetComponentsInChildren<MeshRenderer>();
            Color newColor;
            foreach (MeshRenderer child in children)
            {
                foreach (Material mat in child.materials)
                {
                    newColor = mat.color;
                    newColor.a = alpha;
                    mat.SetColor("_Color", newColor);
                }
            }
        }
    }

    private IEnumerator Execute()
    {
        for (int i = 0; i < 4; i++)
        {
            SetAlpha(0.3f);
            yield return new WaitForSeconds(0.25f);
            SetAlpha(1);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
