using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace Redemption
{
	internal class Redemption : Mod
	{
		public Redemption()
		{
			ModProperties properties = default(ModProperties);
			properties.Autoload = true;
			properties.AutoloadGores = true;
			properties.AutoloadSounds = true;
			properties.AutoloadBackgrounds = true;
			base.Properties = properties;
		}

		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (Main.myPlayer != -1 && !Main.gameMenu && Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<RedePlayer>(this).ZoneXeno)
			{
				music = base.GetSoundSlot(51, "Sounds/Music/XenoCaves");
			}
		}

		public override void Load()
		{
			if (!Main.dedServ)
			{
				base.AddEquipTexture(null, 2, "ArchclothRobe_Legs", "Redemption/Items/Armor/ArchclothRobe_Legs", "", "");
				base.AddEquipTexture(null, 2, "HallamRobes_Legs", "Redemption/Items/Armor/HallamRobes_Legs", "", "");
			}
			Redemption.FaceCustomCurrencyID = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(base.ItemType<AncientGoldCoin>(), 999L));
		}

		public override void PostSetupContent()
		{
			Mod mod = ModLoader.GetMod("BossChecklist");
			if (mod != null)
			{
				Mod mod2 = mod;
				object[] array = new object[5];
				array[0] = "AddBossWithInfo";
				array[1] = "The Keeper";
				array[2] = 3.25f;
				array[3] = new Func<bool>(() => RedeWorld.downedTheKeeper);
				array[4] = string.Concat(new object[]
				{
					"Use a [i:",
					base.ItemType<MysteriousTabletCorrupt>(),
					"] or [i:",
					base.ItemType<MysteriousTabletCrimson>(),
					"] at night"
				});
				mod2.Call(array);
				Mod mod3 = mod;
				object[] array2 = new object[5];
				array2[0] = "AddBossWithInfo";
				array2[1] = "Xenomite Crystal";
				array2[2] = 3.5f;
				array2[3] = new Func<bool>(() => RedeWorld.downedXenomiteCrystal);
				array2[4] = string.Concat(new object[]
				{
					"Kill a Strange Portal by using an [i:",
					base.ItemType<UnstableCrystal>(),
					"], then use a [i:",
					base.ItemType<GeigerCounter>(),
					"]"
				});
				mod3.Call(array2);
				Mod mod4 = mod;
				object[] array3 = new object[5];
				array3[0] = "AddBossWithInfo";
				array3[1] = "Infected Eye";
				array3[2] = 6.25f;
				array3[3] = new Func<bool>(() => RedeWorld.downedInfectedEye);
				array3[4] = "Use a [i:" + base.ItemType<XenoEye>() + "] at night";
				mod4.Call(array3);
			}
		}

		public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
		{
			if (RedeWorld.xenoBiome > 0)
			{
				float num = (float)RedeWorld.xenoBiome / 200f;
				num = Math.Min(num, 1f);
				int num2 = (int)backgroundColor.R;
				int num3 = (int)backgroundColor.G;
				int num4 = (int)backgroundColor.B;
				num2 -= (int)(200f * num * ((float)backgroundColor.R / 255f));
				num4 -= (int)(200f * num * ((float)backgroundColor.B / 255f));
				num3 -= (int)(170f * num * ((float)backgroundColor.G / 255f));
				num2 = Utils.Clamp<int>(num2, 15, 255);
				num3 = Utils.Clamp<int>(num3, 15, 255);
				num4 = Utils.Clamp<int>(num4, 15, 255);
				backgroundColor.R = (byte)num2;
				backgroundColor.G = (byte)num3;
				backgroundColor.B = (byte)num4;
			}
		}

		public static int FaceCustomCurrencyID;
	}
}
