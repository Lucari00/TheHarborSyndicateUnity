using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractTutoClass : MonoBehaviour
{
    public GameObject[] propsObjects;
    public Material AlwaysVisible;
    public Texture textureProp;
    public Material material;
    public bool isFinish;
    public string missionInformationText;

    private List<cakeslice.Outline> outline = new List<cakeslice.Outline>();

    private void Start() {
        isFinish = false;
    }

    public void ObjectVisibleThroughWall() {
        if (propsObjects.Length != 0) {
            AlwaysVisible.SetTexture("_MainTex", textureProp);

            foreach (var obj in propsObjects) {
                Debug.Log(obj.name); 
                cakeslice.Outline o = obj.AddComponent(typeof(cakeslice.Outline)) as cakeslice.Outline;
                Debug.Log(o);
                outline.Add(o);

                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

                // is a character   
                if (renderer == null) {
                    SkinnedMeshRenderer skinRenderer = obj.GetComponent<SkinnedMeshRenderer>();

                    material = skinRenderer.material;
                    skinRenderer.material = AlwaysVisible;
                    return;
                }

                material = renderer.material;
                renderer.material = AlwaysVisible;
            }

            
        }
        
    }

    public void ObjectHiddenThroughWall() {
        if (propsObjects.Length != 0) {
            int i = 0;
            foreach (var obj in propsObjects) {
                Destroy(outline[i]);
                i++;

                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                if (renderer == null) {
                    SkinnedMeshRenderer skinRenderer = obj.GetComponent<SkinnedMeshRenderer>();
                    skinRenderer.material = material;
                    return;
                }
                renderer.material = material;
            }
            
        }
    }
}
