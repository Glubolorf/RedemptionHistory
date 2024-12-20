using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.VCleaver;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class RemoteCleaver : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/VCleaver/VlitchCleaver";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Cleaver");
			Main.projFrames[base.projectile.type] = 5;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 98;
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
			for (int i = base.projectile.oldPos.Length - 1; i > 0; i--)
			{
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldrot[0] = base.projectile.rotation;
			Player player = Main.player[base.projectile.owner];
			float obj = base.projectile.ai[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (!2f.Equals(obj))
					{
						if (!3f.Equals(obj))
						{
							return;
						}
						float obj2 = base.projectile.localAI[0];
						if (!0f.Equals(obj2))
						{
							if (!1f.Equals(obj2))
							{
								if (!2f.Equals(obj2))
								{
									return;
								}
								if (base.projectile.localAI[1] > 90f)
								{
									base.projectile.alpha += 5;
									if (base.projectile.alpha >= 255)
									{
										base.projectile.Kill();
									}
								}
							}
							else
							{
								Projectile projectile = base.projectile;
								int num = projectile.frameCounter + 1;
								projectile.frameCounter = num;
								if (num >= 10)
								{
									base.projectile.frameCounter = 0;
									Projectile projectile2 = base.projectile;
									num = projectile2.frame + 1;
									projectile2.frame = num;
									if (num >= 5)
									{
										base.projectile.frame = 1;
									}
								}
								base.projectile.localAI[1] += 1f;
								if (base.projectile.localAI[1] == 40f)
								{
									if (RedeHelper.ClosestNPC(ref this.target, 2000f, base.projectile.Center, true, player.MinionAttackTargetNPC, null) && base.projectile.Center.X < this.target.Center.X)
									{
										this.repeat = 1;
									}
									if (Main.myPlayer == base.projectile.owner)
									{
										Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y) + RedeHelper.PolarVector(134f, base.projectile.rotation + -1.5707964f), RedeHelper.PolarVector(9f, base.projectile.rotation + -1.5707964f), ModContent.ProjectileType<RedPrismF>(), 1000, base.projectile.knockBack, Main.myPlayer, (float)base.projectile.whoAmI, 0f);
									}
								}
								if (base.projectile.localAI[1] > 40f)
								{
									if (this.repeat == 1)
									{
										base.projectile.rotation += 0.01f;
									}
									else
									{
										base.projectile.rotation -= 0.01f;
									}
									base.projectile.rotation *= 1.04f;
								}
								if (base.projectile.localAI[1] > 120f)
								{
									base.projectile.localAI[0] = 2f;
									return;
								}
							}
						}
						else
						{
							base.projectile.frame = 0;
							base.projectile.localAI[1] += 1f;
							base.projectile.alpha -= 5;
							if (base.projectile.alpha <= 0)
							{
								if (!Main.dedServ)
								{
									Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/NebSound2").WithVolume(0.9f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
								}
								base.projectile.alpha = 0;
								base.projectile.localAI[0] = 1f;
								base.projectile.localAI[1] = 0f;
								return;
							}
						}
					}
					else
					{
						base.projectile.frame = 0;
						float obj2 = base.projectile.localAI[0];
						if (!0f.Equals(obj2))
						{
							if (!1f.Equals(obj2))
							{
								return;
							}
							base.projectile.localAI[1] += 1f;
							ref this.rot.SlowRotation(Utils.ToRotation(base.projectile.DirectionTo(Main.MouseWorld)) + 1.57f, 0.10471976f);
							base.projectile.rotation = this.rot;
							base.projectile.velocity *= 0.96f;
							if (base.projectile.localAI[1] >= 60f && base.projectile.localAI[1] % 5f == 0f && base.projectile.localAI[1] < 130f && Main.myPlayer == base.projectile.owner)
							{
								Projectile.NewProjectile(base.projectile.Center, new Vector2(Utils.NextFloat(Main.rand, -6f, 7f), Utils.NextFloat(Main.rand, -6f, 7f)), ModContent.ProjectileType<CleaverClone1F>(), 1000, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
							}
							if (base.projectile.localAI[1] > 140f)
							{
								base.projectile.alpha += 5;
								if (base.projectile.alpha >= 255)
								{
									base.projectile.Kill();
									return;
								}
							}
						}
						else
						{
							base.projectile.localAI[1] += 1f;
							base.projectile.alpha -= 5;
							if (base.projectile.alpha <= 0)
							{
								base.projectile.alpha = 0;
								base.projectile.localAI[0] = 1f;
								base.projectile.localAI[1] = 0f;
								return;
							}
						}
					}
				}
				else
				{
					float obj2 = base.projectile.localAI[0];
					if (!0f.Equals(obj2))
					{
						if (!1f.Equals(obj2))
						{
							return;
						}
						Projectile projectile3 = base.projectile;
						int num = projectile3.frameCounter + 1;
						projectile3.frameCounter = num;
						if (num >= 10)
						{
							base.projectile.frameCounter = 0;
							Projectile projectile4 = base.projectile;
							num = projectile4.frame + 1;
							projectile4.frame = num;
							if (num >= 5)
							{
								base.projectile.frame = 1;
							}
						}
						base.projectile.localAI[1] += 1f;
						if (base.projectile.localAI[1] < 50f)
						{
							base.projectile.velocity *= 0.94f;
							ref this.rot.SlowRotation(Utils.ToRotation(base.projectile.DirectionTo(Main.MouseWorld)) + 1.57f, 0.10471976f);
							base.projectile.rotation = this.rot;
						}
						else if (base.projectile.localAI[1] <= 70f)
						{
							base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
						}
						if (base.projectile.localAI[1] == 50f)
						{
							Main.PlaySound(SoundID.Item74, base.projectile.position);
							base.projectile.velocity = base.projectile.DirectionTo(Main.MouseWorld) * 40f;
							base.projectile.friendly = true;
						}
						if (base.projectile.localAI[1] > 70f)
						{
							base.projectile.friendly = false;
							base.projectile.alpha += 5;
							base.projectile.velocity *= 0.94f;
							if (base.projectile.alpha >= 255)
							{
								base.projectile.Kill();
								return;
							}
						}
					}
					else
					{
						base.projectile.frame = 0;
						base.projectile.localAI[1] += 1f;
						base.projectile.alpha -= 5;
						if (base.projectile.alpha <= 0)
						{
							base.projectile.alpha = 0;
							base.projectile.localAI[0] = 1f;
							base.projectile.localAI[1] = 0f;
							return;
						}
					}
				}
			}
			else
			{
				base.projectile.frame = 0;
				float obj2 = base.projectile.localAI[0];
				if (!0f.Equals(obj2))
				{
					if (!1f.Equals(obj2))
					{
						if (!2f.Equals(obj2))
						{
							if (!3f.Equals(obj2))
							{
								return;
							}
							base.projectile.friendly = false;
							base.projectile.alpha += 5;
							if (base.projectile.alpha >= 255)
							{
								base.projectile.Kill();
								return;
							}
						}
						else
						{
							base.projectile.localAI[1] += 1f;
							base.projectile.friendly = true;
							if (base.projectile.localAI[1] == 1f)
							{
								base.projectile.velocity.X = (float)((this.repeat == 0) ? 15 : -15);
							}
							if (base.projectile.localAI[1] == 11f)
							{
								base.projectile.velocity.X = (float)((this.repeat == 0) ? -15 : 15);
							}
							base.projectile.velocity.Y = 26f;
							base.projectile.rotation += ((this.repeat == 0) ? 0.1f : -0.1f);
							if (base.projectile.localAI[1] > 20f)
							{
								base.projectile.friendly = false;
								base.projectile.velocity *= 0f;
								base.projectile.localAI[0] = 3f;
								base.projectile.localAI[1] = 0f;
								return;
							}
						}
					}
					else
					{
						base.projectile.localAI[1] += 1f;
						base.projectile.velocity *= 0f;
						if (base.projectile.localAI[1] > 10f)
						{
							base.projectile.friendly = true;
							Main.PlaySound(SoundID.Item71, base.projectile.position);
							base.projectile.localAI[0] = 2f;
							base.projectile.localAI[1] = 0f;
							return;
						}
					}
				}
				else
				{
					base.projectile.localAI[1] += 1f;
					if (RedeHelper.ClosestNPC(ref this.target, 2000f, base.projectile.Center, true, player.MinionAttackTargetNPC, null))
					{
						ref this.rot.SlowRotation((this.target.Center.X > base.projectile.Center.X) ? 0.78f : 5.49f, 0.15707964f);
						base.projectile.rotation = this.rot;
						this.Move(new Vector2((float)((base.projectile.Center.X > this.target.Center.X) ? 200 : -200), -160f), 16f, 3f);
					}
					else
					{
						base.projectile.velocity *= 0.9f;
						ref this.rot.SlowRotation((player.direction == 1) ? 0.78f : 5.49f, 0.15707964f);
						base.projectile.rotation = this.rot;
					}
					base.projectile.alpha -= 5;
					if (base.projectile.alpha <= 0)
					{
						base.projectile.alpha = 0;
						if (RedeHelper.ClosestNPC(ref this.target, 2000f, base.projectile.Center, true, player.MinionAttackTargetNPC, null))
						{
							base.projectile.rotation = ((this.target.Center.X > base.projectile.Center.X) ? 0.78f : 5.49f);
							this.repeat = ((this.target.Center.X > base.projectile.Center.X) ? 0 : 1);
						}
						else
						{
							base.projectile.rotation = ((player.direction == 1) ? 0.78f : 5.49f);
							this.repeat = ((player.direction == 1) ? 0 : 1);
						}
						base.projectile.localAI[0] = 1f;
						base.projectile.localAI[1] = 0f;
						return;
					}
				}
			}
		}

		private void Move(Vector2 offset, float speed, float turnResistance = 10f)
		{
			Vector2 move = this.target.Center + offset - base.projectile.Center;
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

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float collisionPoint = 0f;
			return new bool?(Collision.CheckAABBvLineCollision(Utils.Center(targetHitbox), Utils.Size(targetHitbox), base.projectile.Center, base.projectile.Center + Utils.ToRotationVector2(base.projectile.rotation + -1.5707964f) * 140f, 58f, ref collisionPoint));
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
			{
				lightColor,
				Color.LightGreen,
				lightColor
			}));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/VCleaver/VlitchCleaver_Glow");
			Texture2D trail = base.mod.GetTexture("NPCs/Bosses/VCleaver/VlitchCleaver_Trail");
			SpriteEffects effects = (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			int num215 = texture.Height / 5;
			int y7 = num215 * base.projectile.frame;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.projectile.type]; i++)
			{
				Vector2 value4 = base.projectile.oldPos[i];
				Main.spriteBatch.Draw(trail, value4 + base.projectile.Size / 2f - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), RedeColor.VlitchGlowColour * 0.5f * ((float)(255 - base.projectile.alpha) / 255f), this.oldrot[i], new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, effects, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, effects, 0f);
			spriteBatch.Draw(glowMask, base.projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), RedeColor.COLOR_WHITEFADE2 * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, effects, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public float[] oldrot = new float[6];

		public float rot;

		public int repeat;

		private NPC target;
	}
}
