using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuts : MonoBehaviour
{
    public int id;
    public float duration = 0.5f;
    public void MoveNut(Transform target)
    {
        transform.DOMove(target.position, duration)
                .SetEase(Ease.OutQuad);
    }

}
