using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption
{
	public class RedePlayer : ModPlayer
	{
		public override void UpdateBiomes()
		{
			this.ZoneXeno = (RedeWorld.xenoBiome > 75);
		}

		public override bool CustomBiomesMatch(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>(base.mod);
			return this.ZoneXeno == modPlayer.ZoneXeno;
		}

		public override void CopyCustomBiomesTo(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>(base.mod);
			modPlayer.ZoneXeno = this.ZoneXeno;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte bitsByte = default(BitsByte);
			bitsByte[0] = this.ZoneXeno;
			writer.Write(bitsByte);
		}

		public override Texture2D GetMapBackgroundImage()
		{
			if (this.ZoneXeno)
			{
				return base.mod.GetTexture("XenoBiomeMapBackground");
			}
			return null;
		}

		public override void ReceiveCustomBiomes(BinaryReader reader)
		{
			this.ZoneXeno = reader.ReadByte()[0];
		}

		public override void OnRespawn(Player player)
		{
			if (this.heartEmblem)
			{
				player.statLife = player.statLifeMax2 / 100 * 75;
				player.AddBuff(base.mod.BuffType("HeartEmblemBuff"), 1800, true);
			}
		}

		public override void ResetEffects()
		{
			this.chickenMinion = false;
			this.xenoMinion = false;
			this.examplePet = false;
			this.corpseskullMinion = false;
			this.mk1MicrobotMinion = false;
			this.mk2MicrobotMinion = false;
			this.mk3MicrobotMinion = false;
			this.darkSoulMinion = false;
			this.xenoHatchlingMinion = false;
			this.heartEmblem = false;
			this.moreSeeds = false;
			this.fasterStaves = false;
			this.fasterSeedbags = false;
			this.fasterSpirits = false;
			this.moreSpirits = false;
			this.rainbowCatPet = false;
			this.golemWateringCan = false;
			this.spiritHoming = false;
			this.spiritChicken = false;
			this.extraSeed = false;
			this.spiritPierce = false;
			this.frostburnSeedbag = false;
			this.skeleMinion1 = false;
			this.spiritSkull1 = false;
			this.burnStaves = false;
			this.seedHit = false;
			this.bloodyCollar = false;
			this.taintedNecklace = false;
			this.sapphireBonus = false;
			this.scarletBonus = false;
			this.skeletonCan = false;
			this.enjoyment = false;
			this.ultraFlames = false;
			RedePlayer.reflectProjs = false;
		}

		public override void UpdateDead()
		{
			this.enjoyment = false;
			this.ultraFlames = false;
		}

		public override void UpdateBadLifeRegen()
		{
			if (this.enjoyment)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 15;
			}
			if (this.ultraFlames)
			{
				if (base.player.lifeRegen > 0)
				{
					base.player.lifeRegen = 0;
				}
				base.player.lifeRegenTime = 0;
				base.player.lifeRegen -= 40;
			}
		}

		public override void SetupStartInventory(IList<Item> items)
		{
			Item item = new Item();
			item.SetDefaults(base.mod.ItemType("EmptyCore"), false);
			item.stack = 1;
			items.Add(item);
		}

		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (this.enjoyment && Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
			{
				int num = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 243, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 1.8f;
				Dust dust = Main.dust[num];
				dust.velocity.Y = dust.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(num);
			}
			if (this.ultraFlames && Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
			{
				int num2 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), base.player.width + 4, base.player.height + 4, 92, base.player.velocity.X * 0.4f, base.player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 1.8f;
				Dust dust2 = Main.dust[num2];
				dust2.velocity.Y = dust2.velocity.Y - 0.5f;
				Main.playerDrawDust.Add(num2);
			}
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (this.bloodyCollar)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, base.mod.ProjectileType("BloodPulse"), 50, 0f, base.player.whoAmI, 0f, 0f);
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (this.bloodyCollar)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, 0f, 0f, base.mod.ProjectileType("BloodPulse"), 50, 0f, base.player.whoAmI, 0f, 0f);
			}
		}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteDebuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got infected");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteDebuff2")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got heavily infected");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("EmpoweredBuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " got too empowered");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("RadioactiveFalloutDebuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason(base.player.name + " forgot to wear a gas mask");
			}
			return true;
		}

		public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (junk)
			{
				return;
			}
			if (this.ZoneXeno && liquidType == 0 && questFish == base.mod.ItemType("XenChomper") && Main.rand.Next(1) == 0)
			{
				caughtType = base.mod.ItemType("XenChomper");
			}
		}

		public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			if (this.seedHit && Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 60, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.golemWateringCan)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 60, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 60, 1f, Main.myPlayer, 0f, 0f);
				if (this.moreSeeds)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 60, 1f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 483, 60, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.skeletonCan)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("BoneSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("BoneSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
				}
				if (this.moreSeeds)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("BoneSeed"), 10, 1f, Main.myPlayer, 0f, 0f);
				}
			}
			if (this.spiritChicken)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("SpiritChickenPro"), 9, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.spiritSkull1)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("SpiritSkullPro"), 12, 1f, Main.myPlayer, 0f, 0f);
			}
			if (this.taintedNecklace)
			{
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.rand.Next(4) == 0)
				{
					Projectile.NewProjectile(base.player.Center.X, base.player.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("CorruptSoul"), 50, 1f, Main.myPlayer, 0f, 0f);
				}
			}
		}

		private const int saveVersion = 0;

		public bool examplePet;

		public bool chickenMinion;

		public bool xenoMinion;

		public bool corpseskullMinion;

		public bool mk1MicrobotMinion;

		public bool mk2MicrobotMinion;

		public bool mk3MicrobotMinion;

		public bool darkSoulMinion;

		public bool xenoHatchlingMinion;

		public bool ZoneXeno;

		public bool heartEmblem;

		public bool moreSeeds;

		public bool fasterStaves;

		public bool fasterSeedbags;

		public bool fasterSpirits;

		public bool moreSpirits;

		public bool rainbowCatPet;

		public bool golemWateringCan;

		public bool spiritHoming;

		public bool spiritChicken;

		public bool extraSeed;

		public bool spiritPierce;

		public bool frostburnSeedbag;

		public bool skeleMinion1;

		public bool spiritSkull1;

		public bool burnStaves;

		public bool seedHit;

		public bool bloodyCollar;

		public bool taintedNecklace;

		public bool sapphireBonus;

		public bool scarletBonus;

		public bool skeletonCan;

		public bool enjoyment;

		public bool ultraFlames;

		public static bool reflectProjs;
	}
}
