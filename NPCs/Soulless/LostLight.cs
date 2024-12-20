using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
{
	public class LostLight : ModNPC
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
			base.DisplayName.SetDefault("Strange Light");
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 1;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 8;
			base.npc.height = 8;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
			base.npc.immortal = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (this.floatTimer == 0f)
			{
				NPC npc = base.npc;
				npc.velocity.Y = npc.velocity.Y + 0.005f;
				if (base.npc.velocity.Y > 0.3f)
				{
					this.floatTimer = 1f;
					base.npc.netUpdate = true;
				}
				base.npc.alpha--;
			}
			else if (this.floatTimer == 1f)
			{
				NPC npc2 = base.npc;
				npc2.velocity.Y = npc2.velocity.Y - 0.005f;
				if (base.npc.velocity.Y < -0.3f)
				{
					this.floatTimer = 0f;
					base.npc.netUpdate = true;
				}
				base.npc.alpha++;
			}
			Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 20, base.npc.velocity.X * 0.2f, base.npc.velocity.Y * 0.2f, 20, default(Color), 2f);
			for (int i = 0; i < 2; i++)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 40.0);
				this.vector.Y = (float)(Math.Cos(angle) * 40.0);
				Main.dust[Dust.NewDust(base.npc.Center + this.vector, 2, 2, 261, 0f, 0f, 100, default(Color), 2f)].noGravity = true;
			}
			double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
			this.vector2.X = (float)(Math.Sin(angle2) * 90.0);
			this.vector2.Y = (float)(Math.Cos(angle2) * 90.0);
			Dust dust3 = Main.dust[Dust.NewDust(base.npc.Center + this.vector2, 2, 2, 261, 0f, 0f, 100, default(Color), 1f)];
			dust3.noGravity = true;
			dust3.velocity = -base.npc.DirectionTo(dust3.position) * 4f;
			Lighting.AddLight(base.npc.Center, (float)(255 - base.npc.alpha) * 1f / 255f, (float)(255 - base.npc.alpha) * 1f / 255f, (float)(255 - base.npc.alpha) * 1f / 255f);
			float[] ai = base.npc.ai;
			int num = 2;
			float num2 = ai[num] + 1f;
			ai[num] = num2;
			if (num2 % 180f == 0f)
			{
				switch (Main.rand.Next(9))
				{
				case 0:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Dunn olv syht...", true, true);
					break;
				case 1:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "I il lyht ka wye...", true, true);
					break;
				case 2:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Folke...", true, true);
					break;
				case 3:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Folke ufe...", true, true);
					break;
				case 4:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Dozmu...", true, true);
					break;
				case 5:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Jugh...", true, true);
					break;
				case 6:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Qua sudki uque ka senkar'ka...", true, true);
					break;
				case 7:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "I il lyht ka senkar'ka...", true, true);
					break;
				case 8:
					CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Roma sudki'nin...", true, true);
					break;
				}
			}
			if (Vector2.Distance(base.npc.Center, player.Center) < 600f)
			{
				if (base.npc.ai[0] == 0f)
				{
					num2 = base.npc.ai[1];
					if (!0f.Equals(num2))
					{
						if (!1f.Equals(num2))
						{
							return;
						}
						Vector2 Pos1_2 = new Vector2(543f, 802f) * 16f;
						this.MoveToVector2(Pos1_2);
						if (base.npc.Distance(Pos1_2) < 50f)
						{
							CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Kliq...", true, false);
							base.npc.alpha = 255;
							base.npc.active = false;
							return;
						}
					}
					else
					{
						Vector2 Pos = new Vector2(475f, 802f) * 16f;
						this.MoveToVector2(Pos);
						if (base.npc.Distance(Pos) < 50f)
						{
							base.npc.ai[1] = 1f;
							return;
						}
					}
				}
				else if (base.npc.ai[0] == 1f)
				{
					num2 = base.npc.ai[1];
					if (!0f.Equals(num2))
					{
						if (!1f.Equals(num2))
						{
							if (!2f.Equals(num2))
							{
								if (!3f.Equals(num2))
								{
									if (!4f.Equals(num2))
									{
										return;
									}
									Vector2 Pos2 = new Vector2(308f, 1274f) * 16f;
									this.MoveToVector2(Pos2);
									if (base.npc.Distance(Pos2) < 50f)
									{
										CombatText.NewText(base.npc.getRect(), Color.GhostWhite, "Below...", true, false);
										base.npc.alpha = 255;
										base.npc.active = false;
										return;
									}
								}
								else
								{
									Vector2 Pos3 = new Vector2(308f, 1015f) * 16f;
									this.MoveToVector2(Pos3);
									if (base.npc.Distance(Pos3) < 50f)
									{
										base.npc.ai[1] = 4f;
										return;
									}
								}
							}
							else
							{
								Vector2 Pos4 = new Vector2(408f, 982f) * 16f;
								this.MoveToVector2(Pos4);
								if (base.npc.Distance(Pos4) < 50f)
								{
									base.npc.ai[1] = 3f;
									return;
								}
							}
						}
						else
						{
							Vector2 Pos5 = new Vector2(408f, 947f) * 16f;
							this.MoveToVector2(Pos5);
							if (base.npc.Distance(Pos5) < 50f)
							{
								base.npc.ai[1] = 2f;
								return;
							}
						}
					}
					else
					{
						Vector2 Pos6 = new Vector2(455f, 898f) * 16f;
						this.MoveToVector2(Pos6);
						if (base.npc.Distance(Pos6) < 50f)
						{
							base.npc.ai[1] = 1f;
							return;
						}
					}
				}
				else if (base.npc.ai[0] == 2f)
				{
					Vector2 Pos7 = new Vector2(861f, 1356f) * 16f;
					this.MoveToVector2(Pos7);
					if (base.npc.Distance(Pos7) < 100f)
					{
						base.npc.alpha = 255;
						base.npc.active = false;
						return;
					}
				}
				else if (base.npc.ai[0] == 3f)
				{
					Vector2 Pos8 = new Vector2(1244f, 1189f) * 16f;
					this.MoveToVector2(Pos8);
					if (base.npc.Distance(Pos8) < 100f)
					{
						base.npc.alpha = 255;
						base.npc.active = false;
						return;
					}
				}
				else if (base.npc.ai[0] == 4f)
				{
					Vector2 Pos9 = new Vector2(1306f, 1109f) * 16f;
					this.MoveToVector2(Pos9);
					if (base.npc.Distance(Pos9) < 100f)
					{
						base.npc.alpha = 255;
						base.npc.active = false;
						return;
					}
				}
			}
			else
			{
				base.npc.velocity *= 0f;
			}
		}

		public override bool CheckActive()
		{
			return false;
		}

		public void MoveToVector2(Vector2 p)
		{
			Player player = Main.player[base.npc.target];
			float moveSpeed = 3f;
			if (Vector2.Distance(base.npc.Center, player.Center) < 100f)
			{
				moveSpeed = 12f;
			}
			else if (Vector2.Distance(base.npc.Center, player.Center) < 300f)
			{
				moveSpeed = 6f;
			}
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		public float speed;

		private Vector2 vector;

		private Vector2 vector2;

		public float floatTimer;
	}
}
