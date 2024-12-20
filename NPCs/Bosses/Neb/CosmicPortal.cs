using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class CosmicPortal : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cosmic Portal");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 52;
			base.projectile.height = 46;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 500;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.localAI[0] == 0f)
			{
				base.projectile.alpha -= 2;
				if (base.projectile.alpha <= 0)
				{
					Main.PlaySound(SoundID.Item8, base.projectile.position);
					int dustType = 62;
					int pieCut = 16;
					for (int i = 0; i < pieCut; i++)
					{
						int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 3f);
						Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)pieCut * 6.28f);
						Main.dust[dustID].noLight = false;
						Main.dust[dustID].noGravity = true;
					}
					if (Main.netMode != 1)
					{
						int j = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, ModContent.NPCType<StarWyvernHead>(), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[j].velocity = Main.npc[j].DirectionTo(player.Center) * 10f;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
						}
					}
					base.projectile.localAI[0] = 1f;
					return;
				}
			}
			else
			{
				base.projectile.alpha++;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
		}
	}
}
