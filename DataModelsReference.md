# Data models reference
All data communications with BiomeTitles are done via data objects. When you see that some type name ends with `Data`, it means that data object is implied. Data object may be instance of [`ExpandoObject`](https://learn.microsoft.com/en-us/dotnet/api/system.dynamic.expandoobject?view=net-6.0), [anonymous type](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types) or of any other regular C# class if that class provide required fields. Below you can see explanation for each data type.

## BiomeData
| Type(s)                                                                   | Property name        | Description                                             | Default          |
|------------------------------------------------------------------------|----------------------|---------------------------------------------------------|------------------|
| `string`                                                               | `Key`*               | Unique biome identifier                                 | Title            |
| `string`                                                               | `Title`*             | Displayed biome name in english                         | Key              |
| `string`                                                               | `SubTitle`           | Displayed subtitle in english, usually mod display name | Mod display name |
| `Color`                                                                | `TitleColor`         | Title color                                             | `Color.White`    |
| `Color`                                                                | `TitleStroke`        | Title outline color                                     | `Color.Black`    |
| `Texture2D`                                                            | `Icon`               | Icon to be displayed near title                         | No icon          |
| `BiomeTitle`**<br>`Texture2D`<br>[`TitleBackgroundData`](#titlebackgrounddata) | `TitleBackground`    | Title background                                            | Golden plate     |
| `BiomeTitle`**<br>`Texture2D`<br>[`TitleBackgroundData`](#titlebackgrounddata) | `SubTitleBackground` | Subtitle background                                         | Silver plate     |

<label>* At least `Key` or `Title` must present.</label>
<label>**For `BiomeTitle` see [biome title customization](BiomeTitleCustomization.md).</label>

## TitleBackgroundData
| Type(s)        | Property name         | Description                                                     | Default value |
|-------------|-----------------------|-----------------------------------------------------------------|---------------|
| `Texture2D` | `Texture`*            | Texture to be used as a background                              |               |
| `float`     | `LeftSegmentSize`     | Percent of texture to be preserved from stretching on left side | `44 / 514f`   |
| `float`     | `RightSegmentSize`    | Percent of texture to be preserved from stretching on right side | `44 / 514f`   |
| `float`     | `MiddleSegmentSize`   | Percent of texture to be preserved from stretching in the middle | `44 / 514f`   |
| `float`     | `LeftContentPadding`  | Amount of pixels to pad on left side | `44f`   |
| `float`     | `RightContentPadding` | Amount of pixels to pad on right side | `44f`   |

<label>* Required field.</label>

## Usage example

### Example 1
```csharp
public dynamic GetDataObject()
{
    var myDataObject = new ExpandoObject();
    myDataObject.Field1 = 100;
    myDataObject.Field2 = "Some String";
    return myDataObject;
}
```

### Example 2
```csharp
public dynamic GetDataObject()
{
    return new {
        Field1 = 100,
        Field2 = "Some String"
    }
}
```

### Example 3
```csharp
class MyClass
{
    int Field = 100;
    string Field2 = "Some String";
}

public dynamic GetDataObject()
{
    return new MyClass();
}
```