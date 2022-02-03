> Not compatible with Accelerant, FireStarter, or Tinder Behaviour

# Template
```
{
    "ModBurnableBehaviour": {
                                "BurningMinutes" : 18,
                                "BurningMinutesBeforeAllowedToAdd" : 0,
                                "SuccessModifier" : 35,
                                "TempIncrease" : 2,
                                "DurationOffset" : 0
                            }
}
```

# Parameters

> Note: cannot be used without a [Component](Basic-Information-about-Components.md).

## BurningMinutes
*int*<br/>
Number of minutes this item adds to the remaining burn time of the fire.

## BurningMinutesBeforeAllowedToAdd
*float*<br/>
How long must a fire be burning before this item can be added?

## SuccessModifier
*float*<br/>
Does this item affect the chance for successfully starting a fire? Represents percentage points. Positive values increase the chance, negative values reduce it.

## TempIncrease
*float*<br/>
Temperature increase in °C when added to the fire.

## DurationOffset
*float*<br/>
In-game seconds offset for fire starting duration from this accelerant. NOT scaled by fire starting skill. Positive values mean 'slower', negative values mean 'faster'.
