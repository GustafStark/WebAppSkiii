using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppSkiii.Logic.Interface;
using WebAppSkiii.Model;
using WebAppSkiii.Repository.Interface;
using WebAppSkiii.Service;

namespace WebAppSkiiTests.Service
{
    [TestClass]
    public class SkiServiceTests
    {
        private SkiService skiService;
        private Mock<ISkiRepository> skiRepo;
        private Mock<ISkiSizeLogic> skiLogic;
        [TestInitialize]
        public void SetUp()
        {
            skiRepo = new Mock<ISkiRepository>();
            skiLogic = new Mock<ISkiSizeLogic>();
            skiService = new SkiService(skiRepo.Object, skiLogic.Object);
        }

        [TestMethod()]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public async Task Size_For_UnderFour_Expect_OK(int age)
        {
            //ARRAGE
            int length = 180;
            Style style = Style.Classic;
            skiRepo.Setup(x => x.GetItemAsync(length, style)).ReturnsAsync(new Ski { Length = 180, Style = style });

            //ACT
            var ski = await skiService.GetSki(length, age, Style.Classic);

            //ASSERT
            Assert.AreEqual(length, ski.Length);
            Assert.AreEqual(Style.Classic, ski.Style);
        }

        [TestMethod]
        [DataRow(5, 200)]
        [DataRow(6, 200)]
        [DataRow(7, 210)]
        [DataRow(8, 210)]
        public async Task Size_For_Between_Five_and_Eight_Expect_OK(int age, int expecterdLength)
        {
            //ARRAGE
            int length = 190;
            Style style = Style.Classic;
            skiLogic.Setup(x => x.SizeUnderEight(length, age)).Returns(length);
            skiRepo.Setup(x => x.GetItemAsync(length, style)).ReturnsAsync(new Ski { Length = expecterdLength, Style = style });

            //ACT
            var ski = await skiService.GetSki(length, age, Style.Classic);

            //ASSERT
            Assert.AreEqual(expecterdLength, ski.Length);
            Assert.AreEqual(Style.Classic, ski.Style);
        }
        [TestMethod]
        public async Task Size_For_Classic_Expect_OK()
        {
            //ARRAGE
            int length = 180;
            int age = 10;
            Style style = Style.Classic;

            skiLogic.Setup(x => x.SizeClassic(length)).Returns(180);
            skiRepo.Setup(x => x.GetItemAsync(length, style)).ReturnsAsync(new Ski { Length = length + 20, Style = style });

            //ACT
            var ski = await skiService.GetSki(length, age, Style.Classic);

            //ASSERT
            Assert.AreEqual(length + 20, ski.Length);
            Assert.AreEqual(Style.Classic, ski.Style);
        }
        [TestMethod]
        public async Task Size_For_Classic_MaxLength_Expect_OK()
        {
            //ARRAGE
            int length = 190;
            int age = 10;
            Style style = Style.Classic;

            skiLogic.Setup(x => x.SizeClassic(length)).Returns(length);
            skiRepo.Setup(x => x.GetItemAsync(length, style)).ReturnsAsync(new Ski { Length = 207, Style = style });

            //ACT
            var ski = await skiService.GetSki(length, age, Style.Classic);

            //ASSERT
            Assert.AreEqual(207, ski.Length);
            Assert.AreEqual(Style.Classic, ski.Style);
        }
        [TestMethod]
        public async Task Size_Freestyle_Expect_OK()
        {
            //ARRAGE
            int length = 180;
            int age = 10;
            Style style = Style.Freestyle;

            skiLogic.Setup(x => x.SizeFreeStyle(length)).Returns(length);
            skiRepo.Setup(x => x.GetItemAsync(length, style)).ReturnsAsync(new Ski { Length = length+15, Style = style });

            //ACT
            var ski = await skiService.GetSki(length, age, Style.Freestyle);

            //ASSERT
            Assert.AreEqual(length + 15, ski.Length);
            Assert.AreEqual(Style.Freestyle, ski.Style);
        }
    }

}
