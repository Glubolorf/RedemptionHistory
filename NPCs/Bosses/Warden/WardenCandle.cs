using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class WardenCandle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Warden's Candle");
			Main.projFrames[base.projectile.type] = 9;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 32;
			base.projectile.height = 32;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			NPC npc = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.Center = new Vector2(player.Center.X, player.Center.Y - 80f);
			base.projectile.timeLeft = 10;
			if (player.dead || !player.active || !NPC.AnyNPCs(ModContent.NPCType<WardenIdle>()))
			{
				base.projectile.Kill();
			}
			if (base.projectile.localAI[0] > 0f)
			{
				base.projectile.localAI[0] -= 1f;
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && (proj.type == ModContent.ProjectileType<WardenSwingHitbox>() || proj.type == ModContent.ProjectileType<WardenCandleHitbox>()) && proj.active && base.projectile.localAI[0] <= 0f && base.projectile.frame < 8)
				{
					Main.PlaySound(29, base.projectile.position, 82);
					base.projectile.localAI[0] = 60f;
					for (int i = 0; i < 3; i++)
					{
						Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 1f);
						dust.velocity = -base.projectile.DirectionTo(dust.position);
					}
					base.projectile.frame++;
					player.AddBuff(22, 120, true);
				}
			}
			if (player.wet && !player.lavaWet && base.projectile.frame < 8)
			{
				Main.PlaySound(29, base.projectile.position, 82);
				player.AddBuff(22, 120, true);
				base.projectile.frame = 8;
			}
			if (base.projectile.frame >= 8)
			{
				if (!this.text)
				{
					BaseUtility.Chat("A candle has been extinguished...", Color.LightGray, true);
					this.text = true;
				}
				player.AddBuff(163, 20, true);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (base.projectile.ai[0] != -1f)
			{
				NPC boss = Main.npc[(int)base.projectile.ai[0]];
				if (boss.ai[0] != 31f && boss.ai[0] != 7f && base.projectile.frame < 8)
				{
					Color c = BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
					{
						Color.GhostWhite * 0.75f,
						Color.GhostWhite * 0.25f,
						Color.GhostWhite * 0.75f
					});
					spriteBatch.Draw(base.mod.GetTexture("ExtraTextures/FadeTelegraph"), base.projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 64, 128)), c * 0.25f, Utils.ToRotation(boss.Center - base.projectile.Center), new Vector2(0f, 64f), new Vector2((float)(4 - base.projectile.frame / 2), (float)base.projectile.width / 128f), SpriteEffects.None, 0f);
					spriteBatch.Draw(base.mod.GetTexture("ExtraTextures/FadeTelegraphCap"), base.projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 64, 128)), c * 0.25f, Utils.ToRotation(base.projectile.Center - boss.Center), new Vector2(0f, 64f), new Vector2((float)base.projectile.width / 128f, (float)base.projectile.width / 128f), SpriteEffects.None, 0f);
				}
			}
			return true;
		}

		public bool text;
	}
}
