using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Factory2 : MonoBehaviour
{
    [SerializeField] private GameObject _resource;
    [SerializeField] private float _produceTime = 4f;
    [SerializeField] private int _produceSize = 15;
    [SerializeField] private Text _resText, _resNeedText;
    private int _countSize = 0;
    public int CountResNeed = 0;

    public int CountSize
    {
        get => _countSize;
        set => _countSize = value;
    }

    private void Start()
    {
        StartCoroutine(ProduceRes());
    }

    IEnumerator ProduceRes()
    {
        while (true)
        {
            yield return new WaitForSeconds(_produceTime);

            if (_countSize < _produceSize && CountResNeed > 0)
            {
                GameObject newResource = Instantiate(_resource, transform.position, Quaternion.identity);
                newResource.transform
                    .DOJump(new Vector3(transform.position.x, 0, transform.position.z - 4),
                        2, 1, 0.5f)
                    .OnComplete(() =>
                    {
                        newResource.GetComponent<Item>().CanCollectProp = true;
                    });

                _countSize++;
                CountResNeed--;

                UpdateText();
            }
        }
    }

    public void UpdateText()
    {
        _resText.text = _countSize.ToString();
        _resNeedText.text = CountResNeed.ToString();
    }
}
