using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Game.UI
{
    public class InvertedMaskImage : Image
    {
        private static readonly int STENCIL_COMP = Shader.PropertyToID("_StencilComp");

        public override Material materialForRendering
        {
            get
            {
                Material result = new Material(base.materialForRendering);
                result.SetInt(STENCIL_COMP, (int)CompareFunction.NotEqual);
                return result;
            }
        }
    }
}