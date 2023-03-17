using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesrDraw2D : MonoBehaviour
{
    private Vector3 _size = new Vector3(425f, 700f, 10f);

    private void OnDrawGizmos() //вызываем, чтобы нарисовать место для карт
    {
        Gizmos.color = Color.green; //задаем зеленый цвет
        Gizmos.DrawCube(transform.position, _size); 
    }
}
