using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class AkkaBubble : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bubble");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(523);
			this.aiType = 523;
			base.projectile.width = 32;
			base.projectile.height = 32;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 400;
		}

		public override void AI()
		{
			if (this.ZAPPED)
			{
				base.projectile.hostile = true;
				Projectile projectile = base.projectile;
				int num = projectile.frameCounter + 1;
				projectile.frameCounter = num;
				if (num >= 3)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 6)
					{
						base.projectile.frame = 1;
					}
				}
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && !this.ZAPPED && proj.type == ModContent.ProjectileType<UkkoElectricBlast2>())
				{
					for (int i = 0; i < 7; i++)
					{
						Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 226, 0f, 0f, 100, default(Color), 2f);
						dust.velocity = -base.projectile.DirectionTo(dust.position);
					}
					this.ZAPPED = true;
				}
			}
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(103, 600, false);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item54, base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			int num215 = texture.Height / 6;
			int y7 = num215 * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y7, texture.Width, num215);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)num215 / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, (base.projectile.frame == 0) ? BlendState.NonPremultiplied : BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(144, target.HasBuff(103) ? 320 : 160, true);
		}

		private Player clearCheck;

		public bool ZAPPED;
	}
}
