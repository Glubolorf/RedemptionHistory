using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Armor.Costumes;
using Redemption.NPCs;
using Redemption.NPCs.Bosses.InfectedEye;
using Redemption.NPCs.Bosses.SeedOfInfection;
using Redemption.NPCs.Bosses.TheKeeper;
using Redemption.NPCs.ChickenInvasion;
using Redemption.NPCs.LabNPCs;
using Redemption.NPCs.LabNPCs.New;
using Redemption.NPCs.v08;
using Redemption.NPCs.Varients;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption
{
	public static class RedeHelper
	{
		public static bool IsBunny(this NPC npc)
		{
			return npc.type == 46 || npc.type == 443 || npc.type == 303 || npc.type == 337 || npc.type == 540;
		}

		public static bool IsVanillaWeakSkeleton(this NPC npc)
		{
			return npc.type == 77 || npc.type == -49 || npc.type == -51 || npc.type == -53 || npc.type == -47 || npc.type == 449 || npc.type == 450 || npc.type == 451 || npc.type == 452 || npc.type == 566 || npc.type == 567 || npc.type == 481 || npc.type == 201 || npc.type == -15 || npc.type == 202 || npc.type == 203 || npc.type == 21 || npc.type == 324 || npc.type == 110 || npc.type == 323 || npc.type == 293 || npc.type == 291 || npc.type == 322 || npc.type == -48 || npc.type == -50 || npc.type == -52 || npc.type == -46 || npc.type == 292 || npc.type == 31 || npc.type == 294 || npc.type == 296 || npc.type == 295 || (npc.type >= 39 && npc.type <= 41) || npc.type == 32 || npc.type == 34 || npc.type == 44 || npc.type == 167 || npc.type == 197 || npc.type == 273 || npc.type == 274 || npc.type == 275 || npc.type == 276 || npc.type == 287 || npc.type == 285 || npc.type == 286 || npc.type == 289 || npc.type == 277 || npc.type == 279 || npc.type == 278 || npc.type == 280 || npc.type == 283 || npc.type == 284 || npc.type == 281 || npc.type == 282 || npc.type == 172 || npc.type == 269 || npc.type == 270 || npc.type == 271 || npc.type == 272;
		}

		public static bool IsInfected(this NPC npc)
		{
			return npc.type == ModContent.NPCType<HazmatSkeleton>() || npc.type == ModContent.NPCType<HazmatZombie>() || npc.type == ModContent.NPCType<InfectedCaveBat>() || npc.type == ModContent.NPCType<InfectedDemonEye>() || npc.type == ModContent.NPCType<InfectedDiggerHead>() || npc.type == ModContent.NPCType<InfectedDiggerBody>() || npc.type == ModContent.NPCType<InfectedDiggerTail>() || npc.type == ModContent.NPCType<InfectedGiantBat>() || npc.type == ModContent.NPCType<InfectedGiantWormBody>() || npc.type == ModContent.NPCType<InfectedGiantWormHead>() || npc.type == ModContent.NPCType<InfectedGiantWormTail>() || npc.type == ModContent.NPCType<InfectedGiantWormTail>() || npc.type == ModContent.NPCType<InfectedZombie>() || npc.type == ModContent.NPCType<SludgyBoi>() || npc.type == ModContent.NPCType<XenoChomper>() || npc.type == ModContent.NPCType<XenomiteEye>() || npc.type == ModContent.NPCType<XenomiteGargantuan>() || npc.type == ModContent.NPCType<XenomiteGolem>() || npc.type == ModContent.NPCType<XenonRoller>() || npc.type == ModContent.NPCType<RadiumDiggerBody>() || npc.type == ModContent.NPCType<RadiumDiggerTail>() || npc.type == ModContent.NPCType<RadiumDiggerHead>() || npc.type == ModContent.NPCType<InfectedEye>() || npc.type == ModContent.NPCType<Blisterling>() || npc.type == ModContent.NPCType<Blisterling2>() || npc.type == ModContent.NPCType<InfectionHive>() || npc.type == ModContent.NPCType<SludgyBlob>() || npc.type == ModContent.NPCType<SludgyBoi2>() || npc.type == ModContent.NPCType<Stage2Scientist>() || npc.type == ModContent.NPCType<WalterInfected>() || npc.type == ModContent.NPCType<XenoChomper2>() || npc.type == ModContent.NPCType<XenomiteBeast>() || npc.type == ModContent.NPCType<SpikyRadioactiveSlime>() || npc.type == ModContent.NPCType<SneezyInfectedFlinx>() || npc.type == ModContent.NPCType<RadiumRampager>() || npc.type == ModContent.NPCType<RadiumDigger2Tail>() || npc.type == ModContent.NPCType<RadiumDigger2Head>() || npc.type == ModContent.NPCType<RadiumDigger2Body>() || npc.type == ModContent.NPCType<RadioactiveSlime>() || npc.type == ModContent.NPCType<NuclearSlime>() || npc.type == ModContent.NPCType<InfectedSwarmer>() || npc.type == ModContent.NPCType<InfectedSnowFlinx>() || npc.type == ModContent.NPCType<InfectedChicken>() || npc.type == ModContent.NPCType<GreenPigron>() || npc.type == ModContent.NPCType<DecayedGhoul>() || npc.type == ModContent.NPCType<BobTheBlob>() || npc.type == ModContent.NPCType<Injector>() || npc.type == ModContent.NPCType<BileBoomer>() || npc.type == ModContent.NPCType<IrradiatedSpear>() || npc.type == ModContent.NPCType<VirusJelly>() || npc.type == ModContent.NPCType<Superbug>() || npc.type == ModContent.NPCType<BloatedFaceMonster>() || npc.type == ModContent.NPCType<BloatedGoldfish>() || npc.type == ModContent.NPCType<IrradiatedWorldFeederTail>() || npc.type == ModContent.NPCType<IrradiatedWorldFeederHead>() || npc.type == ModContent.NPCType<IrradiatedWorldFeederBody>() || npc.type == ModContent.NPCType<NerveParasite>() || npc.type == ModContent.NPCType<RadioactiveSlimer>() || npc.type == ModContent.NPCType<Xenoling>() || npc.type == ModContent.NPCType<Superbug2>() || npc.type == ModContent.NPCType<Blisterface2>() || npc.type == ModContent.NPCType<IrradiatedBehemoth2>() || npc.type == ModContent.NPCType<PZ2BodyCover>() || npc.type == ModContent.NPCType<PZ2Fight>() || npc.type == ModContent.NPCType<Stage3Scientist2>() || npc.type == ModContent.NPCType<SeedGrowth>() || npc.type == ModContent.NPCType<SoI>();
		}

		public static bool IsGhostly(this NPC npc)
		{
			return npc.type == 84 || npc.type == 179 || npc.type == 83 || npc.type == 533 || npc.type == 288 || npc.type == 182 || npc.type == 316 || npc.type == 140 || npc.type == 82 || npc.type == 253 || npc.type == 330 || npc.type == ModContent.NPCType<TheKeeper>() || npc.type == ModContent.NPCType<AAAA>() || npc.type == ModContent.NPCType<DarkSoul>() || npc.type == ModContent.NPCType<DarkSoul2>() || npc.type == ModContent.NPCType<DarkSoul3>() || npc.type == ModContent.NPCType<SkullDigger>() || npc.type == ModContent.NPCType<WanderingSoul>() || npc.type == ModContent.NPCType<IrradiatedSpear>() || npc.type == ModContent.NPCType<SoullessAssassin>() || npc.type == ModContent.NPCType<SoullessDueller>() || npc.type == ModContent.NPCType<SoullessWanderer>() || npc.type == ModContent.NPCType<BileBoomer>() || npc.type == ModContent.NPCType<Shadebug>();
		}

		public static bool IsDragonlike(this NPC npc)
		{
			return npc.type == 551 || npc.type == 558 || npc.type == 559 || npc.type == 560 || npc.type == 170 || npc.type == 180 || npc.type == 171 || npc.type == 370 || npc.type == ModContent.NPCType<GreenPigron>() || (npc.type >= 87 && npc.type <= 92) || (npc.type >= 454 && npc.type <= 459);
		}

		public static bool IsDemon(this NPC npc)
		{
			return npc.type == 62 || npc.type == 66 || npc.type == 24 || npc.type == 156;
		}

		public static bool IsSoulless(this NPC npc)
		{
			return npc.type == ModContent.NPCType<SoullessAssassin>() || npc.type == ModContent.NPCType<SoullessDueller>() || npc.type == ModContent.NPCType<SoullessWanderer>() || npc.type == ModContent.NPCType<ShadesoulNPC>() || npc.type == ModContent.NPCType<SmallShadesoulNPC>() || npc.type == ModContent.NPCType<TheSoulless2>() || npc.type == ModContent.NPCType<TheSoulless>() || npc.type == ModContent.NPCType<Shadebug>();
		}

		public static bool IsAnySkeleton(this NPC npc)
		{
			return npc.type == 77 || npc.type == -49 || npc.type == -51 || npc.type == -53 || npc.type == -47 || npc.type == 449 || npc.type == 450 || npc.type == 451 || npc.type == 452 || npc.type == 566 || npc.type == 567 || npc.type == 481 || npc.type == 201 || npc.type == -15 || npc.type == 202 || npc.type == 203 || npc.type == 21 || npc.type == 324 || npc.type == 110 || npc.type == 323 || npc.type == 293 || npc.type == 291 || npc.type == 322 || npc.type == -48 || npc.type == -50 || npc.type == -52 || npc.type == -46 || npc.type == 292 || npc.type == 31 || npc.type == 294 || npc.type == 296 || npc.type == 295 || (npc.type >= 39 && npc.type <= 41) || npc.type == 32 || npc.type == 34 || npc.type == 68 || npc.type == 44 || npc.type == 167 || npc.type == 197 || npc.type == 273 || npc.type == 274 || npc.type == 275 || npc.type == 276 || npc.type == 287 || npc.type == 285 || npc.type == 286 || npc.type == 289 || npc.type == 277 || npc.type == 279 || npc.type == 278 || npc.type == 280 || npc.type == 283 || npc.type == 284 || npc.type == 281 || npc.type == 282 || npc.type == 172 || npc.type == 269 || npc.type == 270 || npc.type == 271 || npc.type == 272 || npc.type == 35 || npc.type == 36 || npc.type == ModContent.NPCType<BoneChicken>() || npc.type == ModContent.NPCType<ChickmanChickromancer>() || npc.type == ModContent.NPCType<BloodBoiledSkeleton>() || npc.type == ModContent.NPCType<SkeletonAssassin2>() || npc.type == ModContent.NPCType<SkeletonWanderer2>() || npc.type == ModContent.NPCType<SkeletonWarden>() || npc.type == ModContent.NPCType<Vepdor>() || npc.type == ModContent.NPCType<BoneLeviathanBody>() || npc.type == ModContent.NPCType<BoneLeviathanHead>() || npc.type == ModContent.NPCType<BoneLeviathanTail>() || npc.type == ModContent.NPCType<BoneSpider>() || npc.type == ModContent.NPCType<CorpseWalkerPriest>() || npc.type == ModContent.NPCType<DeathGardener>() || npc.type == ModContent.NPCType<HazmatSkeleton>() || npc.type == ModContent.NPCType<SkeleDruid>() || npc.type == ModContent.NPCType<Skelemies>() || npc.type == ModContent.NPCType<Skelemies2>() || npc.type == ModContent.NPCType<SkeletonAssassin>() || npc.type == ModContent.NPCType<SkeletonDueller>() || npc.type == ModContent.NPCType<SkeletonMinion>() || npc.type == ModContent.NPCType<SkeletonNoble>() || npc.type == ModContent.NPCType<SkeletonNobleArmoured>() || npc.type == ModContent.NPCType<SkeletonNobleArmoured2>() || npc.type == ModContent.NPCType<SkeletonNobleArmoured3>() || npc.type == ModContent.NPCType<SkeletonWanderer>();
		}

		public static bool IsFullTBot(this Player player)
		{
			return ((BasePlayer.HasChestplate(player, ModContent.ItemType<TBotVanityChestplate>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<TBotVanityLegs>(), true)) || (BasePlayer.HasChestplate(player, ModContent.ItemType<AndroidArmour>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<AndroidPants>(), true)) || (BasePlayer.HasChestplate(player, ModContent.ItemType<JanitorOutfit>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<JanitorPants>(), true))) && (BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityEyes>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityGoggles>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<AdamHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<OperatorHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<VoltHead>(), true));
		}

		public static bool IsTBotHead(this Player player)
		{
			return BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityEyes>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityGoggles>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<AdamHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<OperatorHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<VoltHead>(), true);
		}

		public static Vector2 PolarVector(float radius, float theta)
		{
			return new Vector2((float)Math.Cos((double)theta), (float)Math.Sin((double)theta)) * radius;
		}

		public static Item MakeItemFromID(int type)
		{
			if (type <= 0)
			{
				return null;
			}
			if (type >= 3930)
			{
				return ItemLoader.GetItem(type).item;
			}
			Item item = new Item();
			item.SetDefaults(type, true);
			return item;
		}

		public static bool ClosestNPC(ref NPC target, float maxDistance, Vector2 position, bool ignoreTiles = false, int overrideTarget = -1)
		{
			bool foundTarget = false;
			if (overrideTarget != -1 && (Main.npc[overrideTarget].Center - position).Length() < maxDistance)
			{
				target = Main.npc[overrideTarget];
				return true;
			}
			for (int i = 0; i < 200; i++)
			{
				NPC possibleTarget = Main.npc[i];
				if ((possibleTarget.Center - position).Length() < maxDistance && possibleTarget.active && possibleTarget.chaseable && !possibleTarget.dontTakeDamage && !possibleTarget.friendly && possibleTarget.lifeMax > 5 && !possibleTarget.immortal && (Collision.CanHit(position, 0, 0, possibleTarget.Center, 0, 0) || ignoreTiles))
				{
					target = Main.npc[i];
					foundTarget = true;
					maxDistance = (target.Center - position).Length();
				}
			}
			return foundTarget;
		}

		public static byte GetLiquidLevel(int x, int y, RedeHelper.LiquidType liquidType = RedeHelper.LiquidType.Any)
		{
			if (x < 0 || x >= Main.maxTilesX)
			{
				return 0;
			}
			if (y < 0 || y >= Main.maxTilesY)
			{
				return 0;
			}
			Tile tile = Main.tile[x, y];
			if (tile == null || tile.liquid == 0)
			{
				return 0;
			}
			if (liquidType == RedeHelper.LiquidType.Any)
			{
				return tile.liquid;
			}
			if (liquidType.HasFlag(RedeHelper.LiquidType.Water) && !tile.lava() && !tile.honey())
			{
				return tile.liquid;
			}
			if (liquidType.HasFlag(RedeHelper.LiquidType.Lava) && tile.lava())
			{
				return tile.liquid;
			}
			if (liquidType.HasFlag(RedeHelper.LiquidType.Honey) && tile.honey())
			{
				return tile.liquid;
			}
			return 0;
		}

		public static float GradtoRad(float Grad)
		{
			return Grad * 3.1415927f / 180f;
		}

		public static Vector2 RandomPositin(Vector2 pos1, Vector2 pos2)
		{
			Random rand = new Random();
			return new Vector2((float)(rand.Next((int)pos1.X, (int)pos2.X) + 1), (float)(rand.Next((int)pos1.Y, (int)pos2.Y) + 1));
		}

		public static int GetNearestAlivePlayer(NPC npc)
		{
			float NearestPlayerDist = 4.8151624E+09f;
			int NearestPlayer = -1;
			foreach (Player player in Main.player)
			{
				if (player.Distance(npc.Center) < NearestPlayerDist && player.active)
				{
					NearestPlayerDist = player.Distance(npc.Center);
					NearestPlayer = player.whoAmI;
				}
			}
			return NearestPlayer;
		}

		public static Vector2 VelocityFPTP(Vector2 pos1, Vector2 pos2, float speed)
		{
			Vector2 move = pos2 - pos1;
			return move * (speed / (float)Math.Sqrt((double)(move.X * move.X + move.Y * move.Y)));
		}

		public static float RadtoGrad(float Rad)
		{
			return Rad * 180f / 3.1415927f;
		}

		public static int GetNearestNPC(Vector2 Point, bool Friendly = false, bool NoBoss = false)
		{
			float NearestNPCDist = -1f;
			int NearestNPC = -1;
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && (!NoBoss || !npc.boss) && (Friendly || (!npc.friendly && npc.lifeMax > 5)) && (NearestNPCDist == -1f || npc.Distance(Point) < NearestNPCDist))
				{
					NearestNPCDist = npc.Distance(Point);
					NearestNPC = npc.whoAmI;
				}
			}
			return NearestNPC;
		}

		public static int GetNearestPlayer(Vector2 Point, bool Alive = false)
		{
			float NearestPlayerDist = -1f;
			int NearestPlayer = -1;
			foreach (Player player in Main.player)
			{
				if ((!Alive || (player.active && !player.dead)) && (NearestPlayerDist == -1f || player.Distance(Point) < NearestPlayerDist))
				{
					NearestPlayerDist = player.Distance(Point);
					NearestPlayer = player.whoAmI;
				}
			}
			return NearestPlayer;
		}

		public static Vector2 VelocityToPoint(Vector2 A, Vector2 B, float Speed)
		{
			Vector2 Move = B - A;
			return Move * (Speed / (float)Math.Sqrt((double)(Move.X * Move.X + Move.Y * Move.Y)));
		}

		public static Vector2 RandomPointInArea(Vector2 A, Vector2 B)
		{
			return new Vector2((float)(Main.rand.Next((int)A.X, (int)B.X) + 1), (float)(Main.rand.Next((int)A.Y, (int)B.Y) + 1));
		}

		public static Vector2 RandomPointInArea(Rectangle Area)
		{
			return new Vector2((float)Main.rand.Next(Area.X, Area.X + Area.Width), (float)Main.rand.Next(Area.Y, Area.Y + Area.Height));
		}

		public static float RotateBetween2Points(Vector2 A, Vector2 B)
		{
			return (float)Math.Atan2((double)(A.Y - B.Y), (double)(A.X - B.X));
		}

		public static Vector2 CenterPoint(Vector2 A, Vector2 B)
		{
			return new Vector2((A.X + B.X) / 2f, (A.Y + B.Y) / 2f);
		}

		public static Vector2 PolarPos(Vector2 Point, float Distance, float Angle, int XOffset = 0, int YOffset = 0)
		{
			return new Vector2
			{
				X = Distance * (float)Math.Sin((double)RedeHelper.RadtoGrad(Angle)) + Point.X + (float)XOffset,
				Y = Distance * (float)Math.Cos((double)RedeHelper.RadtoGrad(Angle)) + Point.Y + (float)YOffset
			};
		}

		public static bool Chance(float chance)
		{
			return Utils.NextFloat(Main.rand) <= chance;
		}

		public static Vector2 SmoothFromTo(Vector2 From, Vector2 To, float Smooth = 60f)
		{
			return From + (To - From) / Smooth;
		}

		public static float DistortFloat(float Float, float Percent)
		{
			float DistortNumber = Float * Percent;
			int Counter = 0;
			while (DistortNumber.ToString().Split(new char[]
			{
				','
			}).Length > 1)
			{
				DistortNumber *= 10f;
				Counter++;
			}
			return Float + (float)Main.rand.Next(0, (int)DistortNumber + 1) / (float)Math.Pow(10.0, (double)Counter) * (float)((Main.rand.Next(2) == 0) ? -1 : 1);
		}

		public static Vector2 FoundPosition(Vector2 tilePos)
		{
			Vector2 Screen = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2));
			Vector2 FullScreen = tilePos - Main.mapFullscreenPos;
			FullScreen *= Main.mapFullscreenScale / 16f;
			FullScreen = FullScreen * 16f + Screen;
			return new Vector2((float)((int)FullScreen.X), (float)((int)FullScreen.Y));
		}

		public static void MoveTowards(this NPC npc, Vector2 playerTarget, float speed, float turnResistance)
		{
			Vector2 Move = playerTarget - npc.Center;
			float Length = Move.Length();
			if (Length > speed)
			{
				Move *= speed / Length;
			}
			Move = (npc.velocity * turnResistance + Move) / (turnResistance + 1f);
			Length = Move.Length();
			if (Length > speed)
			{
				Move *= speed / Length;
			}
			npc.velocity = Move;
		}

		public static bool Placement(int x, int y)
		{
			for (int i = x - 16; i < x + 16; i++)
			{
				for (int j = y - 16; j < y + 16; j++)
				{
					if (Main.tile[i, j].liquid > 0)
					{
						return false;
					}
					int[] TileArray = new int[]
					{
						41,
						43,
						44,
						189,
						196,
						147,
						53,
						60,
						40,
						23,
						199,
						25,
						203
					};
					for (int ohgodilovememes = 0; ohgodilovememes < TileArray.Length - 1; ohgodilovememes++)
					{
						if (Main.tile[i, j].type == (ushort)TileArray[ohgodilovememes])
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		public static bool PlacementTower(int x, int y)
		{
			for (int i = x - 16; i < x + 16; i++)
			{
				for (int j = y - 16; j < y + 16; j++)
				{
					if (Main.tile[i, j].liquid > 0)
					{
						return false;
					}
					int[] TileArray = new int[]
					{
						41,
						43,
						44,
						189,
						196,
						147,
						53,
						60,
						40
					};
					for (int ohgodilovememes = 0; ohgodilovememes < TileArray.Length - 1; ohgodilovememes++)
					{
						if (Main.tile[i, j].type == (ushort)TileArray[ohgodilovememes])
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		public static bool NextBool(this UnifiedRandom rand, int chance, int total)
		{
			return rand.Next(total) < chance;
		}

		public static void CreateDust(Player player, int dust, int count)
		{
			for (int i = 0; i < count; i++)
			{
				Dust.NewDust(player.position, player.width, player.height / 2, dust, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
		{
			return new Vector2((float)(Math.Cos((double)rot) * ((double)vecToRot.X - (double)origin.X) - Math.Sin((double)rot) * ((double)vecToRot.Y - (double)origin.Y)) + origin.X, (float)(Math.Sin((double)rot) * ((double)vecToRot.X - (double)origin.X) + Math.Cos((double)rot) * ((double)vecToRot.Y - (double)origin.Y)) + origin.Y);
		}

		public static bool Contains(this Rectangle rect, Vector2 pos)
		{
			return rect.Contains((int)pos.X, (int)pos.Y);
		}

		public static bool AnyProjectiles(int type)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == type)
				{
					return true;
				}
			}
			return false;
		}

		public static int CountProjectiles(int type)
		{
			int p = 0;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == type)
				{
					p++;
				}
			}
			return p;
		}

		[Flags]
		public enum LiquidType
		{
			None = 0,
			Water = 1,
			Lava = 2,
			Honey = 4,
			Any = 7
		}
	}
}
