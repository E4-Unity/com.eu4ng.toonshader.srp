using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

namespace Unity.Rendering.Toon
{
    public class UnityToonShaderOutline : RenderObjects
    {
        /* 필드 */
        bool m_IsInitialized = false;
        
        /* ScriptableRendererFeature 인터페이스 */
        public override void Create()
        {
            // 처음 생성 시에만 초기화
            if (!m_IsInitialized)
            {
                m_IsInitialized = true;
                
                // RenderObjectsSettings 설정
                settings.Event = RenderPassEvent.BeforeRenderingOpaques; // Outline 을 Opaques 보다 먼저 렌더링
                settings.overrideMode = RenderObjectsSettings.OverrideMaterialMode.None;

                // RenderObjectsSettings.FilterSettings 설정
                settings.filterSettings.LayerMask = ~0; // Everything
                settings.filterSettings.PassNames = new[] { "Outline" };
            }

            base.Create();
        }
    }
}
