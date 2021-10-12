# ModComponent

ModComponent is an infrastructure library for modding [The Long Dark](http://www.thelongdark.com/) by [Hinterland Studio Inc.](http://hinterlandgames.com/)

This library itself does not provide or change any game items or game mechanics, but rather provides a framework for mods to make it easier to create new items and mechanics and integrate them properly into the game.

# Why Is This Even Required?

Any new game item must be configured in a specific way or it won't work with the already existing logic in the game. E.g. any consumable food item is expected to define its number of calories in a specific [Component](https://docs.unity3d.com/Manual/Components.html) carrying a specific field. If that component is not present or does not have that field, the game will not recognize the item as consumable food.

There are also numerous functions that operate on the game's items and some of them would simply ignore any new item, because of the way they were written. Using [Harmony](https://github.com/pardeike/Harmony) those methods can be "patched" to make them aware of new items and allow proper results.

This must be done for food, tools, weapons, etc. and each of those types of items needs different patches and different components. Figuring this out is often not easy and can take a lot of time and trial-and-error.
Instead of having every modder doing this, I decided to create ModComponent to do the integration only once and then provide a (rather simple) API for others to use.

Ideally this will also decouple mods that use ModComponent from the internal working of TLD, so that changes in Hinterland's code would only need to be addressed in ModComponent and not in the actual mods themselves (repair one, fix all)