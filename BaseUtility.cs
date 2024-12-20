using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption
{
	public class BaseUtility
	{
		public static void LogFancy(string logText)
		{
			BaseUtility.LogFancy("", logText, null);
		}

		public static void LogFancy(string prefix, Exception e)
		{
			BaseUtility.LogFancy(prefix, null, e);
		}

		public static void LogFancy(string prefix, string logText, Exception e = null)
		{
			if (e != null)
			{
				ErrorLogger.Log(prefix + e.Message);
				ErrorLogger.Log(e.StackTrace);
				ErrorLogger.Log(">---------<");
				return;
			}
			ErrorLogger.Log(prefix + logText);
		}

		public static void OpenChestUI(int i, int j)
		{
			Player player = Main.player[Main.myPlayer];
			Tile tile = Main.tile[i, j];
			Main.mouseRightRelease = false;
			int num = i;
			int num2 = j;
			if (tile.frameX % 36 != 0)
			{
				num--;
			}
			if (tile.frameY != 0)
			{
				num2--;
			}
			if (player.sign >= 0)
			{
				Main.PlaySound(11, -1, -1, 1, 1f, 0f);
				player.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
			}
			if (Main.editChest)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				Main.editChest = false;
				Main.npcChatText = "";
			}
			if (player.editedChestName)
			{
				NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f, 0f, 0f, 0, 0, 0);
				player.editedChestName = false;
			}
			if (Main.netMode != 1)
			{
				int num3 = Chest.FindChest(num, num2);
				if (num3 >= 0)
				{
					Main.stackSplit = 600;
					if (num3 == player.chest)
					{
						player.chest = -1;
						Main.PlaySound(11, -1, -1, 1, 1f, 0f);
					}
					else
					{
						player.chest = num3;
						Main.playerInventory = true;
						Main.recBigList = false;
						player.chestX = num;
						player.chestY = num2;
						Main.PlaySound((player.chest < 0) ? 10 : 12, -1, -1, 1, 1f, 0f);
					}
					Recipe.FindRecipes();
				}
				return;
			}
			if (num == player.chestX && num2 == player.chestY && player.chest >= 0)
			{
				player.chest = -1;
				Recipe.FindRecipes();
				Main.PlaySound(11, -1, -1, 1, 1f, 0f);
				return;
			}
			NetMessage.SendData(31, -1, -1, NetworkText.FromLiteral(""), num, (float)num2, 0f, 0f, 0, 0, 0);
			Main.stackSplit = 600;
		}

		public static void DisplayTime(double time = -1.0, Color? overrideColor = null, bool sync = false)
		{
			string text = "AM";
			if (time <= -1.0)
			{
				time = Main.time;
			}
			if (!Main.dayTime)
			{
				time += 54000.0;
			}
			time = time / 86400.0 * 24.0;
			time = time - 7.5 - 12.0;
			if (time < 0.0)
			{
				time += 24.0;
			}
			if (time >= 12.0)
			{
				text = "PM";
			}
			int num = (int)time;
			double num2 = time - (double)num;
			num2 = (double)((int)(num2 * 60.0));
			string text2 = string.Concat(num2);
			if (num2 < 10.0)
			{
				text2 = "0" + text2;
			}
			if (num > 12)
			{
				num -= 12;
			}
			if (num == 0)
			{
				num = 12;
			}
			string s = string.Concat(new object[]
			{
				"Time: ",
				num,
				":",
				text2,
				" ",
				text
			});
			BaseUtility.Chat(s, (overrideColor != null) ? overrideColor.Value : new Color(255, 240, 20), sync);
		}

		public static int CheckForGore(Mod mod, string goreName, IDictionary<string, int> gores = null)
		{
			if (mod == null)
			{
				return -1;
			}
			if (mod.GetGoreSlot("Gores/" + goreName) > 0)
			{
				return mod.GetGoreSlot("Gores/" + goreName);
			}
			if (gores == null && mod is GoreInfo)
			{
				gores = ((GoreInfo)mod).GetGoreArray();
			}
			if (gores == null)
			{
				gores = (IDictionary<string, int>)typeof(ModGore).GetField("gores", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
			}
			foreach (string text in gores.Keys)
			{
				if (text.Contains(mod.Name) && text.Contains(goreName))
				{
					return ModGore.GetGoreSlot(text);
				}
			}
			return -1;
		}

		public static int CalcValue(int plat, int gold, int silver, int copper, bool sellPrice = false)
		{
			int num = copper + silver * 100;
			num += gold * 10000;
			num += plat * 1000000;
			if (sellPrice)
			{
				num *= 5;
			}
			return num;
		}

		public static void AddTooltips(Item item, string[] tooltips)
		{
			BaseUtility.AddTooltips(item.modItem, tooltips);
		}

		public static void AddTooltips(ModItem item, string[] tooltips)
		{
			string text = "";
			for (int i = 0; i < tooltips.Length; i++)
			{
				text = text + tooltips[i] + ((i == tooltips.Length - 1) ? "" : "\n");
			}
			item.Tooltip.SetDefault(text);
		}

		public static NPC NPCByName(string n)
		{
			if (n.Contains(":"))
			{
				string text = n.Split(new char[]
				{
					':'
				})[0];
				string text2 = n.Split(new char[]
				{
					':'
				})[1];
				return ModLoader.GetMod(text).GetNPC(text2).npc;
			}
			string[] loadedMods = ModLoader.GetLoadedMods();
			foreach (string text3 in loadedMods)
			{
				Mod mod = ModLoader.GetMod(text3);
				ModNPC npc = mod.GetNPC(n);
				if (npc != null)
				{
					return npc.npc;
				}
			}
			return null;
		}

		public static Item ItemByName(string n)
		{
			if (n.Contains(":"))
			{
				string text = n.Split(new char[]
				{
					':'
				})[0];
				string text2 = n.Split(new char[]
				{
					':'
				})[1];
				return ModLoader.GetMod(text).GetItem(text2).item;
			}
			string[] loadedMods = ModLoader.GetLoadedMods();
			foreach (string text3 in loadedMods)
			{
				Mod mod = ModLoader.GetMod(text3);
				ModItem item = mod.GetItem(n);
				if (item != null)
				{
					return item.item;
				}
			}
			return null;
		}

		public static Projectile ProjByName(string n)
		{
			if (n.Contains(":"))
			{
				string text = n.Split(new char[]
				{
					':'
				})[0];
				string text2 = n.Split(new char[]
				{
					':'
				})[1];
				return ModLoader.GetMod(text).GetProjectile(text2).projectile;
			}
			string[] loadedMods = ModLoader.GetLoadedMods();
			foreach (string text3 in loadedMods)
			{
				Mod mod = ModLoader.GetMod(text3);
				ModProjectile projectile = mod.GetProjectile(n);
				if (projectile != null)
				{
					return projectile.projectile;
				}
			}
			return null;
		}

		public static ModTile TileByName(string n)
		{
			if (n.Contains(":"))
			{
				string text = n.Split(new char[]
				{
					':'
				})[0];
				string text2 = n.Split(new char[]
				{
					':'
				})[1];
				return ModLoader.GetMod(text).GetTile(text2);
			}
			string[] loadedMods = ModLoader.GetLoadedMods();
			foreach (string text3 in loadedMods)
			{
				Mod mod = ModLoader.GetMod(text3);
				ModTile tile = mod.GetTile(n);
				if (tile != null)
				{
					return tile;
				}
			}
			return null;
		}

		public static bool CanHit(Rectangle rect, Rectangle rect2)
		{
			return Collision.CanHit(new Vector2((float)rect.X, (float)rect.Y), rect.Width, rect.Height, new Vector2((float)rect2.X, (float)rect2.Y), rect2.Width, rect2.Height);
		}

		public static void PlaySound(Mod mod, object soundType, int x, int y, string sound, bool stop = true, bool newInstance = true, float? overrideVolume = null, float? overridePitch = null)
		{
			int num = (soundType is int) ? ((int)soundType) : 0;
			if (soundType is SoundType)
			{
				SoundType soundType2 = (SoundType)soundType;
				switch (soundType2)
				{
				case 2:
					num = 2;
					break;
				case 3:
					num = 3;
					break;
				case 4:
					num = 4;
					break;
				default:
					if (soundType2 == 50)
					{
						num = -1;
					}
					else
					{
						num = 0;
					}
					break;
				}
			}
			BaseUtility.PlaySound(soundType, x, y, mod.Name + "/Sounds/" + ((num == -1) ? "Custom/" : ((num == 2) ? "Item/" : ((num == 3) ? "NPCHit/" : "NPCKilled/"))) + sound, stop, newInstance, overrideVolume, overridePitch);
		}

		public static void PlaySound(object soundType, int x, int y, object sound, bool stop = true, bool newInstance = true, float? overrideVolume = null, float? overridePitch = null)
		{
			if (Main.netMode == 2 || Main.dedServ || Main.soundVolume == 0f)
			{
				return;
			}
			Rectangle rectangle;
			rectangle..ctor((int)(Main.screenPosition.X - (float)(Main.screenWidth * 2)), (int)(Main.screenPosition.Y - (float)(Main.screenHeight * 2)), Main.screenWidth * 5, Main.screenHeight * 5);
			Rectangle rectangle2;
			rectangle2..ctor(x, y, 1, 1);
			bool flag = rectangle2.Intersects(rectangle);
			if ((x == -1 && y == -1) || flag)
			{
				float num = 0f;
				int num2 = (soundType is int) ? ((int)soundType) : 0;
				if (soundType is SoundType)
				{
					SoundType soundType2 = (SoundType)soundType;
					switch (soundType2)
					{
					case 2:
						num2 = 2;
						break;
					case 3:
						num2 = 3;
						break;
					case 4:
						num2 = 4;
						break;
					default:
						if (soundType2 == 50)
						{
							num2 = -1;
						}
						else
						{
							num2 = 0;
						}
						break;
					}
				}
				int num3;
				SoundEffect[] array;
				SoundEffectInstance[] array2;
				switch (num2)
				{
				case -1:
					num3 = SoundLoader.GetSoundSlot(50, (string)sound);
					if (BaseUtility.soundField == null)
					{
						BaseUtility.soundField = typeof(SoundLoader).GetField("customSounds", BindingFlags.Static | BindingFlags.NonPublic);
					}
					if (BaseUtility.soundInstanceField == null)
					{
						BaseUtility.soundInstanceField = typeof(SoundLoader).GetField("customSoundInstances", BindingFlags.Static | BindingFlags.NonPublic);
					}
					array = (SoundEffect[])BaseUtility.soundField.GetValue(null);
					array2 = (SoundEffectInstance[])BaseUtility.soundInstanceField.GetValue(null);
					break;
				case 0:
				case 1:
					return;
				case 2:
					if (sound is string)
					{
						num3 = SoundLoader.GetSoundSlot(2, (string)sound);
					}
					else
					{
						num3 = (int)sound;
					}
					array = Main.soundItem;
					array2 = Main.soundInstanceItem;
					num = (float)Main.rand.Next(-6, 7) * 0.01f;
					break;
				case 3:
					if (sound is string)
					{
						num3 = SoundLoader.GetSoundSlot(3, (string)sound);
					}
					else
					{
						num3 = (int)sound;
					}
					array = Main.soundNPCHit;
					array2 = Main.soundInstanceNPCHit;
					num = (float)Main.rand.Next(-10, 11) * 0.01f;
					break;
				case 4:
					if (sound is string)
					{
						num3 = SoundLoader.GetSoundSlot(4, (string)sound);
					}
					else
					{
						num3 = (int)sound;
					}
					array = Main.soundNPCKilled;
					array2 = Main.soundInstanceNPCKilled;
					num = (float)Main.rand.Next(-10, 11) * 0.01f;
					break;
				default:
					return;
				}
				SoundEffect soundEffect = array[num3];
				if (stop && num3 != -1 && array2[num3] != null)
				{
					array2[num3].Stop();
				}
				float val = 0f;
				float num4 = 1f;
				if (flag)
				{
					Vector2 vector;
					vector..ctor(Main.screenPosition.X + (float)Main.screenWidth * 0.5f, Main.screenPosition.Y + (float)Main.screenHeight * 0.5f);
					float num5 = Math.Abs((float)x - vector.X);
					float num6 = Math.Abs((float)y - vector.Y);
					float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
					val = ((float)x - vector.X) / ((float)Main.screenWidth * 0.5f);
					num4 = 1f - num7 / ((float)Main.screenWidth * 1.5f);
				}
				SoundEffectInstance soundEffectInstance = newInstance ? soundEffect.CreateInstance() : ((array2[num3].State == null) ? array2[num3] : soundEffect.CreateInstance());
				soundEffectInstance.Volume = Math.Max(0f, Math.Min(1f, ((overrideVolume != null) ? overrideVolume.Value : num4) * Main.soundVolume));
				soundEffectInstance.Pitch = ((overridePitch != null) ? overridePitch.Value : num);
				soundEffectInstance.Pan = Math.Max(-1f, Math.Min(1f, val));
				Main.PlaySoundInstance(soundEffectInstance);
				array2[num3] = soundEffectInstance;
				switch (num2)
				{
				case -1:
					BaseUtility.soundField.SetValue(null, array);
					BaseUtility.soundInstanceField.SetValue(null, array2);
					return;
				case 0:
				case 1:
					break;
				case 2:
					Main.soundItem = array;
					Main.soundInstanceItem = array2;
					return;
				case 3:
					Main.soundNPCHit = array;
					Main.soundInstanceNPCHit = array2;
					return;
				case 4:
					Main.soundNPCKilled = array;
					Main.soundInstanceNPCKilled = array2;
					break;
				default:
					return;
				}
			}
		}

		public static Vector2 TileToPos(Vector2 tile)
		{
			return tile * new Vector2(16f, 16f);
		}

		public static Vector2 PosToTile(Vector2 pos)
		{
			return pos / new Vector2(16f, 16f);
		}

		public static int TicksToSeconds(int ticks)
		{
			return ticks / 60;
		}

		public static int SecondsToTicks(int seconds)
		{
			return seconds * 60;
		}

		public static int TicksToMinutes(int ticks)
		{
			return BaseUtility.TicksToSeconds(ticks) / 60;
		}

		public static int MinutesToTicks(int minutes)
		{
			return BaseUtility.SecondsToTicks(minutes) * 60;
		}

		public static Color[] AddToArray(Color[] array, Color valueToAdd, int indexAt = -1)
		{
			Array.Resize<Color>(ref array, (indexAt + 1 > array.Length + 1) ? (indexAt + 1) : (array.Length + 1));
			if (indexAt == -1)
			{
				array[array.Length - 1] = valueToAdd;
			}
			else
			{
				List<Color> list = Enumerable.ToList<Color>(array);
				list.Insert(indexAt, valueToAdd);
				array = list.ToArray();
			}
			return array;
		}

		public static string[] AddToArray(string[] array, string valueToAdd, int indexAt = -1)
		{
			Array.Resize<string>(ref array, (indexAt + 1 > array.Length + 1) ? (indexAt + 1) : (array.Length + 1));
			if (indexAt == -1)
			{
				array[array.Length - 1] = valueToAdd;
			}
			else
			{
				List<string> list = Enumerable.ToList<string>(array);
				list.Insert(indexAt, valueToAdd);
				array = list.ToArray();
			}
			return array;
		}

		public static int[] AddToArray(int[] array, int valueToAdd, int indexAt = -1)
		{
			Array.Resize<int>(ref array, (indexAt + 1 > array.Length + 1) ? (indexAt + 1) : (array.Length + 1));
			if (indexAt == -1)
			{
				array[array.Length - 1] = valueToAdd;
			}
			else
			{
				List<int> list = Enumerable.ToList<int>(array);
				list.Insert(indexAt, valueToAdd);
				array = list.ToArray();
			}
			return array;
		}

		public static int[] CombineArrays(int[] array1, int[] array2)
		{
			int[] array3 = new int[array1.Length + array2.Length];
			for (int i = 0; i < array1.Length; i++)
			{
				array3[i] = array1[i];
			}
			for (int j = 0; j < array2.Length; j++)
			{
				array3[array1.Length + j] = array2[j];
			}
			return array3;
		}

		public static int[] FillArray(int[] array, int value)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
			return array;
		}

		public static bool InArray(int[] array, int value)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (value == array[i])
				{
					return true;
				}
			}
			return false;
		}

		public static bool InArray(int[] array, int value, ref int index)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (value == array[i])
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		public static bool InArray(float[] array, float value)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (value == array[i])
				{
					return true;
				}
			}
			return false;
		}

		public static bool InArray(float[] array, float value, ref int index)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (value == array[i])
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		public static Color ColorBrightness(Color color, int factor)
		{
			int num = Math.Max(0, Math.Min(255, (int)color.R + factor));
			int num2 = Math.Max(0, Math.Min(255, (int)color.G + factor));
			int num3 = Math.Max(0, Math.Min(255, (int)color.B + factor));
			return new Color(num, num2, num3, (int)color.A);
		}

		public static Color ColorMult(Color color, float mult)
		{
			int num = Math.Max(0, Math.Min(255, (int)((float)color.R * mult)));
			int num2 = Math.Max(0, Math.Min(255, (int)((float)color.G * mult)));
			int num3 = Math.Max(0, Math.Min(255, (int)((float)color.B * mult)));
			return new Color(num, num2, num3, (int)color.A);
		}

		public static Color ColorClamp(Color color1, Color color2)
		{
			int r = (int)color1.R;
			int g = (int)color1.G;
			int b = (int)color1.B;
			int a = (int)color1.A;
			if (r < (int)color2.R)
			{
				r = (int)color2.R;
			}
			if (g < (int)color2.G)
			{
				g = (int)color2.G;
			}
			if (b < (int)color2.B)
			{
				b = (int)color2.B;
			}
			if (a < (int)color2.A)
			{
				a = (int)color2.A;
			}
			return new Color(r, g, b, a);
		}

		public static Color ColorBrightnessClamp(Color color1, Color color2)
		{
			float num = (float)color1.R / 255f;
			float num2 = (float)color1.G / 255f;
			float num3 = (float)color1.B / 255f;
			float num4 = (float)color2.R / 255f;
			float num5 = (float)color2.G / 255f;
			float num6 = (float)color2.B / 255f;
			float num7 = (num4 > num5) ? num4 : ((num5 > num6) ? num5 : num6);
			num *= num7;
			num2 *= num7;
			num3 *= num7;
			return new Color(num, num2, num3, (float)color1.A / 255f);
		}

		public static Color BuffColorize(Color buffColor, Color lightColor)
		{
			Color color = BaseUtility.ColorBrightnessClamp(buffColor, lightColor);
			return BaseUtility.ColorClamp(BaseUtility.Colorize(buffColor, lightColor), color);
		}

		public static Color Colorize(Color tint, Color lightColor)
		{
			float num = (float)lightColor.R / 255f;
			float num2 = (float)lightColor.G / 255f;
			float num3 = (float)lightColor.B / 255f;
			float num4 = (float)lightColor.A / 255f;
			Color result = tint;
			float num5 = (float)((byte)((float)result.R * num));
			float num6 = (float)((byte)((float)result.G * num2));
			float num7 = (float)((byte)((float)result.B * num3));
			float num8 = (float)((byte)((float)result.A * num4));
			result.R = (byte)num5;
			result.G = (byte)num6;
			result.B = (byte)num7;
			result.A = (byte)num8;
			return result;
		}

		public Color Rainbow(float percent)
		{
			Color color;
			color..ctor(255, 50, 50);
			Color color2;
			color2..ctor(50, 255, 50);
			Color color3;
			color3..ctor(90, 90, 255);
			Color color4;
			color4..ctor(255, 255, 50);
			if (percent <= 0.25f)
			{
				return Color.Lerp(color, color3, percent / 0.25f);
			}
			if (percent <= 0.5f)
			{
				return Color.Lerp(color3, color2, (percent - 0.25f) / 0.25f);
			}
			if (percent <= 0.75f)
			{
				return Color.Lerp(color2, color4, (percent - 0.5f) / 0.25f);
			}
			return Color.Lerp(color4, color, (percent - 0.75f) / 0.25f);
		}

		public static Vector2 ClampToWorld(Vector2 position, bool tilePos = false)
		{
			if (tilePos)
			{
				position.X = (float)((int)MathHelper.Clamp(position.X, 0f, (float)Main.maxTilesX));
				position.Y = (float)((int)MathHelper.Clamp(position.Y, 0f, (float)Main.maxTilesY));
			}
			else
			{
				position.X = (float)((int)MathHelper.Clamp(position.X, 0f, (float)(Main.maxTilesX * 16)));
				position.Y = (float)((int)MathHelper.Clamp(position.Y, 0f, (float)(Main.maxTilesY * 16)));
			}
			return position;
		}

		public static float GetTotalDistance(Vector2[] points)
		{
			float num = 0f;
			for (int i = 1; i < points.Length; i++)
			{
				num += Vector2.Distance(points[i - 1], points[i]);
			}
			return num;
		}

		public static Rectangle ScaleRectangle(Rectangle rect, float scale)
		{
			float num = ((float)rect.Width * scale - (float)rect.Width) / 2f;
			float num2 = ((float)rect.Height * scale - (float)rect.Height) / 2f;
			int num3 = rect.X - (int)num;
			int num4 = rect.Y - (int)num2;
			int num5 = rect.Width + (int)(num * 2f);
			int num6 = rect.Height + (int)(num2 * 2f);
			return new Rectangle(num3, num4, num5, num6);
		}

		public static float MultiLerp(float percent, params float[] floats)
		{
			float num = 1f / ((float)floats.Length - 1f);
			float num2 = num;
			int num3 = 0;
			while (percent / num2 > 1f && num3 < floats.Length - 2)
			{
				num2 += num;
				num3++;
			}
			return MathHelper.Lerp(floats[num3], floats[num3 + 1], (percent - num * (float)num3) / num);
		}

		public static Vector2 MultiLerpVector(float percent, params Vector2[] vectors)
		{
			float num = 1f / ((float)vectors.Length - 1f);
			float num2 = num;
			int num3 = 0;
			while (percent / num2 > 1f && num3 < vectors.Length - 2)
			{
				num2 += num;
				num3++;
			}
			return Vector2.Lerp(vectors[num3], vectors[num3 + 1], (percent - num * (float)num3) / num);
		}

		public static Color MultiLerpColor(float percent, params Color[] colors)
		{
			float num = 1f / ((float)colors.Length - 1f);
			float num2 = num;
			int num3 = 0;
			while (percent / num2 > 1f && num3 < colors.Length - 2)
			{
				num2 += num;
				num3++;
			}
			return Color.Lerp(colors[num3], colors[num3 + 1], (percent - num * (float)num3) / num);
		}

		public static float RotationTo(Vector2 startPos, Vector2 endPos)
		{
			return (float)Math.Atan2((double)(endPos.Y - startPos.Y), (double)(endPos.X - startPos.X));
		}

		public static Vector2 FlipVector(Vector2 origin, Vector2 point, bool flipX = true, bool flipY = true)
		{
			float num = point.X - origin.X;
			float num2 = point.Y - origin.Y;
			if (flipX)
			{
				num *= -1f;
			}
			if (flipY)
			{
				num2 *= -1f;
			}
			return origin + new Vector2(num, num2);
		}

		public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
		{
			float num = (float)(Math.Cos((double)rot) * (double)(vecToRot.X - origin.X) - Math.Sin((double)rot) * (double)(vecToRot.Y - origin.Y) + (double)origin.X);
			float num2 = (float)(Math.Sin((double)rot) * (double)(vecToRot.X - origin.X) + Math.Cos((double)rot) * (double)(vecToRot.Y - origin.Y) + (double)origin.Y);
			return new Vector2(num, num2);
		}

		public static Vector2 GetRandomPosNear(Vector2 pos, int minDistance, int maxDistance, bool circular = false)
		{
			return BaseUtility.GetRandomPosNear(pos, Main.rand, minDistance, maxDistance, circular);
		}

		public static Vector2 GetRandomPosNear(Vector2 pos, UnifiedRandom rand, int minDistance, int maxDistance, bool circular = false)
		{
			int num = maxDistance - minDistance;
			if (!circular)
			{
				float num2 = pos.X + (float)((Main.rand.Next(2) == 0) ? (-(float)(minDistance + rand.Next(num))) : (minDistance + rand.Next(num)));
				float num3 = pos.Y + (float)((Main.rand.Next(2) == 0) ? (-(float)(minDistance + rand.Next(num))) : (minDistance + rand.Next(num)));
				return new Vector2(num2, num3);
			}
			return BaseUtility.RotateVector(pos, pos + new Vector2((float)(minDistance + rand.Next(num))), MathHelper.Lerp(0f, 6.2831855f, (float)rand.NextDouble()));
		}

		public static void Chat(string s, Color color, bool sync = true)
		{
			BaseUtility.Chat(s, color.R, color.G, color.B, sync);
		}

		public static void Chat(string s, byte colorR = 255, byte colorG = 255, byte colorB = 255, bool sync = true)
		{
			if (Main.netMode == 0)
			{
				Main.NewText(s, colorR, colorG, colorB, false);
				return;
			}
			if (Main.netMode == 1)
			{
				Main.NewText(s, colorR, colorG, colorB, false);
				return;
			}
			if (sync && Main.netMode == 2)
			{
				NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color((int)colorR, (int)colorG, (int)colorB), -1);
			}
		}

		public static Vector2[] ChainVector2(Vector2 start, Vector2 end, float jump = 0f)
		{
			List<Vector2> list = new List<Vector2>();
			if (jump <= 0f)
			{
				jump = 16f;
			}
			Vector2 vector = end - start;
			vector.Normalize();
			float num = Vector2.Distance(start, end);
			for (float num2 = 0f; num2 < num; num2 += jump)
			{
				list.Add(start + vector * num2);
			}
			return list.ToArray();
		}

		public static Point[] ChainPoint(Point start, Point end, float jump = 0f)
		{
			List<Point> list = new List<Point>();
			if (jump <= 0f)
			{
				jump = 16f;
			}
			Vector2 vector = Utils.ToVector2(end) - Utils.ToVector2(start);
			vector.Normalize();
			float num = Vector2.Distance(Utils.ToVector2(start), Utils.ToVector2(end));
			for (float num2 = 0f; num2 < num; num2 += jump)
			{
				Vector2 vector2 = Utils.ToVector2(start) + vector * num2;
				Point item;
				item..ctor((int)vector2.X, (int)vector2.Y);
				list.Add(item);
			}
			return list.ToArray();
		}

		private static FieldInfo soundField;

		private static FieldInfo soundInstanceField;
	}
}
