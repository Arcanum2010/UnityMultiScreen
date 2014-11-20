using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class SendToPlugin : MonoBehaviour {

	public RenderTexture renderTexture;

//	GCHandle pixelsHandle;

//	Color[] pixels;

	Texture2D tex;
	
	[DllImport ("SendTexture")]
	#if UNITY_GLES_RENDERER
		private static extern void SetTextureFromUnity(IntPtr texture, int w, int h);
	#else
		private static extern void SetTextureFromUnity(IntPtr texture);
	#endif



	void Start () {
		RenderTexture.active = renderTexture;
		tex = new Texture2D(renderTexture.width, renderTexture.height);

		UpdatePixelsFromRenderTexture ();

		#if UNITY_GLES_RENDERER
			SetTextureFromUnity (tex.GetNativeTexturePtr(), tex.width, tex.height);
		#else
			SetTextureFromUnity (tex.GetNativeTexturePtr());
		#endif

//		pixelsHandle = GCHandle.Alloc (pixels, GCHandleType.Pinned);
	}

	void OnPostRender() {
		UpdatePixelsFromRenderTexture ();
	//	SendTexture (pixelsHandle.AddrOfPinnedObject (), renderTexture.width, renderTexture.height);
	}

	void UpdatePixelsFromRenderTexture() {
		tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		tex.Apply();

//		pixels = tempTex.GetPixels();
	}

	void OnDestroy() {
	//	pixelsHandle.Free ();
	}
}