using System.Collections;
using UnityEngine;

public class BoxCastDebug : MonoBehaviour
{
    public static BoxCastDebug instance;
    public BoxCastDebug()
    {
        instance = this;
    }
    Vector2 boxSize;
    float distance = 10f;

    Vector3 startPos;
    Vector2 direction;

    //void DebugDrawBoxCast(Vector2 origin, Vector2 size, Vector2 direction, float distance)
    //{
    //    Vector2 upperLeft = origin - (size / 2f);
    //    Vector2 upperRight = origin + new Vector2(size.x / 2f, -size.y / 2f);
    //    Vector2 lowerLeft = origin + new Vector2(-size.x / 2f, size.y / 2f);
    //    Vector2 lowerRight = origin + (size / 2f);

    //    Vector2[] corners = { upperLeft, upperRight, lowerRight, lowerLeft };

    //    for (int i = 0; i < corners.Length; i++)
    //    {
    //        Vector2 start = corners[i];
    //        Vector2 end = corners[(i + 1) % corners.Length];

    //        Debug.DrawLine(start, end, Color.blue);
    //    }

    //    Vector2 endPoint = origin + (direction.normalized * distance);
    //    Debug.DrawLine(origin, endPoint, Color.green);
    //}
    static public void DebugDrawBoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }
    }
        public void StartDrawing(Vector3 startPos, Vector2 dir, Vector2 boxSize)
    {
        this.startPos = startPos;
        direction = dir;
        this.boxSize = boxSize;
        StartCoroutine(DrawBox());
    }
    IEnumerator DrawBox()
    {
        while(true)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(startPos, boxSize, 45f, direction, distance);

            // Visualize the casted box
            DebugDrawBoxCast(startPos, boxSize, 0f, direction, distance);

            // Visualize the hit points
            foreach (RaycastHit2D hit in hits)
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}