using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	internal class ModExplosiveComponent : EquippableModComponent
	{
		public float killPlayerRange = 5;

		public float explosionDelay = 5;

		public string explosionAudio = "";

		protected override void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModExplosiveComponent>(this);
			base.Awake();
		}

		[HideFromIl2Cpp]
		internal void OnExplode()
		{
			GameAudioManager.Play3DSound(this.explosionAudio, this.gameObject);
			GameManager.GetConditionComponent().AddHealth(-1 * GetDamageToPlayer(), DamageSource.Unspecified);
			Destroy(this.gameObject);
		}

		[HideFromIl2Cpp]
		private float GetDamageToPlayer()
		{
			if (killPlayerRange == 0f) return 0f;
			float distance = Vector3.Distance(GameManager.GetVpFPSPlayer().transform.position, this.transform.position);
			if (distance <= 0) return 100f;
			return 100f * killPlayerRange * killPlayerRange / (distance * distance);
		}

		public ModExplosiveComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
