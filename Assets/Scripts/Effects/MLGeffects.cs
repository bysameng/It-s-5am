using UnityEngine;
using System.Collections;

public class MLGeffects : MonoBehaviour {
	
	
	public int mlgEffect;
	public bool displayingMLG;
	
	//prefabs
	public Texture2D MLG1;
	public Texture2D MLG2;
	public Texture blackTexture;
	public GameObject light1;
	public GameObject SuperParticle;
	public GameObject ttext1;
	public AudioClip dubstep;
	public GameObject flare1;
	public Font MLGFont;
	public Light lightToIncrease;
	
	
	
	//effect helpers
	private bool playingAudio = false;
	private bool shakingScreen = false;
	private float shakeStrength;

	private bool timeSlowed = false;
	private bool pulsingFog = false;
	private bool zoomingFov = false;
	private bool cancelZoom = false;
	private bool displayingWords = false;
	private bool blankedScreen = false;

	
	//messages
	private string[] wordsArray;
	public TextAsset wordsArrayFile;
	
	private string[] wordsArray360 = {"360BLAZITFGT", "LOL360", "tHrEeSiXtY", "<360>", "360sWaG",
					"~x360x~", "360NOSCOPE", "lol 360", "360YOLO", "360"};
	
//	private string[] wordsArray420 = {"420YOLOSWAGMLGBRO", "WHERE ARE MY BOSS BOYS", "8v1 ME FGT", "REKT", "420",
//					"420pwnt", "420yolo"};
	
	private const int MESSAGEARRAYSIZE = 25;
	private const int MESSAGE360ARRAYSIZE = 10;
	private const int MESSAGE420ARRAYSIZE = 7;
	
	//camera object
	private vp_FPCamera playerCamera;
	private GameObject FPSCamera;
	private GameObject GUICamera;
	
	//array of things to rotate
	private GameObject[] objectsToRotate;
	
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
				m_MLGStyle.fontSize = 100;
				m_MLGStyle.font = MLGFont;
				m_MLGStyle.fontStyle = FontStyle.Bold;
				
				
			}
			return m_MLGStyle;
		}
	}
	
	
	// Use this for initialization
	void Start () {
		wordsArray = wordsArrayFile.text.Split('\n');
		mlgEffect = 0;
		FPSCamera = GameObject.Find ("FPSCamera");
		playerCamera = FPSCamera.GetComponent<vp_FPCamera> ();

		/*
		GUICamera = GameObject.Find ("GUICamera");
		GameObject newobject = new GameObject();
		MLGText text = newobject.AddComponent<MLGText>();
		text.drawRotatingText("HELLO", new Vector2(0,0), 100f);
		*/
	}
	
	
	
	void OnGUI () {
		if (blankedScreen)
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture, ScaleMode.StretchToFill, true, 10.0F);
	}
	
	
	void Update () {
		if (shakingScreen)
			playerCamera.ShakeSpeed = shakeStrength;
		else 
			playerCamera.ShakeSpeed = 0;
		
		//test
		if (Input.GetKeyDown (KeyCode.K) && mlgEffect != 3)
			mlgEffect = 3;
		if (mlgEffect == 3) {
			ShakeScreen (5f, .5f);
			mlgEffect = 0;
		}
	}
	
	public void DisplayTexture(Texture texture, Vector2 location, float duration){
		StartCoroutine(TextureDisplayer(texture, location, 1f, duration));
	}
	
	public void DisplayLightShow (float duration, bool flash = false) {
		StartCoroutine (LightShow (0.05f, duration, flash));
	}
	
	
	
	public void PlayDubstep (float duration) {
		if (!playingAudio) {
			StartCoroutine (DubstepPlayer (duration));
			playingAudio = true;
		}	
	}


	public void ShakeScreen(float strength){
		shakingScreen = true;
		shakeStrength = strength;
		if (strength == 0) shakingScreen = false;
	}
	
	
	public void ShakeScreen (float strength, float duration) {
		if (!shakingScreen) {
			shakingScreen = true;
			shakeStrength = strength;
			StartCoroutine (ScreenShaker (duration));	
		}
	}
	
	public void ParticleShow (Vector3 location, float duration) {
		StartCoroutine (ParticleShower (location, duration));
	}



	public bool DisplayWords (string words) {
		return DisplayWords (words, 1f, new Vector2(0,0), 50, 0);
	}

	public bool DisplayWords (string words, float duration) {
		return DisplayWords (words, duration, new Vector2(0,0), 50, 0);
	}

	public bool DisplayWords (string words, float duration, Vector2 location) {
		return DisplayWords (words, duration, location, 50, 0);
	}

	public bool DisplayWords (string words, float duration, Vector2 location, int size) {
		return DisplayWords (words, duration, location, size, 0);
	}

	public bool DisplayWords (string words, float duration, Vector2 location, int size, float rotationSpeed) {
		if (displayingWords) return false;
		StartCoroutine (WordDisplayer (words, duration, location, size, rotationSpeed));
		return true;
	}

	
	public void DisplayRandomWords(){
		Vector2 location = new Vector2(Random.Range(-Screen.width/2, Screen.width/2), Random.Range (-Screen.height/2, Screen.height/2));
		float rotation, duration;
		int size;
rotation = 0;
		size = Random.Range (10, 100);
		Color color = new Color(Random.Range (.9f, 1f), Random.Range (.9f, 1f), Random.Range(.9f, 1f));
		duration = Random.Range (.01f, .5f);
		displayingWords = false;
		DisplayRandomWords(rotation, size, location, duration, color);

	}


	public void DisplayRandomWords (float rotation, int size, Vector2 location, float duration, Color? c = null) {
		string message;
		if (rotation > 330 && rotation < 390)
			message = wordsArray[Random.Range (0, wordsArray.Length)];
		else
			message = wordsArray[Random.Range (0, wordsArray.Length)];
		StartCoroutine (WordDisplayer (message, duration, location, size, rotation, true, c));
	}
	
	public void PulseFog(Color color, float fadein, float sustain, float fadeout){
		if (!pulsingFog)
			StartCoroutine(FogPulsar(color, fadein, sustain, fadeout));
	}
	
	
	public void SlowTime (float strength, float duration, float fadeout) {
		if (!timeSlowed) {
			Time.timeScale = strength;
		}
	}


	public void ZoomFov (float to, float seconds, bool stoppable = false){
		if (!zoomingFov){
			StartCoroutine (Zoomer( to, seconds));
		}
		else if (stoppable) {
			cancelZoom = true;
			StartCoroutine (Zoomer (to, seconds));
				
		}
	}

	public void IncreaseLights(float to, float seconds){
		StartCoroutine (LightIncreaser(to, seconds));
	}

	public void BlankScreen(Color color, float seconds = 0f){
		StartCoroutine(ScreenBlanker(color, seconds));
	}


	////////////////////////////////////////////////////////////////////////////////////////////////

	
	
	IEnumerator TextureDisplayer(Texture texture, Vector2 location, float size, float duration){
		GUI.DrawTexture(new Rect(location.x, location.y, Screen.width*size, Screen.height*size), texture, ScaleMode.StretchToFill);

		yield return new WaitForSeconds(duration);
	}
	
	IEnumerator FogPulsar(Color color, float fadein, float sustain, float fadeout){
		pulsingFog = true;
		transform.GetComponent<FogEffects>().Pulse(color, fadein, sustain, fadeout);
		yield return new WaitForSeconds(fadein+sustain+fadeout+fadeout);
		pulsingFog = false;
	}
	
	IEnumerator WordDisplayer (string words, float duration, Vector2 location, int size, float rotationSpeed, bool backgroundeffect = false,  Color? c = null) {
		if (!backgroundeffect)
		displayingWords = true;
		GameObject textObject = new GameObject("text");
		
		MLGText mlgtext = textObject.AddComponent<MLGText>();
		mlgtext.SetColor(c.HasValue ? c.Value : new Color(.12f, .12f, .12f, .5f));
		if (rotationSpeed == 0)
			mlgtext.drawText(words, size, location);
		else mlgtext.drawRotatingText(words, location, rotationSpeed);


		/*
		GameObject textObject2 = (GameObject)Instantiate(ttext1, FPSCamera.transform.position + FPSCamera.transform.forward * 5, FPSCamera.transform.rotation);
		
		Vector3 difference = new Vector3(location.x, location.y, 0);
		textObject.transform.position += difference;
		
		textObject2.transform.parent = FPSCamera.transform;
		//textObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 5;
		
		textObject2.GetComponent<TextMesh>().text = words;
		textObject2.GetComponent<EffectManipulator>().Rotate(duration, rotationSpeed);
		 */
		yield return new WaitForSeconds(duration);
		Destroy (textObject);
		displayingWords = false;
		//Destroy (textObject2);
	}
		
	/*
	IEnumerator WordDisplayer (string words, Vector3 location, float duration, float rotation) {
		GameObject textObject = (GameObject)Instantiate(ttext1, location, playerCamera.transform.rotation);
		textObject.transform.parent = transform;
		textObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 5;
		textObject.GetComponent<TextMesh> ().text = words;
		for (int i = 0; i < rotation; i++){
			textObject.transform.rotation += rotation / duration / rotation;
			yield return new WaitForSeconds(rotation / duration);
		}
			
		yield return new WaitForSeconds(duration);
		Destroy (textObject);
	}
	*/
	
	
	IEnumerator ParticleShower (Vector3 location, float duration) {
		GameObject particles = (GameObject)Instantiate(SuperParticle, location, Quaternion.identity);
		yield return new WaitForSeconds(duration);
		Destroy (particles);
	}
	
	
	
	IEnumerator ScreenShaker (float duration) {
		yield return new WaitForSeconds(duration);
		shakingScreen = false;
	}
	
	
	
	IEnumerator DubstepPlayer (float duration) {
		audio.clip = dubstep;
		audio.Play ();
		yield return new WaitForSeconds(duration);
		audio.Stop ();
		playingAudio = false;
	}
	
	
	
	IEnumerator LightShow (float freq, float seconds, bool flash = false) {
		GameObject lightGameObject = new GameObject ("The Light");
		lightGameObject.AddComponent<Light>();
		lightGameObject.transform.position = transform.position;
		lightGameObject.GetComponent<Light>().intensity = 1;
		lightGameObject.GetComponent<Light>().range = 20;
		
		int numFlares = Random.Range(2,5);
		GameObject [] flares = new GameObject[numFlares];
		for (int i = 0; i < numFlares; i++){
			flares[i] = (GameObject)Instantiate(flare1, transform.position, Quaternion.identity);
			flares[i].transform.position = playerCamera.transform.position + playerCamera.transform.forward * 5;
			flares[i].transform.position += new Vector3((Random.value-.5f)*5, (Random.value-.5f)*5, (Random.value-.5f)*5);
		}
			
		for (int i = 0; i < seconds*100; i++) {
			for (int j = 0; j < numFlares; j++)
				flares[j].transform.position += new Vector3((Random.value-.5f)/2, (Random.value-.5f)/2, (Random.value-.5f)/2);
			if (flash)
				lightGameObject.GetComponent<Light>().intensity = (1/seconds)*Mathf.Sin(i);
			lightGameObject.GetComponent<Light>().color = new Color (Mathf.Sin (freq * i + 0) * 127.0f + 20, 
				Mathf.Sin (freq * i + 2.0f) * 127 + 20, Mathf.Sin (freq * i + 4) * 127 + 20);
			lightGameObject.transform.parent = transform;
			yield return new WaitForSeconds(.01f);
		}
		Destroy (lightGameObject);
		for (int i = 0; i < numFlares; i++){
			Destroy(flares[i]);
		}
	}
		

	IEnumerator Zoomer(float to, float seconds){
		bool cancelThis = true;
		if (zoomingFov) cancelThis = false;

		zoomingFov = true;
		float originalFov = FPSCamera.camera.fieldOfView;
		for (float t = 0; t < seconds; t+=Time.deltaTime){
			if (cancelZoom && cancelThis) {
				cancelZoom = false;
				zoomingFov = false;
				yield break;
			}
			FPSCamera.camera.fieldOfView = Mathf.SmoothStep(originalFov, to, t/seconds);
			yield return null;
		}
		zoomingFov = false;
	}

	IEnumerator LightIncreaser(float to, float seconds){
		float originalIntensity = lightToIncrease.intensity;
		for (float t = 0; t < seconds; t+=Time.deltaTime){
			lightToIncrease.intensity = Mathf.SmoothStep(originalIntensity, to, t/seconds);
			yield return null;
		}
	}

	IEnumerator ScreenBlanker(Color color, float seconds){
		blankedScreen = true;
		if (seconds != 0){
			yield return new WaitForSeconds(seconds);
			blankedScreen = false;
		}
		yield return null;

	}

}
