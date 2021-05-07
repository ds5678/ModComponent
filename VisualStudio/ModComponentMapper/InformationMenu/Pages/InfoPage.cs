using System.Collections.Generic;

namespace ModComponentMapper
{
	internal abstract class InfoPage
	{
		internal string pageName;

		internal readonly Visibility menuVisibility;
		internal readonly Visibility visibility;

		internal InfoPage(string name)
		{
			this.pageName = name;
			menuVisibility = new Visibility();
			menuVisibility.SetVisible(false);
			visibility = new Visibility(menuVisibility);
		}

		internal void AddToGUI(GUI gui)
		{
			GUIBuilder guiBuilder = new GUIBuilder(pageName, gui);
			MakeGUIContents(guiBuilder);
			guiBuilder.AddPage(this);
		}

		protected abstract void MakeGUIContents(GUIBuilder guiBuilder);

		internal void SetMenuVisible(bool visible) => menuVisibility.SetVisible(visible);
		public bool IsVisible() => visibility.IsVisible();
		internal bool IsUserVisible() => visibility.IsSelfVisible();
		public void SetVisible(bool visible) => visibility.SetVisible(visible);


		internal delegate void OnVisibilityChange(bool visible);

		internal void AddVisibilityListener(OnVisibilityChange listener)
		{
			visibility.AddVisibilityListener(listener);
		}

		internal class Visibility
		{

			internal readonly List<OnVisibilityChange> visibilityListeners = new List<OnVisibilityChange>();
			internal readonly List<Visibility> children = new List<Visibility>();
			internal bool parentVisible = true;
			internal bool visible = true;

			internal Visibility() { }

			internal Visibility(Visibility parent)
			{
				parent.children.Add(this);
				SetParentVisible(parent.IsVisible());
			}

			internal void AddChild(Visibility child)
			{
				children.Add(child);
				child.SetParentVisible(IsVisible());
			}

			internal bool IsSelfVisible()
			{
				return visible;
			}

			internal bool IsVisible()
			{
				return parentVisible && visible;
			}

			internal void SetParentVisible(bool parentVisible)
			{
				if (this.parentVisible == parentVisible)
				{
					return;
				}

				this.parentVisible = parentVisible;
				if (visible)
				{
					ChangeVisibility(parentVisible);
				}
			}

			internal void SetVisible(bool visible)
			{
				if (this.visible == visible)
				{
					return;
				}

				this.visible = visible;
				if (parentVisible)
				{
					ChangeVisibility(visible);
				}
			}

			internal void ChangeVisibility(bool visible)
			{
				foreach (Visibility child in children)
				{
					child.SetParentVisible(visible);
				}

				foreach (OnVisibilityChange listener in visibilityListeners)
				{
					listener.Invoke(visible);
				}
			}

			internal void AddVisibilityListener(OnVisibilityChange listener)
			{
				visibilityListeners.Add(listener);
			}
		}
	}
}
