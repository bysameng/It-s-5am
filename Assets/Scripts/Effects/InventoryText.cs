using UnityEngine;
using System.Collections;

public class InventoryText {
	
	public string message;
	public int fontSize = Screen.width / 32;
	public FontStyle fontStyle;
	public Vector2 messagePosition;
	public bool displayingText;
	public bool selected = false;

	protected GUIStyle m_MLGStyle = null;
	public GUIStyle MLGStyle
	{
		get
		{
			if (m_MLGStyle == null)
			{
				m_MLGStyle = new GUIStyle("Label");
				m_MLGStyle.alignment = TextAnchor.MiddleLeft;
				m_MLGStyle.fontSize = fontSize;
				m_MLGStyle.font = (Font)Resources.Load("Fonts/Lekton-Regular");
				//m_MLGStyle.fontStyle = FontStyle.Bold;	
			}
			return m_MLGStyle;
		}
	}
	
	
	public void DrawText(string words, Vector2 location){
			message = words;
			messagePosition = location;
			if (!selected){
				MLGStyle.normal.textColor = new Color(1f, 1f, 1f, .3f);
			}
			else MLGStyle.normal.textColor = Color.white;
			GUI.Label(new Rect(messagePosition.x, messagePosition.y, Screen.width, Screen.height), message, MLGStyle);
	}
}
