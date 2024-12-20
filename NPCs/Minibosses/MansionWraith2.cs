using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Weapons.PostML.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses
{
	public class MansionWraith2 : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Minibosses/MansionWraith";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Void Amalgam");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 40000;
			base.npc.damage = 110;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 92;
			base.npc.height = 136;
			base.npc.value = (float)Item.buyPrice(0, 6, 0, 0);
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.alpha = 255;
			base.npc.HitSound = SoundID.NPCHit54;
			base.npc.DeathSound = SoundID.NPCDeath52;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ModContent.ItemType<VesselFrag>();
			RedeWorld.downedMansionWraith = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<DaggerOfOathkeeper>(), 1, false, 0, false, false);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 35; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void AI()
		{
			if (this.floatTimer == 0f)
			{
				this.velY += 0.005f;
				if (this.velY > 0.3f)
				{
					this.floatTimer = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (this.floatTimer == 1f)
			{
				this.velY -= 0.005f;
				if (this.velY < -0.3f)
				{
					this.floatTimer = 0f;
					base.npc.netUpdate = true;
				}
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 6.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 142;
				if (base.npc.frame.Y >= 1136)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.ai[0] == 0f)
			{
				base.npc.ai[0] = 1f;
				base.npc.netUpdate = true;
				return;
			}
			if (base.npc.ai[0] == 1f)
			{
				base.npc.velocity *= 0f;
				base.npc.alpha -= 4;
				if (base.npc.alpha < 40)
				{
					base.npc.ai[0] = 2f;
					base.npc.netUpdate = true;
					return;
				}
			}
			else
			{
				if (base.npc.ai[0] == 2f)
				{
					base.npc.ai[2] = (float)Main.rand.Next(-120, 20);
					base.npc.ai[0] = 3f;
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[0] == 3f)
				{
					this.Move(new Vector2(0f, this.velY));
					float[] ai = base.npc.ai;
					int num = 2;
					float num2 = ai[num] + 1f;
					ai[num] = num2;
					if (num2 > 180f)
					{
						base.npc.ai[0] = 4f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
				}
				else if (base.npc.ai[0] == 4f)
				{
					base.npc.ai[2] += 1f;
					base.npc.velocity *= 0.96f;
					if (base.npc.ai[2] < 60f)
					{
						base.npc.ai[1] += 0.1f;
						base.npc.velocity = -base.npc.DirectionTo(player.Center) * base.npc.ai[1];
					}
					if (base.npc.ai[2] == 60f)
					{
						this.Dash(30);
					}
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.7f) && base.npc.ai[2] == 90f)
					{
						this.Dash(25);
					}
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.4f) && base.npc.ai[2] == 120f)
					{
						this.Dash(20);
					}
					if (base.npc.ai[2] == 150f)
					{
						base.npc.velocity *= 0f;
					}
					if (base.npc.ai[2] >= 160f)
					{
						base.npc.ai[2] = 0f;
						base.npc.ai[0] = 2f;
					}
				}
			}
		}

		public void Dash(int speed)
		{
			Player player = Main.player[base.npc.target];
			Main.PlaySound(SoundID.NPCDeath51, (int)base.npc.position.X, (int)base.npc.position.Y);
			int dustType = 261;
			int pieCut = 16;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			base.npc.ai[3] = 2f;
			base.npc.ai[1] = 0f;
			base.npc.velocity = base.npc.DirectionTo(player.Center) * (float)speed;
		}

		public void Move(Vector2 offset)
		{
			Entity entity = Main.player[base.npc.target];
			this.speed = 3f;
			Vector2 move = entity.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 40f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.npc.velocity = move;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public float speed;

		public float floatTimer;

		public float velY;
	}
}
