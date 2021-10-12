# Template
```
{
    "ModClothingComponent" : {
                            "DisplayNameLocalizationId" : "GAMEPLAY_DeerSkinCoat",
                            "DescriptionLocalizatonId" : "GAMEPLAY_DeerSkinCoatDescription",
                            "InventoryActionLocalizationId" : "GAMEPLAY_DeerSkinCoatAction",
                            "WeightKG": 2.75,
                            "DaysToDecay" : 1000,
                            "MaxHP" : 450,
                            "InitialCondition" : "Perfect",
                            "InventoryCategory" : "Clothing",
                            "PickUpAudio" : "Play_SndInvLeatherHide",
                            "PutBackAudio" : "Play_SndInvLeatherHide",
                            "StowAudio" : "Play_InventoryStow",
                            "WornOutAudio" : "Play_LeatherWornOut",
                            "InspectOnPickup" : true,
                            "InspectDistance" : 1,
                            "InspectAngles" : [180, -30, 180],
                            "InspectOffset" : [0.05, 0.05, 0],
                            "InspectScale" :  [0.38, 0.38, 0.38],
                            "NormalModel" : "",
                            "InspectModel" : "",

                            "Region" : "Chest",
                            "MinLayer" : "Top",
                            "MaxLayer" : "Top2",
                            "MovementSound" : "LeatherHide",
                            "Footwear" : "Deerskin",
                            "DaysToDecayWornOutside" : 450,
                            "DaysToDecayWornInside" : 1000,
                            "Warmth" : 3,
                            "WarmthWhenWet" : 2,
                            "Windproof" : 3,
                            "Waterproofness" : 60,
                            "Toughness" : 10,
                            "SprintBarReduction" : 10,
                            "DecreaseAttackChance" : 0,
                            "IncreaseFleeChance" : 0,
                            "HoursToDryNearFire" : 5,
                            "HoursToDryWithoutFire" : 10,
                            "HoursToFreeze" : 3,
                            "MainTexture" : "CLTH_deerskin",
                            "BlendTexture" : "CLTH_deerskin_blend_M_",
                            "DrawLayer" : 42,
                            "ImplementationType" : ""
                        }
}
```

# Parameters

This component, like the others, uses all the parameters from the [[Generic Component Documentation]].

## Region
`Head`, `Hands`, `Chest`, `Legs`, `Feet`, or `Accessory`<br/>
The body region this clothing item can be worn.

## MinLayer
`Base`, `Mid`, `Top`, or `Top2`<br/>
The innermost layer at which the clothing item can be worn. From innermost to outermost: Base, Mid, Top, Top2. Examples: Legs + Base = Longjohns, Legs + Top = Pants; Feet + Mid = Socks, Feet + Top = Boots

## MaxLayer
`Base`, `Mid`, `Top`, or `Top2`<br/>
The outermost layer at which the clothing item can be worn. From innermost to outermost: Base, Mid, Top, Top2. Examples: Legs + Base = Longjohns, Legs + Top = Pants; Feet + Mid = Socks, Feet + Top = Boots

## MovementSound
`None`, `HeavyNylon`, `LeatherHide`, `LightCotton`, `LightNylon`, `SoftCloth`, or `Wool`<br/>
The type of sound to make when moving while wearing this clothing item.

## Footwear
`None`, `Boots`, `Deerskin`, or `Shoes`
The type footwear (as in Boots) this clothing item represents. Leave at 'None' if it is not a footwear item at all.

## DaysToDecayWornOutside
*float*<br/>
Number of days it takes for this clothing item to decay from 100% to 0% while being worn and outside. 0 means 'Does not decay from being worn'.

## DaysToDecayWornInside
*float*<br/>
Number of days it takes for this clothing item to decay from 100% to 0% while being worn and inside. 0 means 'Does not decay from being worn'.

## Warmth
*float*<br/>
Warmth bonus in degrees celsius when the clothing item is in perfect condition and completely dry. The actual bonus value will scale with condition and wetness.

## WarmthWhenWet
*float*<br/>
Warmth bonus in degrees celsius when the clothing item is in perfect condition and completely wet. The actual bonus value will scale with condition and wetness.

## Windproof
*float*<br/>
Windproof bonus in degrees celsius when the clothing item is in perfect condition and completely wet. The actual bonus value will scale with condition and wetness.

## Waterproofness
*float*<br/>
How much water is repelled by this clothing item? 100 means 'never gets wet'

## Toughness
*float*<br/>
Damage reduction in per cent when receiving certain types of damage (e.g. a coat protects against wolves, but not falling). 100 means 'Receive no damage', 0 means 'Receive full damage'. Actual bonus will scale with condition.

## SprintBarReduction
*float*<br/>
Sprint stamina reduction in per cent. 100 means 'No sprint stamina', 0 means 'Full sprint stamina'.

## DecreaseAttackChance
*int*<br/>
Decreases the chance that a wolf will attack. Only applies in certain situations. 100 means 'guaranteed not to attack'; 0 means 'same as without the buff'

## IncreaseFleeChance
*int*<br/>
Increases the chance that a wolf will flee immediately when spotting the player. 100 means 'guaranteed to flee'; 0 means 'same as without the buff'

## HoursToDryNearFire
*float*<br/>
Hours required to dry this clothing item next to a fire when it is completely wet. That's the same amount of time it takes to unfreeze, so a completely frozen item will take twice this time to be completely dry again.

## HoursToDryWithoutFire
*float*<br/>
Hours required to dry this clothing item without a fire when it is completely wet. That's the same amount of time it takes to unfreeze, so a completely frozen item will take twice this time to be completely dry again.

## HoursToFreeze
*float*<br/>
Hours required for this clothing to completely freeze once it got wet.

## MainTexture
*string*<br/>
Base name of the texture to represent this clothing item in the paper doll view. All required actual texture paths will be derived from this name.

## BlendTexture
*string*<br/>
Name of the blend texture used for the paper doll view.

## DrawLayer
*int*<br/>
Drawing layer (as in drawing order) to be used for this clothing item. Items with higher values are drawn over items with lower values.

## ImplementationType - Currently Disabled
*string*<br/>
The name of the type implementing the specific game logic of this item. Use 'Namespace.TypeName,AssemblyName', e.g. 'ClothingPack.SkiGogglesImplementation,Clothing-Pack'. Leave empty if this item needs no special game logic. Untested