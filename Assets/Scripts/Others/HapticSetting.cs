using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TanvasTouch.Model;
using System.IO;

public class HapticSetting : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;
    private HapticServiceAdapter mHapticServiceAdapter;
    private HapticView mHapticView;
    private HapticTexture mHapticTexture;
    private HapticMaterial mHapticMaterial;
    private HapticSprite mHapticSprite;
    //private ScreenOrientation _previousOrientation = ScreenOrientation.Unknown;
    //private int _previousWidth = 0;
    //private int _previousHeight = 0;

    private string imagePath;

    // Called at start of application.
    void Start()
    {
        //Connect to the service and begin intializing the haptic resources.        
        InitHaptics();
        //ShowHapticSprite();
    }

    // Following Start() this is called in a loop.
    void Update()
    {
        if (mHapticView != null)
        {
            //Ensure haptic view orientation matches current screen orientation.
            mHapticView.SetOrientation(Screen.orientation);

            //Retrieve x and y position of component.
            Mesh _mesh = gameObject.GetComponent<MeshFilter>().mesh;
            Vector3[] _meshVerts = _mesh.vertices;
            for (var i = 0; i < _mesh.vertexCount; ++i)
            {
                _meshVerts[i] = _camera.WorldToScreenPoint(gameObject.transform.TransformPoint(_meshVerts[i]));
            }

            //Set the size and position of the haptic sprite to correspond to component's boundry.
            mHapticSprite.SetPosition((int)(_meshVerts[0].x), (int)(_meshVerts[0].y));
            mHapticSprite.SetSize((double)_meshVerts[1].x - _meshVerts[0].x, (double)_meshVerts[1].y - _meshVerts[0].y);
        }

    }

    //initializing the application's haptic resources
    public void InitHaptics()
    {
        Debug.Log("I see haptics");

        //Retrieve texture data from bitmap.
        string imagePath = "";
        //imagePath = "Textures/noise/noise_texture3";

        switch (this.gameObject.name)

        {

            //C1 version: haptics become finer with less current
            
            case "HapticMeshA":
                imagePath = "Textures/test/test1-test";
                Debug.Log("texture is textureA");
                break;
            case "HapticMeshB":
                imagePath = "Textures/test/test2-test";
                Debug.Log("texture is textureB");
                break;
            case "HapticMeshC":
                imagePath = "Textures/test/test2-test";
                Debug.Log("texture is textureC");
                break;
            case "HapticMeshD":
                imagePath = "Textures/test/test3-test";
                Debug.Log("texture is textureD");
                break;
                /*
            //C2 version: haptics become coarser with less current
            case "HapticMeshA":
                imagePath = "Textures/test/test3-test";
                Debug.Log("texture is textureA");
                break;
            case "HapticMeshB":
                imagePath = "Textures/test/test2-test";
                Debug.Log("texture is textureB");
                break;
            case "HapticMeshC":
                imagePath = "Textures/test/test2-test";
                Debug.Log("texture is textureC");
                break;
            case "HapticMeshD":
                imagePath = "Textures/test/test1-test";
                Debug.Log("texture is textureD");
                break;
            */
            // Resistor haptics
            case "HapticMesh1":
                imagePath = "Textures/stripes/stripe1";
                Debug.Log("texture is texture1");
                break;

            case "HapticMesh2":
                imagePath = "Textures/stripes/stripe1two";
                Debug.Log("texture is texture2");
                break;

            case "HapticMesh3":
                imagePath = "Textures/stripes/stripe1three";
                Debug.Log("texture is texture3");
                break;

            case "HapticMesh4":
                imagePath = "Textures/stripes/stripe4";
                Debug.Log("texture is texture4");
                break;

            case "HapticMesh5":
                imagePath = "Textures/dots/dots4";
                break;

        }

        //Get the service adapter
        mHapticServiceAdapter = HapticServiceAdapter.GetInstance();

        //Create the haptic view with the service adapter instance and then activate it.
        mHapticView = HapticView.Create(mHapticServiceAdapter);
        mHapticView.Activate();

        //Set orientation of haptic view based on screen orientation.
        mHapticView.SetOrientation(Screen.orientation);





        /*
        {
            case "HapticMesh1":
                imagePath = "Textures/checker_highres/checker_hr0";
            break;

            case "HapticMesh2":
                imagePath = "Textures/checker_highres/checker_hr1";
            break;

            case "HapticMesh3":
                imagePath = "Textures/checker_highres/checker_hr2";
            break;

            case "HapticMesh4":
                imagePath = "Textures/checker_highres/checker_hr3";
            break;

            case "HapticMesh5":
                imagePath = "Textures/checker_highres/checker_hr4";
            break;

        }
        */
        /*
        {
            case "HapticMesh1":
                imagePath = "Textures/sandpaper/sandpaper0";
            break;

            case "HapticMesh2":
                imagePath = "Textures/sandpaper/sandpaper1";
            break;

            case "HapticMesh3":
                imagePath = "Textures/sandpaper/sandpaper2";
            break;

            case "HapticMesh4":
                imagePath = "Textures/sandpaper/sandpaper3";
            break;

            case "HapticMesh5":
                imagePath = "Textures/sandpaper/sandpaper4";
            break;

        }
        */

        Texture2D _texture = Resources.Load(imagePath) as Texture2D;
        byte[] textureData = TanvasTouch.HapticUtil.CreateHapticDataFromTexture(_texture, TanvasTouch.HapticUtil.Mode.Brightness);

        //Create a haptic texture with the retrieved texture data.
        mHapticTexture = HapticTexture.Create(mHapticServiceAdapter);
        mHapticTexture.SetSize(_texture.width, _texture.height);
        mHapticTexture.SetData(textureData);

        //Create a haptic material with the created haptic texture.
        mHapticMaterial = HapticMaterial.Create(mHapticServiceAdapter);
        mHapticMaterial.SetTexture(0, mHapticTexture);

        //Create a haptic sprite with the haptic material.
        mHapticSprite = HapticSprite.Create(mHapticServiceAdapter);
        mHapticSprite.SetMaterial(mHapticMaterial);

        //Add the haptic sprite to the haptic view.
        mHapticView.AddSprite(mHapticSprite);
    }

    void OnDestroy()
    {
        // EB: troubleshoot later!
        mHapticView.Deactivate();
    }

    void ShowHapticSprite()
    {

        mHapticSprite.SetPosition(0, 0);
        mHapticSprite.SetSize(2048, 1536);
        mHapticView.AddSprite(mHapticSprite);

    }

    void HideHapticSprite()
    {
        mHapticView.RemoveSprite(mHapticSprite);
    }

    public void ActivateHaptic(bool setActive)
    {
        if (mHapticView != null)
        {
            if (setActive) mHapticView.Activate();
            else mHapticView.Deactivate();

        }
        //mHapticSprite.SetEnabled(true);

    }

    


}
