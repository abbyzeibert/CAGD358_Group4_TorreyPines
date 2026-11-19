using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class MultiScanManager : MonoBehaviour
{
    //prefabs to scan
    [SerializeField] List<GameObject> prefabsToSpawn = new List<GameObject>();

    //ARTrackedImageManager reference
    private ARTrackedImageManager imageManager;
    private string prevName = "None";

    //Dictionary to reference spawned prefabs with tracked image name
    private Dictionary<string, GameObject> arObjects;

    //Initialization and references assigning
    private void Start()
    {
        imageManager = gameObject.GetComponent<ARTrackedImageManager>();
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
        foreach (GameObject prefab in prefabsToSpawn)
        {
            var arObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            arObject.name = prefab.name;
            arObject.gameObject.SetActive(false);
            arObjects.Add(arObject.name, arObject);
        }
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
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
           UpdateTrackedImage(trackedImage);
        }
    }
    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage == null) return;
        /*if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
        }
        */
        //Currently some error with switching images, loaded first fine, then covered first and scanned
        //another, flickered a bit between first and other, usually settling on first but eventually swapped to second
        //Then couldn't swap back to first, no flickering or anything.  Should try printing prevname and stuff..
        //Update: Only cottontail causes this?  Swapping between deer and coyote worked fine, swapping to cottontail fine
        //But can't scan other codes once on cottontail??  Unsure whats causing this, needs further testing.
        if (prevName != "None" && trackedImage.referenceImage.name != prevName)
        {
            arObjects[prevName].gameObject.SetActive(false);
        }
        arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
        arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        arObjects[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;
        prevName = trackedImage.referenceImage.name;
    }
}
