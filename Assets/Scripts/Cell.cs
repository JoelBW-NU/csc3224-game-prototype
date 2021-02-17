using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    [HideInInspector]
    public Grid grid;
    public enum CellPosition { CENTER, TOP, LEFT, BOTTOM, RIGHT, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT };
    public CellPosition position;

    BoxCollider2D cellCollider;
    public float grapplePointDist = 0.5f;

    [HideInInspector]
    public GameObject grapplePointPrefab;

    [SerializeField]
    float cellPadding = 1f;

    List<GrapplePoint> grapplePoints;

    public void Initialise()
    {
        cellCollider = GetComponent<BoxCollider2D>();
        grapplePoints = new List<GrapplePoint>();
        Vector3 newScale = transform.localScale;
        newScale.Set(newScale.x * grid.scaleX, newScale.y * grid.scaleY, 1);
        transform.localScale = newScale;
        foreach (CellEdge edge in GetComponentsInChildren<CellEdge>())
        {
            edge.grid = grid;
        }
    }

    void Populate(int numPoints)
    {
        int pointsPlaced = 0;
        //Vector2 prevPosition = new Vector2(cellCollider.bounds.center.x, cellCollider.bounds.center.y);

        while (pointsPlaced < numPoints)
        {
            Vector2 position = new Vector2(Random.Range(cellCollider.bounds.min.x + cellPadding, cellCollider.bounds.max.x - cellPadding), Random.Range(cellCollider.bounds.min.y + cellPadding, cellCollider.bounds.max.y - cellPadding));
            GrapplePoint grapplePoint = Instantiate(grapplePointPrefab, position, Quaternion.identity).GetComponent<GrapplePoint>();
            grapplePoint.grapple = grid.playerGrapple;
            grapplePoints.Add(grapplePoint);
            //prevPosition = position;
            ++pointsPlaced;
        }
    }

    void Depopulate()
    {
        foreach (GrapplePoint grapplePoint in grapplePoints)
        {
            if (!grapplePoint.countdownBegun)
            {
                grapplePoint.DestroyPoint();
            }
        }
        grapplePoints.Clear();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Screen Bounds"))
        {
            Populate(grid.numPointsPerCell);
        }
        else if (col.gameObject.CompareTag("Player")) 
        {
            grid.NewCenter(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Screen Bounds"))
        {
            Depopulate();
            grid.RemoveCell(transform.position);
            Destroy(gameObject);
        }
    }
}