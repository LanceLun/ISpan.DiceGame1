﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ISpan.DiceGame.Dice;

namespace ISpan.DiceGame
{
	public class YcdDiceGame : BaseDiceGame
	{
		private readonly IRandomProvider _porvider = null;
		private const int diceCount = 4;
		private int minPoint = -1;
		public YcdDiceGame() : base("香腸遊戲") { }
        public YcdDiceGame(IRandomProvider provider): base("香腸遊戲")
		{
			this._porvider = provider;
        }

		/// <summary>
		/// 傳回最小一對骰子
		/// </summary>
		/// <returns>若沒有一對, 傳回空陣列</returns>
		private Dice[] ComputeSmallestPair(out int minPoint)
		{
			minPoint = 0;

			Dice[] dices = this._dices.ToArray();
			Array.Sort(dices);
			if (dices[0].Value == dices[1].Value ||
				dices[1].Value == dices[2].Value ||
				dices[2].Value == dices[3].Value)
			{
				minPoint = GetPointOfSmallestPair(dices);
				return new Dice[] { dices[0], dices[1] };
			}
			else
			{
				return Array.Empty<Dice>();
			}
		}
		private int GetPointOfSmallestPair(Dice[] sortedDices)
		{
			if (sortedDices[0].Value == sortedDices[1].Value)
				return sortedDices[0].Value;
			else if (sortedDices[1].Value == sortedDices[2].Value)
				return sortedDices[1].Value;
			else
				return sortedDices[2].Value;

		}

		/// <summary>
		/// 扣除最小一對骰子之後的點數
		/// </summary>
		/// <returns></returns>
		public override int ComputePoints()
		{
			return _dices.Sum(dice => dice.Value) - minPoint * 2;
		}

		public override string GetInformation()
		{
			return (_dices.Count != diceCount)
			? $"錯誤: Count of Dices= {_dices.Count}"
			: $"骰子: {string.Join(",", _dices.Select(d => d.ToString()))}\t\t點數:{ComputePoints(),2}";
			//? $"錯誤: Count of Dices= {_dices.Count}"
			//: $"骰子: {string.Join(",", _dices.Select(d => d.Value))}\t\t點數:{ComputePoints(),2}";
		}

		/// <summary>
		/// 隨機建立四顆骰子,若四顆全不同,自動重新產生
		/// </summary>
		public override void Play()
		{
			do
			{
				this.EmptyDices();

				for (int i = 0; i < diceCount; i++)
				{
					_dices.Add(Dice.Roll(_porvider));
				}
			} while (ComputeSmallestPair(out this.minPoint).Length != 2);
		}
	}
}
