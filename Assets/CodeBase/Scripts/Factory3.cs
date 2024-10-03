using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Factory3 : MonoBehaviour
{
    [SerializeField] private GameObject _resource;
    [SerializeField] private float _produceTime = 4f;
    [SerializeField] private int _produceSize = 15;
    [SerializeField] private Text _resText, _resNeedAText, _resNeedBText;
    private int _countSize = 0;
    public int CountResNeedA = 0;
    public int CountResNeedB = 0;

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

            if (_countSize < _produceSize && CountResNeedA > 0 && CountResNeedB > 0)
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
                CountResNeedA--;
                CountResNeedB--;

                UpdateText();
            }
        }
    }

    public void UpdateText()
    {
        _resText.text = _countSize.ToString();
        _resNeedAText.text = CountResNeedA.ToString();
        _resNeedBText.text = CountResNeedB.ToString();
    }

}
