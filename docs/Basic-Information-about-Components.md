# Component Types
There are 17 component types. Each item must have exactly one component in the JSON file. An item may have any number of behaviours in the JSON file.

## ModBedComponent

This component defines a bedroll-like item. Currently disabled.

## ModBodyHarvestComponent

This component defines a carcass item.

## ModClothingComponent

This component defines a wearable clothing item.  
The items from [Clothing-Pack](https://github.com/ds5678/Clothing-Pack) use this component.
> The Ski Goggles use additional custom code because their behaviour is not part of the vanilla game.

## ModCollectibleComponent

This component defines a custom narrative collectible item.

## ModCookableComponent

This component defines an item that can be cooked, but not eaten. When cooking finishes, the item will be replaced with another item.<br/>
The raw long-grain rice from [Food-Pack](https://github.com/ds5678/Food-Pack) uses this component and turns into cooked long-grain rice, which uses a ModFoodComponent.

## ModCookingPotComponent

This component defines items similar to the cooking pot. It is currently disabled.

## ModExplosiveComponent

This is new and still early in development.

## ModFirstAidComponent

This component defines a medical item. Implemented but untested.

## ModFoodComponent

This component defines a consumable food or drink item.<br/>
Most items from [Food-Pack](https://github.com/ds5678/Food-Pack) use this component.

## ModGenericComponent

This component defines a simple item without a primary function.  
Since these items can still be picked up and assigned additional behaviours, they can be useful to design crafting ingredients (like scrap metal), fire starting equipment (like books), and items that can be harvested to yield useful resources (like newspaper).

## ModGenericEquippableComponent

This component defines a simple equippable item.<br/>
The [Binoculars](https://github.com/ds5678/Binoculars) use this component.
> The Binoculars use additional custom code because their behaviour is not part of the vanilla game.

## ModLiquidComponent

This component defines a container of water or kerosene.

## ModPowderComponent

This component defines a container of gunpowder.

## ModRandomItemComponent

This component defines a spawn point for equally probable items.

## ModRandomWeightedItemComponent

This component defines a spawn point for inequally probable items.

## ModRifleComponent

This component defines a custom rifle. Currently disabled.

## ModToolComponent

This component defines a usable tool. These tools can also have custom implementations.

# JSON files

Each item must have exactly one json file which cannot be shared with other items. This json file can only have one component, but any number of behaviours can be included if desirable.

## ModGenericComponent

This component serves as the base for all the others. As a result, its parameters are used when defining the data for any item. However, for brevity and consistency, I have only included the documentation for these parameters on the [[Generic Component Documentation]] page.

## Template JSON files

Each page of documentation will include template json text.