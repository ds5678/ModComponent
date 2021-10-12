# Template
```
{
    "ModBurnableComponent": {
                                "BurningMinutes" : 18,
                                "BurningMinutesBeforeAllowedToAdd" : 0,
                                "SuccessModifier" : 35,
                                "TempIncrease" : 2
                            }
}
```

# Parameters

> Note: cannot be used without a [[Component|Basic Information about Components]].

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
