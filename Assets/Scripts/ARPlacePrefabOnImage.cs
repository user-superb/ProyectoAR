using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacePrefabOnImage : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_ImageManager;

    // List of prefabs to instantiate - these should be named the same
    // as their corresponding 2D images in the reference image library 
    public GameObject[] ArPrefabs;

    //Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    void OnEnable() => m_ImageManager.trackablesChanged.AddListener(OnChanged);

    void OnDisable() => m_ImageManager.trackablesChanged.RemoveListener(OnChanged);

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event

            // Get the ame of the referance image
            var imageName = newImage.referenceImage.name;
            // Now loop over the array of prefabs
            foreach (var curPrefab in ArPrefabs)
            {
                // Check wether this prefab matches the tracked image name, and that
                // the prefab hasn't already been created
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Instantiate the prefab, parenting it to the ARTrackedImage
                    var newPrefab = Instantiate(curPrefab, newImage.transform);
                    // Add the created prefab to our array
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
            }

            Debug.Log(newImage.referenceImage.name);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
            _instantiatedPrefabs[updatedImage.referenceImage.name].SetActive(updatedImage.trackingState == TrackingState.Tracking);
        }

        foreach (var removed in eventArgs.removed)
        {
            // Handle removed event
            TrackableId removedImageTrackableId = removed.Key;
            ARTrackedImage removedImage = removed.Value;

            // Destroy its prefab
            Destroy(_instantiatedPrefabs[removedImage.referenceImage.name]);
            // Also remove the instance from our array
            _instantiatedPrefabs.Remove(removedImage.referenceImage.name);
        }
    }
}
