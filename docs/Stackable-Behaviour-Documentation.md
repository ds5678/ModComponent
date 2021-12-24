# Template
```
{
    "ModStackableBehaviour": {
                                "SingleUnitTextId" : "GAMEPLAY_HotCocoaBoxStackSingle",
                                "MultipleUnitTextId" : "GAMEPLAY_HotCocoaBoxStackMultiple",
                                "StackSprite" : "",
                                "UnitsPerItem" : 8,
                                "ChanceFull" : 80
                             }
}
```

# Parameters

> Note: cannot be used without a [Component](Basic-Information-about-Components.md).

## SingleUnitTextId
*string*<br/>
Localization key to be used for stacks with only one item. E.g. '2 arrows'.

## MultipleUnitTextId
*string*<br/>
Localization key to be used for stacks with multiple items. E.g. '2 arrows'.

## StackSprite
*string*<br/>
An optional sprite name (from a UIAtlas) that will be add to the stack. Not tested

## UnitsPerItem
*int*<br/>
The default number of units to make a full stack. For example, Coffee tins and Herbal Tea boxes each have 5 units.

## ChanceFull
*float*<br/>
Percent chance of the item having a full stack.