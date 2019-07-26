﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthDb.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthDb.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class Chuck
    {
        public Card Card { get; set; }
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public string ConcatEnglishName { get; set; }

        public override string ToString()
        {
            return $@"
    /// <summary>
    /// {Card.Id}
    /// {EnglishName}
    /// {ChineseName}
    /// {Card.Set}
    /// {Card.Class}
    /// {Card.Type}
    /// Collectible = {Card.Collectible}
    /// {Card.Text}
    /// </summary>
    {ConcatEnglishName},
";
        }
    }

    [TestClass]
    public class ChuckTest
    {
        [TestMethod]
        public void Test1()
        {
            Dictionary<string, Card> dic = Cards.All;
            List<Chuck> list = new List<Chuck>();
            StringBuilder builder = new StringBuilder();
            foreach (Card item in dic.Values)
            {
                var enName = item.GetLocName(Locale.enUS);
                if (!string.IsNullOrEmpty(enName))
                {
                    var tempName = enName.Replace(" ", string.Empty).ToLower();
                    Chuck chuck = new Chuck();
                    chuck.Card = item;
                    chuck.ChineseName = item.GetLocName(Locale.zhCN);
                    chuck.EnglishName = item.GetLocName(Locale.enUS);
                    chuck.ConcatEnglishName = tempName;
                    list.Add(chuck);
                }
            }

            var tempList = list.OrderBy(x => x.ConcatEnglishName);
            foreach (var item in tempList)
            {
                builder.AppendLine($"{item}");
            }
            Console.WriteLine(builder);

        }
    }
}
