using UnityEngine;
using System.Collections;

public class MLGText : MonoBehaviour{
	
	public string message;
	public int fontSize = Screen.width / 32;
	public FontStyle fontStyle;
	public Vector2 messagePosition;
	public bool displayingText;
	public Vector2 pivotPoint;
	public float rotationSpeed;
	public bool displaying = true;
	private float angle = 0;
	public Color mlgcolor = new Color(.12f, .12f, .12f, .5f);
	
		//mlgfont style
	protected GUIStyle m_MLGStyle = null;
	public GUIStyle MLGStyle
	{
		get
		{
			if (m_MLGStyle == null)
			{
				m_MLGStyle = new GUIStyle("Label");
				m_MLGStyle.alignment = TextAnchor.MiddleCenter;
				m_MLGStyle.fontSize = fontSize;
				m_MLGStyle.font = (Font)Resources.Load("Fonts/Lekton-Regular");
				m_MLGStyle.normal.textColor = mlgcolor;
				//m_MLGStyle.fontStyle = FontStyle.Bold;	
			}
			return m_MLGStyle;
		}
	}

	public void SetColor(Color c){
		MLGStyle.normal.textColor = c;
	}
	
	public void drawText(string text){
		message = text;
		fontStyle = FontStyle.Bold;
		fontSize = 50;
		messagePosition = new Vector2(0, 0);
		pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
		rotationSpeed = 0;
		displayingText = true;
	}
	
	public void drawText(string text, int size){
		drawText(text);
		fontSize = size;
	}
	
	public void drawText(string text, Vector2 position){
		drawText(text);
		messagePosition = position;	
		pivotPoint += position;
	}
	
	public void drawText(string text, int size, Vector2 position){
		drawText(text, size);
	
		messagePosition = position;
		pivotPoint += position;
	}
	
	public void drawText(string text, int size, Vector2 position, FontStyle style){
		drawText(text, size, position);
		m_MLGStyle.fontStyle = style;
	}
	
	public void drawRotatingText(string text,  Vector2 position, float speed = 1f){
		drawText (text);
		rotationSpeed = speed;
	}
	
	public void drawScreenPositionText(string text, Vector3 position){
		//print (position);
		//Vector3 convert = new Vector3 (position.x, Screen.height/2 - position.y/8, 0);
		//print (convert);
		drawText (text, new Vector2(position.x - Screen.width/2, position.y - Screen.height/2));	
	}
		
	// Use this for initialization
	void OnGUI () {
		if (displayingText){


			if (rotationSpeed != 0){
				Matrix4x4 matrixBackup = GUI.matrix;
				angle += rotationSpeed*Time.deltaTime;
				GUIUtility.RotateAroundPivot(angle, pivotPoint);
				GUI.Label(new Rect(messagePosition.x, messagePosition.y, Screen.width, Screen.height), message, MLGStyle);
				GUI.matrix = matrixBackup;
				
			}
			else
			GUI.Label(new Rect(messagePosition.x, messagePosition.y, Screen.width, Screen.height), message, MLGStyle);

		}
	}
	
	
}
