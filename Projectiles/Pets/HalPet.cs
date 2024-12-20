using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Critters;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Quest.Zephos;
using Redemption.Items.Usable.Potions;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PreHM.Ranged;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Pets
{
	public class HalPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hal");
			Main.projFrames[base.projectile.type] = 16;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(236);
			base.projectile.width = 20;
			base.projectile.height = 34;
			this.aiType = 236;
		}

		public override bool PreAI()
		{
			Main.player[base.projectile.owner].dino = false;
			return true;
		}

		public override void AI()
		{
			if (base.projectile.velocity.Y >= 1f || base.projectile.velocity.Y <= -1f)
			{
				if (base.projectile.frame < 8)
				{
					base.projectile.frame = 8;
				}
				Projectile projectile = base.projectile;
				int num = projectile.frameCounter + 1;
				projectile.frameCounter = num;
				if (num >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 15)
					{
						base.projectile.frame = 8;
					}
				}
			}
			else if (base.projectile.velocity.X == 0f)
			{
				base.projectile.frame = 0;
			}
			else
			{
				base.projectile.frameCounter += (int)(base.projectile.velocity.X * 0.5f);
				if (base.projectile.frameCounter >= 5 || base.projectile.frameCounter <= -5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile3 = base.projectile;
					int num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 7)
					{
						base.projectile.frame = 1;
					}
				}
			}
			Player player = Main.player[base.projectile.owner];
			if (Main.rand.Next(1000000) == 0)
			{
				int num = Main.rand.Next(2);
				if (num != 0)
				{
					if (num == 1)
					{
						Projectile.NewProjectile(new Vector2(Main.screenPosition.X + (float)Main.screenWidth, player.Center.Y + (float)Main.rand.Next(-500, 500)), new Vector2(-6f, 0f), ModContent.ProjectileType<HalPetSPEEN>(), 9999, 20f, base.projectile.owner, 0f, 0f);
					}
				}
				else
				{
					Projectile.NewProjectile(new Vector2(Main.screenPosition.X, player.Center.Y + (float)Main.rand.Next(-500, 500)), new Vector2(6f, 0f), ModContent.ProjectileType<HalPetSPEEN>(), 9999, 20f, base.projectile.owner, 0f, 0f);
				}
			}
			if (player.HeldItem.type == 357 && Main.rand.Next(900) == 0)
			{
				int num = Main.rand.Next(2);
				if (num != 0)
				{
					if (num == 1)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "yum yum", false, false);
					}
				}
				else
				{
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "soup?", false, false);
				}
			}
			if (player.HeldItem.type == 2425 && Main.rand.Next(2) == 0)
			{
				switch (Main.rand.Next(4))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "phish", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "yum yum", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "fishy", false, false);
					break;
				case 3:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "gimmi phish", false, false);
					break;
				case 4:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "feed me", false, false);
					break;
				}
			}
			if ((player.HeldItem.type == 1920 || player.HeldItem.type == 1919) && Main.rand.Next(500) == 0)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "cookie", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "yum yum", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "gimmi cookie", false, false);
					break;
				}
			}
			if (player.HeldItem.type == 215 && Main.rand.Next(1200) == 0)
			{
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "*toot*", false, false);
			}
			if ((player.HeldItem.type == 1920 || player.HeldItem.type == 1919) && Main.rand.Next(800) == 0)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "cookie", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "yum yum", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "gimmi cookie", false, false);
					break;
				}
			}
			if ((player.HeldItem.type == ModContent.ItemType<ChickenEgg>() || player.HeldItem.type == ModContent.ItemType<FriedEgg>() || player.HeldItem.type == ModContent.ItemType<LongEgg>() || player.HeldItem.type == ModContent.ItemType<SuspEgg>() || player.HeldItem.type == ModContent.ItemType<ChickenEggGold>() || player.HeldItem.type == ModContent.ItemType<SuspiciousFriedEgg>()) && Main.rand.Next(300) == 0)
			{
				int num = Main.rand.Next(2);
				if (num != 0)
				{
					if (num == 1)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "chikcen", false, false);
					}
				}
				else
				{
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "egg", false, false);
				}
			}
			if ((player.HeldItem.type == ModContent.ItemType<ChickenGoldItem>() || player.HeldItem.type == ModContent.ItemType<ChickenItem>() || player.HeldItem.type == ModContent.ItemType<ChickenLeghornItem>() || player.HeldItem.type == ModContent.ItemType<ChickenLongItem>() || player.HeldItem.type == ModContent.ItemType<ChickenRedItem>() || player.HeldItem.type == ModContent.ItemType<ChickenVlitchItem>()) && Main.rand.Next(300) == 0)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "chkcien funni", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "chikcen", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "i laugh at chieken", false, false);
					break;
				}
			}
			if ((player.HeldItem.type == ModContent.ItemType<FriedChicken>() || player.HeldItem.type == ModContent.ItemType<BucketOfChicken>()) && Main.rand.Next(2) == 0)
			{
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "gimmi chciken", false, false);
			}
			if (player.HeldItem.type == ModContent.ItemType<HallamDevWeapon>() && Main.rand.Next(2000) == 0)
			{
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "meow", false, false);
			}
			if (player.HasBuff(10) && Main.rand.Next(400) == 0)
			{
				switch (Main.rand.Next(6))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "hello?", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "where is hoomun?", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "mamaa", false, false);
					break;
				case 3:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "where you at?", false, false);
					break;
				case 4:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "why you leave?", false, false);
					break;
				case 5:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, ":<", false, false);
					break;
				}
			}
			if (player.HasBuff(30) && Main.rand.Next(3000) == 0)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "yummy blood", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "you bleeding", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "blod", false, false);
					break;
				}
			}
			if (player.HasBuff(32) && Main.rand.Next(3000) == 0)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "hurry up", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "you're too slow", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "move move move", false, false);
					break;
				}
			}
			if (player.HasBuff(47) && Main.rand.Next(1000) == 0)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "is it cold in there?", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "i'll get you out!", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "brrr", false, false);
					break;
				}
			}
			if (player.HasBuff(148) && Main.rand.Next(5000) == 0)
			{
				int num = Main.rand.Next(2);
				if (num != 0)
				{
					if (num == 1)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "*nom*", false, false);
					}
				}
				else
				{
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "that bite wasn't from me", false, false);
				}
			}
			if (player.HasBuff(120) && Main.rand.Next(900) == 0)
			{
				switch (Main.rand.Next(4))
				{
				case 0:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "stinky", false, false);
					break;
				case 1:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "you stinky", false, false);
					break;
				case 2:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "uh oh stinky", false, false);
					break;
				case 3:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "ew", false, false);
					break;
				}
			}
			if (Main.rand.Next(40000) == 0)
			{
				switch (Main.rand.Next(15))
				{
				case 0:
					break;
				case 1:
					goto IL_C3B;
				case 2:
					goto IL_C5D;
				case 3:
					goto IL_C7F;
				case 4:
					goto IL_CA1;
				case 5:
					goto IL_CC3;
				case 6:
					goto IL_CE5;
				case 7:
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "I have too much swagger for the dagger", false, false);
					goto IL_EF0;
				case 8:
					if (Redemption.calamityLoaded)
					{
						switch (Main.rand.Next(3))
						{
						case 0:
							CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "this is a message to my master this is a fight you cannot win i think that past your great disasters their victory stirs below your skin", false, false);
							goto IL_EF0;
						case 1:
							CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "Cirrus isn't squishy, don't believe what Fabsol says...", false, false);
							goto IL_EF0;
						case 2:
							CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "playing calamity? cringe", false, false);
							goto IL_EF0;
						default:
							goto IL_EF0;
						}
					}
					break;
				case 9:
					if (Redemption.fargoLoaded)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "fargo GOD", false, false);
						goto IL_EF0;
					}
					goto IL_C3B;
				case 10:
					if (Redemption.GirusSilence)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "spooky", false, false);
						goto IL_EF0;
					}
					goto IL_C5D;
				case 11:
					if (!Redemption.musicPackLoaded)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "You should get Mod of Redemption: Music Pack", false, false);
						goto IL_EF0;
					}
					goto IL_C7F;
				case 12:
					if (Redemption.sacredToolsLoaded)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "Ara Ara", false, false);
						goto IL_EF0;
					}
					goto IL_CA1;
				case 13:
				{
					if (!Redemption.thoriumLoaded)
					{
						goto IL_CC3;
					}
					int num = Main.rand.Next(2);
					if (num == 0)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "Ragnarok is a well designed boss with no flaws what-so-ever", false, false);
						goto IL_EF0;
					}
					if (num != 1)
					{
						goto IL_EF0;
					}
					CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "That sprite isn't vanilla-styled, bad mod", false, false);
					goto IL_EF0;
				}
				case 14:
					if (Redemption.tremorLoaded)
					{
						CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "What the duck?", false, false);
						goto IL_EF0;
					}
					goto IL_CE5;
				default:
					goto IL_EF0;
				}
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "Always watching...", false, false);
				goto IL_EF0;
				IL_C3B:
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "You got any pasta?", false, false);
				goto IL_EF0;
				IL_C5D:
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "You think you're safe?", false, false);
				goto IL_EF0;
				IL_C7F:
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "yum yum", false, false);
				goto IL_EF0;
				IL_CA1:
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "Pitiful fool...", false, false);
				goto IL_EF0;
				IL_CC3:
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "cheese", false, false);
				goto IL_EF0;
				IL_CE5:
				CombatText.NewText(base.projectile.getRect(), Color.DeepPink, "*nom*", false, false);
			}
			IL_EF0:
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.halPet = false;
			}
			if (modPlayer.halPet)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
