using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveAndScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(Vector3.one * 3, 2f)
            .SetLoops(5, LoopType.Yoyo)
            .SetEase(Ease.InOutBack)
            .OnComplete(() =>
            {
                Debug.Log("Move Done");
            });
        transform.DOScale(Vector3.one * 2, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            //Move();
        }
    }

    void Move()
    {
        transform.position += Vector3.one * 3 * Time.deltaTime;
    }
}
