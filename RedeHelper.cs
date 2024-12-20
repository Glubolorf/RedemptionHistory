using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Utilities;

namespace Redemption
{
	public static class RedeHelper
	{
		public static Vector2 PolarVector(float radius, float theta)
		{
			return new Vector2((float)Math.Cos((double)theta), (float)Math.Sin((double)theta)) * radius;
		}

		public static bool ClosestNPC(ref NPC target, float maxDistance, Vector2 position, bool ignoreTiles = false, int overrideTarget = -1)
		{
			bool foundTarget = false;
			if (overrideTarget != -1 && (Main.npc[overrideTarget].Center - position).Length() < maxDistance)
			{
				target = Main.npc[overrideTarget];
				return true;
			}
			for (int i = 0; i < Main.npc.Length; i++)
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

		public static float rotateBetween2Points(Vector2 A, Vector2 B)
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

		public delegate void ExtraAction();

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
