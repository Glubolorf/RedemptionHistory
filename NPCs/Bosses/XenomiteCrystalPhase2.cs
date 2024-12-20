using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class XenomiteCrystalPhase2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Crystal");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 54;
			base.npc.height = 50;
			base.npc.friendly = false;
			base.npc.damage = 32;
			base.npc.defense = 0;
			base.npc.lifeMax = 2000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 2, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 85;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.boss = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossXeno1");
			base.npc.alpha = 50;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 467;
			this.animationType = 34;
			this.bossBag = base.mod.ItemType("XenomiteCrystalBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
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
					Main.dust[num].velocity *= 5f;
					Main.dust[num].noGravity = true;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 188;
			RedeWorld.downedXenomiteCrystal = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteCrystalTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteGlaive"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("InfectedMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteYoyo"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenoCanister"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(12, 22), false, 0, false, false);
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
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("PuriumFlame"), 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(200) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 50, (int)base.npc.position.Y + 50, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (base.npc.life < 750 && Main.rand.Next(100) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 50, (int)base.npc.position.Y + 50, base.mod.NPCType("XenomitePiece"), 0, 0f, 0f, 0f, 0f, 255);
			}
			this.specialAttack2++;
			if (this.specialAttack2 == 300)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(0f, -8f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(0f, 8f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(-8f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(8f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(8f, 8f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(8f, -8f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(-8f, 8f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(-8f, -8f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				this.specialAttack2 = 0;
			}
			if (base.npc.life < 800 && this.specialAttack2 == 150)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(0f, -10f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(0f, 10f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(-10f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(10f, 0f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(10f, 10f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(10f, -10f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(-10f, 10f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 28f), new Vector2(-10f, -10f), base.mod.ProjectileType("XenomiteFragmentPro"), 20, 3f, 255, 0f, 0f);
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

		private Player player;

		public int specialAttack2;
	}
}
