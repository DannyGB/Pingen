using NUnit.Framework;
using FluentAssertions;
using pin_number_gen.application;
using Moq;
using pin_number_gen.infrastructure;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace pin_number_gen.tests
{
    public class PinGeneratorServiceTests
    {
        private Mock<IPinRepository> _repo;
        private System.Range _range = new System.Range(0, 10_000);
        
        [SetUp]
        public void Setup()
        {
            _repo = new Mock<IPinRepository>();
        }

        [Test]
        public async Task Generator_Should_Return_Valid_Pin()
        {
            var expected = "0001";
            _repo.Setup(x => x.ReadNext()).ReturnsAsync(expected);
            _repo.Setup(x => x.Initialise(_range, It.IsAny<List<string>>()));

            var sut = new PinGeneratorService(_repo.Object);
            var result = await sut.GeneratePin(new GeneratePinRequest());

            result.Should().NotBeNull();
            result.Should().NotBeNull();
            result.Pin.Should().Be(expected);

            _repo.Verify(x => x.ReadNext(), Times.Once);
            _repo.Verify(x => x.Initialise(_range, It.IsAny<List<string>>()), Times.Once);
        }
    }
}