using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class WardenBall : ModProjectile
	{
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
			if (!host.active || host.type != ModContent.NPCType<WardenIdle>())
			{
				base.projectile.Kill();
			}
			if (host.ai[1] == 1f || (host.ai[1] == 3f && host.ai[0] == 5f) || host.ai[0] == 24f)
			{
				base.projectile.Center = new Vector2(host.Center.X + (float)((host.spriteDirection == 1) ? 9 : -9), host.Center.Y + 150f);
			}
			base.projectile.timeLeft = 10;
			Vector2 position = base.projectile.Center;
			Vector2 vector2_4 = host.Center - position;
			base.projectile.rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) + 1.57f;
			base.projectile.alpha = host.alpha;
			if (host.ai[0] == 28f && host.ai[1] <= 2f)
			{
				base.projectile.hostile = true;
				if (host.ai[1] == 2f)
				{
					Lighting.AddLight(base.projectile.Center, 1f, 1f, 1f);
					base.projectile.ai[1] += 1f;
					base.projectile.localAI[0] += 5.2f;
					if (base.projectile.ai[1] % 69f == 0f)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChainSwing"), (int)base.projectile.position.X, (int)base.projectile.position.Y);
					}
					if (base.projectile.localAI[1] < 180f)
					{
						base.projectile.localAI[1] += 1f;
					}
					if (base.projectile.ai[1] % 10f == 0f && Main.myPlayer == base.projectile.owner)
					{
						for (int i = 0; i < 16; i++)
						{
							int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 261, 0f, 0f, 100, Color.White, 1f);
							Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)i / 16f * 6.28f);
							Main.dust[dustID].noLight = false;
							Main.dust[dustID].noGravity = true;
						}
						Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<ShadeburstProj>(), 53, 1f, Main.myPlayer, 1f, 0f);
					}
				}
				base.projectile.Center = host.Center + Utils.RotatedBy(Vector2.One, (double)MathHelper.ToRadians(base.projectile.localAI[0]), default(Vector2)) * base.projectile.localAI[1];
				return;
			}
			if (host.ai[0] == 4f && host.ai[1] <= 2f)
			{
				base.projectile.hostile = true;
				if (host.ai[1] == 2f)
				{
					Lighting.AddLight(base.projectile.Center, 1f, 1f, 1f);
					base.projectile.ai[1] += 1f;
					base.projectile.localAI[0] += 3f * ((host.ai[3] + 1f) / 8f + 1f);
					float obj = host.ai[3];
					int soundTimer;
					if (!0f.Equals(obj))
					{
						if (!1f.Equals(obj))
						{
							if (!2f.Equals(obj))
							{
								if (!3f.Equals(obj))
								{
									if (!4f.Equals(obj))
									{
										soundTimer = 106;
									}
									else
									{
										soundTimer = 73;
									}
								}
								else
								{
									soundTimer = 80;
								}
							}
							else
							{
								soundTimer = 87;
							}
						}
						else
						{
							soundTimer = 96;
						}
					}
					else
					{
						soundTimer = 106;
					}
					if (base.projectile.ai[1] % (float)soundTimer == 0f)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChainSwing"), (int)base.projectile.position.X, (int)base.projectile.position.Y);
					}
					if (base.projectile.localAI[1] < 160f)
					{
						base.projectile.localAI[1] += 1f;
					}
					if (base.projectile.ai[1] % 30f / (host.ai[3] + 1f) == 0f && Main.myPlayer == base.projectile.owner)
					{
						for (int j = 0; j < 16; j++)
						{
							int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 261, 0f, 0f, 100, Color.White, 1f);
							Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)j / 16f * 6.28f);
							Main.dust[dustID2].noLight = false;
							Main.dust[dustID2].noGravity = true;
						}
						Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<ShadeburstProj>(), 53, 1f, Main.myPlayer, 1f, 0f);
					}
				}
				base.projectile.Center = host.Center + Utils.RotatedBy(Vector2.One, (double)MathHelper.ToRadians(base.projectile.localAI[0]), default(Vector2)) * base.projectile.localAI[1];
				return;
			}
			base.projectile.hostile = false;
			base.projectile.ai[1] = 0f;
			base.projectile.localAI[0] = 0f;
			base.projectile.localAI[1] = 0f;
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
