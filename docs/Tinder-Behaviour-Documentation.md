> Not compatible with Accelerant, Burnable, or FireStarter Behaviour

# Template
```
{
    "ModTinderBehaviour": {
                                "DurationOffset" : 0,
                                "SuccessModifier" : 40
                            }
}
```

# Parameters

> Note: cannot be used without a [Component](Basic-Information-about-Components.md).

## DurationOffset
*float*<br/>
In-game seconds offset for fire starting duration from this accelerant. NOT scaled by fire starting skill. Positive values mean 'slower', negative values mean 'faster'.

## SuccessModifier
*float*<br/>
Does this item affect the chance for success? Represents percentage points. Positive values increase the chance, negative values reduce it.