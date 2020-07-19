using UnityEngine;

public class KeySetter : MonoBehaviour
{
    [SerializeField]
	int keyIndex;
	bool isClicked;
    [SerializeField]
    Keys keys;

	public void Click(bool value) => isClicked = value;

	void OnGUI()
	{
		Event e = Event.current;
		if (isClicked)
		{
			if (e.isKey) keys.keys[keyIndex] = e.keyCode;
			else if (e.isMouse)
			{
				int mouseButton = e.button;

				if (mouseButton == 0) { keys.keys[keyIndex] = KeyCode.Mouse0; return; }
				if (mouseButton == 1) { keys.keys[keyIndex] = KeyCode.Mouse1; return; }
				if (mouseButton == 2) { keys.keys[keyIndex] = KeyCode.Mouse2; return; }
				if (mouseButton == 3) { keys.keys[keyIndex] = KeyCode.Mouse3; return; }
				if (mouseButton == 4) { keys.keys[keyIndex] = KeyCode.Mouse4; return; }
				if (mouseButton == 5) { keys.keys[keyIndex] = KeyCode.Mouse5; return; }
				if (mouseButton == 6) { keys.keys[keyIndex] = KeyCode.Mouse6; return; }
			}
		}
	}
}
