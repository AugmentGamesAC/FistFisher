using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Highlighter : MonoBehaviour
{
	[SerializeField]
	private Material m_HighlightMaterial;
	[SerializeField]
	private short m_MaterialID = 1;
	[SerializeField]
	private MeshRenderer m_MeshRenderer;

    public void Highlight(bool state)
    {
		m_MeshRenderer = GetComponent<MeshRenderer>();
		Material[] materials = m_MeshRenderer.GetComponent<Renderer>().materials;
		if (m_MaterialID < 0 || materials.Length-1 > m_MaterialID) return;
		materials[m_MaterialID] = state ? m_HighlightMaterial : null;
		m_MeshRenderer.GetComponent<Renderer>().materials = materials;
	}
}
