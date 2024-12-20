using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class NebPortal1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebulous Portal");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 74;
			base.projectile.height = 80;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			base.projectile.alpha -= 2;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 60f)
			{
				Main.PlaySound(SoundID.Item8, base.projectile.position);
				int num = 62;
				int num2 = 16;
				for (int i = 0; i < num2; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num3].noLight = false;
					Main.dust[num3].noGravity = true;
				}
				int num4 = Main.rand.Next(4);
				if (num4 == 0 && Main.netMode != 1)
				{
					int num5 = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 420, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num5].lifeMax = 4000;
					Main.npc[num5].life = 4000;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num5, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (num4 == 1 && Main.netMode != 1)
				{
					int num6 = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 421, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num6].lifeMax = 4000;
					Main.npc[num6].life = 4000;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num6, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (num4 == 2 && Main.netMode != 1)
				{
					int num7 = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 423, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num7].lifeMax = 4000;
					Main.npc[num7].life = 4000;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num7, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (num4 == 3 && Main.netMode != 1)
				{
					int num8 = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 421, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num8].lifeMax = 4000;
					Main.npc[num8].life = 4000;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num8, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			if (base.projectile.localAI[0] >= 120f)
			{
				int num9 = 62;
				int num10 = 16;
				for (int j = 0; j < num10; j++)
				{
					int num11 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num9, 0f, 0f, 100, Color.White, 2f);
					Main.dust[num11].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)num10 * 6.28f);
					Main.dust[num11].noLight = false;
					Main.dust[num11].noGravity = true;
				}
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			int num = 62;
			int num2 = 20;
			for (int i = 0; i < num2; i++)
			{
				int num3 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 2f);
				Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)num2 * 6.28f);
				Main.dust[num3].noLight = false;
				Main.dust[num3].noGravity = true;
			}
		}
	}
}
