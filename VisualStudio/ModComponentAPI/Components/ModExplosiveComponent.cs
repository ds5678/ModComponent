using MelonLoader.TinyJSON;
using ModComponentUtils;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponentAPI
{
	public class ModExplosiveComponent : EquippableModComponent
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
			UnityEngine.Object.Destroy(this.gameObject);
		}

		[HideFromIl2Cpp]
		private float GetDamageToPlayer()
		{
			if (killPlayerRange == 0f) return 0f;
			float distance = Vector3.Distance(GameManager.GetVpFPSPlayer().transform.position, this.transform.position);
			if (distance <= 0) return 100f;
			return 100f * killPlayerRange * killPlayerRange / (distance * distance);
		}

		[HideFromIl2Cpp]
		public void TriggerCountdown()
		{
			ModExplosiveSave explosiveSave = ModComponentUtils.ComponentUtils.GetComponent<ModExplosiveSave>(this);
			if (explosiveSave is null)
			{
				Logger.LogError("Could not trigger countdown. No ModExplosiveSave!");
				return;
			}
			explosiveSave.TriggerCountdown(explosionDelay);
		}

		public ModExplosiveComponent(System.IntPtr intPtr) : base(intPtr) { }
	}

	internal class ModExplosiveSave : ModSaveBehaviour
	{
		public bool isTriggered = false;
		public float timeUntilExplosion = float.MaxValue;
		public ModExplosiveSave(System.IntPtr intPtr) : base(intPtr) { }

		[HideFromIl2Cpp]
		private void TriggerExplosion()
		{
			ModExplosiveComponent explosiveComponent = ModComponentUtils.ComponentUtils.GetComponent<ModExplosiveComponent>(this);
			if (explosiveComponent is null)
			{
				Logger.LogError("Could not trigger explosion. No ModExplosiveComponent!");
				return;
			}
			explosiveComponent.OnExplode();
		}

		[HideFromIl2Cpp]
		internal void TriggerCountdown(float timeUntilExplosion)
		{
			if (isTriggered) return;
			isTriggered = true;
			this.timeUntilExplosion = timeUntilExplosion;
		}

		public void Update()
		{
			if (!isTriggered) return;
			timeUntilExplosion -= UnityEngine.Time.deltaTime;
			if (timeUntilExplosion <= 0) TriggerExplosion();
		}

		[HideFromIl2Cpp]
		public override void Deserialize(string data)
		{
			if (string.IsNullOrWhiteSpace(data)) return;
			ModExplosiveData modExplosiveData = JSON.Load(data).Make<ModExplosiveData>();
			this.isTriggered = modExplosiveData.isTriggered;
			this.timeUntilExplosion = modExplosiveData.timeUntilExplosion;
		}

		[HideFromIl2Cpp]
		public override string Serialize()
		{
			return JSON.Dump(new ModExplosiveData(this));
		}
	}

	internal struct ModExplosiveData
	{
		public bool isTriggered;
		public float timeUntilExplosion;
		public ModExplosiveData(ModExplosiveSave modExplosiveSave)
		{
			this.isTriggered = modExplosiveSave.isTriggered;
			this.timeUntilExplosion = modExplosiveSave.timeUntilExplosion;
		}
	}
}
