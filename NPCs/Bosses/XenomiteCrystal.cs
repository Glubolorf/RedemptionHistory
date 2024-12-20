using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class XenomiteCrystal : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Crystal");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 164;
			base.npc.height = 82;
			base.npc.friendly = false;
			base.npc.damage = 30;
			base.npc.defense = 10;
			base.npc.lifeMax = 4500;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 10;
			base.npc.boss = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossXeno1");
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 34;
			this.animationType = 34;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteCrystalGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				for (int i = 0; i < 35; i++)
				{
					int num = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num].velocity *= 3f;
					Main.dust[num].noGravity = true;
				}
			}
		}

		public override bool CheckDead()
		{
			base.npc.SetDefaults(base.mod.NPCType("XenomiteCrystalPhase2"), -1f);
			Main.NewText("The crystal has shattered...", Color.ForestGreen.R, Color.ForestGreen.G, Color.ForestGreen.B, false);
			return false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			base.npc.rotation += base.npc.velocity.X / 40f * (float)base.npc.direction;
			base.npc.rotation += base.npc.velocity.Y / 40f * (float)base.npc.direction;
			base.npc.ai[0] += 1f;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			this.startTimer++;
			if (this.startTimer <= 120)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.alpha -= 2;
				base.npc.dontTakeDamage = true;
			}
			if (this.startTimer > 120)
			{
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
			}
			if (this.startTimer == 120)
			{
				for (int i = 0; i < 25; i++)
				{
					int num = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 3.5f);
					Main.dust[num].velocity *= 3f;
					Main.dust[num].noGravity = true;
				}
			}
			if (this.beginFight)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] >= 280f)
				{
					float num2 = 12f;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num3 = 10;
					int num4 = base.mod.ProjectileType("XenomiteFragmentPro");
					float num5 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
					base.npc.ai[1] = 0f;
				}
				if (base.npc.life < 4000 && Main.rand.Next(149) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (base.npc.life < 2000 && Main.rand.Next(100) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (base.npc.life < 3000)
				{
					this.specialAttack1++;
					if (this.specialAttack1 == 600)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(0f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 15, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(0f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 15, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 15, 3f, 255, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 15, 3f, 255, 0f, 0f);
						this.specialAttack1 = 0;
					}
				}
				if (base.npc.life < 1500 && this.specialAttack1 == 300)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 15, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(5f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 15, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, 5f), base.mod.ProjectileType("XenomiteFragmentPro"), 15, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 86f, base.npc.position.Y + 44f), new Vector2(-5f, -5f), base.mod.ProjectileType("XenomiteFragmentPro"), 15, 3f, 255, 0f, 0f);
				}
			}
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("PuriumFlame"), 0f, 0f, 0, default(Color), 1f);
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.beginFight;
		}

		private Player player;

		public int specialAttack1;

		private int startTimer;

		private bool beginFight;
	}
}
