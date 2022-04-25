using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrickFormation : MonoBehaviour
{

    // horizontal and vertical lines of grid. see documentation for the GetPosition method: https://1drv.ms/x/s!AqWxe94iWLB9iCINjNfq6Ah9FUl6?e=1dCQFs
    [SerializeField] int _gridHorizontal, _gridVertical, _startingBricks;
    [SerializeField] Transform _brickTransform;                             // need this to read brick prefabs scale.

    private int _brickPerGrid, brickCount = 0;

    // list of grids on the tray the character is carrying
    public List<GameObject> bricksOnTray = new List<GameObject>();
    

    private void Start() 
    {
        SettingConfigs();
        GameEvents.Instance.onObstacleCollisionExit += BrickReposition;
        GameEvents.Instance.onObstacleBrickCollision += RemoveBrickFromArray;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.onObstacleCollisionExit -= BrickReposition;
        GameEvents.Instance.onObstacleBrickCollision -= RemoveBrickFromArray;
    }

    private void SettingConfigs()
    {
        _brickPerGrid = _gridHorizontal * _gridVertical;
        StartCoroutine(AddBrick(_startingBricks));
        DOTween.SetTweensCapacity(ObjectPooler.Instance.pools[0].size, 750);
    }

    public IEnumerator AddBrick(int NumberToAdd) 
    {
        for (int i = 0; i < NumberToAdd; i++) 
        {
            var gridFloor = Mathf.FloorToInt(brickCount / _brickPerGrid);
            GameObject spawningBrick = ObjectPooler.Instance.SpawnFromPool("Brick", GetPosition(brickCount), Quaternion.identity, transform);
            if (i % 5 == 0)
            {
                yield return new WaitForEndOfFrame();
            }
            spawningBrick.transform.localRotation = Quaternion.Euler(Vector3.zero);
            bricksOnTray.Add(spawningBrick);
            brickCount++;

        }
    }

    public void RemoveBrickFromArray(GameObject objToRemove)
    {
        bricksOnTray.Remove(objToRemove);
        brickCount--;
    }

    public IEnumerator PopToBridge()
    {
        if (bricksOnTray.Count > 0)
        {
            GameObject lastBrick = bricksOnTray.Last();
            bricksOnTray.Remove(lastBrick);
            yield return new WaitForEndOfFrame();
            ObjectPooler.Instance.ReturnToPool("Brick", lastBrick);
        } 
        else
        {
            GameEvents.Instance.GameOver();
        }
    }


    private Vector3 GetPosition(int brickIndex) 
    {
        int gridFloor = Mathf.FloorToInt(brickIndex / _brickPerGrid);
        int xLocation = Mathf.FloorToInt((brickIndex % _brickPerGrid) % _gridHorizontal);
        int zLocation = Mathf.CeilToInt((brickIndex % _brickPerGrid) / _gridHorizontal);

        return new Vector3(-.4375f + (xLocation * _brickTransform.localScale.x), 
                          (_brickTransform.localScale.y + gridFloor * _brickTransform.localScale.y),
                          (-.4583f + (zLocation * _brickTransform.localScale.z)));
    }

    public void BrickReposition()
    {
        brickCount = 0;
        foreach (GameObject brick in bricksOnTray)
        {
            brick.transform.DOLocalJump(GetPosition(brickCount), 1, 1, .3f).SetEase(Ease.OutExpo).SetDelay(.5f);
            brickCount++;
        }
    }
    
    #region FirstAttempt (i'm not using this system anymore)
    /*
     * 
    // This grid system was my first attempt.


    [System.Serializable]
    public class Grid
    {
        public List<GameObject> objectsInGrid = new List<GameObject>();
        public string gridName;
        public int gridNumber;
        public int gridCapacity;
        private bool isFull;
        private bool isEmpty;

        public void AddToGrid(GameObject obj)
        {
            if (objectsInGrid.Count < gridCapacity)
            {
                objectsInGrid.Add(obj);
            }
        }

        public void RemoveSpecific(GameObject obj)
        {
            objectsInGrid.Remove(obj);
        }

        public GameObject Pop()
        {
            if (objectsInGrid.Count > 0)
            {
                GameObject temp = objectsInGrid[objectsInGrid.Count - 1];
                objectsInGrid.Remove(temp);
                return temp;
            }
            else
            {
                return default(GameObject);
            }
        }

        public bool GridFullCheck()
        {
            return isFull = objectsInGrid.Count < gridCapacity ? false : true;
        }

        public bool GridEmptyCheck()
        {
            return isEmpty = objectsInGrid.Count == 0 ? true : false;
        }

        public void MoveDown()
        {

        }
    }
    */
    #endregion
}
