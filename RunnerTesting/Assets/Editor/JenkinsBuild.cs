using UnityEditor;

public class JenkinsBuild
{
   static void PreformBuild()
    {
        string[] __scenes__ = { "Assests/Scenes/SampleScene.unity" };
        BuildPipeline.BuildPlayer(__scenes__, "D:/Git/TestBuild", BuildTarget.StandaloneWindows, BuildOptions.None);
    }
}
