extern alias Hinterland;
using Hinterland;

namespace ModComponent.Mapper
{
	internal static class DefaultDrawLayers
	{
		public static int GetDefaultDrawLayer(ClothingRegion clothingRegion, ClothingLayer clothingLayer)
		{
			switch (clothingRegion)
			{
				case ClothingRegion.Head:
					return GetDefaultHeadDrawLayer(clothingLayer);
				case ClothingRegion.Accessory:
					return 40;
				case ClothingRegion.Hands:
					return 25;
				case ClothingRegion.Chest:
					return GetDefaultChestDrawLayer(clothingLayer);
				case ClothingRegion.Legs:
					return GetDefaultLegsDrawLayer(clothingLayer);
				case ClothingRegion.Feet:
					return GetDefaultFeetDrawLayer(clothingLayer);
				default:
					return 60;
			}
		}

		public static int GetDefaultHeadDrawLayer(ClothingLayer clothingLayer)
		{
			switch (clothingLayer)
			{
				case ClothingLayer.Base:
					return 41;
				case ClothingLayer.Mid:
					return 42;
				default:
					return 60;
			}
		}

		public static int GetDefaultChestDrawLayer(ClothingLayer clothingLayer)
		{
			switch (clothingLayer)
			{
				case ClothingLayer.Base:
					return 21;
				case ClothingLayer.Mid:
					return 22;
				case ClothingLayer.Top:
					return 26;
				case ClothingLayer.Top2:
					return 27;
				default:
					return 60;
			}
		}

		public static int GetDefaultLegsDrawLayer(ClothingLayer clothingLayer)
		{
			switch (clothingLayer)
			{
				case ClothingLayer.Base:
					return 1;
				case ClothingLayer.Mid:
					return 2;
				case ClothingLayer.Top:
					return 15;
				case ClothingLayer.Top2:
					return 16;
				default:
					return 60;
			}
		}

		public static int GetDefaultFeetDrawLayer(ClothingLayer clothingLayer)
		{
			switch (clothingLayer)
			{
				case ClothingLayer.Base:
					return 5;
				case ClothingLayer.Mid:
					return 6;
				case ClothingLayer.Top:
					return 13;
				default:
					return 60;
			}
		}

		public static int MaybeGetDefaultDrawLayer(int drawLayer, ClothingRegion clothingRegion, ClothingLayer clothingLayer)
		{
			if (drawLayer > 0) return drawLayer;
			else return GetDefaultDrawLayer(clothingRegion, clothingLayer);
		}
	}
}