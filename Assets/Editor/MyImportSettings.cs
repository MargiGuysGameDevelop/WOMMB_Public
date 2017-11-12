using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class MyImportSetting : AssetPostprocessor{

	void OnPostprocessModel (GameObject _o) {

		ModelImporter modelImporter = assetImporter as ModelImporter;

		var times = DateTime.Now.ToString ("h:mm:ss tt");

		Debug.Log ("匯入模型"+_o.name+ "於" + times +"!");

		modelImporter.materialName = ModelImporterMaterialName.BasedOnMaterialName;
		modelImporter.materialSearch = ModelImporterMaterialSearch.Everywhere;


		if (modelImporter.clipAnimations.Length != 0) {
			Debug.Log ("總共有"+modelImporter.clipAnimations.Length.ToString()+"個動作!");
			foreach(ModelImporterClipAnimation clip in modelImporter.clipAnimations){
				clip.loop = true;
                clip.loopTime = true;
				clip.wrapMode = WrapMode.Loop;
				clip.lockRootRotation = true;
                clip.keepOriginalPositionY = clip.keepOriginalPositionXZ = clip.keepOriginalOrientation = true;
			}
		}
	}

    //Material OnAssignMaterialModel(Material material,Renderer renderer) {

    //    // Find if there is a material at the material path
    //    // Turn this off to always regeneration materials
    //    //if (AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material)))
    //    //    return AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material));

    //    // Create a new material asset using the specular shader
    //    // but otherwise the default values from the model
    //    material.shader = Shader.Find("Mobile/Diffuse");
    //    AssetDatabase.CreateAsset(material, "Assets/" + material.name + ".mat");
    //    return material;
    //}
}
