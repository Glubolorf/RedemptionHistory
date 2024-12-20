using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption
{
	public class BasePlayer
	{
		public static bool ConsumeAmmo(Player player, Item item, Item ammo)
		{
			bool result = true;
			if (player.magicQuiver && ammo.ammo == AmmoID.Arrow && Main.rand.Next(5) == 0)
			{
				result = false;
			}
			if (player.ammoBox && Main.rand.Next(5) == 0)
			{
				result = false;
			}
			if (player.ammoPotion && Main.rand.Next(5) == 0)
			{
				result = false;
			}
			if (player.ammoCost80 && Main.rand.Next(5) == 0)
			{
				result = false;
			}
			if (player.ammoCost75 && Main.rand.Next(4) == 0)
			{
				result = false;
			}
			if (!PlayerHooks.ConsumeAmmo(player, item, ammo))
			{
				result = false;
			}
			if (!ItemLoader.ConsumeAmmo(item, ammo, player))
			{
				result = false;
			}
			return result;
		}

		public static void ReduceSlot(Player player, int slot, int amount)
		{
			player.inventory[slot].stack -= amount;
			if (player.inventory[slot].stack <= 0)
			{
				player.inventory[slot] = new Item();
			}
		}

		public static bool HasEmptySlots(Player player, int slotCount, bool includeInventory = true, bool includeCoins = false, bool includeAmmo = false)
		{
			int num = 0;
			for (int i = includeInventory ? 0 : (includeCoins ? 50 : 54); i < (includeAmmo ? 58 : (includeCoins ? 54 : 50)); i++)
			{
				Item item = player.inventory[i];
				if (item == null || item.IsBlank())
				{
					num++;
					if (num >= slotCount)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static int GetEmptySlot(Player player, bool includeInventory = true, bool includeCoins = false, bool includeAmmo = false)
		{
			for (int i = includeInventory ? 0 : (includeCoins ? 50 : 54); i < (includeAmmo ? 58 : (includeCoins ? 54 : 50)); i++)
			{
				Item item = player.inventory[i];
				if (item == null || item.IsBlank())
				{
					return i;
				}
			}
			return -1;
		}

		public static bool DowngradeMoney(Player player, int moneySlot, ref int splitSlot)
		{
			Item item = player.inventory[moneySlot];
			if (item == null || item.type <= 71 || item.type >= 75)
			{
				return false;
			}
			int num = item.type - 1;
			splitSlot = BasePlayer.GetEmptySlot(player, false, true, false);
			if (splitSlot == -1)
			{
				splitSlot = BasePlayer.GetEmptySlot(player, true, false, false);
			}
			if (splitSlot == -1)
			{
				return false;
			}
			player.inventory[splitSlot].SetDefaults(num, false);
			player.inventory[splitSlot].stack = 100;
			player.inventory[moneySlot].stack--;
			if (player.inventory[moneySlot].stack <= 0)
			{
				player.inventory[moneySlot] = new Item();
			}
			return true;
		}

		public static void UseHeldItem(Player player)
		{
			if (Main.myPlayer == player.whoAmI && player.itemAnimation == 0)
			{
				MPlayer.useItem = true;
			}
		}

		public static bool ReduceMana(Player player, int amount, bool autoRefill = true)
		{
			if (autoRefill && player.manaFlower && player.statMana < (int)((float)amount * player.manaCost))
			{
				player.QuickMana();
			}
			if (player.statMana >= (int)((float)amount * player.manaCost))
			{
				player.statMana -= (int)((float)amount * player.manaCost);
				if (player.statMana < 0)
				{
					player.statMana = 0;
				}
				return true;
			}
			return false;
		}

		public static bool HasHelmet(Player player, int itemType, bool vanity = true)
		{
			return BasePlayer.HasArmor(player, itemType, 0, vanity);
		}

		public static bool HasChestplate(Player player, int itemType, bool vanity = true)
		{
			return BasePlayer.HasArmor(player, itemType, 1, vanity);
		}

		public static bool HasLeggings(Player player, int itemType, bool vanity = true)
		{
			return BasePlayer.HasArmor(player, itemType, 2, vanity);
		}

		public static bool HasArmor(Player player, int itemType, int armorType, bool vanity = true)
		{
			if (vanity)
			{
				if (armorType == 0)
				{
					return (player.armor[10] != null && player.armor[10].type == itemType) || (player.armor[0] != null && player.armor[0].type == itemType);
				}
				if (armorType == 1)
				{
					return (player.armor[11] != null && player.armor[11].type == itemType) || (player.armor[1] != null && player.armor[1].type == itemType);
				}
				if (armorType == 2)
				{
					return (player.armor[12] != null && player.armor[12].type == itemType) || (player.armor[2] != null && player.armor[2].type == itemType);
				}
			}
			else
			{
				if (armorType == 0)
				{
					return player.armor[0] != null && player.armor[0].type == itemType;
				}
				if (armorType == 1)
				{
					return player.armor[1] != null && player.armor[1].type == itemType;
				}
				if (armorType == 2)
				{
					return player.armor[2] != null && player.armor[2].type == itemType;
				}
			}
			return false;
		}

		public static int GetMoneySum(Player player, bool includeInventory = false)
		{
			int num = 0;
			for (int i = includeInventory ? 0 : 50; i < 54; i++)
			{
				Item item = player.inventory[i];
				if (item != null)
				{
					if (item.type == 71)
					{
						num += item.stack;
					}
					else if (item.type == 72)
					{
						num += item.stack * 100;
					}
					else if (item.type == 73)
					{
						num += item.stack * 10000;
					}
					else if (item.type == 74)
					{
						num += item.stack * 1000000;
					}
				}
			}
			return num;
		}

		public static int GetItemstackSum(Player player, int type, bool typeIsAmmo = false, bool includeAmmo = false, bool includeCoins = false)
		{
			return BasePlayer.GetItemstackSum(player, new int[]
			{
				type
			}, typeIsAmmo, includeAmmo, includeCoins);
		}

		public static int GetItemstackSum(Player player, int[] types, bool typeIsAmmo = false, bool includeAmmo = false, bool includeCoins = false)
		{
			int num = 0;
			if (includeCoins)
			{
				for (int i = 50; i < 54; i++)
				{
					Item item = player.inventory[i];
					if (item != null && (typeIsAmmo ? BaseUtility.InArray(types, item.ammo) : BaseUtility.InArray(types, item.type)))
					{
						num += item.stack;
					}
				}
			}
			if (includeAmmo)
			{
				for (int j = 54; j < 58; j++)
				{
					Item item2 = player.inventory[j];
					if (item2 != null && (typeIsAmmo ? BaseUtility.InArray(types, item2.ammo) : BaseUtility.InArray(types, item2.type)))
					{
						num += item2.stack;
					}
				}
			}
			for (int k = 0; k < 50; k++)
			{
				Item item3 = player.inventory[k];
				if (item3 != null && (typeIsAmmo ? BaseUtility.InArray(types, item3.ammo) : BaseUtility.InArray(types, item3.type)))
				{
					num += item3.stack;
				}
			}
			return num;
		}

		public static bool HasItem(Player player, int[] types, int[] counts = null, bool includeAmmo = false, bool includeCoins = false)
		{
			int num = 0;
			return BasePlayer.HasItem(player, types, ref num, counts, includeAmmo, includeCoins);
		}

		public static bool HasItem(Player player, int[] types, ref int index, int[] counts = null, bool includeAmmo = false, bool includeCoins = false)
		{
			if (types == null || types.Length == 0)
			{
				return false;
			}
			if (counts == null || counts.Length == 0)
			{
				counts = BaseUtility.FillArray(new int[types.Length], 1);
			}
			int num = -1;
			if (includeCoins)
			{
				for (int i = 50; i < 54; i++)
				{
					Item item = player.inventory[i];
					if (item != null && BaseUtility.InArray(types, item.type, ref num) && item.stack >= counts[num])
					{
						index = i;
						return true;
					}
				}
			}
			if (includeAmmo)
			{
				for (int j = 54; j < 58; j++)
				{
					Item item2 = player.inventory[j];
					if (item2 != null && BaseUtility.InArray(types, item2.type, ref num) && item2.stack >= counts[num])
					{
						index = j;
						return true;
					}
				}
			}
			for (int k = 0; k < 50; k++)
			{
				Item item3 = player.inventory[k];
				if (item3 != null && BaseUtility.InArray(types, item3.type, ref num) && item3.stack >= counts[num])
				{
					index = k;
					return true;
				}
			}
			return false;
		}

		public static bool HasAllItems(Player player, int[] types, ref int[] indicies, int[] counts = null, bool includeAmmo = false, bool includeCoins = false)
		{
			if (types == null || types.Length == 0)
			{
				return false;
			}
			if (counts == null || counts.Length == 0)
			{
				counts = BaseUtility.FillArray(new int[types.Length], 1);
			}
			int[] array = new int[types.Length];
			bool[] array2 = new bool[types.Length];
			if (includeCoins)
			{
				for (int i = 50; i < 54; i++)
				{
					for (int j = 0; j < types.Length; j++)
					{
						if (!array2[j])
						{
							Item item = player.inventory[i];
							if (item != null && item.type == types[j] && item.stack >= counts[j])
							{
								array2[j] = true;
								array[j] = i;
							}
						}
					}
				}
			}
			if (includeAmmo)
			{
				for (int k = 54; k < 58; k++)
				{
					for (int l = 0; l < types.Length; l++)
					{
						if (!array2[l])
						{
							Item item2 = player.inventory[k];
							if (item2 != null && item2.type == types[l] && item2.stack >= counts[l])
							{
								array2[l] = true;
								array[l] = k;
							}
						}
					}
				}
			}
			for (int m = 0; m < 50; m++)
			{
				for (int n = 0; n < types.Length; n++)
				{
					if (!array2[n])
					{
						Item item3 = player.inventory[m];
						if (item3 != null && item3.type == types[n] && item3.stack >= counts[n])
						{
							array2[n] = true;
							array[n] = m;
						}
					}
				}
			}
			bool[] array3 = array2;
			for (int num = 0; num < array3.Length; num++)
			{
				if (!array3[num])
				{
					return false;
				}
			}
			return true;
		}

		public static bool HasItem(Player player, int type, int count = 1, bool includeAmmo = false, bool includeCoins = false)
		{
			int num = 0;
			return BasePlayer.HasItem(player, type, ref num, count, includeAmmo, includeCoins);
		}

		public static bool HasItem(Player player, int type, ref int index, int count = 1, bool includeAmmo = false, bool includeCoins = false)
		{
			if (includeCoins)
			{
				for (int i = 50; i < 54; i++)
				{
					Item item = player.inventory[i];
					if (item != null && item.type == type && item.stack >= count)
					{
						index = i;
						return true;
					}
				}
			}
			if (includeAmmo)
			{
				for (int j = 54; j < 58; j++)
				{
					Item item2 = player.inventory[j];
					if (item2 != null && item2.type == type && item2.stack >= count)
					{
						index = j;
						return true;
					}
				}
			}
			for (int k = 0; k < 50; k++)
			{
				Item item3 = player.inventory[k];
				if (item3 != null && item3.type == type && item3.stack >= count)
				{
					index = k;
					return true;
				}
			}
			index = -1;
			return false;
		}

		public static bool HasAmmo(Player player, int ammoType, ref int index, int count = 1, bool includeAmmo = false, bool includeCoins = false, bool ignoreConsumable = false)
		{
			if (includeCoins)
			{
				for (int i = 50; i < 54; i++)
				{
					Item item = player.inventory[i];
					if (item != null && item.ammo == ammoType && ((!ignoreConsumable && !item.consumable) || item.stack >= count))
					{
						index = i;
						return true;
					}
				}
			}
			if (includeAmmo)
			{
				for (int j = 54; j < 58; j++)
				{
					Item item2 = player.inventory[j];
					if (item2 != null && item2.ammo == ammoType && ((!ignoreConsumable && !item2.consumable) || item2.stack >= count))
					{
						index = j;
						return true;
					}
				}
			}
			for (int k = 0; k < 50; k++)
			{
				Item item3 = player.inventory[k];
				if (item3 != null && item3.ammo == ammoType && ((!ignoreConsumable && !item3.consumable) || item3.stack >= count))
				{
					index = k;
					return true;
				}
			}
			index = -1;
			return false;
		}

		public static bool IsInSet(string setName, params Item[] items)
		{
			return BasePlayer.IsInSet(null, setName, items);
		}

		public static bool IsInSet(Mod mod, string setName, params Item[] items)
		{
			int i = 0;
			while (i < items.Length)
			{
				Item item = items[i];
				bool result;
				if (item == null || item.IsBlank())
				{
					result = false;
				}
				else if (!item.Name.StartsWith(setName))
				{
					result = false;
				}
				else
				{
					if (mod == null || item.modItem == null || item.modItem.mod.Name.ToLower().Equals(mod.Name.ToLower()))
					{
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			return true;
		}

		public static bool HasArmorSet(Player player, string armorName, bool vanity = false)
		{
			return BasePlayer.HasArmorSet(null, player, armorName, vanity);
		}

		public static bool HasArmorSet(Mod mod, Player player, string armorName, bool vanity = false)
		{
			Item item = player.armor[vanity ? 10 : 0];
			Item item2 = player.armor[vanity ? 11 : 1];
			Item item3 = player.armor[vanity ? 12 : 2];
			return BasePlayer.IsInSet(mod, armorName, new Item[]
			{
				item,
				item2,
				item3
			});
		}

		public static bool IsVanitySlot(int slot, bool acc = true)
		{
			if (!acc)
			{
				return slot >= 10 && slot <= 12;
			}
			return slot >= 13 && slot <= 18;
		}

		public static bool HasAccessories(Player player, int[] types, bool normal, bool vanity, bool oneOf)
		{
			int num = 0;
			bool flag = false;
			return BasePlayer.HasAccessories(player, types, normal, vanity, oneOf, ref flag, ref num);
		}

		public static bool HasAccessories(Player player, int[] types, bool normal, bool vanity, bool oneOf, ref bool social, ref int index)
		{
			int num = 0;
			if (vanity)
			{
				for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
				{
					Item item = player.armor[i];
					if (item != null && !item.IsBlank())
					{
						foreach (int num2 in types)
						{
							if (item.type == num2)
							{
								index = i;
								social = true;
								if (oneOf)
								{
									return true;
								}
								num++;
							}
						}
					}
				}
			}
			if (normal)
			{
				for (int k = 3; k < 8 + player.extraAccessorySlots; k++)
				{
					Item item2 = player.armor[k];
					if (item2 != null && !item2.IsBlank())
					{
						foreach (int num3 in types)
						{
							if (item2.type == num3)
							{
								index = k;
								social = false;
								if (oneOf)
								{
									return true;
								}
								num++;
							}
						}
					}
				}
			}
			return num >= types.Length;
		}

		public static bool HasAccessory(Player player, int type, bool normal, bool vanity)
		{
			int num = 0;
			bool flag = false;
			return BasePlayer.HasAccessory(player, type, normal, vanity, ref flag, ref num);
		}

		public static bool HasAccessory(Player player, int type, bool normal, bool vanity, ref bool social, ref int index)
		{
			if (vanity)
			{
				for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
				{
					Item item = player.armor[i];
					if (item != null && !item.IsBlank() && item.type == type)
					{
						index = i;
						social = true;
						return true;
					}
				}
			}
			if (normal)
			{
				for (int j = 3; j < 8 + player.extraAccessorySlots; j++)
				{
					Item item2 = player.armor[j];
					if (item2 != null && !item2.IsBlank() && item2.type == type)
					{
						index = j;
						social = false;
						return true;
					}
				}
			}
			return false;
		}
	}
}
