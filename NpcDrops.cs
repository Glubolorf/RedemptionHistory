using System;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.PreHM;
using Redemption.Items.Armor.Single;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.HM;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Placeable.Furniture.Misc;
using Redemption.Items.Placeable.Plants;
using Redemption.Items.Placeable.Tiles;
using Redemption.Items.Quest;
using Redemption.Items.Quest.Daerel;
using Redemption.Items.Quest.Zephos;
using Redemption.Items.Usable;
using Redemption.Items.Usable.Potions;
using Redemption.Items.Usable.Summons;
using Redemption.Items.Weapons.HM.Druid;
using Redemption.Items.Weapons.HM.Druid.Seedbags;
using Redemption.Items.Weapons.HM.Druid.Staves;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.HM.Summon;
using Redemption.Items.Weapons.PostML.Druid.Staves;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PreHM.Druid;
using Redemption.Items.Weapons.PreHM.Druid.Seedbags;
using Redemption.Items.Weapons.PreHM.Druid.Staves;
using Redemption.Items.Weapons.PreHM.Magic;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.Items.Weapons.PreHM.Summon;
using Redemption.NPCs.Critters;
using Redemption.NPCs.Friendly;
using Redemption.NPCs.HM;
using Redemption.NPCs.Lab;
using Redemption.NPCs.Minibosses;
using Redemption.NPCs.Minibosses.SkullDigger;
using Redemption.NPCs.PostML;
using Redemption.NPCs.PreHM;
using Redemption.NPCs.Wasteland;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class NpcDrops : GlobalNPC
	{
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == 20 && Main.bloodMoon)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<CorpseFlowerBag>(), false);
				nextSlot++;
			}
			if (type == 19 && NPC.downedBoss2 && !Main.dayTime)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<TacticalBow>(), false);
				nextSlot++;
			}
			if (type == 178 && NPC.downedGolemBoss)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<SteamMinigun>(), false);
				nextSlot++;
			}
			if (type == 17)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<RopeHook>(), false);
				nextSlot++;
				if (RedeQuests.DnecklaceQuest)
				{
					shop.item[nextSlot].SetDefaults(ModContent.ItemType<ShellNecklace>(), false);
					nextSlot++;
				}
			}
			if (type == 368 && RedeQuests.ZskullQuest && Utils.NextBool(Main.rand, 10))
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<RubySkull2>(), false);
				nextSlot++;
			}
			if (type == 108)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<NoidanSauva>(), false);
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Pommisauva>(), false);
				nextSlot++;
			}
		}

		public override void NPCLoot(NPC npc)
		{
			if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<RedePlayer>().ZoneEvilXeno || Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<RedePlayer>().ZoneEvilXeno2) && Utils.NextBool(Main.rand, 4))
			{
				Item.NewItem(npc.getRect(), ModContent.ItemType<Biohazard>(), 1, false, 0, false, false);
			}
			RedePlayer redePlayer = Main.LocalPlayer.GetModPlayer<RedePlayer>();
			if (npc.type == ModContent.NPCType<Chicken>() || npc.type == ModContent.NPCType<RedChicken>() || npc.type == ModContent.NPCType<LeghornChicken>() || npc.type == ModContent.NPCType<VlitchChicken>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ChickenEgg>(), Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AkisClaws>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<ChickenGold>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ChickenEggGold>(), 1, false, 0, false, false);
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AkisClaws>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<RainbowChicken>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SuspEgg>(), 1, false, 0, false, false);
				if (Utils.NextBool(Main.rand, 100))
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HallamHoodie>(), 1, false, 0, false, false);
						break;
					case 1:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HallamLeggings>(), 1, false, 0, false, false);
						break;
					case 2:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HallamRobes>(), 1, false, 0, false, false);
						break;
					}
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HallamDevWeapon>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<ForestGolem>() || npc.type == ModContent.NPCType<EbonForestGolem>() || npc.type == ModContent.NPCType<ShadeForestGolem>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LivingTwig>(), Main.rand.Next(8, 21), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 40))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LivingWoodYoyo>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100) && !Main.hardMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HeartOfTheThorns>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LivingWoodRapier>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Whisperwind>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<ForestGolemBlooming>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LivingTwig>(), Main.rand.Next(12, 27), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(3, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 313, 1, false, 0, false, false);
				if (Utils.NextBool(Main.rand, 100) && !Main.hardMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HeartOfTheThorns>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LivingWoodRapier>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Whisperwind>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<ForestGolemWounded>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LivingTwig>(), Main.rand.Next(6, 13), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 27, Main.rand.Next(1, 3), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 40, 2, false, 0, false, false);
				if (Utils.NextBool(Main.rand, 5))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3502, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100) && !Main.hardMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HeartOfTheThorns>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LivingWoodRapier>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Whisperwind>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<MoltenGolem>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 174, Main.rand.Next(2, 7), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 173, Main.rand.Next(2, 9), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 16))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ForgottenGreatsword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DeathsClaw>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<ShieldedZombie>())
			{
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 216, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1304, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Falcon>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<Skelemies>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<OldTophat>(), 1, false, 0, false, false);
				if (Main.hardMode && RedeWorld.nukeDropped)
				{
					int num = Main.rand.Next(2);
					if (num != 0)
					{
						if (num == 1)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PlasmaShield>(), 1, false, 0, false, false);
						}
					}
					else
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PlasmaSaber>(), 1, false, 0, false, false);
					}
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<OldXenomiteBlade>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 90 : 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TiedRapier>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 25))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 118, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 954, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 955, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 204))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1166, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1274, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<Skelemies2>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<YeOldeHat>(), 1, false, 0, false, false);
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<OldXenomiteBlade>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 90 : 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TiedRapier>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 25))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 118, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 954, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 955, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 204))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1166, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1274, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SkeletonNoble>())
			{
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<RustyNoblesSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SwordSlicer>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassChunk>(), Main.rand.Next(8, 15), false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SkeletonNobleArmoured>())
			{
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassCleaver>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassArmour>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassHelm>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassLeggings>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SwordSlicer>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassChunk>(), Main.rand.Next(12, 20), false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SkeletonNobleArmoured3>())
			{
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<IthonicCap>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<IthonicGreaves>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<IthonicTabard>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SwordSlicer>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<KaniteOre>(), Main.rand.Next(7, 16), false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SkeletonNobleArmoured2>())
			{
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PureIronSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PureIronArmour>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PureIronHelm>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PureIronLeggings>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SwordSlicer>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GathicCryoCrystal>(), Main.rand.Next(6, 15), false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SkeletonWarden>())
			{
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<WardensBow>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SilverwoodBow>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DarkSteelBow>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 40, Main.rand.Next(6, 13), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassChunk>(), Main.rand.Next(8, 15), false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SkeletonDueller>())
			{
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<OldRapier>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SilverRapier>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassChunk>(), Main.rand.Next(8, 15), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientGoldCoin>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientGoldCoin>(), 1, false, 0, false, false);
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientGoldCoin>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TiedRapier>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 25))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 118, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 954, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 955, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 204))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1166, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1274, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<Vepdor>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SilverRapier>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VepdorHat>(), 1, false, 0, false, false);
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassChunk>(), Main.rand.Next(28, 41), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientGoldCoin>(), Main.rand.Next(10, 21), false, 0, false, false);
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BlindJustice>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<TheSoulless>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BlackenedHeart>(), 1, false, 0, false, false);
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 80 : 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Dusksong>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<TreeBug>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 9, Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TreeBugShell>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<CoastScarab>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 169, Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CoastScarabShell>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<UndeadExecutioner1>())
			{
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BronzeGreatsword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ForgottenSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GoldenEdge>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Godslayer>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<UndeadExecutioner2>())
			{
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BronzeGreatsword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ForgottenSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GoldenEdge>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Godslayer>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<UndeadExecutioner3>())
			{
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BronzeGreatsword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ForgottenSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GoldenEdge>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Godslayer>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<ChickenCultist>())
			{
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ChickenEgg>(), Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ChickenContract>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<InfectedZombie>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(3, 6), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<InfectedCaveBat>())
			{
				if (Utils.NextBool(Main.rand, 250))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1325, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 18, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<InfectedGiantBat>())
			{
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 893, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 18, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(4, 9), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<InfectedGiantWormHead>())
			{
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 215, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(3, 7), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<InfectedDiggerHead>())
			{
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 215, 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(8, 19), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<InfectedDemonEye>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(2, 5), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<CorruptedTBot>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GirusChip>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedStarliteBar>(), Main.rand.Next(2, 4), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedHeroSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 300))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedWormMedallion>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<UndeadViolinist>())
			{
				if (Utils.NextBool(Main.rand, 18))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Violin>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 75))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<NestoriWig>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MysteriousArtifact>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<Blobble>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 23, Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1309, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<LivingBloom>())
			{
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AnglonicMysticBlossom>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100) && !Main.hardMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HeartOfTheThorns>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 5))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 313, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 5))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 315, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 5))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 314, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 5))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 317, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 5) && RedeWorld.downedThorn)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Nightshade>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Whisperwind>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<CorpseWalkerPriest>())
			{
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorpseWalkerStaff>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HonorsReach>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Spellsong>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<UnstablePortal>())
			{
				if (Utils.NextBool(Main.rand, 75))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DewittWig>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 250))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BrothersPainting>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 75))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<NestoriWig>(), 1, false, 0, false, false);
				}
				if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Violin>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Flatcap>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 75))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<RayensTophat>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 15))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MysteriousFlowerPetal>(), 8, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<Android>() || npc.type == ModContent.NPCType<Apidroid>())
			{
				if (Utils.NextBool(Main.rand, 15))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AIChip>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CarbonMyofibre>(), Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Mk1Capacitator>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Mk1Plating>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AndroidHead>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<OmegaAndroid>())
			{
				if (Utils.NextBool(Main.rand, 10))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GirusChip>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CarbonMyofibre>(), Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 5))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchBattery>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(1, 5), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchScale>(), Main.rand.Next(1, 4), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedHeroSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 300))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedWormMedallion>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<PrototypeSilver>())
			{
				if (Utils.NextBool(Main.rand, 10))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AIChip>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CarbonMyofibre>(), Main.rand.Next(1, 5), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Mk2Capacitator>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Mk2Plating>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PrototypeSilverHead>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<OmegaPrototype>())
			{
				if (Utils.NextBool(Main.rand, 5))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GirusChip>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CarbonMyofibre>(), Main.rand.Next(1, 5), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchBattery>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(2, 7), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchScale>(), Main.rand.Next(2, 6), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedHeroSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 300))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedWormMedallion>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SpacePaladin>())
			{
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AIChip>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ArtificalMuscle>(), Main.rand.Next(1, 3), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Mk3Capacitator>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Mk3Plating>(), 1, false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<OmegaSpacePaladin>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GirusChip>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ArtificalMuscle>(), Main.rand.Next(1, 3), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchBattery>(), Main.rand.Next(1, 3), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(6, 17), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchScale>(), Main.rand.Next(4, 13), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 10))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedHeroSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 30))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedWormMedallion>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<CorruptedDrone1>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GirusChip>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchBattery>(), Main.rand.Next(2, 4), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(8, 15), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchScale>(), Main.rand.Next(6, 15), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 10))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedHeroSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 30))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedWormMedallion>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<CorruptedCopter1>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GirusChip>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchBattery>(), Main.rand.Next(4, 6), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(12, 27), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VlitchScale>(), Main.rand.Next(16, 37), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 10))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedHeroSword>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 30))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CorruptedWormMedallion>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<XenoChomper>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FoldedShotgun>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<XenomiteGolem>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(4, 8), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InfectedGolemEgg>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SuspiciousXenomiteShard>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<XenomiteGargantuan>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Xenomite>(), Main.rand.Next(2, 6), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 10))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InfectedGolemEgg>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<RadioactiveLauncher>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SuspiciousXenomiteShard>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<XenonRoller>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(2, 6), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteBoulderFlail>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SuspiciousXenomiteShard>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SludgyBoi>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(2, 4), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SludgeSpoon>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<SnowyBoi>())
			{
				if (Utils.NextBool(Main.rand, 75))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DewittWig>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 15))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrostyStaff>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 15))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrozenGrasp>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MysteriousArtifact>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<CrusherHead>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3, Main.rand.Next(80, 161), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<HazmatZombie>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(2, 4), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GasMask>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HazmatSuit>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FoldedShotgun>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<HazmatSkeleton>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(2, 4), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GasMask>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HazmatSuit>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FoldedShotgun>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 25))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 118, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 954, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 955, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 204))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1166, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1274, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<RadiumDiggerHead>() || npc.type == ModContent.NPCType<RadiumDigger2Head>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Xenomite>(), Main.rand.Next(1, 5), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<RadiumHook>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SuspiciousXenomiteShard>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<LivingBlackGloop>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BlackGloop>(), Main.rand.Next(2, 5), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<ForestNymph>() || npc.type == ModContent.NPCType<EbonNymph>() || npc.type == ModContent.NPCType<ShadeNymph>() || npc.type == ModContent.NPCType<HallowNymph>())
			{
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3102, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 10) && !Main.hardMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HeartOfTheThorns>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3093, Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 313, Main.rand.Next(1, 4), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 315, Main.rand.Next(1, 4), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 314, Main.rand.Next(1, 4), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 317, Main.rand.Next(1, 4), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 4) && RedeWorld.downedThorn)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Nightshade>(), Main.rand.Next(1, 4), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 62, Main.rand.Next(2, 7), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 90 : 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ForestNymphsSickle>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 600 : 750))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<UkkosStave>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<BloodWormHead>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 69, Main.rand.Next(1, 5), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<BloodDiggerHead>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 69, 3, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1332, Main.rand.Next(2, 6), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<ForestSpider>() && Main.hardMode)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2607, Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Whisperwind>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<RogueTBot>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ScrapMetal>(), Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<BoneLeviathanHead>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BoneLeviathanFlail>(), 1, false, 0, false, false);
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 80 : 100))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Godslayer>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<DarkSoul>() && Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Dusksong>(), 1, false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<WanderingSoul>() && Utils.NextBool(Main.rand, 25))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ForgottenSword>(), 1, false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<SkeleDruid>())
			{
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DruidHat>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GloomShroomBag>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GloomMushroom>(), Main.rand.Next(5, 7), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<MoonflareBat>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MoonflareFragment>(), Main.rand.Next(1, 4), false, 0, false, false);
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Spellsong>(), 1, false, 0, false, false);
				}
				if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 900 : 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BlindJustice>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == 477 && Utils.NextBool(Main.rand, 4))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BrokenHeroStave>(), 1, false, 0, false, false);
			}
			if (npc.type == 113 && !Main.expertMode)
			{
				if (Utils.NextBool(Main.rand, 8))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DruidEmblem>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 6))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<WallsClaw>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == 395 && Utils.NextBool(Main.rand, 9))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MartianShieldGenerator>(), 1, false, 0, false, false);
			}
			if (npc.type == 262 && !Main.expertMode)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SoulOfBloom>(), Main.rand.Next(90, 121), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 7))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PlanterasStave>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<LickyLickyCactus>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 276, Main.rand.Next(6, 10), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1113, 1, false, 0, false, false);
				}
			}
			if (npc.type == 4 && !Main.expertMode && Utils.NextBool(Main.rand, 3))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EyeStalkBag>(), 1, false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<MartianScamArtist>())
			{
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<UltraVioletPlating>(), Main.rand.Next(8, 21), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2806, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2807, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2808, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MartianTreeBag>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 6))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MartianScamHat>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == 245 && Utils.NextBool(Main.rand, 8) && !Main.expertMode)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<LihzWorldStave>(), 1, false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<DeathGardener>())
			{
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SkeletonCan>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GloomMushroom>(), Main.rand.Next(5, 8), false, 0, false, false);
			}
			if (npc.type == 439)
			{
				if (!Main.expertMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CreationFragment>(), Main.rand.Next(12, 61), false, 0, false, false);
				}
				if (Main.expertMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CreationFragment>(), Main.rand.Next(18, 91), false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<RaggedZombie>())
			{
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DeathsGraspBag>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 216, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1304, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 400 : 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Falcon>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<FatPirate>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ExBarrel>(), Main.rand.Next(8, 27), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 4))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GoldenOrangeBag>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 12))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FriedEgg>(), Main.rand.Next(2, 5), false, 0, false, false);
				}
				switch (Main.rand.Next(5))
				{
				case 0:
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BlueSpice>(), Main.rand.Next(1, 8), false, 0, false, false);
					break;
				case 1:
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GreenSpice>(), Main.rand.Next(1, 7), false, 0, false, false);
					break;
				case 2:
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<OrangeSpice>(), Main.rand.Next(1, 6), false, 0, false, false);
					break;
				case 3:
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<RedSpice>(), Main.rand.Next(1, 5), false, 0, false, false);
					break;
				case 4:
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<WhiteSpice>(), Main.rand.Next(1, 4), false, 0, false, false);
					break;
				}
				if (Utils.NextBool(Main.rand, 4))
				{
					int num = Main.rand.Next(2);
					if (num != 0)
					{
						if (num == 1)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ChiliSpray>(), 1, false, 0, false, false);
						}
					}
					else
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BarrelBombarder>(), 1, false, 0, false, false);
					}
				}
				if (Utils.NextBool(Main.rand, 12))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2425, Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 12))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2426, Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 12))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 357, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 12))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3195, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 8000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 905, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 4000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 855, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 854, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2584, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 672, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1277, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1279, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1280, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1278, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 1000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3033, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3263, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3264, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 500))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3265, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<MarbleChessHorse>())
			{
				if (Utils.NextBool(Main.rand, 10))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ElegantStave>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3081, Main.rand.Next(12, 18), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<GraniteCluster>())
			{
				if (Utils.NextBool(Main.rand, 10))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GaucheWorldStave>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3086, Main.rand.Next(12, 18), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<SkullDigger>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AbandonedTeddy>(), 1, false, 0, false, false);
				if (Utils.NextBool(Main.rand, 50))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<VictorBattletome>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ForgottenSword>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AncientBrassChunk>(), Main.rand.Next(20, 41), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SkullDiggerFlail>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SkullDiggerMask>(), 1, false, 0, false, false);
			}
			if ((npc.type == 31 || npc.type == 294 || npc.type == 296 || npc.type == 295) && Utils.NextBool(Main.rand, 75))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DungeonHammer>(), 1, false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<SunkenCaptain>())
			{
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 275, Main.rand.Next(4, 12), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1112, Main.rand.Next(3, 6), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2625, Main.rand.Next(3, 8), false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2626, Main.rand.Next(5, 8), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1278, 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GhostCutlass>(), 1, false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<WalterInfected>() && Utils.NextBool(Main.rand, 1000))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Crowbar>(), 1, false, 0, false, false);
			}
			if ((npc.type == 6 || npc.type == -11 || npc.type == -12 || npc.type == 57 || npc.type == 7 || npc.type == 94 || npc.type == 81 || npc.type == -1 || npc.type == -2 || npc.type == 239 || npc.type == 465 || npc.type == 181 || npc.type == 173 || npc.type == -23 || npc.type == -22 || npc.type == 174 || npc.type == 183 || npc.type == -25 || npc.type == -24 || npc.type == 241 || npc.type == 242) && Utils.NextBool(Main.rand, 500))
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EldritchRoot>(), 1, false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<BloodBoiledSkeleton>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DarkShard>(), Main.rand.Next(2, 6), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 200))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3016, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<BobTheBlob>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(27, 49), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Starlite>(), Main.rand.Next(25, 39), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HazmatSuit>(), 1, false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 23, Main.rand.Next(20, 101), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SludgeSpoon>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<GreenPigron>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(4, 9), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 3))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3532, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<DecayedGhoul>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(1, 3), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 15))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3794, Main.rand.Next(1, 4), false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<RadiumRampager>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(4, 8), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 8))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 319, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<XenomiteBeast>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(9, 21), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Starlite>(), Main.rand.Next(4, 10), false, 0, false, false);
			}
			if (npc.type == ModContent.NPCType<RadioactiveSlime>() || npc.type == ModContent.NPCType<SpikyRadioactiveSlime>() || npc.type == ModContent.NPCType<NuclearSlime>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(6, 8), false, 0, false, false);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 23, Main.rand.Next(3, 8), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 10000))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1309, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<InfectedSnowFlinx>() || npc.type == ModContent.NPCType<SneezyInfectedFlinx>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(1, 5), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 150))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 951, 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 20))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 393, 1, false, 0, false, false);
				}
			}
			if (npc.type == ModContent.NPCType<BabbyDragonHead>())
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 575, Main.rand.Next(15, 31), false, 0, false, false);
				if (Utils.NextBool(Main.rand, 2))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3457, Main.rand.Next(5, 11), false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GildedStar>(), Main.rand.Next(4, 9), false, 0, false, false);
			}
			if (npc.type == 77 || npc.type == 197 || npc.type == 110)
			{
				if (Utils.NextBool(Main.rand, 250))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<NoidanSauva>(), 1, false, 0, false, false);
				}
				if (Utils.NextBool(Main.rand, 250))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Pommisauva>(), 1, false, 0, false, false);
				}
			}
			if (npc.type == 398 && !Main.expertMode)
			{
				if (Utils.NextBool(Main.rand, 7))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MoonlordStave>(), 1, false, 0, false, false);
				}
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Keycard>(), 1, false, 0, false, false);
			}
		}
	}
}
