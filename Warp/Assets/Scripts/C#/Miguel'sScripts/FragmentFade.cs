using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentFade : MonoBehaviour
{
    //bool stopFading;
    public int fadeOutCount = 0;
    MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();

        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(2f);

        Color originalColor = this.mesh.material.color;
        Color transparentColor = this.mesh.material.color;
        transparentColor = new Color(transparentColor.r, transparentColor.g, transparentColor.b, 0);

        Color fragmentColor = this.mesh.material.color;

        while (fadeOutCount < 3) // Fade in and out 3 times
        {
            for (float t = 0.0f; t < 0.3f; t += Time.deltaTime)
            {
                fragmentColor = Color.Lerp(fragmentColor, transparentColor, t);
                mesh.material.color = fragmentColor;

                yield return null;

            }

            //fadeOutCount += 1;
            yield return null;

            for (float t = 0.0f; t < 0.3f; t += Time.deltaTime)
            {
                fragmentColor = Color.Lerp(fragmentColor, originalColor, t);
                mesh.material.color = fragmentColor;

                yield return null;
            }
            fadeOutCount += 1;
        }

        // After fading in and out 3 times, fade out but dont fade back in

        yield return null;
        for (float t = 0.0f; t < 0.3f; t += Time.deltaTime)
        {
            fragmentColor = Color.Lerp(fragmentColor, transparentColor, t);
            mesh.material.color = fragmentColor;

            yield return null;

        }

        // After fading out, destroy this object's parents. This removes all parts of the remains, including the fragments
        Destroy(transform.parent.gameObject); 
        yield break;
    }
}
