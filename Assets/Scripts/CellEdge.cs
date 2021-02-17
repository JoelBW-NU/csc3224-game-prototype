using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEdge : MonoBehaviour
{
    enum Edge { TOP, RIGHT, LEFT, BOTTOM };

    [SerializeField]
    Edge edge;

    [HideInInspector]
    public Grid grid;

    public bool colliderActive = false;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Screen Bounds") && colliderActive)
        {
            if (edge == Edge.TOP)
            {
                grid.NewCell(Cell.CellPosition.TOP, transform.parent.position);
            } 
            else if (edge == Edge.BOTTOM)
            {
                grid.NewCell(Cell.CellPosition.BOTTOM, transform.parent.position);
            }
            else if (edge == Edge.LEFT)
            {
                grid.NewCell(Cell.CellPosition.LEFT, transform.parent.position);
            }
            else if (edge == Edge.RIGHT)
            {
                grid.NewCell(Cell.CellPosition.RIGHT, transform.parent.position);
            }
        }

        foreach (CellEdge edge in transform.parent.GetComponentsInChildren<CellEdge>())
        {
            edge.colliderActive = true;
        }
        colliderActive = true;
    }
}
