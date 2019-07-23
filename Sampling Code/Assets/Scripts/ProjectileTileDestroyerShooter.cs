using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileTileDestoyerShooter : MonoBehaviour
{
    #region Properties
    public GameObject projectilePrefab;
    #endregion
    #region Implementation
    private ProjectileTileDestroyer InstantiateNewProjectile()
    {
        GameObject _newProjectile = Instantiate(projectilePrefab) as GameObject;
        _newProjectile.transform.position = transform.position;
        return _newProjectile.GetComponent<ProjectileTileDestroyer>();
    }
    public void ShootTo(Vector2 pDirection)
    {
        ProjectileTileDestroyer _newProjectile = InstantiateNewProjectile();
        _newProjectile.ShootTo(pDirection);
    }
    #endregion
    #region Unity callbacks
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ShootTo((worldMousePosition - (Vector2)transform.position).normalized);
        }
    }
    #endregion
}