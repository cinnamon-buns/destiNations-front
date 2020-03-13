using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    float heightMin = 2.0f;
    float heightMax = 2000.0f;
    float pre_point2Dist;
    float cameraHeight = 10.0f;
    Vector3 pre_t1;
    Vector3 pre_t2;

    void Update()
    {
        if (Input.touchCount >= 2)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            if (t2.phase == TouchPhase.Began)
            {
                pre_point2Dist = Vector2.Distance(t1.position, t2.position);
            }
            else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
            {
                float point2Dist = Vector2.Distance(t1.position, t2.position);
                float dist1 = Vector2.Distance(t1.position, pre_t1);
                float dist2 = Vector2.Distance(t2.position, pre_t2);
                if (Mathf.Sign(point2Dist - pre_point2Dist) >= 0)
                {
                    cameraHeight = (dist1 + dist2) / 100.0f;
                }
                else if (Mathf.Sign(point2Dist - pre_point2Dist) <= 0)
                {
                    cameraHeight = -(dist1 + dist2) / 100.0f;
                }

                if (cameraHeight != 0)
                {
                    float y = this.transform.position.y;
                    float heightLimitJudg = cameraHeight + y;
                    if (heightLimitJudg > (heightMax + 1.0f))
                    {
                        heightLimitJudg = heightMax;
                        this.transform.position = new Vector3(0, heightLimitJudg, 0);
                    }
                    else if (heightLimitJudg < (heightMin - 1.0f))
                    {
                        heightLimitJudg = heightMin;
                        this.transform.position = new Vector3(0, heightLimitJudg, 0);
                    }
                    else
                    {
                        this.transform.Translate(0, 0, cameraHeight);
                    }
                }
                pre_point2Dist = point2Dist;
            }
            pre_t1 = t1.position;
            pre_t2 = t2.position;
        }
    }
}