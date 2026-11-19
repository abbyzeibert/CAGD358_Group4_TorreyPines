using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImagesTracker : MonoBehaviour
{

    [Header("Prefabs to Spawn")]
    [SerializeField] List<GameObject> prefabToSpawn = new List<GameObject>();

    private ARTrackedImageManager _trackedImageManager;

    private Dictionary<string, GameObject> _arObjects;


    /// <summary>
    /// Initilization and references assigning
    /// </summary>
    private void Start()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        if ( _trackedImageManager == null )
        {
            return;
        }
        _arObjects = new Dictionary<string, GameObject>();
        SetUpSceneElements();
    }


    private void SetUpSceneElements()
    {
    
    }
}
