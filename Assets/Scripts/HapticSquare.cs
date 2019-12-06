using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TanvasTouch.Model;

//[System.Serializable]
public class HapticSquare : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;
    private HapticServiceAdapter hapticServiceAdapter;
    private HapticView hapticView;
    private HapticTexture hapticTexture;
    private HapticMaterial hapticMaterial;
    private HapticSprite hapticSprite;
    private static Dictionary<string, HapticSprite> hapticSprites = new Dictionary<string, HapticSprite>();


    //Called at start of application.
    void Start()
    {
        Debug.Log("executing haptics script");
        //_camera = Camera.main; // the camera tagged "MainCamera" 

        //Connect to the service and begin intializing the haptic resources.
        InitHaptics();
    }


    public sealed class HapticType
    {

        // circuit versions
        public static readonly string CHECKERS = "Textures/options/checker-circuit";

        public static readonly string NOISEHIGH = "Textures/options/noise-sharp-circuit";
        public static readonly string NOISEMED = "Textures/options/noise-original-circuit";
        public static readonly string NOISELOW = "Textures/options/noise-blur-circuit";

        public static readonly string STRIPEHIGH = "Textures/options/stripe-circuit";
        public static readonly string STRIPEMED = "Textures/options/stripe-medium-circuit";
        public static readonly string STRIPELOW = "Textures/options/stripe-soft-circuit";

        public static readonly string STRIPEHIGH2 = "Textures/options/stripe-d1mm-circuitv2";
        public static readonly string STRIPEMED2 = "Textures/options/stripe-d4mm-circuitv2";
        public static readonly string STRIPELOW2 = "Textures/options/stripe-d8mm-circuitv2";

        public static readonly string DOTS = "Textures/options/dots-circuit";

        //public static readonly string BAR01 = "Textures/bars/stripe-density1mm";
        //public static readonly string BAR02 = "Textures/bars/stripe-density2mm";
        //public static readonly string BAR03 = "Textures/bars/stripe-density4mm";
        //public static readonly string BAR04 = "Textures/bars/stripe-density8mm";

        //public static readonly string BAR11 = "Textures/bars/stripe-pulse";
        //public static readonly string BAR12 = "Textures/bars/stripe-pulse-blur2";
        //public static readonly string BAR13 = "Textures/bars/stripe-pulse-blur4";

        //public static readonly string BAR21 = "Textures/bars/stripe-pulse";
        //public static readonly string BAR22 = "Textures/bars/lines-blurW10";
        //public static readonly string BAR23 = "Textures/bars/lines-blurW20";

        //public static readonly string BAR33 = "Textures/bars/lines-blursoft";

        public static readonly string BARA = "Textures/options/finals/stripes-strong-bar";
        public static readonly string BARB = "Textures/options/finals/stripes-medium-bar";
        public static readonly string BARC = "Textures/options/finals/stripes-weak-bar";

        // raw versions
        public static readonly string STRIPEHIGHV = "Textures/options/finals/stripes-strong";
        public static readonly string STRIPEMEDV = "Textures/options/finals/stripes-medium";
        public static readonly string STRIPELOWV = "Textures/options/finals/stripes-weak";

        public static readonly string STRIPEHIGHH = "Textures/options/finals/stripes-strong-horizontal";
        public static readonly string STRIPEMEDH = "Textures/options/finals/stripes-medium-horizontal";
        public static readonly string STRIPELOWH = "Textures/options/finals/stripes-weak-horizontal";

        /*
        public static readonly string CHECKERS = "Textures/options/raw/checker";

        public static readonly string NOISEHIGH = "Textures/options/raw/noise-sharp";
        public static readonly string NOISEMED = "Textures/options/raw/noise-original";
        public static readonly string NOISELOW = "Textures/options/raw/noise-blur";

        public static readonly string DOTS = "Textures/options/raw/dots";
        */

    }

    void Update()
    {

        if (hapticView != null)
        {
            //Ensure haptic view orientation matches current screen orientation.
            hapticView.SetOrientation(Screen.orientation);


            //Retrieve x and y position of square.
            Mesh _mesh = gameObject.GetComponent<MeshFilter>().mesh;
            Vector3[] _meshVerts = _mesh.vertices;
            for (var i = 0; i < _mesh.vertexCount; ++i)
            {
                _meshVerts[i] = _camera.WorldToScreenPoint(gameObject.transform.TransformPoint(_meshVerts[i]));
            }

            //Set the size and position of the haptic sprite to correspond to square.
            hapticSprite.SetPosition((int)(_meshVerts[0].x), (int)(_meshVerts[0].y));
            hapticSprite.SetSize((double)_meshVerts[1].x - _meshVerts[0].x, (double)_meshVerts[1].y - _meshVerts[0].y);

        }
    }

    void InitHaptics()
    {
        //Get the service adapter
        hapticServiceAdapter = HapticServiceAdapter.GetInstance();

        if (hapticServiceAdapter != null)
        {
            //Create the haptic view with the service adapter instance and then activate it.
            hapticView = HapticView.Create(hapticServiceAdapter);
            hapticView.Activate();
            hapticView.SetOrientation(Screen.orientation); //Set orientation of haptic view based on screen orientation.
        }

    }

    public void UpdateHaptics(string type)
    {

        if (hapticView != null)
        {
            hapticView.RemoveSprite(hapticSprite);

            if (hapticSprites.ContainsKey(type))
            {
                hapticSprite = hapticSprites[type];
                hapticView.AddSprite(hapticSprite);
            }
            else
            {
                AddHapticSprite(type);
            }
        }
    }

    private void AddHapticSprite(string type)
    {
        //HapticServiceAdapter hapticServiceAdapter = HapticServiceAdapter.GetInstance();

        Texture2D texture = Resources.Load(type) as Texture2D;
        byte[] textureData = TanvasTouch.HapticUtil.CreateHapticDataFromTexture(texture, TanvasTouch.HapticUtil.Mode.Brightness);

        HapticTexture hapticTexture = HapticTexture.Create(hapticServiceAdapter);
        hapticTexture.SetSize(texture.width, texture.height);
        hapticTexture.SetData(textureData);

        HapticMaterial hapticMaterial = HapticMaterial.Create(hapticServiceAdapter);
        hapticMaterial.SetTexture(0, hapticTexture);

        hapticSprite = HapticSprite.Create(hapticServiceAdapter);
        hapticSprite.SetMaterial(hapticMaterial);
        //hapticSprite.SetSize(texture.width, texture.height);
        //hapticSprite.SetPosition((Screen.width - texture.width) / 2, ((int)transform.localPosition.y) + (Screen.height - texture.height) / 2);

        hapticView.AddSprite(hapticSprite);

        hapticSprites.Add(type, hapticSprite);

    }

    public void SetEnabled(bool enabled)
    {
        if (hapticSprite != null)
        {
            hapticSprite.SetEnabled(enabled);
        }
    }

    // maybe switch from these two functions to SetEnabled function
    public void ActivateHaptic()
    {
        if (hapticView != null)
        {
            hapticView.Activate();
        }
    }

    public void DeactivateHaptic()
    {
        if (hapticView != null)
        {
            hapticView.Deactivate();
        }
    }

    void OnDestroy()
    {
        if (hapticView != null) hapticView.Deactivate();
    }
}
