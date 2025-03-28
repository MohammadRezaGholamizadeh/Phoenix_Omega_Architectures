using FluentAssertions;
using ServiceLayer.Services.ColorService.Contracts;
using ServiceLayer.Services.ColorService.Contracts.Dtos;
using System.Linq;
using System.Threading.Tasks;
using TestTools.AutoServiceConfigurationImplementation;
using Xunit;

namespace UnitServiceTestsLayer.ColorsAppServiceTests
{
    public class ColorAppServiceTest : UnitSut<IColorService>
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
