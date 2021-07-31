using System;
using UnityEngine;

namespace ModComponentMapper.CraftingMenu
{
	internal class RecipeDisplayItem : MonoBehaviour
	{
		public static RecipeDisplayItem Replace(BlueprintDisplayItem original)
		{
			RecipeDisplayItem replacement = original.gameObject.AddComponent<RecipeDisplayItem>();

			replacement.m_Available = original.m_Available;
			replacement.m_Background = original.m_Background;
			replacement.m_Button = original.m_Button;
			replacement.m_DisplayName = original.m_DisplayName;
			replacement.m_Icon = original.m_Icon;
			replacement.m_Root = original.m_Root;
			replacement.m_Unavailable = original.m_Unavailable;
			replacement.m_Disabled = original.m_Disabled;
			replacement.m_Normal = original.m_Normal;
			replacement.m_Selected = original.m_Selected;
			replacement.m_Index = original.m_Index;

			EventDelegate.Set(replacement.m_Button.onClick, new Action(() => replacement.OnButtonClicked()));
			GameObject.Destroy(original);

			return replacement;
		}

		public void Clear()
		{
			this.m_Icon.enabled = false;
			this.m_DisplayName.text = string.Empty;
			this.m_Available.enabled = false;
			this.m_Unavailable.enabled = false;
			this.m_Button.SetState(UIButtonColor.State.Disabled, true);
			this.m_Background.color = this.m_Normal;
		}

		public void OnButtonClicked()
		{
			if (this.m_ClickedDelegate != null)
			{
				this.m_ClickedDelegate(this.m_Index);
			}
		}

		public void SetSelected(bool selected)
		{
			this.m_Button.SetState(selected ? UIButtonColor.State.Hover : UIButtonColor.State.Normal, true);
			this.m_Background.color = (selected ? this.m_Selected : this.m_Normal);
			this.m_Root.color = Utils.GetColorWithAlpha(this.m_Root.color, this.m_CanCraftRecipe ? 1f : this.m_Disabled.a);
		}

		public void Setup(BlueprintItem bpi)
		{
			Panel_Crafting panel_Crafting = InterfaceManager.m_Panel_Crafting;
			this.m_CanCraftRecipe = panel_Crafting.CanCraftBlueprint(bpi);
			string name = bpi.m_CraftedResult.name.Replace("GEAR_", "ico_CraftItem__");
			this.m_Icon.mainTexture = panel_Crafting.m_CraftingIconBundle.LoadAsset<Texture2D>(name);
			this.m_Icon.enabled = true;
			this.m_DisplayName.text = bpi.GetDisplayedNameWithCount();
			this.m_Available.enabled = this.m_CanCraftRecipe;
			this.m_Unavailable.enabled = !this.m_CanCraftRecipe;
			this.m_Background.color = this.m_Normal;
			this.m_Root.color = Utils.GetColorWithAlpha(this.m_Root.color, this.m_CanCraftRecipe ? 1f : this.m_Disabled.a);
		}

		public UISprite m_Available;

		public UISprite m_Background;

		public UIButton m_Button;

		public UILabel m_DisplayName;

		public UITexture m_Icon;

		public UIWidget m_Root;

		public UISprite m_Unavailable;

		public Color m_Disabled;

		public Color m_Normal;

		public Color m_Selected;

		public int m_Index = -1;

		public Action<int> m_ClickedDelegate;

		private bool m_CanCraftRecipe;
	}
}
