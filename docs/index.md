# ModComponent

ModComponent is an infrastructure library for modding [The Long Dark](http://www.thelongdark.com/) by [Hinterland Studio Inc.](http://hinterlandgames.com/)

This library itself does not provide or change any game items or game mechanics, but rather provides a framework for mods to make it easier to create new items and mechanics and integrate them properly into the game.

# Why Is This Even Required?

Any new game item must be configured in a specific way or it won't work with the already existing logic in the game. E.g. any consumable food item is expected to define its number of calories in a specific [Component](https://docs.unity3d.com/Manual/Components.html) carrying a specific field. If that component is not present or does not have that field, the game will not recognize the item as consumable food.

There are also numerous functions that operate on the game's items and some of them would simply ignore any new item, because of the way they were written. Using [Harmony](https://github.com/pardeike/Harmony) those methods can be "patched" to make them aware of new items and allow proper results.

This must be done for food, tools, weapons, etc. and each of those types of items needs different patches and different components. Figuring this out is often not easy and can take a lot of time and trial-and-error.
Instead of having every modder doing this, I decided to create ModComponent to do the integration only once and then provide a (rather simple) API for others to use.

Ideally this will also decouple mods that use ModComponent from the internal working of TLD, so that changes in Hinterland's code would only need to be addressed in ModComponent and not in the actual mods themselves (repair one, fix all)

# Contents

## Feature Explanations

[Features](Features.md)

[Alcohol](Alcohol.md)

[Alternative Actions](Alternative-Actions.md)

[Blueprints](Blueprints.md)

[Gear Spawns](Gear-Spawns.md)

## Lists

[Item Names](Item-Names.md)

[Localizations](Localizations.md)

[Loot Tables](Loot-Tables.md)

[Scenes](Scenes.md)

[Skill Type List](Skill-Type-List.md)

[Sound Names](Sound-Names.md)

## Developer Pages

[Setup and Basic Configuration](Setup-and-Basic-Configuration.md)

[For Developers](For-Developers.md)

[3D Models](3D-Models.md)

[Architecture](Architecture.md)

[Auto Mapper](Auto-Mapper.md)

[Basic Item Configuration](Basic-Item-Configuration.md)

[Clothing Item Configuration](Clothing-Item-Configuration.md)

[Item Mod Tutorial](Item-Mod-Tutorial.md)

## Component JSON Documentation

[Basic Information about Components](Basic-Information-about-Components.md)

[Generic Component Documentation](Generic-Component-Documentation.md)

[Bed Component Documentation](Bed-Component-Documentation.md)

[Body Harvest Component Documentation](Body-Harvest-Component-Documentation.md)

[Charcoal Component Documentation](Charcoal-Component-Documentation.md)

[Clothing Component Documentation](Clothing-Component-Documentation.md)

[Cookable Component Documentation](Cookable-Component-Documentation.md)

[Cooking Pot Component Documentation](Cooking-Pot-Component-Documentation.md)

[Collectible Component Documentation](Collectible-Component-Documentation.md)

[Explosive Component Documentation](Explosive-Component-Documentation.md)

[First Aid Component Documentation](First-Aid-Component-Documentation.md)

[Food Component Documentation](Food-Component-Documentation.md)

[Generic Equippable Component Documentation](Generic-Equippable-Component-Documentation.md)

[Liquid Component Documentation](Liquid-Component-Documentation.md)

[Powder Component Documentation](Powder-Component-Documentation.md)

[Purification Component Documentation](Purification-Component-Documentation.md)

[Random Item Component Documentation](Random-Item-Component-Documentation.md)

[Random Weighted Item Component Documentation](Random-Weighted-Item-Component-Documentation.md)

[Research Component Documentation](Research-Component-Documentation.md)

[Rifle Component Documentation](Rifle-Component-Documentation.md)

[Tool Component Documentation](Tool-Component-Documentation.md)

## Behaviour JSON Documentation

[Basic Information about Behaviours](Basic-Information-about-Behaviours.md)

[Accelerant Behaviour Documentation](Accelerant-Behaviour-Documentation.md)

[Burnable Behaviour Documentation](Burnable-Behaviour-Documentation.md)

[Carrying Capacity Behaviour Documentation](Carrying-Capacity-Behaviour-Documentation.md)

[Evolve Behaviour Documentation](Evolve-Behaviour-Documentation.md)

[Firestarter Behaviour Documentation](Firestarter-Behaviour-Documentation.md)

[Harvestable Behaviour Domentation](Harvestable-Behaviour-Domentation.md)

[Millable Behaviour Documentation](Millable-Behaviour-Documentation.md)

[Repairable Behaviour Documentation](Repairable-Behaviour-Documentation.md)

[Scent Behaviour Documentation](Scent-Behaviour-Documentation.md)

[Sharpenable Behaviour Documentation](Sharpenable-Behaviour-Documentation.md)

[Stackable Behaviour Documentation](Stackable-Behaviour-Documentation.md)

[Tinder Behaviour Documentation](Tinder-Behaviour-Documentation.md)
