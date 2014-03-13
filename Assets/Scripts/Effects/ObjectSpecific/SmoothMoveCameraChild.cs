using UnityEngine;
using System.Collections;

public class SmoothMoveCameraChild : MonoBehaviour
{
	
	vp_FPController m_Controller = null;
	
	void Start()
	{
		m_Controller = transform.root.GetComponent<vp_FPController>();
	}
	
	void LateUpdate()
	{
		transform.position = m_Controller.SmoothPosition;
	}
	
}