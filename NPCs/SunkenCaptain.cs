using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class SunkenCaptain : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sunken Captain");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 44;
			base.npc.height = 56;
			base.npc.friendly = false;
			base.npc.damage = 33;
			base.npc.defense = 0;
			base.npc.lifeMax = 1250;
			base.npc.HitSound = SoundID.NPCHit54;
			base.npc.DeathSound = SoundID.NPCDeath52;
			base.npc.value = (float)Item.buyPrice(0, 5, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 22;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 82;
			this.animationType = 316;
			base.npc.alpha = 255;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = 2;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 20; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 89, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = base.mod.ItemType("AncientGoldCoin");
			RedeWorld.downedSunkenCaptain = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void AI()
		{
			this.sunkenTimer++;
			if (this.sunkenTimer == 1)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				string text = "The full moon's light reflects the ocean waves...";
				Color rarityGreen = Colors.RarityGreen;
				byte r = rarityGreen.R;
				Color rarityGreen2 = Colors.RarityGreen;
				byte g = rarityGreen2.G;
				Color rarityGreen3 = Colors.RarityGreen;
				Main.NewText(text, r, g, rarityGreen3.B, false);
				if (Main.netMode != 1)
				{
					int num = Main.rand.Next(2);
					if (num == 0)
					{
						Vector2 vector;
						vector..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-300, -200));
						base.npc.Center = Main.player[base.npc.target].Center + vector;
						base.npc.netUpdate = true;
					}
					if (num == 1)
					{
						Vector2 vector2;
						vector2..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-300, -200));
						base.npc.Center = Main.player[base.npc.target].Center + vector2;
						base.npc.netUpdate = true;
					}
				}
			}
			if (this.sunkenTimer <= 120)
			{
				base.npc.velocity.X = 0f;
				base.npc.alpha--;
				base.npc.dontTakeDamage = true;
			}
			if (this.sunkenTimer > 120)
			{
				base.npc.dontTakeDamage = false;
			}
			if (Main.rand.Next(250) == 0 && this.sunkenTimer > 120 && NPC.CountNPCS(base.mod.NPCType("SunkenDeckhand")) <= 4)
			{
				NPC.NewNPC((int)(base.npc.position.X + Utils.NextFloat(Main.rand, (float)base.npc.width)), (int)(base.npc.position.Y + Utils.NextFloat(Main.rand, (float)base.npc.width)), base.mod.NPCType("SunkenDeckhand"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (Main.rand.Next(250) == 0 && this.sunkenTimer > 120 && NPC.CountNPCS(base.mod.NPCType("SunkenParrot")) <= 2)
			{
				NPC.NewNPC((int)(base.npc.position.X + Utils.NextFloat(Main.rand, (float)base.npc.width)), (int)(base.npc.position.Y + Utils.NextFloat(Main.rand, (float)base.npc.width)), base.mod.NPCType("SunkenParrot"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (Main.rand.Next(80) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 26f, base.npc.position.Y + 42f), new Vector2(2f, 0f), base.mod.ProjectileType("Fog1"), 0, 0f, 255, 0f, 0f);
			}
			if (Main.rand.Next(80) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 26f, base.npc.position.Y + 42f), new Vector2(-2f, 0f), base.mod.ProjectileType("Fog2"), 0, 0f, 255, 0f, 0f);
			}
			if (Main.rand.Next(80) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 26f, base.npc.position.Y + 24f), new Vector2(2f, 0f), base.mod.ProjectileType("Fog3"), 0, 0f, 255, 0f, 0f);
			}
			if (Main.rand.Next(80) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 26f, base.npc.position.Y + 24f), new Vector2(-2f, 0f), base.mod.ProjectileType("Fog4"), 0, 0f, 255, 0f, 0f);
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Ocean.Chance * ((!RedeWorld.downedSunkenCaptain && !NPC.AnyNPCs(base.mod.NPCType("SunkenCaptain")) && Main.moonPhase == 0 && !Main.dayTime) ? 0.3f : 0f);
		}

		private int sunkenTimer;
	}
}
