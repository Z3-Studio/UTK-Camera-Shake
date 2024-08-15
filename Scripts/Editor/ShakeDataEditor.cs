using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;
using Cinemachine;
using Z3.UIBuilder.Editor;

namespace Z3.CameraShake.Editor
{
    /// <summary>
    /// Used to create the Noise Settings and display
    /// </summary>
    [CustomEditor(typeof(ShakeData))]
    public class ShakeDataEditor : Z3Editor<ShakeData>
    {
        [MenuItem("Assets/Create/Z3/Shake Data", false, 0)]
        public static void CreateColorGradient(MenuCommand _)
        {
            // Create assets
            ShakeData shakeData = CreateInstance<ShakeData>();
            NoiseSettings noiseProfile = CreateInstance<NoiseSettings>();
            noiseProfile.name = nameof(NoiseSettings);

            // Set Noise Settings Field
            shakeData.GetType()
                .GetField(ShakeData.NoiseSettingsField, BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(shakeData, noiseProfile);

            // Get path and name
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{path}/{nameof(ShakeData)}.asset");

            // Create asset and insert the NoiseSettings as SubAsset
            AssetDatabase.CreateAsset(shakeData, assetPathAndName);
            AssetDatabase.AddObjectToAsset(noiseProfile, shakeData);

            // Save
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = base.CreateInspectorGUI();

            TitleView.AddTitle(inspector, "Noise Settings");

            VisualElement noiseSettings = Target.NoiseSettings.ToVisualElement();
            inspector.Add(noiseSettings);

            return inspector;
        }
    }
}