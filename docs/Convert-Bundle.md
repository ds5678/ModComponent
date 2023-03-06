## How to convert your old asset bundle & modcomponent file for use with Addressables.

First, Install the "**Addressables**" unity package.\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/install_addressables_.png)

Goto "**Window**" -> "**Asset Management**" -> "**Addressables**" -> "**Settings**"\
You will ge tthe following prompt.\
Click "**Create Addressables Settings**"\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/create_settings.png)\

You will then get this prompt\
Go ahead and "**Convert**"\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/convert_bundle.png)\

Once that is completed\
Goto "**Window**" -> "**Asset Management**" -> "**Addressables**" -> "**Groups**"\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/groups.png)\

Remove any **Localization** files in the bundle (they are now added into the .modcomponent zip file)\
Right click on the group and select "**Simplify Andressable Names**"\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/simplify_names.png)\

The list should look like this once it's done.\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/simplify_done.png)\

Goto **Window** -> **Asset Management** -> **Addressables** -> **Profiles**\
Change **Local** from **Built-in** to **Custom**\
And change **BuildPath** & **LoadPath** to **.\AssetBundles\**\
(any folder will work honestly, this is where the built files will end up)\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/default_profile.png)\

Goto "**Window**" -> "**Asset Management**" -> "**Addressables**" -> "**Settings**"\
And under **Catalog** change **Player Version Override** to your preferred name\
(this will change the naming of the bundle files, *must not conflict with other bundles)\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/override_name.png)\

Under **Content Update** make sure **Build & Load Paths** is set to "**Local**"\
(the values will inherit from the default profile we changed)\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/build_remote_catalog.png)\

Select the asset group from the **Addressables Groups** window\
And then in the inspector change **Bundle Naming Mode** to **Filename**\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/set_naming_mode.png)

At the top of the **Addressables Groups** window\
Select "**Build**" -> "**New Build**" -> "**Default Build Script**"\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/build_bundle.png)

Congrats! You should now have the bundle files built in the **BuildPath** folder\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/bundle_files.png)

In your .modcomponent file create two new folders **bundle** and **localizations**\
Copy your new bundle files into **bundle** ignoring the .hash file\
Then copy your localization files into **localizations** \
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/zip_folders.png)

Then modifiy any blueprints you might have from the old format to the new CraftingRevisions format\
**OLD**\
![](https://raw.githubusercontent.com/dommrogers/ModComponent/master/Images/convert-bundle/blueprint_changes.png)\
**NEW**\

## Congrats, you have now converted your ModComponent bundle.

