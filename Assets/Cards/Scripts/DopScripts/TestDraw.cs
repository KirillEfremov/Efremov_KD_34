using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour
{
    private Vector3 _size = new Vector3(70f, 1f, 100f);

    private void OnDrawGizmos() //вызываем, чтобы нарисовать место для карт
    {
        Gizmos.color = Color.green; //задаем зеленый цвет
        Gizmos.DrawCube(transform.position, _size); //рисуем куб по позиции поля _размер (70,1,100)
    }
}
