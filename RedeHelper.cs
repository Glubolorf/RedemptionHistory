using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Utilities;

namespace Redemption
{
	public static class RedeHelper
	{
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
			Random random = new Random();
			return new Vector2((float)(random.Next((int)pos1.X, (int)pos2.X) + 1), (float)(random.Next((int)pos1.Y, (int)pos2.Y) + 1));
		}

		public static int GetNearestAlivePlayer(NPC npc)
		{
			float num = 4.8151624E+09f;
			int result = -1;
			foreach (Player player2 in Main.player)
			{
				if (player2.Distance(npc.Center) < num && player2.active)
				{
					num = player2.Distance(npc.Center);
					result = player2.whoAmI;
				}
			}
			return result;
		}

		public static Vector2 VelocityFPTP(Vector2 pos1, Vector2 pos2, float speed)
		{
			Vector2 vector = pos2 - pos1;
			return vector * (speed / (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y)));
		}

		public static float RadtoGrad(float Rad)
		{
			return Rad * 180f / 3.1415927f;
		}

		public static int GetNearestNPC(Vector2 Point, bool Friendly = false, bool NoBoss = false)
		{
			float num = -1f;
			int result = -1;
			foreach (NPC npc2 in Main.npc)
			{
				if (npc2.active && (!NoBoss || !npc2.boss) && (Friendly || (!npc2.friendly && npc2.lifeMax > 5)) && (num == -1f || npc2.Distance(Point) < num))
				{
					num = npc2.Distance(Point);
					result = npc2.whoAmI;
				}
			}
			return result;
		}

		public static int GetNearestPlayer(Vector2 Point, bool Alive = false)
		{
			float num = -1f;
			int result = -1;
			foreach (Player player2 in Main.player)
			{
				if ((!Alive || (player2.active && !player2.dead)) && (num == -1f || player2.Distance(Point) < num))
				{
					num = player2.Distance(Point);
					result = player2.whoAmI;
				}
			}
			return result;
		}

		public static Vector2 VelocityToPoint(Vector2 A, Vector2 B, float Speed)
		{
			Vector2 vector = B - A;
			return vector * (Speed / (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y)));
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
			Vector2 result = default(Vector2);
			result.X = Distance * (float)Math.Sin((double)RedeHelper.RadtoGrad(Angle)) + Point.X + (float)XOffset;
			result.Y = Distance * (float)Math.Cos((double)RedeHelper.RadtoGrad(Angle)) + Point.Y + (float)YOffset;
			return result;
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
			float num = Float * Percent;
			int num2 = 0;
			while (num.ToString().Split(new char[]
			{
				','
			}).Length > 1)
			{
				num *= 10f;
				num2++;
			}
			return Float + (float)Main.rand.Next(0, (int)num + 1) / (float)Math.Pow(10.0, (double)num2) * (float)((Main.rand.Next(2) == 0) ? -1 : 1);
		}

		public static Vector2 FoundPosition(Vector2 tilePos)
		{
			Vector2 vector;
			vector..ctor((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2));
			Vector2 vector2 = tilePos - Main.mapFullscreenPos;
			vector2 *= Main.mapFullscreenScale / 16f;
			vector2 = vector2 * 16f + vector;
			Vector2 result;
			result..ctor((float)((int)vector2.X), (float)((int)vector2.Y));
			return result;
		}

		public static void MoveTowards(this NPC npc, Vector2 playerTarget, float speed, float turnResistance)
		{
			Vector2 vector = playerTarget - npc.Center;
			float num = vector.Length();
			if (num > speed)
			{
				vector *= speed / num;
			}
			vector = (npc.velocity * turnResistance + vector) / (turnResistance + 1f);
			num = vector.Length();
			if (num > speed)
			{
				vector *= speed / num;
			}
			npc.velocity = vector;
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
					int[] array = new int[]
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
					for (int k = 0; k < array.Length - 1; k++)
					{
						if (Main.tile[i, j].type == (ushort)array[k])
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
					int[] array = new int[]
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
					for (int k = 0; k < array.Length - 1; k++)
					{
						if (Main.tile[i, j].type == (ushort)array[k])
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
