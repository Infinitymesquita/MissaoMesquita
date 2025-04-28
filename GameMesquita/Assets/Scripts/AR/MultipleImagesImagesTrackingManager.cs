using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImagesImagesTrackingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    ARTrackedImageManager aRTrackedImageManager;
    Dictionary<string, GameObject> arObjects;

    private void Awake()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
        arObjects = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject newArObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newArObject.name = prefab.name;
            newArObject.gameObject.SetActive(false);
            arObjects.Add(newArObject.name, newArObject);
        }
    }
    private void OnDestroy()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }
    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage trackedImage in obj.added)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in obj.updated)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in obj.removed)
        {
            arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }
    }
    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }
        if (prefabsToSpawn != null)
        {
            arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
            arObjects[trackedImage.referenceImage.name].gameObject.transform.position = trackedImage.transform.position;
        }
    }
}
