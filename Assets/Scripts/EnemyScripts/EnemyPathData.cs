using UnityEngine;

[CreateAssetMenu(fileName = "PathData", menuName = "ScriptableObjects/EnemyPathData", order = 1)]
public class EnemyPathData : ScriptableObject
{
    public GameObject CircleMove;
    public GameObject ReverseCircleMove;
    public GameObject CurveMove;
    public GameObject ReverseCurveMove;
    public GameObject StandAtTopLeft;
    public GameObject StandAtTopMiddle;
    public GameObject StandAtTopRight;
    public GameObject StandAtMiddleLeft;
    public GameObject StandAtCenter;
    public GameObject StandAtMiddleRight;
    public GameObject StandAtButtomLeft;
    public GameObject StandAtButtomMiddle;
    public GameObject StandAtButtomRight;

    public GameObject GetPath(EnemyMovingBehavior pathToChoose)
    {
        switch (pathToChoose)
        {
            case EnemyMovingBehavior.CircleMove:
                return CircleMove;
            case EnemyMovingBehavior.ReverseCircleMove:
                return ReverseCircleMove;
            case EnemyMovingBehavior.CurveMove:
                return CurveMove;
            case EnemyMovingBehavior.ReverseCurveMove:
                return ReverseCurveMove;
            case EnemyMovingBehavior.StandAtTopLeft:
                return StandAtTopLeft;
            case EnemyMovingBehavior.StandAtTopMiddle:
                return StandAtTopMiddle;
            case EnemyMovingBehavior.StandAtTopRight:
                return StandAtTopRight;
            case EnemyMovingBehavior.StandAtMiddleLeft:
                return StandAtMiddleLeft;
            case EnemyMovingBehavior.StandAtCenter:
                return StandAtCenter;
            case EnemyMovingBehavior.StandAtMiddleRight:
                return StandAtMiddleRight;
            case EnemyMovingBehavior.StandAtButtomLeft:
                return StandAtButtomLeft;
            case EnemyMovingBehavior.StandAtButtomMiddle:
                return StandAtButtomMiddle;
            case EnemyMovingBehavior.StandAtButtomRight:
                return StandAtButtomRight;
            default:
                Debug.Log("Not defined path");
                return null;
        }
    }
}
