using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardGenerator))]//Override UI that would appear in the inspector
public class BoardGeneratorInspector : Editor
{
	public override void OnInspectorGUI()//Custom UI
	{
		var current = (BoardGenerator)target;
		DrawDefaultInspector();//Show default options then add...
        //For each BoardGenerator public method a button
		if (GUILayout.Button("Clear"))
			current.Clear();
		if (GUILayout.Button("Generate"))
			current.Generate();
		if (GUILayout.Button("Generate Perlin"))
			current.GeneratePerlin();
		if (GUILayout.Button("Grow"))
			current.Grow();
		if (GUILayout.Button("Shrink"))
			current.Shrink();
		if (GUILayout.Button("Snap Marker"))
			current.SnapMarker();

		GUILayout.Label("");//spacer
		if (GUILayout.Button("Save"))
			current.Save();
		if (GUILayout.Button("Load"))
			current.Load();

		if (GUI.changed)
			current.UpdateMarker();
	}

	// Thanks Mr. R
	// JULY 21, 2017 AT 4:16 PM
	void OnSceneGUI()
    /*
    With this snippet, we grab the current event, make sure that the shift key is also pressed, 
    and then if so, will intercept keyboard events for the arrow keys to move the cursor instead. 
    Note that this trick only works if the inspector is "locked" and the scene pane has focus.
    */
	{
		var current = (BoardGenerator)target;
		Event e = Event.current;
		if (!e.shift)
			return;

		switch (e.type)
		{
			case EventType.KeyDown:
				{
					switch (Event.current.keyCode)
					{
						case KeyCode.LeftArrow:
							current.MoveMarker(new Point(-1, 0));
							e.Use();
							break;
						case KeyCode.RightArrow:
							current.MoveMarker(new Point(1, 0));
							e.Use();
							break;
						case KeyCode.UpArrow:
							current.MoveMarker(new Point(0, 1));
							e.Use();
							break;
						case KeyCode.DownArrow:
							current.MoveMarker(new Point(0, -1));
							e.Use();
							break;
					}
					break;
				}
		}
	}
}