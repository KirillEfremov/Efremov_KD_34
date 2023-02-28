using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class CamerMove : MonoBehaviour
{

    [SerializeField] private Transform camer;
    [SerializeField] private Vector3 positionPlayer1;
    [SerializeField] private Vector3 positionPlayer2;
    private bool camerMove;
    public void CamerMoveToPlayer2()
    {
        StartCoroutine(PositionCamer(camer, positionPlayer2, 1.5f));
        StartCoroutine(PositionCamerToPlayer2Rotation(camer, 1.5f));
    }

    public void CamerMoveToPlayer1()
    {
        StartCoroutine(PositionCamer(camer, positionPlayer1, 1.5f));
        StartCoroutine(PositionCamerToPlayer1Rotation(camer, 1.5f));
    }

    private IEnumerator PositionCamerToPlayer2Rotation(Transform obj, float TravelTime)
    {
        yield return new WaitForSeconds(0.5f);

        float t = 0;

        while (t < 1)
        {
            obj.localRotation = Quaternion.Slerp(obj.localRotation, Quaternion.Euler(50, -180, 0f), t * t * 0.1f);

            t += Time.deltaTime / TravelTime;

            yield return null;
        }
        obj.rotation = Quaternion.Euler(50, -180, 0f);
    }

    private IEnumerator PositionCamerToPlayer1Rotation(Transform obj, float TravelTime)
    {
        yield return new WaitForSeconds(0.5f);

        float t = 0;

        while (t < 1)
        {
            obj.localRotation = Quaternion.Slerp(obj.localRotation, Quaternion.Euler(50, -0f, 0f), t * t * 0.1f);

            t += Time.deltaTime / TravelTime;

            yield return null;
        }
        obj.rotation = Quaternion.Euler(50, -0f, 0f);
    }

    private IEnumerator PositionCamer(Transform obj, Vector3 target, float TravelTime)
    {
        camerMove = true;
        yield return new WaitForSeconds(0.5f);
        Vector3 startPosition = obj.position;
        float t = 0;

        while (t < 1)
        {
            obj.position = Vector3.Lerp(startPosition, target, t);

            t += Time.deltaTime / TravelTime;

            yield return null;
        }
        obj.position = target;
        camerMove = false;
    }

    private IEnumerator Position(Transform obj, Vector3 target, float TravelTime)
    {
        Vector3 startPosition = obj.position;
        float t = 0;

        while (t < 1)
        {
            obj.position = Vector3.Lerp(startPosition, target + new Vector3(0, 0.2f, 0), t * t);
            t += Time.deltaTime / TravelTime;
            yield return null;
        }
        obj.transform.position = target + new Vector3(0, 0.2f, 0);
    }
}
