using UnityEngine;
using System.Collections;

public class InGameNodeData : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer render;

    [SerializeField]
    private BoxCollider2D boxCollider2D;

    public int EndNodeID { get; set; }

    public void DeleteNode()
    {
        StartCoroutine(_DeleteNode());
    }

    private IEnumerator _DeleteNode()
    {
        yield return StartCoroutine(Tween.TweenMaterial.TweenAlpha(render.material, 0f, 0.5f));

        ObjectPoolManager.Instance.Free(this.gameObject);
    }

    public DeviceInfo NodeDeviceInfo { get; set; }

    public BoxCollider2D GetBoxCollider2D()
    {
        return boxCollider2D;
    }

    private void OnDisable()
    {
        Material material = render.material;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);

        boxCollider2D.enabled = false;
    }
}