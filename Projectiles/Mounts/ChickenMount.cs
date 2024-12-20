using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Mounts
{
	public class ChickenMount : ModMountData
	{
		public override void SetDefaults()
		{
			base.mountData.spawnDust = 0;
			base.mountData.buff = base.mod.BuffType("ChickenMountBuff");
			base.mountData.heightBoost = 3;
			base.mountData.fallDamage = 0.5f;
			base.mountData.runSpeed = 12f;
			base.mountData.dashSpeed = 8f;
			base.mountData.flightTimeMax = 0;
			base.mountData.fatigueMax = 0;
			base.mountData.jumpHeight = 3;
			base.mountData.acceleration = 0.19f;
			base.mountData.jumpSpeed = 4f;
			base.mountData.blockExtraJumps = false;
			base.mountData.totalFrames = 6;
			base.mountData.constantJump = true;
			int[] array = new int[base.mountData.totalFrames];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 6;
			}
			base.mountData.playerYOffsets = array;
			base.mountData.xOffset = 4;
			base.mountData.bodyFrame = 3;
			base.mountData.yOffset = 10;
			base.mountData.playerHeadOffset = 22;
			base.mountData.standingFrameCount = 1;
			base.mountData.standingFrameDelay = 12;
			base.mountData.standingFrameStart = 0;
			base.mountData.runningFrameCount = 6;
			base.mountData.runningFrameDelay = 12;
			base.mountData.runningFrameStart = 0;
			base.mountData.flyingFrameCount = 0;
			base.mountData.flyingFrameDelay = 0;
			base.mountData.flyingFrameStart = 0;
			base.mountData.inAirFrameCount = 1;
			base.mountData.inAirFrameDelay = 12;
			base.mountData.inAirFrameStart = 1;
			base.mountData.idleFrameCount = 1;
			base.mountData.idleFrameDelay = 12;
			base.mountData.idleFrameStart = 0;
			base.mountData.idleFrameLoop = true;
			base.mountData.swimFrameCount = base.mountData.inAirFrameCount;
			base.mountData.swimFrameDelay = base.mountData.inAirFrameDelay;
			base.mountData.swimFrameStart = base.mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				base.mountData.textureWidth = base.mountData.backTexture.Width;
				base.mountData.textureHeight = base.mountData.backTexture.Height;
			}
		}

		public override void UpdateEffects(Player player)
		{
			if (Math.Abs(player.velocity.X) > player.mount.DashSpeed - player.mount.RunSpeed / 2f)
			{
				player.noKnockback = true;
			}
		}
	}
}
