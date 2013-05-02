using UnityEngine;

public interface ISelectableTarget{
	void setSelected(bool selected);
	Vector3 getPosition();
	Transform getTransform();
}
