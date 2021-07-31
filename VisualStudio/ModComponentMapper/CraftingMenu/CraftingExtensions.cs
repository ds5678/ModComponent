using UnityEngine;

namespace ModComponentMapper.CraftingMenu
{
	internal static class CraftingExtensions
	{
		public static void Move(this UIButton btn, float xOffset, float yOffset, float zOffset)
		{
			Vector3 pos = btn.transform.localPosition;
			btn.transform.localPosition = new Vector3(pos.x + xOffset, pos.y + yOffset, pos.z + zOffset);
		}

		public static void SetSpriteName(this UIButton btn, string newSpriteName)
		{
			UISprite sprite = btn.GetComponent<UISprite>();
			sprite.spriteName = newSpriteName;
			sprite.OnInit();
		}

		public static UIButton Instantiate(this UIButton original)
		{
			UIButton copy = GameObject.Instantiate(original.gameObject).GetComponent<UIButton>();
			copy.transform.parent = original.transform.parent;
			copy.transform.localPosition = original.transform.localPosition;
			copy.transform.localScale = Vector3.one;
			return copy;
		}
	}
}
