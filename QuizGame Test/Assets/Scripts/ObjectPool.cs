using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : Singleton<ObjectPool>
{
    //List of the objects to do pooled
   public GameObject[] PrefabsForPool;
   
   

    // List of the pooled objects
   private List<GameObject> _pooledObjects = new List<GameObject>();
    public Transform Canvas;

   public GameObject GetObjectfromPool(string objectName)
   {
       //try to get a pooled instance
       var instance  = _pooledObjects.FirstOrDefault(obj => obj.name == objectName);
       //if we have a pooled instance allreaedy
       if(instance != null)
       {
           
           _pooledObjects.Remove(instance);
           instance.SetActive(true);
           return instance;
       }

       //if we don't have a pooled instance
       
       
           var prefab = PrefabsForPool.FirstOrDefault(obj => obj.name == objectName);
       
       if(prefab !=null)
       {
           //Create a new Instance
    
           var newInstance = Instantiate(prefab, prefab.transform.position = new Vector3(27, 29, 0), Quaternion.identity) as GameObject;
           newInstance.transform.SetParent(Canvas.transform, false);
           newInstance.name = objectName;
           return newInstance;
       
       }
   
       Debug.LogWarning("Object pool desert have a prefab far the object with name " + objectName);
       return null;
   }
    
    public void PoolObject(GameObject obj)
    {
        obj.SetActive(false);
        _pooledObjects.Add(obj);
    }
}