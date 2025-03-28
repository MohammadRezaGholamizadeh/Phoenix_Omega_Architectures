using FluentAssertions;
using ServiceLayer.Services.ColorService.Contracts;
using ServiceLayer.Services.ColorService.Contracts.Dtos;
using TestTools.AutoServiceConfigurationImplementation;
using Xunit;

namespace IntegrationServiceTestsLayer.ColorsAppServiceTests
{
    public class ColorAppServiceTest : IntegrationSut<IColorService>
    {
        [Fact]
        public async Task Add_Color_Properly()
        {
            var addColorDto = new AddColorDto()
            {
                Title = "dummy_Title",
                ColorHex = "dummy_HexColor"
            };

            await Sut.Add(addColorDto);

            var expected = Context.Colors.ToList();
            expected.Should().HaveCount(1);
        }
    }
}
