#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using EasyQuestSwitch.Fields;

namespace EasyQuestSwitch.Types
{
    [AddComponentMenu("")]
    public class Type_Material : Type_Base
    {
        [System.NonSerialized]
        private Material type;

        public SharedShader Shader = new SharedShader();
        public SharedColor MainColor = new SharedColor();
        [InspectorName("GPU Instancing")]
        public SharedBool GPUInstancing = new SharedBool();
        private SharedString ShaderPath = new SharedString();

        public override void Setup(Object type)
        {
            Material material = (Material)type;
            Shader.Setup(material.shader);
            ShaderPath.Setup(material.shader.name);
            MainColor.Setup(material.color);
            GPUInstancing.Setup(material.enableInstancing);
        }

        public override void Upgrade(Object type, int currentVersion)
        {
            Material material = (Material)type;
            if (currentVersion < EQS_Data.UpdateFixMatsAndLights)
            {
                MainColor.Setup(material.color);
                GPUInstancing.Setup(material.enableInstancing);
            }

            if (currentVersion < EQS_Data.UpdateAddIOS)
            {
                Shader.iOS = Shader.Quest;
                MainColor.iOS = MainColor.Quest;
                GPUInstancing.iOS = GPUInstancing.Quest;
                ShaderPath.iOS = ShaderPath.Quest;
            }
        }

        public override void Process(Object type, BuildTarget buildTarget)
        {
            Material material = (Material)type;
            if(Shader.Get(buildTarget) != null)
            {
                switch (buildTarget)
                {
                    case BuildTarget.StandaloneWindows:
                        ShaderPath.PC = Shader.Get(buildTarget).name;
                        break;
                    case BuildTarget.Android:
                        ShaderPath.Quest = Shader.Get(buildTarget).name;
                        break;
                    case BuildTarget.iOS:
                        ShaderPath.iOS = Shader.Get(buildTarget).name;
                        break;
                }
            }
            else if(Shader.Get(buildTarget) == null && !string.IsNullOrEmpty(ShaderPath.Get(buildTarget)))
            {
                switch(buildTarget)
                {
                    case BuildTarget.StandaloneWindows:
                        Shader.PC = UnityEngine.Shader.Find(ShaderPath.PC);
                        break;
                    case BuildTarget.Android:
                        Shader.Quest = UnityEngine.Shader.Find(ShaderPath.Quest);
                        break;
                    case BuildTarget.iOS:
                        Shader.iOS = UnityEngine.Shader.Find(ShaderPath.iOS);
                        break;
                }
            }

            if(Shader.Get(buildTarget) == null)
            {
                throw new MissingReferenceException();
            }
            material.shader = Shader.Get(buildTarget);
            material.color = MainColor.Get(buildTarget);
            material.enableInstancing = GPUInstancing.Get(buildTarget);
        }
    }
}
#endif

