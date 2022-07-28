extern alias Hinterland;
using Hinterland;

namespace ModComponent.Mapper;

internal static class DefaultDrawLayers
{
	public static int GetDefaultDrawLayer(ClothingRegion clothingRegion, ClothingLayer clothingLayer)
	{
		return clothingRegion switch
		{
			ClothingRegion.Head => GetDefaultHeadDrawLayer(clothingLayer),
			ClothingRegion.Accessory => 40,
			ClothingRegion.Hands => 25,
			ClothingRegion.Chest => GetDefaultChestDrawLayer(clothingLayer),
			ClothingRegion.Legs => GetDefaultLegsDrawLayer(clothingLayer),
			ClothingRegion.Feet => GetDefaultFeetDrawLayer(clothingLayer),
			_ => 60,
		};
	}

	public static int GetDefaultHeadDrawLayer(ClothingLayer clothingLayer)
	{
		return clothingLayer switch
		{
			ClothingLayer.Base => 41,
			ClothingLayer.Mid => 42,
			_ => 60,
		};
	}

	public static int GetDefaultChestDrawLayer(ClothingLayer clothingLayer)
	{
		return clothingLayer switch
		{
			ClothingLayer.Base => 21,
			ClothingLayer.Mid => 22,
			ClothingLayer.Top => 26,
			ClothingLayer.Top2 => 27,
			_ => 60,
		};
	}

	public static int GetDefaultLegsDrawLayer(ClothingLayer clothingLayer)
	{
		return clothingLayer switch
		{
			ClothingLayer.Base => 1,
			ClothingLayer.Mid => 2,
			ClothingLayer.Top => 15,
			ClothingLayer.Top2 => 16,
			_ => 60,
		};
	}

	public static int GetDefaultFeetDrawLayer(ClothingLayer clothingLayer)
	{
		return clothingLayer switch
		{
			ClothingLayer.Base => 5,
			ClothingLayer.Mid => 6,
			ClothingLayer.Top => 13,
			_ => 60,
		};
	}

	public static int MaybeGetDefaultDrawLayer(int drawLayer, ClothingRegion clothingRegion, ClothingLayer clothingLayer)
	{
		return drawLayer > 0 
			? drawLayer 
			: GetDefaultDrawLayer(clothingRegion, clothingLayer);
	}
}