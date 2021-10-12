If a mod uses only pre-existing game mechanics (like [Food-Pack](https://github.com/ds5678/Food-Pack)), it can be created without writing a single line of code. Writing code will only be required for creating new game mechanics.

# Boot Strapping

The `AutoMapper` will scan the directory "mods/auto-mapped" (recursively) and try to handle all files encountered.

The following file extensions are supported:

* `.unity3d`: This will be treated as an asset bundle and loaded accordingly.  
The assets in the bundle will be mapped and made available.
* `.json` : This will be treated as the details for an item inside an asset. If an item does not have a corresponding json file (or the json is incorrectly named), it will cause an error.
* `.bnk`: This will be treated as a sound bank and loaded accordingly.  
The sounds (event names) in bank will be made available.
* `.dll`: This will be treated as an assembly and loaded accordingly.  
The classes in the assembly will NOT be loaded or inspected for further bootstrapping. If you need bootstrapping from your assembly (e.g. harmony) put it directly in the "mods" directory, so MelonLoader will handle it.

All other files in `auto-mapped` will be ignored!