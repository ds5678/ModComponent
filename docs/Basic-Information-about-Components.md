# Component Types
There are 17 component types. Each item must have exactly one component in the JSON file. An item may have any number of behaviours in the JSON file.

## ModBedComponent
[JSON Document](Bed-Component-Documentation.md) <br>
This component defines a bedroll-like item. Currently disabled.

## ModBodyHarvestComponent
[JSON Document](Body-Harvest-Component-Documentation.md) <br>
This component defines a carcass item.

## ModCharcoalComponent
[JSON Document](Charcoal-Component-Documentation.md) <br>
This component defines a charcoal item.

## ModClothingComponent
[JSON Document](Clothing-Component-Documentation.md) <br>
This component defines a wearable clothing item.  
The items from [Clothing-Pack](https://github.com/ds5678/Clothing-Pack) use this component.
> The Ski Goggles use additional custom code because their behaviour is not part of the vanilla game.

## ModCollectibleComponent
[JSON Document](Collectible-Component-Documentation.md) <br>
This component defines a custom narrative collectible item.

## ModCookableComponent
[JSON Document](Cookable-Component-Documentation.md) <br>
This component defines an item that can be cooked, but not eaten. When cooking finishes, the item will be replaced with another item.<br/>
The raw long-grain rice from [Food-Pack](https://github.com/ds5678/Food-Pack) uses this component and turns into cooked long-grain rice, which uses a ModFoodComponent.

## ModCookingPotComponent
[JSON Document](Cooking-Pot-Component-Documentation.md) <br>
This component defines items similar to the cooking pot. It is currently disabled.

## ModExplosiveComponent
[JSON Document](Explosive-Component-Documentation.md) <br>
This is new and still early in development.

## ModFirstAidComponent
[JSON Document](First-Aid-Component-Documentation.md) <br>
This component defines a medical item. Implemented but untested.

## ModFoodComponent
[JSON Document](Food-Component-Documentation.md) <br>
This component defines a consumable food or drink item.<br/>
Most items from [Food-Pack](https://github.com/ds5678/Food-Pack) use this component.

## ModGenericComponent
[JSON Document](Generic-Component-Documentation.md) <br>
This component defines a simple item without a primary function.  

Since these items can still be picked up and assigned additional behaviours, they can be useful to design crafting ingredients (like scrap metal), fire starting equipment (like books), and items that can be harvested to yield useful resources (like newspaper).

## ModGenericEquippableComponent
[JSON Document](Generic-Equippable-Component-Documentation.md) <br>
This component defines a simple equippable item.

The [Binoculars](https://github.com/ds5678/Binoculars) use this component.
> The Binoculars use additional custom code because their behaviour is not part of the vanilla game.

## ModLiquidComponent
[JSON Document](Liquid-Component-Documentation.md) <br>
This component defines a container of water or kerosene.

## ModPowderComponent
[JSON Document](Powder-Component-Documentation.md) <br>
This component defines a container of gunpowder.

## ModPurificationComponent
[JSON Document](Purification-Component-Documentation.md) <br>
This component defines a water purification item.

## ModRandomItemComponent
[JSON Document](Random-Item-Component-Documentation.md) <br>
This component defines a spawn point for equally probable items.

## ModRandomWeightedItemComponent
[JSON Document](Random-Weighted-Item-Component-Documentation.md) <br>
This component defines a spawn point for inequally probable items.

## ModResearchComponent
[JSON Document](Research-Component-Documentation.md) <br>
This component defines a research book.

## ModRifleComponent
[JSON Document](Rifle-Component-Documentation.md) <br>
This component defines a custom rifle. Currently disabled.

## ModToolComponent
[JSON Document](Tool-Component-Documentation.md) <br>
This component defines a usable tool. These tools can also have custom implementations.

# JSON files

Each item must have exactly one json file which cannot be shared with other items. This json file can only have one component, but any number of behaviours can be included if desirable.

## ModGenericComponent

This component serves as the base for all the others. As a result, its parameters are used when defining the data for any item. However, for brevity and consistency, I have only included the documentation for these parameters on the [Generic Component Documentation](Generic-Component-Documentation.md) page.

## Template JSON files

Each page of documentation will include template json text.


## Example

If someone was adding another book to the game, this could potentially be the json file for that book. Notice that the component and the added behaviours are all in one file.
```
{
    "ModGenericComponent": {
                                "DisplayNameLocalizationId" : "GAMEPLAY_BookM",
                                "DescriptionLocalizatonId" : "GAMEPLAY_BookMDescription",
                                "WeightKG": 0.5,
                                "DaysToDecay" : 0,
                                "MaxHP" : 100,
                                "InitialCondition" : "Perfect",
                                "InventoryCategory" : "Firestarting",
                                "PickUpAudio" : "",
                                "PutBackAudio" : "",
                                "StowAudio" : "Play_InventoryStow",
                                "WornOutAudio" : "",
                                "InspectOnPickup" : true,
                                "InspectDistance" : 0.4,
                                "InspectAngles" : [0, 0, 0],
                                "InspectOffset" : [0, 0, 0],
                                "InspectScale" :  [1, 1, 1]
                            },
    "ModStackableBehaviour": {
                                "SingleUnitTextId" : "GAMEPLAY_BookMSingle",
                                "MultipleUnitTextId" : "GAMEPLAY_BookMMultiple",
                                "StackSprite" : "",
                                "UnitsPerItem" : 1
                            },
    "ModBurnableBehaviour": {
                                "BurningMinutes" : 18,
                                "BurningMinutesBeforeAllowedToAdd" : 0,
                                "SuccessModifier" : 35,
                                "TempIncrease" : 2.0
                            }
}
```
