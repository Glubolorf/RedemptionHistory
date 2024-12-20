﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class VlitchCore3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Core");
		}

		public override void SetDefaults()
		{
			base.npc.width = 122;
			base.npc.height = 138;
			base.npc.friendly = false;
			base.npc.damage = 80;
			base.npc.defense = 0;
			base.npc.lifeMax = 18000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 5;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.boss = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossVlitch1");
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 82;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCoreGore1"), 1f);
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
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
			if (Main.rand.Next(6) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
			this.specialAttack1++;
			if (this.specialAttack1 == 30)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, -8f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
			}
			if (this.specialAttack1 == 60)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, -6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
			}
			if (this.specialAttack1 == 90)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
			}
			if (this.specialAttack1 == 120)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, 6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
			}
			if (this.specialAttack1 == 150)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, 8f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
			}
			if (this.specialAttack1 == 180)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, 6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
			}
			if (this.specialAttack1 == 210)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-8f, 0f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
			}
			if (this.specialAttack1 >= 240)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, -6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				this.specialAttack1 = 0;
			}
			if (base.npc.life < 6000)
			{
				this.specialAttack2++;
				if (this.specialAttack2 == 10)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, -8f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack2 == 20)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, -6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack2 == 30)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack2 == 40)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, 6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack2 == 50)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, 8f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack2 == 60)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, 6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack2 == 70)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-8f, 0f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				}
				if (this.specialAttack2 >= 80)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, -6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
					this.specialAttack2 = 0;
				}
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

		public int specialAttack1;

		public int specialAttack2;
	}
}