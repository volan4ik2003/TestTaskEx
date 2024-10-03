using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackResources : MonoBehaviour
{
    [SerializeField] private Transform HolderParent;
    [SerializeField] private int stackSize = 10;
    private Factory _factory;
    private Factory2 _factory2;
    private Factory3 _factory3;

    private bool IsInDropArea;
    private Vector3 dropPos;
    private Stack<Transform> collectedRes = new Stack<Transform>();
    private float pos = 0;

    void Start()
    {
        _factory = FindObjectOfType<Factory>();
        _factory2 = FindObjectOfType<Factory2>();
        _factory3 = FindObjectOfType<Factory3>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DropB"))
        {
            IsInDropArea = true;
            dropPos = other.transform.position;

            StartCoroutine(DropResB());
        }
        else if (other.CompareTag("DropC"))
        {
            IsInDropArea = true;
            dropPos = other.transform.position;

            StartCoroutine(DropResC());
        }
        else
        {
            Item localItem = null;
            other.TryGetComponent(out localItem);

            if (other.CompareTag("ResourceA") && localItem != null && !localItem.IsCollectedProp && localItem.CanCollectProp && collectedRes.Count < stackSize)
            {
                CollectResource(other.transform, localItem, "ResourceA", _factory);
                _factory.UpdateText();
            }
            else if (other.CompareTag("ResourceB") && localItem != null && !localItem.IsCollectedProp && localItem.CanCollectProp && collectedRes.Count < stackSize)
            {
                CollectResource(other.transform, localItem, "ResourceB", _factory2);
                _factory2.UpdateText();
            }
            else if (other.CompareTag("ResourceC") && localItem != null && !localItem.IsCollectedProp && localItem.CanCollectProp && collectedRes.Count < stackSize)
            {
                CollectResource(other.transform, localItem, "ResourceC", _factory3);
                _factory3.UpdateText();
            }
        }
    }

    private void CollectResource(Transform resource, Item localItem, string resourceTag, MonoBehaviour factory)
    {
        collectedRes.TryPeek(out Transform result);

        if (result == null || result.gameObject.CompareTag(resourceTag))
        {
            resource.DOJump(HolderParent.position, 2, 1, 0.2f).OnComplete(() =>
            {
                resource.SetParent(HolderParent);
                resource.localPosition = new Vector3(0, pos, 0.1f);
                resource.localRotation = Quaternion.identity;
                pos += 0.3f;
            });

            collectedRes.Push(resource);
            localItem.IsCollectedProp = true;

            if (factory is Factory)
                ((Factory)factory).CountSize--;
            else if (factory is Factory2)
                ((Factory2)factory).CountSize--;
            else if (factory is Factory3)
                ((Factory3)factory).CountSize--;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropB") || other.CompareTag("DropC"))
        {
            IsInDropArea = false;
            pos = 0;
        }
    }

    IEnumerator DropResB(float _delay = 0)
    {
        while (IsInDropArea)
        {
            yield return new WaitForSeconds(_delay);
            collectedRes.TryPeek(out Transform result);
            if (collectedRes.Count > 0 && result.gameObject.CompareTag("ResourceA"))
            {
                DropResource(result, _factory2, ref _factory2.CountResNeed);
                _factory2.UpdateText();
            }
        }
    }

    IEnumerator DropResC(float _delay = 0)
    {
        while (IsInDropArea)
        {
            yield return new WaitForSeconds(_delay);
            collectedRes.TryPeek(out Transform result);

            if (collectedRes.Count > 0 && result.gameObject.CompareTag("ResourceA"))
            {
                DropResource(result, _factory3, ref _factory3.CountResNeedA);
                _factory3.UpdateText();
            }
            else if (collectedRes.Count > 0 && result.gameObject.CompareTag("ResourceB"))
            {
                DropResource(result, _factory3, ref _factory3.CountResNeedB);
                _factory3.UpdateText();
            }
        }
    }

    private void DropResource(Transform resource, MonoBehaviour factory, ref int factoryResNeed)
    {
        Transform newItem = collectedRes.Pop();
        newItem.parent = null;
        newItem.DOJump(dropPos, 2, 1, 0.2f).OnComplete(() =>
        {
            newItem.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f).OnComplete(() => Destroy(newItem.gameObject));
        });

        factoryResNeed++;
    }
}
