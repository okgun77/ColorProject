using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectController : MonoBehaviour
{
    public GameObject[] objectsToToggle;
    public Material[] materialsToToggle; // 토글할 머티리얼 배열
    public SkinnedMeshRenderer[] skinnedMeshRenderers; // Skinned Mesh Renderer 배열

    public float toggleInterval = 3f;
    
    void Start()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleObjects();
            ToggleMaterials();
        }
        
        // 3초마다 ToggleObjects와 ToggleMaterials 함수를 반복 호출
        // InvokeRepeating("ToggleObjects", toggleInterval, toggleInterval);
        // InvokeRepeating("ToggleMaterials", toggleInterval, toggleInterval);
    }

    void ToggleObjects()
    {
        foreach (var obj in objectsToToggle)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    void ToggleMaterials()
    {
        foreach (var renderer in skinnedMeshRenderers)
        {
            Material[] currentMaterials = renderer.materials;

            foreach (var toggleMaterial in materialsToToggle)
            {
                int materialIndex = System.Array.IndexOf(currentMaterials, toggleMaterial);

                if (materialIndex != -1)
                {
                    // 머티리얼이 있으면 제거
                    var newMaterials = currentMaterials.ToList();
                    newMaterials.RemoveAt(materialIndex);
                    renderer.materials = newMaterials.ToArray();
                }
                else
                {
                    // 머티리얼이 없으면 추가
                    var newMaterials = currentMaterials.ToList();
                    newMaterials.Add(toggleMaterial);
                    renderer.materials = newMaterials.ToArray();
                }
            }
        }
    }
}