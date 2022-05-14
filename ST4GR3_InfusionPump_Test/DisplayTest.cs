using System;
using IP_BusinessLogicLayer;
using IP_BusinessLogicLayer.Interfaces;
using IP_DataAccessLayer;
using NUnit.Framework;
using ST4GR3_InfusionPumpApplication;
using NSubstitute;
using ST4GR3_InfusionPumpApplication.Interfaces;

namespace ST4GR3_InfusionPump_Test
{
    public class Tests
    {
        private Display _uut;
        private IMenuController _menuController;
        //Menu list
        private IButton _startButton;
        private IButton _pauseButton;
        private IButton _stopButton;

        private string[] _menuOne;

        [SetUp]
        public void Setup()
        {
            _menuController = Substitute.For<IMenuController>();
            //_menuList = new MenuList();
            _startButton = Substitute.For<IButton>();
            _pauseButton = Substitute.For<IButton>();
            _stopButton = Substitute.For<IButton>();

            _uut = new Display(_menuController, _startButton, _pauseButton, _stopButton);
        }

        [Test]
        public void TestOfRunMethodDisplay()
        {
            _uut.Run();

            _menuController.Received().FindMenuArray(0);
        }

        [Test]
        public void TestOfRunMethod()
        {
            _menuOne = new String[] { "Hovedmenu:", "Prime", "", "Batteristatus:    %" };
            _uut.Run();

            Assert.That(_menuOne, Is.EqualTo(_menuController.FindMenuArray(0)));
            _startButton.IsPressed();
            //Meget whitebox.. Testen vil ikke køre...
        }
    }
}