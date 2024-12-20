using System;
using Microsoft.Xna.Framework;

namespace Redemption.StructureHelper.ChestHelper.GUI
{
	internal class PoolChanceRuleElement : ChestRuleElement
	{
		public PoolChanceRuleElement() : base(new ChestRulePoolChance())
		{
			this.color = new Color(50, 50, 200);
			base.Append(this.chanceSetter);
			base.Append(this.countSetter);
		}

		public PoolChanceRuleElement(ChestRule rule) : base(rule)
		{
			this.color = new Color(50, 50, 200);
			this.chanceSetter.Value = (int)Math.Round((double)((rule as ChestRulePoolChance).chance * 100f));
			this.countSetter.Value = (rule as ChestRulePoolChance).itemsToGenerate;
			base.Append(this.chanceSetter);
			base.Append(this.countSetter);
		}

		public override void Update(GameTime gameTime)
		{
			if (this.countSetter.Value > this.rule.pool.Count)
			{
				this.countSetter.Value = this.rule.pool.Count;
			}
			if (this.chanceSetter.Value > 100)
			{
				this.chanceSetter.Value = 100;
			}
			(this.rule as ChestRulePoolChance).itemsToGenerate = this.countSetter.Value;
			(this.rule as ChestRulePoolChance).chance = (float)this.chanceSetter.Value / 100f;
			base.Update(gameTime);
		}

		private NumberSetter chanceSetter = new NumberSetter(100, "Chance", 100, "%");

		private NumberSetter countSetter = new NumberSetter(1, "Amount to Pick", 140, "");
	}
}
