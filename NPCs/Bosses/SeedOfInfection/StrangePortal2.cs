using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.SeedOfInfection
{
	public class StrangePortal2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Strange Portal");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 42;
			base.projectile.height = 42;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			InfectionTextPlayer modPlayer = player.GetModPlayer<InfectionTextPlayer>();
			ShakeScreen modPlayer2 = player.GetModPlayer<ShakeScreen>();
			modPlayer.text = true;
			modPlayer2.shakeSubtle = true;
			base.projectile.velocity.X = 0f;
			base.projectile.velocity.Y = 0f;
			if (base.projectile.timeLeft <= 30)
			{
				if (modPlayer.alphaText > 0f)
				{
					modPlayer.alphaText -= 10f;
				}
			}
			else if (modPlayer.alphaText < 255f)
			{
				modPlayer.alphaText += 10f;
			}
			float[] localAI = base.projectile.localAI;
			int num = 0;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 % 10f == 0f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(0f, 0f), base.mod.ProjectileType("StrangePortal3"), 0, 0f, base.projectile.owner, (float)(this.rotSwitch ? 0 : 1), 0f);
				if (this.rotSwitch)
				{
					this.rotSwitch = false;
					return;
				}
				this.rotSwitch = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 74, 0f, 0f, 100, default(Color), 3.5f);
				Main.dust[dustIndex].velocity *= 2.9f;
			}
			if (Main.netMode != 1)
			{
				int j = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, base.mod.NPCType("SoI"), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public bool rotSwitch;
	}
}
