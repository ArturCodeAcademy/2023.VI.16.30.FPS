using UnityEngine;

public abstract class HoldableItem : MonoBehaviour
{
    [field: SerializeField] public bool Droppable { get; protected set; }
    [field: SerializeField] public Vector3 HoldPosition { get; protected set; }
    [field: SerializeField] public Quaternion HoldRotation { get; protected set; }

    public abstract void OnPickup();
    public abstract void OnDrop();
    public abstract void OnHide();
    public abstract void OnShow();
}
