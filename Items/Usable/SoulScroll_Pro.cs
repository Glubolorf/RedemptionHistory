using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Friendly;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class SoulScroll_Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul Scroll");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 52;
			base.projectile.height = 52;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.velocity *= 0f;
			base.projectile.Center = new Vector2(player.Center.X, player.Center.Y - 100f);
			if (base.projectile.localAI[0] < 50f)
			{
				Projectile projectile = base.projectile;
				int num = projectile.frameCounter + 1;
				projectile.frameCounter = num;
				if (num >= 6)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 8)
					{
						base.projectile.frame = 7;
					}
				}
			}
			else
			{
				base.projectile.alpha -= 4;
				Projectile projectile3 = base.projectile;
				int num = projectile3.frameCounter + 1;
				projectile3.frameCounter = num;
				if (num >= 6)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile4 = base.projectile;
					num = projectile4.frame - 1;
					projectile4.frame = num;
					if (num < 0)
					{
						base.projectile.Kill();
					}
				}
			}
			if (base.projectile.frame >= 5)
			{
				base.projectile.localAI[0] += 1f;
				if (base.projectile.localAI[0] == 1f && Main.myPlayer == base.projectile.owner)
				{
					Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<BlindingLight>(), 0, 0f, Main.myPlayer, 0f, 0f);
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/NebSound2").WithVolume(0.9f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
					}
				}
				if (base.projectile.localAI[0] == 25f)
				{
					Main.PlaySound(SoundID.NPCDeath39, base.projectile.position);
					for (int i = 0; i < 200; i++)
					{
						this.target = Main.npc[i];
						if (!this.target.boss && Lists.IsSoulless.Contains(this.target.type))
						{
							this.target.Transform(ModContent.NPCType<LostSoul2>());
						}
					}
				}
			}
		}

		private NPC target;
	}
}
