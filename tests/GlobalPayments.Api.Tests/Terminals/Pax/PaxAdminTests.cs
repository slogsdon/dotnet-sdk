﻿using GlobalPayments.Api.Entities;
using GlobalPayments.Api.Services;
using GlobalPayments.Api.Terminals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalPayments.Api.Tests.Terminals.Pax {
    [TestClass]
    public class PaxAdminTests {
        IDeviceInterface _device;

        public PaxAdminTests() {
            _device = DeviceService.Create(new ConnectionConfig {
                DeviceType = DeviceType.PAX_PX7,
                ConnectionMode = ConnectionModes.TCP_IP,
                IpAddress = "10.12.220.172",
                //IpAddress = "192.168.0.31",
                Port = "10009",
                RequestIdProvider = new RandomIdProvider()
            });
            Assert.IsNotNull(_device);
        }

        [TestMethod]
        public void Initialize() {
            _device.OnMessageSent += (message) => {
                Assert.IsNotNull(message);
                Assert.IsTrue(message.StartsWith("[STX]A00[FS]1.35[FS][ETX]"));
            };

            var response = _device.Initialize();
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.DeviceResponseText);
            Assert.IsNotNull(response.SerialNumber);
        }

        [TestMethod]
        public void Cancel() {
            _device.OnMessageSent += (message) => {
                Assert.IsNotNull(message);
                Assert.AreEqual("[STX]A14[FS]1.35[FS][ETX]^", message);
            };

            _device.Cancel();
        }

        [TestMethod]
        public void Reset() {
            _device.OnMessageSent += (message) => {
                Assert.IsNotNull(message);
                Assert.IsTrue(message.StartsWith("[STX]A16[FS]1.35[FS][ETX]"));
            };

            var response = _device.Reset();
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.DeviceResponseText);
        }

        [TestMethod, Ignore]
        public void Reboot() {
            _device.OnMessageSent += (message) => {
                Assert.IsNotNull(message);
                Assert.AreEqual("[STX]A26[FS]1.31[FS][ETX][", message);
            };

            var response = _device.Reboot();
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.DeviceResponseText);
        }

        [TestMethod]
        public void GetSignature() {
            _device.OnMessageSent += (message) => {
                Assert.IsNotNull(message);
                Assert.IsTrue(message.StartsWith("[STX]A08[FS]1.35[FS]0[FS][ETX]"));
            };

            var response = _device.GetSignatureFile();
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.DeviceResponseText);
        }

        [TestMethod]
        public void PromptForSignature() {
            _device.OnMessageSent += (message) => {
                Assert.IsNotNull(message);
                Assert.IsTrue(message.StartsWith("[STX]A20"));
            };

            var response = _device.PromptForSignature();
            Assert.IsNotNull(response);
            Assert.AreEqual("OK", response.DeviceResponseText);
        }

        [TestMethod]
        public void TurnOffBeeps() {
            var response = _device.DisableHostResponseBeep();
            Assert.IsNotNull(response);
        }
    }
}
