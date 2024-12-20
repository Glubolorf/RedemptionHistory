using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.SeedOfInfection
{
	public class SoIDeath : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/SeedOfInfection/SoI_FreakingOut";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Seed of Infection");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 96;
			base.projectile.height = 114;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 240;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 6)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 0.1f;
			base.projectile.rotation += base.projectile.localAI[0] / 40f;
			base.projectile.velocity.X = 0f;
			base.projectile.velocity.Y = 0f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath10, (int)base.projectile.position.X, (int)base.projectile.position.Y);
			for (int i = -32; i <= 32; i++)
			{
				Projectile.NewProjectile(base.projectile.Center, 10f * Utils.RotatedBy(Vector2.UnitX, 0.09817477042468103 * (double)i, default(Vector2)), ModContent.ProjectileType<InfectionDust>(), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			Gore.NewGore(base.projectile.position, base.projectile.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore1"), 1f);
			Gore.NewGore(base.projectile.position, base.projectile.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore2"), 1f);
			Gore.NewGore(base.projectile.position, base.projectile.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore3"), 1f);
			Gore.NewGore(base.projectile.position, base.projectile.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore4"), 1f);
			Gore.NewGore(base.projectile.position, base.projectile.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore5"), 1f);
			Gore.NewGore(base.projectile.position, base.projectile.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore6"), 1f);
			Gore.NewGore(base.projectile.position, base.projectile.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore7"), 1f);
			for (int num67 = 0; num67 < 20; num67++)
			{
				int num68 = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 74, 0f, 0f, 100, default(Color), 3.5f);
				Main.dust[num68].velocity *= 3f;
				Main.dust[num68].noGravity = true;
			}
		}
	}
}
