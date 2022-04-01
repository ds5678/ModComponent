# Shader Substitution

ModComponent has built-in for substituting dummy shaders for Hinterland shaders. You can include these 2 files in your Unity projects to fix shaders at runtime.

Recursive substitution affects a game object and all its children:

```cs
using UnityEngine;

namespace ModComponent.SceneLoader.Shaders
{
    public class SubstituteShadersRecursive : MonoBehaviour
    {
    }
}
```

Single substitution affects only that game object:

```cs
using UnityEngine;

namespace ModComponent.SceneLoader.Shaders
{
    public class SubstituteShadersSingle : MonoBehaviour
    {
    }
}
```

Note that the ripped project shaders have to be prefixed with `Dummy`. For example, `Dummy_LongDark/Diffuse` gets switched to `_LongDark/Diffuse` at runtime. This is to prevent conflicts in `Shader.Find()`.