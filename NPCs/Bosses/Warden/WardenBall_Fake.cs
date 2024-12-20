using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class WardenBall_Fake : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Warden/WardenBall";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ball");
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 26;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.ignoreWater = true;
			base.projectile.hide = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCs.Add(index);
		}

		public override void AI()
		{
			NPC host = Main.npc[(int)base.projectile.ai[0]];
			if (!host.active || host.type != ModContent.NPCType<WardenIdle_Fake>())
			{
				base.projectile.Kill();
			}
			if (host.ai[1] == 1f)
			{
				base.projectile.Center = new Vector2(host.Center.X + (float)((host.spriteDirection == 1) ? 9 : -9), host.Center.Y + 150f);
				host.ai[1] = 0f;
			}
			base.projectile.timeLeft = 10;
			Vector2 position = base.projectile.Center;
			Vector2 vector2_4 = host.Center - position;
			base.projectile.rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) + 1.57f;
			base.projectile.alpha = host.alpha;
			this.Move(new Vector2(host.Center.X + (float)((host.spriteDirection == 1) ? 9 : -9), host.Center.Y + 150f), 9f, 20f);
		}

		public void Move(Vector2 offset, float speed, float turnResistance)
		{
			NPC npc = Main.npc[(int)base.projectile.ai[0]];
			Vector2 move = offset - base.projectile.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			move = (base.projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			base.projectile.velocity = move;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			NPC host = Main.npc[(int)base.projectile.ai[0]];
			Texture2D ballTexture = Main.projectileTexture[base.projectile.type];
			Vector2 anchorPos = base.projectile.Center;
			Texture2D chainTexture = ModContent.GetTexture("Redemption/NPCs/Bosses/Warden/WardenChain");
			Vector2 HeadPos = host.Center + new Vector2((float)((host.spriteDirection == 1) ? 9 : -9), 29f);
			Rectangle? sourceRectangle = null;
			Vector2 origin = new Vector2((float)chainTexture.Width * 0.5f, (float)chainTexture.Height * 0.5f);
			float num = (float)chainTexture.Height;
			Vector2 vector2_4 = anchorPos - HeadPos;
			float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
			bool flag = true;
			if (float.IsNaN(HeadPos.X) && float.IsNaN(HeadPos.Y))
			{
				flag = false;
			}
			if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
			{
				flag = false;
			}
			while (flag)
			{
				if ((double)vector2_4.Length() < (double)num + 1.0)
				{
					flag = false;
				}
				else
				{
					Vector2 vector2_5 = vector2_4;
					vector2_5.Normalize();
					HeadPos += vector2_5 * num;
					vector2_4 = anchorPos - HeadPos;
					Main.spriteBatch.Draw(chainTexture, HeadPos - Main.screenPosition, sourceRectangle, lightColor * ((float)(255 - base.projectile.alpha) / 255f), rotation, origin, 1f, SpriteEffects.None, 0f);
				}
			}
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, 0, ballTexture.Width, ballTexture.Height);
			Vector2 origin2 = new Vector2((float)(ballTexture.Width / 2), (float)(ballTexture.Height / 2));
			spriteBatch.Draw(ballTexture, position, new Rectangle?(rect), lightColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin2, base.projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
