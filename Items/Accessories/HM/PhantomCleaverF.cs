using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class PhantomCleaverF : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/VCleaver/CleaverClone1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Phantom Cleaver");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 46;
			base.projectile.height = 280;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			int npc = (int)base.projectile.ai[0];
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.rotation = 3.1415927f;
			float obj = base.projectile.localAI[1];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					return;
				}
				base.projectile.friendly = true;
				base.projectile.velocity.X = 0f;
				Projectile projectile = base.projectile;
				projectile.velocity.Y = projectile.velocity.Y + 10f;
				base.projectile.alpha += 10;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			else
			{
				if (npc < 0 || npc >= 200 || !Main.npc[npc].active)
				{
					base.projectile.velocity *= 0f;
				}
				else
				{
					base.projectile.Center = new Vector2(npc2.Center.X, npc2.position.Y - 200f);
				}
				base.projectile.alpha -= 5;
				if (base.projectile.alpha <= 80)
				{
					base.projectile.friendly = true;
					base.projectile.localAI[1] = 1f;
					return;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(Color.Red) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
