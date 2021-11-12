using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bouncer : MonoBehaviour
{
    public IEnumerator BounceCube(GameObject gameObject)
    {
        gameObject.transform.DOScale(new Vector3(0f, 0f, 0f), 0f);
        gameObject.transform.DOScale(new Vector3(0.53009f, 0.53009f, 0.53009f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(new Vector3(0.53009f, 0.53009f, 0.53009f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(new Vector3(0.53009f, 0.53009f, 0.53009f), 0.2f);
    }
    public IEnumerator BounceSprite(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(new Vector3(0.8410099f, 0.8410099f, 0.8410099f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.DOScale(new Vector3(0.8410099f, 0.8410099f, 0.8410099f), 0.2f);
    }
}
