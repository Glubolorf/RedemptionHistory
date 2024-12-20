using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class ScreenPlayer : ModPlayer
	{
		public override void Initialize()
		{
			ScreenPlayer.NebCutsceneflag = false;
			ScreenPlayer.NebCutscene = false;
		}

		public override void PostUpdate()
		{
			if (this.rumbleDuration > 0)
			{
				this.rumbleDuration--;
			}
			if (this.lockScreen)
			{
				if (this.interpolantTimer < 100)
				{
					this.interpolantTimer++;
				}
			}
			else if (this.interpolantTimer > 0)
			{
				this.interpolantTimer--;
			}
			this.ScreenFocusInterpolant = Utils.InverseLerp(15f, 80f, (float)this.interpolantTimer, true);
			this.lockScreen = false;
		}

		public override void UpdateDead()
		{
			this.lockScreen = false;
			this.rumbleDuration = 0;
		}

		public void Rumble(int duration, int intensity = 10)
		{
			this.rumbleDuration = duration;
			this.rumbleStrength = intensity;
		}

		public override void ModifyScreenPosition()
		{
			if (this.ScreenFocusInterpolant > 0f && !RedeConfigClient.Instance.CameraLockDisable)
			{
				Vector2 idealScreenPosition = this.ScreenFocusPosition - new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
				Main.screenPosition = Vector2.Lerp(Main.screenPosition, idealScreenPosition, this.ScreenFocusInterpolant);
			}
			Redemption.Inst.cameraOffset *= 0.9f;
			Main.screenPosition += Redemption.Inst.cameraOffset;
			if (this.rumbleDuration > 0)
			{
				int r = this.rumbleStrength;
				Main.screenPosition.X = Main.screenPosition.X + (float)Main.rand.Next(-r, r + 1);
				Main.screenPosition.Y = Main.screenPosition.Y + (float)Main.rand.Next(-r, r + 1);
			}
			if (this.ScreenShakeIntensity > 0.1f)
			{
				Main.screenPosition += new Vector2(Utils.NextFloat(Main.rand, this.ScreenShakeIntensity), Utils.NextFloat(Main.rand, this.ScreenShakeIntensity));
				this.ScreenShakeIntensity *= 0.9f;
			}
			if (ScreenPlayer.NebCutscene)
			{
				if (!ScreenPlayer.NebCutsceneflag)
				{
					this.up = 0f;
					ScreenPlayer.NebCutsceneflag = true;
					this.setCurrentVecScreenPos = base.player.Center;
				}
				this.yeet = base.player.Center.Y - this.setCurrentVecScreenPos.Y;
				this.up += (400f - this.up) / 32f;
				this.yeet2 = this.up + this.yeet;
				Main.screenPosition.Y = Main.screenPosition.Y - this.yeet2;
				return;
			}
			if (Math.Abs(this.yeet2) > 2f)
			{
				this.yeet2 -= this.yeet2 / 20f;
				Main.screenPosition.Y = Main.screenPosition.Y - this.yeet2;
				if (Math.Abs(this.yeet2) > 20f)
				{
					Main.screenPosition.X = Main.screenPosition.X + (float)Main.rand.Next(-10, 11);
					Main.screenPosition.Y = Main.screenPosition.Y + (float)Main.rand.Next(-10, 11);
				}
			}
			ScreenPlayer.NebCutsceneflag = false;
		}

		public int rumbleDuration;

		public int rumbleStrength;

		public int interpolantTimer;

		public bool lockScreen;

		public static bool NebCutscene;

		public static bool NebCutsceneflag;

		public Vector2 setCurrentVecScreenPos;

		private float up;

		public float yeet;

		public float yeet2;

		public float ScreenShakeIntensity;

		public Vector2 ScreenFocusPosition;

		public float ScreenFocusInterpolant;
	}
}
