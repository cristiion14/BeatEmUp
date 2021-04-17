using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//CINEMACHINE CAMERA SHAKE
public class CameraShake : MonoBehaviour
{




    private void Update()
    {
        
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        //store the original pos of the camera before it starts shaking to be able to reset it back 
        Vector3 originalPos = transform.localPosition;

        //how much time has passed since started to shake
        float elapsed = 0f;
        //keep shaking for duration amount

        while (elapsed < duration)
        {
            //calculate the offset that needs to be apply to the camera 
            //Offset it by a random amount every frame

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            //apply the offset to the camera pos
            transform.localPosition = new Vector3(x, y, originalPos.z);

            //increase elapsed by the time it went by
            elapsed += Time.deltaTime;

            //wait until the next frame is drawn
            yield return null;

        }
        //reset the position
        transform.localPosition = originalPos;
    }
}
