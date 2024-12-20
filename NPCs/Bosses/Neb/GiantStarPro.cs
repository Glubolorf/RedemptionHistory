using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.Neb.Clone;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class GiantStarPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Giant Star");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 354;
			base.projectile.height = 339;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 180;
			base.projectile.alpha = 255;
			base.projectile.scale = 1f;
		}

		public override void AI()
		{
			NPC npc = Main.npc[(int)base.projectile.ai[0]];
			if (!npc.active || (npc.type != ModContent.NPCType<NebP1>() && npc.type != ModContent.NPCType<NebP1_Clone>() && npc.type != ModContent.NPCType<NebP2>() && npc.type != ModContent.NPCType<NebP2_Clone>()))
			{
				base.projectile.Kill();
			}
			base.projectile.Center = npc.Center;
			base.projectile.localAI[1] += 1f;
			if (base.projectile.localAI[1] == 10f)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/NebSound2"), (int)base.projectile.position.X, (int)base.projectile.position.Y);
			}
			base.projectile.rotation += 0.1f;
			float obj = base.projectile.localAI[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (2f.Equals(obj))
					{
						if (base.projectile.alpha < 255)
						{
							base.projectile.alpha += 20;
						}
						base.projectile.scale -= 0.06f;
						base.projectile.scale = MathHelper.Clamp(base.projectile.scale, 0.01f, 1.5f);
						if (base.projectile.scale <= 0.01f)
						{
							base.projectile.Kill();
						}
					}
				}
				else
				{
					if (base.projectile.alpha > 0)
					{
						base.projectile.alpha -= 20;
					}
					base.projectile.scale += 0.06f;
					base.projectile.scale = MathHelper.Clamp(base.projectile.scale, 0.01f, 1.5f);
					if (base.projectile.scale >= 1.5f)
					{
						base.projectile.localAI[0] = 2f;
					}
				}
			}
			else
			{
				base.projectile.scale = 0.01f;
				base.projectile.localAI[0] = 1f;
			}
			if (base.projectile.scale > 0.8f)
			{
				base.projectile.hostile = true;
				return;
			}
			base.projectile.hostile = false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), Main.DiscoColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), Main.DiscoColor * 0.7f * ((float)(255 - base.projectile.alpha) / 255f), -base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
