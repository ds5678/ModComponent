# Behaviours

Behaviours are additional functions that can be attached to items.  
All behaviours can be attached to any item and most behaviours can be combined with others.

## ModAccelerantBehaviour

The item becomes an accelerant that can be used to increase the chance to successfully start a fire.
> This cannot be combined with ModFireStarterBehaviour

## ModBurnableBehaviour

The item can be burned in a fire.

## ModCarryingCapacityBehaviour

This item can change carrying capacity.

## ModEvolveBehaviour

The item will slowly evolve into another item. Curing is an example of this.

## ModFireStarterBehaviour

The item can be used to start fires.  
Fire starters can either be reusable or not.

> This cannot be combined with ModAccelerantBehaviour


## ModHarvestableBehaviour

The item can be harvested to received some other items or materials.  
The behaviour defines which and how many items will be provided.


## ModRepairableBehaviour

The item's condition can be restored by repairing it.  
The required materials and tools can be configured.

## ModMillableBehaviour

The item can be repaired at a milling machine. This is a new addition to ModComponent.

## ModScentBehaviour

The item gives off a scent that wildlife can smell.

## ModSharpenableBehaviour

The item can be sharpened with a whetstone.

## ModStackableBehaviour

The item can form stacks with itself. Also enables spawning item stacks similar to the Coffee Tin and Herbal Tea Box.

# JSON files

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
