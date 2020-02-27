using nobnak.Gist.Events;
using nobnak.Gist.GLTools;
using nobnak.Gist.Resizable;
using UnityEngine;

[ExecuteAlways]
public class TestDrawTexture : MonoBehaviour {

    [SerializeField]
    protected RenderTextureEvent Created = new RenderTextureEvent();

    [SerializeField]
    protected Texture mainTex;
    [SerializeField]
    protected Texture subTex;

    protected ResizableRenderTexture targetTex;
    protected Copy copy;

    #region unity
    private void OnEnable() {
        targetTex = new ResizableRenderTexture();
        copy = new Copy();

        targetTex.AfterCreateTexture += v => Created.Invoke(v);
    }
    private void OnDisable() {
        if (targetTex != null) {
            targetTex.Dispose();
            targetTex = null;
        }
        if (copy != null) {
            copy.Dispose();
            copy = null;
        }
    }
    private void Update() {
        var c = Camera.main;
        var w = c.pixelWidth;
        var h = c.pixelHeight;

        var s = new Vector2Int(w, h);
        targetTex.Size = s;

        Graphics.Blit(mainTex, targetTex);

        var outputSize = 0.2f;
        var offset = 10f;
#if false
        var prevTex = RenderTexture.active;
        RenderTexture.active = targetTex;
        GL.PushMatrix();
        GL.LoadIdentity();
        GL.LoadPixelMatrix();
        var outputHeight = outputSize * s.y;
        var outputWidth = outputSize * s.x;
        var outputRect = new Rect(10f, 10f + outputHeight, outputWidth, -outputHeight);
        Graphics.DrawTexture(outputRect, subTex);
        GL.PopMatrix();
        RenderTexture.active = prevTex;
#else
        var outputHeight = outputSize * subTex.height / s.y;
        var outputWidth = outputSize * subTex.width / s.x;
        copy.Blit(subTex, targetTex, outputWidth, outputHeight,
            offset / s.y,
            offset / s.x
            );
#endif
    }
#endregion
}
