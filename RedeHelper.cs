using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Armor.Vanity;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption
{
	public static class RedeHelper
	{
		public static object GetFieldValue(this Type type, string fieldName, object obj = null, BindingFlags? flags = null)
		{
			if (flags == null)
			{
				flags = new BindingFlags?(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			return type.GetField(fieldName, flags.Value).GetValue(obj);
		}

		public static T GetFieldValue<T>(this Type type, string fieldName, object obj = null, BindingFlags? flags = null)
		{
			if (flags == null)
			{
				flags = new BindingFlags?(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			return (T)((object)type.GetField(fieldName, flags.Value).GetValue(obj));
		}

		public static bool IsFullTBot(this Player player)
		{
			return (((BasePlayer.HasChestplate(player, ModContent.ItemType<TBotVanityChestplate>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<TBotVanityLegs>(), true)) || (BasePlayer.HasChestplate(player, ModContent.ItemType<AndroidArmour>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<AndroidPants>(), true)) || (BasePlayer.HasChestplate(player, ModContent.ItemType<JanitorOutfit>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<JanitorPants>(), true))) && (BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityEyes>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityGoggles>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<AdamHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<OperatorHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<VoltHead>(), true))) || Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower;
		}

		public static bool IsTBotHead(this Player player)
		{
			return BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotEyes_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityEyes>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Femi>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotGoggles_Masc>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<TBotVanityGoggles>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<AdamHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<OperatorHead>(), true) || BasePlayer.HasHelmet(player, ModContent.ItemType<VoltHead>(), true);
		}

		public static bool IsRadiationProtected(this Player player)
		{
			return BasePlayer.HasAccessory(player, ModContent.ItemType<GasMask>(), true, false) || BasePlayer.HasAccessory(player, ModContent.ItemType<HazmatSuit>(), true, false) || BasePlayer.HasAccessory(player, ModContent.ItemType<HEVSuit>(), true, false);
		}

		public static bool IsNebVanity(this Player player)
		{
			return BasePlayer.HasHelmet(player, ModContent.ItemType<NebuleusMask>(), true) && BasePlayer.HasChestplate(player, ModContent.ItemType<NebuleusVanity>(), true);
		}

		public static bool IsHolyKnight(this Player player)
		{
			return BasePlayer.HasHelmet(player, ModContent.ItemType<ArmorHKHead>(), true) && BasePlayer.HasChestplate(player, ModContent.ItemType<ArmorHK>(), true) && BasePlayer.HasLeggings(player, ModContent.ItemType<ArmorHKLeggings>(), true);
		}

		public static bool IsHal(this Player player)
		{
			return BasePlayer.HasHelmet(player, ModContent.ItemType<HallamHoodie>(), true) && BasePlayer.HasChestplate(player, ModContent.ItemType<HallamLeggings>(), true);
		}

		public static Vector2 TurnRight(this Vector2 vec)
		{
			return new Vector2(-vec.Y, vec.X);
		}

		public static Vector2 TurnLeft(this Vector2 vec)
		{
			return new Vector2(vec.Y, -vec.X);
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

		public static bool ClosestNPC(ref NPC target, float maxDistance, Vector2 position, bool ignoreTiles = false, int overrideTarget = -1, RedeHelper.SpecialCondition specialCondition = null)
		{
			if (specialCondition == null)
			{
				specialCondition = ((NPC possibleTarget) => true);
			}
			bool foundTarget = false;
			if (overrideTarget != -1 && (Main.npc[overrideTarget].Center - position).Length() < maxDistance)
			{
				target = Main.npc[overrideTarget];
				return true;
			}
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC possibleTarget2 = Main.npc[i];
				if ((possibleTarget2.Center - position).Length() < maxDistance && possibleTarget2.active && possibleTarget2.chaseable && !possibleTarget2.dontTakeDamage && !possibleTarget2.friendly && possibleTarget2.lifeMax > 5 && !possibleTarget2.immortal && (Collision.CanHit(position, 0, 0, possibleTarget2.Center, 0, 0) || ignoreTiles) && specialCondition(possibleTarget2))
				{
					target = Main.npc[i];
					foundTarget = true;
					maxDistance = (target.Center - position).Length();
				}
			}
			return foundTarget;
		}

		public static int MinionHordeIdentity(Projectile projectile)
		{
			int identity = 0;
			for (int p = 0; p < 1000; p++)
			{
				if (Main.projectile[p].active && Main.projectile[p].type == projectile.type && Main.projectile[p].owner == projectile.owner)
				{
					if (projectile.whoAmI == p)
					{
						break;
					}
					identity++;
				}
			}
			return identity;
		}

		public static bool UseAmmo(this Projectile projectile, int ammoID, ref int shoot, ref float speed, ref int Damage, ref float KnockBack, bool dontConsume = false)
		{
			Player player = Main.player[projectile.owner];
			Item item = new Item();
			bool hasFoundAmmo = false;
			for (int i = 54; i < 58; i++)
			{
				if (player.inventory[i].ammo == ammoID && player.inventory[i].stack > 0)
				{
					item = player.inventory[i];
					hasFoundAmmo = true;
					break;
				}
			}
			if (!hasFoundAmmo)
			{
				for (int j = 0; j < 54; j++)
				{
					if (player.inventory[j].ammo == ammoID && player.inventory[j].stack > 0)
					{
						item = player.inventory[j];
						hasFoundAmmo = true;
						break;
					}
				}
			}
			if (hasFoundAmmo)
			{
				shoot = item.shoot;
				if (player.magicQuiver && (ammoID == AmmoID.Arrow || ammoID == AmmoID.Stake))
				{
					KnockBack = (float)((int)((double)KnockBack * 1.1));
					speed *= 1.1f;
				}
				speed += item.shootSpeed;
				if (item.ranged)
				{
					if (item.damage > 0)
					{
						Damage += (int)((float)item.damage * player.rangedDamage);
					}
				}
				else
				{
					Damage += item.damage;
				}
				if (ammoID == AmmoID.Arrow && player.archery)
				{
					if (speed < 20f)
					{
						speed *= 1.2f;
						if (speed > 20f)
						{
							speed = 20f;
						}
					}
					Damage = (int)((double)((float)Damage) * 1.2);
				}
				KnockBack += item.knockBack;
				bool flag2 = dontConsume;
				if (player.magicQuiver && ammoID == AmmoID.Arrow && Main.rand.Next(5) == 0)
				{
					flag2 = true;
				}
				if (player.ammoBox && Main.rand.Next(5) == 0)
				{
					flag2 = true;
				}
				if (player.ammoPotion && Main.rand.Next(5) == 0)
				{
					flag2 = true;
				}
				if (player.ammoCost80 && Main.rand.Next(5) == 0)
				{
					flag2 = true;
				}
				if (player.ammoCost75 && Main.rand.Next(4) == 0)
				{
					flag2 = true;
				}
				if (!flag2 && item.consumable)
				{
					item.stack--;
					if (item.stack <= 0)
					{
						item.active = false;
						item.TurnToAir();
					}
				}
				return true;
			}
			return false;
		}

		public static void SlowRotation(this float currentRotation, float targetAngle, float speed)
		{
			float actDirection = Utils.ToRotation(new Vector2((float)Math.Cos((double)currentRotation), (float)Math.Sin((double)currentRotation)));
			targetAngle = Utils.ToRotation(new Vector2((float)Math.Cos((double)targetAngle), (float)Math.Sin((double)targetAngle)));
			int f;
			if ((double)Math.Abs(actDirection - targetAngle) > 3.141592653589793)
			{
				f = -1;
			}
			else
			{
				f = 1;
			}
			if (actDirection <= targetAngle + speed * 2f && actDirection >= targetAngle - speed * 2f)
			{
				actDirection = targetAngle;
			}
			else if (actDirection <= targetAngle)
			{
				actDirection += speed * (float)f;
			}
			else if (actDirection >= targetAngle)
			{
				actDirection -= speed * (float)f;
			}
			actDirection = Utils.ToRotation(new Vector2((float)Math.Cos((double)actDirection), (float)Math.Sin((double)actDirection)));
			currentRotation = actDirection;
		}

		public static float AngularDifference(float angle1, float angle2)
		{
			angle1 = Utils.ToRotation(RedeHelper.PolarVector(1f, angle1));
			angle2 = Utils.ToRotation(RedeHelper.PolarVector(1f, angle2));
			if ((double)Math.Abs(angle1 - angle2) > 3.141592653589793)
			{
				return 6.2831855f - Math.Abs(angle1 - angle2);
			}
			return Math.Abs(angle1 - angle2);
		}

		private static float X(float t, float x0, float x1, float x2, float x3)
		{
			return (float)((double)x0 * Math.Pow((double)(1f - t), 3.0) + (double)(x1 * 3f * t) * Math.Pow((double)(1f - t), 2.0) + (double)(x2 * 3f) * Math.Pow((double)t, 2.0) * (double)(1f - t) + (double)x3 * Math.Pow((double)t, 3.0));
		}

		private static float Y(float t, float y0, float y1, float y2, float y3)
		{
			return (float)((double)y0 * Math.Pow((double)(1f - t), 3.0) + (double)(y1 * 3f * t) * Math.Pow((double)(1f - t), 2.0) + (double)(y2 * 3f) * Math.Pow((double)t, 2.0) * (double)(1f - t) + (double)y3 * Math.Pow((double)t, 3.0));
		}

		public static void DrawBezier(SpriteBatch spriteBatch, Texture2D texture, string glowMaskTexture, Color drawColor, Vector2 startingPos, Vector2 endPoints, Vector2 c1, Vector2 c2, float chainsPerUse, float rotDis)
		{
			for (float i = 0f; i <= 1f; i += chainsPerUse)
			{
				if (i != 0f)
				{
					float projTrueRotation = Utils.ToRotation(new Vector2(RedeHelper.X(i, startingPos.X, c1.X, c2.X, endPoints.X) - RedeHelper.X(i - chainsPerUse, startingPos.X, c1.X, c2.X, endPoints.X), RedeHelper.Y(i, startingPos.Y, c1.Y, c2.Y, endPoints.Y) - RedeHelper.Y(i - chainsPerUse, startingPos.Y, c1.Y, c2.Y, endPoints.Y))) - 1.5707964f + rotDis;
					spriteBatch.Draw(texture, new Vector2(RedeHelper.X(i, startingPos.X, c1.X, c2.X, endPoints.X) - Main.screenPosition.X, RedeHelper.Y(i, startingPos.Y, c1.Y, c2.Y, endPoints.Y) - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), drawColor, projTrueRotation, new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
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

		public static int GetNearestAlivePlayer(this NPC npc)
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

		public static int GetNearestAlivePlayer(this Projectile projectile)
		{
			float NearestPlayerDist = 4.8151624E+09f;
			int NearestPlayer = -1;
			foreach (Player player in Main.player)
			{
				if (player.Distance(projectile.Center) < NearestPlayerDist && player.active)
				{
					NearestPlayerDist = player.Distance(projectile.Center);
					NearestPlayer = player.whoAmI;
				}
			}
			return NearestPlayer;
		}

		public static Vector2 GetNearestAlivePlayerVector(this NPC npc)
		{
			float NearestPlayerDist = 4.8151624E+09f;
			Vector2 NearestPlayer = Vector2.Zero;
			foreach (Player player in Main.player)
			{
				if (player.Distance(npc.Center) < NearestPlayerDist && player.active)
				{
					NearestPlayerDist = player.Distance(npc.Center);
					NearestPlayer = player.Center;
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

		public static Vector2 Spread(float xy)
		{
			return new Vector2(Utils.NextFloat(Main.rand, -xy, xy - 1f), Utils.NextFloat(Main.rand, -xy, xy - 1f));
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

		public static Vector2 GetOrigin(Texture2D tex, int frames = 1)
		{
			return new Vector2((float)(tex.Width / 2), (float)(tex.Height / frames / 2));
		}

		public static Vector2 GetOrigin(Rectangle rect, int frames = 1)
		{
			return new Vector2((float)(rect.Width / 2), (float)(rect.Height / frames / 2));
		}

		public static void ProjectileExploson(Vector2 pos, float StartAngle, int Streams, int type, int damage, float ProjSpeed, float ai0 = 0f, float ai1 = 0f)
		{
			if (Main.netMode != 1)
			{
				for (int i = 0; i < Streams; i++)
				{
					Vector2 direction = Utils.RotatedBy(Vector2.Normalize(new Vector2(1f, 1f)), (double)MathHelper.ToRadians((float)(360 / Streams * i) + StartAngle), default(Vector2));
					direction.X *= ProjSpeed;
					direction.Y *= ProjSpeed;
					int proj = Projectile.NewProjectile(pos.X, pos.Y, direction.X, direction.Y, type, damage, 0f, Main.myPlayer, ai0, ai1);
					Main.projectile[proj].Center = pos;
				}
			}
		}

		public static void Shoot(this NPC npc, Vector2 position, int projType, int damage, Vector2 velocity, bool customSound, LegacySoundStyle sound, string soundString = "", float ai0 = 0f, float ai1 = 0f)
		{
			Mod mod = Redemption.Inst;
			Player player = Main.player[npc.target];
			if (customSound)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(mod.GetLegacySoundSlot(50, soundString), (int)npc.position.X, (int)npc.position.Y);
				}
			}
			else
			{
				Main.PlaySound(sound, (int)npc.position.X, (int)npc.position.Y);
			}
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(position, velocity, projType, damage / 3, 0f, Main.myPlayer, ai0, ai1);
			}
		}

		public static void SpawnNPC(this NPC npc, int posX, int posY, int npcType, float ai0 = 0f, float ai1 = 0f, float ai2 = 0f, float ai3 = 0f)
		{
			Redemption inst = Redemption.Inst;
			Player player = Main.player[npc.target];
			if (Main.netMode != 1)
			{
				int i = NPC.NewNPC(posX, posY, npcType, 0, ai0, ai1, ai2, ai3, 255);
				if (i != 200 && Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public static void Dash(this NPC npc, int speed, bool directional, LegacySoundStyle sound, Vector2 target)
		{
			Player player = Main.player[npc.target];
			Main.PlaySound(sound, (int)npc.position.X, (int)npc.position.Y);
			if (target == Vector2.Zero)
			{
				target = player.Center;
			}
			if (directional)
			{
				npc.velocity = npc.DirectionTo(target) * (float)speed;
				return;
			}
			npc.velocity.X = (float)((target.X > npc.Center.X) ? speed : (-(float)speed));
		}

		public static void LookAtPlayer(this NPC npc)
		{
			if (Main.player[npc.target].Center.X > npc.Center.X)
			{
				npc.spriteDirection = 1;
				return;
			}
			npc.spriteDirection = -1;
		}

		public static void LookAtPlayer(this Projectile projectile)
		{
			if (Main.player[projectile.owner].Center.X > projectile.Center.X)
			{
				projectile.spriteDirection = 1;
				return;
			}
			projectile.spriteDirection = -1;
		}

		public static void LookByVelocity(this NPC npc)
		{
			if (npc.velocity.X > 0f)
			{
				npc.spriteDirection = 1;
				return;
			}
			npc.spriteDirection = -1;
		}

		public static void MoveToVector2(this NPC npc, Vector2 p, float moveSpeed)
		{
			float velMultiplier = 1f;
			Vector2 dist = p - npc.Center;
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
			npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			npc.velocity *= moveSpeed;
			npc.velocity *= velMultiplier;
		}

		public static void MoveToVector2(this Projectile projectile, Vector2 p, float moveSpeed)
		{
			float velMultiplier = 1f;
			Vector2 dist = p - projectile.Center;
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
			projectile.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			projectile.velocity *= moveSpeed;
			projectile.velocity *= velMultiplier;
		}

		public static void Move(this NPC npc, Vector2 vector, float speed, float turnResistance = 10f, bool toPlayer = false)
		{
			Player player = Main.player[npc.target];
			Vector2 move = (toPlayer ? (player.Center + vector) : vector) - npc.Center;
			float magnitude = RedeHelper.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = RedeHelper.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			npc.velocity = move;
		}

		public static void Move(this Projectile projectile, Vector2 vector, float speed, float turnResistance = 10f, bool toPlayer = false)
		{
			Player player = Main.player[projectile.owner];
			Vector2 move = (toPlayer ? (player.Center + vector) : vector) - projectile.Center;
			float magnitude = RedeHelper.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			move = (projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = RedeHelper.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			projectile.velocity = move;
		}

		public static void MoveToNPC(this NPC npc, NPC target, Vector2 vector, float speed, float turnResistance = 10f)
		{
			Vector2 move = target.Center + vector - npc.Center;
			float magnitude = RedeHelper.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = RedeHelper.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			npc.velocity = move;
		}

		public static float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public static bool Sight(this NPC npc, float range = -1f, bool lineOfSight = false)
		{
			Player player = Main.player[npc.target];
			return (!lineOfSight || Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height)) && (range < 0f || npc.Distance(Main.player[npc.target].Center) <= range) && ((npc.Center.X > player.Center.X && npc.spriteDirection == -1) || (npc.Center.X < player.Center.X && npc.spriteDirection == 1));
		}

		public static void JumpDownPlatform(this NPC npc, ref bool canJump, int yOffset = 12)
		{
			Player player = Main.player[npc.target];
			Point tile = Utils.ToTileCoordinates(npc.Bottom);
			if (Main.tileSolidTop[(int)Framing.GetTileSafely(tile.X, tile.Y).type] && Main.tile[tile.X, tile.Y].active() && npc.Center.Y + (float)yOffset < player.Center.Y)
			{
				Point tile2 = Utils.ToTileCoordinates(npc.BottomRight);
				canJump = true;
				if ((Main.tile[tile.X - 1, tile.Y - 1].active() && Main.tileSolid[(int)Framing.GetTileSafely(tile.X - 1, tile.Y - 1).type]) || (Main.tile[tile2.X + 1, tile2.Y - 1].active() && Main.tileSolid[(int)Framing.GetTileSafely(tile2.X + 1, tile2.Y - 1).type]) || npc.collideX)
				{
					npc.velocity.X = 0f;
				}
			}
		}

		public static void JumpDownPlatform(this NPC npc, Vector2 vector, ref bool canJump, int yOffset = 12)
		{
			Point tile = Utils.ToTileCoordinates(npc.Bottom);
			if (Main.tileSolidTop[(int)Framing.GetTileSafely(tile.X, tile.Y).type] && Main.tile[tile.X, tile.Y].active() && npc.Center.Y + (float)yOffset < vector.Y)
			{
				Point tile2 = Utils.ToTileCoordinates(npc.BottomRight);
				canJump = true;
				if ((Main.tile[tile.X - 1, tile.Y - 1].active() && Main.tileSolid[(int)Framing.GetTileSafely(tile.X - 1, tile.Y - 1).type]) || (Main.tile[tile2.X + 1, tile2.Y - 1].active() && Main.tileSolid[(int)Framing.GetTileSafely(tile2.X + 1, tile2.Y - 1).type]) || npc.collideX)
				{
					npc.velocity.X = 0f;
				}
			}
		}

		public static bool NPCHasAnyBuff(this NPC npc)
		{
			for (int i = 0; i < BuffLoader.BuffCount; i++)
			{
				if (npc.HasBuff(i))
				{
					return true;
				}
			}
			return false;
		}

		public static void HorizontallyMove(NPC npc, Vector2 vector, float moveInterval, float moveSpeed, int maxJumpTilesX, int maxJumpTilesY, bool jumpUpPlatforms)
		{
			if (npc.velocity.X < -moveSpeed || npc.velocity.X > moveSpeed)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.velocity *= 0.8f;
				}
			}
			else if (npc.velocity.X < moveSpeed && vector.X > npc.Center.X)
			{
				npc.velocity.X = npc.velocity.X + moveInterval;
				if (npc.velocity.X > moveSpeed)
				{
					npc.velocity.X = moveSpeed;
				}
			}
			else if (npc.velocity.X > -moveSpeed && vector.X < npc.Center.X)
			{
				npc.velocity.X = npc.velocity.X - moveInterval;
				if (npc.velocity.X < -moveSpeed)
				{
					npc.velocity.X = -moveSpeed;
				}
			}
			if (BaseAI.HitTileOnSide(npc, 3, true) && ((npc.velocity.X < 0f && npc.direction == -1) || (npc.velocity.X > 0f && npc.direction == 1)))
			{
				Vector2 newVec = BaseAI.AttemptJump(npc.position, npc.velocity, npc.width, npc.height, npc.direction, vector, (float)npc.directionY, maxJumpTilesX, maxJumpTilesY, moveSpeed, jumpUpPlatforms, false);
				if (!npc.noTileCollide)
				{
					Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 0);
				}
				if (npc.velocity != newVec)
				{
					npc.velocity = newVec;
					npc.netUpdate = true;
				}
			}
		}

		public delegate bool SpecialCondition(NPC possibleTarget);

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
