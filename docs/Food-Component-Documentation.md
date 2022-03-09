> Disclaimer: Not well-tested, but appears to be working

# Template
```
{
    "ModFoodComponent" : {
                            "DisplayNameLocalizationId" : "GAMEPLAY_HotCocoaCup",
                            "DescriptionLocalizatonId" : "GAMEPLAY_HotCocoaCupDescription",
                            "InventoryActionLocalizationId" : "GAMEPLAY_HotCocoaCupAction",
                            "WeightKG": 0.1,
                            "DaysToDecay" : 0,
                            "MaxHP" : 100,
                            "InitialCondition" : "Perfect",
                            "InventoryCategory" : "Food",
                            "PickUpAudio" : "",
                            "PutBackAudio" : "",
                            "StowAudio" : "Play_InventoryStow",
                            "WornOutAudio" : "",
                            "InspectOnPickup" : true,
                            "InspectDistance" : 0.4,
                            "InspectAngles" : [0, 0, 0],
                            "InspectOffset" : [0, 0, 0],
                            "InspectScale" :  [1, 1, 1],
                            "NormalModel" : "",
                            "InspectModel" : "",

                            "Cooking" : true,
                            "CookingMinutes" : 8,
                            "CookingUnitsRequired" : 1,
                            "CookingWaterRequired" : 0,
                            "CookingResult" : "",
                            "BurntMinutes" : 30,
                            "Type" : "Liquid",
                            "CookingAudio" : "",
                            "StartCookingAudio" : "",

                            "DaysToDecayOutdoors" : 0,
                            "DaysToDecayIndoors" : 0,
                            "Calories" : 150,
                            "Servings" : 1,
                            "EatingTime" : 1,
                            "EatingAudio" : "",
                            "EatingPackagedAudio" : "",
                            "ThirstEffect" : 0,

                            "FoodPoisoning" : 0,
                            "FoodPoisoningLowCondition" : 0,
                            "ParasiteRiskIncrements" : [],

                            "Natural" : false,
                            "Raw" : false,
                            "Drink" : true,
                            "Meat" : false,
                            "Fish" : false,
                            "Canned" : false,

                            "Opening" : false,
                            "OpeningWithCanOpener" : false,
                            "OpeningWithKnife" : false,
                            "OpeningWithHatchet" : false,
                            "OpeningWithSmashing" : false,

                            "AffectCondition" : false,
                            "ConditionRestBonus" : 0,
                            "ConditionRestMinutes" : 0,

                            "AffectRest" : false,
                            "InstantRestChange" : 0,
                            "RestFactorMinutes" : 0,

                            "AffectCold" : false,
                            "InstantColdChange" : 0,
                            "ColdFactorMinutes" : 60,

                            "ContainsAlcohol" : false,
                            "AlcoholPercentage" : 0,
                            "AlcoholUptakeMinutes" : 45
                        }
}
```

# Parameters

This component uses all the parameters from the [[Generic Component Documentation]] and the [[Cookable Component Documentation]].

## DaysToDecayOutdoors
*int*<br/>
0 means 'Never'. This overrides the Basic Property 'DaysToDecay'.

## DaysToDecayIndoors
*int*<br/>
0 means 'Never'. This overrides the Basic Property 'DaysToDecay'.

## Calories
*int*<br/>
For one complete item with all servings. Calories remaining will scale with weight.

## Servings
*int*<br/>
The number of servings contained in this item. Each consumation will be limited to one serving. 1 means 'Comsume completely' - the way all pre-existing food items work. Currently disabled, but you can set a value for when it's re-enabled.

## EatingTime
*int*<br/>
Realtime seconds it takes to eat one complete serving.

## EatingAudio
*string*<br/>
Sound to use when the item is either unpackaged or already open

## EatingPackagedAudio
*string*<br/>
Sound to use when the item is still packaged and unopened. Leave empty for unpackaged food

## ThirstEffect
*int*<br/>
How does this affect your thirst? Represents change in percentage points. Negative values increase thirst, positive values reduce thirst.

## FoodPoisoning
*int*<br/>
Chance in percent to contract food poisoning from an item above 20% condition

## FoodPoisoningLowCondition
*int*<br/>
Chance in percent to contract food poisoning from an item below 20% condition

## ParasiteRiskIncrements
*array of float numbers*<br/>
Parasite Risk increments in percent for each unit eaten. Leave empty for no parasite risk.

## Natural
*bool*<br/>
Is the food item naturally occurring meat or plant?

## Raw
*bool*<br/>
Is the food item raw or cooked?

## Drink
*bool*<br/>
Is the food item something to drink? (This mainly affects the names of actions and position in the radial menu)

## Meat
*bool*<br/>
Is the food item meat directly from an animal? (E.g. wolf steak, but not beef jerky - mainly for statistics)

## Fish
*bool*<br/>
Is the food item fish directly from an animal? (E.g. salmon, but not canned sardines - mainly for statistics)

## Canned
*bool*<br/>
Is the food item canned? Canned items will yield a 'Recycled Can' when opened properly.

## Opening
*bool*<br/>
Does this item require a tool for opening it? If not enabled, the other settings in this section will be ignored.

## OpeningWithCanOpener
*bool*<br/>
Can it be opened with a can opener?

## OpeningWithKnife
*bool*<br/>
Can it be opened with a knife?

## OpeningWithHatchet
*bool*<br/>
Can it be opened with a hatchet?

## OpeningWithSmashing
*bool*<br/>
Can it be opened by smashing?

## AffectCondition
*bool*<br/>
Does this item affect 'Condition' while sleeping? If not enabled, the other settings in this section will be ignored.

## ConditionRestBonus
*float*<br/>
How much additional condition is restored per hour?

## ConditionRestMinutes
*float*<br/>
Amount of in-game minutes the 'ConditionRestBonus' will be applied.

## AffectRest
*bool*<br/>
Does this item affect 'Rest'? If not enabled, the other settings in this section will be ignored.

## InstantRestChange
*float*<br/>
How much 'Rest' is restored/drained immediately after consuming the item. Represents change in percentage points. Negative values drain rest, positive values restore rest.

## RestFactorMinutes
*int*<br/>
Amount of in-game minutes the 'RestFactor' will be applied.

## AffectCold
*bool*<br/>
Does this item affect 'Cold'? If not enabled, the other settings in this section will be ignored.

## InstantColdChange
*float*<br/>
How much 'Cold' is restored/drained immediately after consuming the item. Represents change in percentage points. Negative values make it feel colder, positive values make it feel warmer.

## ColdFactorMinutes
*int*<br/>
Amount of in-game minutes the 'ColdFactor' will be applied.

## ContainsAlcohol
*bool*<br/>
Does this item contain Alcohol? If not enabled, the other settings in this section will be ignored. Currently disabled, but you can set a value for when it's re-enabled.

## AlcoholPercentage
*float*<br/>
How much of the item's weight is alcohol?

## AlcoholUptakeMinutes
*float*<br/>
How many in-game minutes does it take for the alcohol to be fully absorbed? This is scaled by current hunger level (the hungrier the faster). The simulated blood alcohol level will slowly raise over this time. Real-life value is around 45 mins for liquids.
