using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class MultiScanManager
{
    //prefabs to scan
    [SerializeField] List<GameObject> prefabsToSpawn = new List<GameObject>();

    //ARTrackedImageManager reference
    private ARTrackedImageManager imageManager;

    //Dictionary to reference spawned prefabs with tracked image name
    private Dictionary<string, GameObject> arObjects;

    //Initialization and references assigning
    private void Start()
    {
        //imageManager = gameObject.GetComponent<ARTrackedImageManager>();
        if (imageManager == null) return;
        imageManager.trackablesChanged.AddListener(OnImagesTrackedChanged);
        arObjects = new Dictionary<string, GameObject>();
        SetupSceneElements();
    }

    private void OnDestroy()
    {
        imageManager.trackablesChanged.RemoveListener(OnImagesTrackedChanged);
    }

    //setup scene elements
    private void SetupSceneElements()
    {
        //foreach (var prefab:GameObject in prefabToSpawn)
        //{
        //    var arObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        //    arObject.name = prefab.name;
        //    arObject.gameObject.SetActive(false);
        //    arObjects.Add(arObject.name, arObject);
        //}
    }

    //update traccked images and prefabs
    private void OnImagesTrackedChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }
        //foreach (var trackedImage:KeyValuePair<TrackableId, ARTrackedImage> in eventArgs.added)
        //{
         //   UpdateTrackedImage(trackedImage.Value);
        //}
    }
    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage == null) return;
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }
        arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
        arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        arObjects[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;
    }
}
