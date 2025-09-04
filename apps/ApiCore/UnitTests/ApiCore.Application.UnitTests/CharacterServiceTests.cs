using ApiCore.Clients.Contracts;
using ApiCore.Contracts;
using ApiCore.Models;
using Moq;

namespace ApiCore.Application.UnitTests;

public class CharacterServiceTests {
	private readonly Mock<IStarWarsApiClient> _mockStarWarsApiClient;
	private readonly Mock<IUserService> _mockUserService;
	private readonly CharacterService _characterService;

	public CharacterServiceTests() {
		_mockStarWarsApiClient = new Mock<IStarWarsApiClient>();
		_mockUserService = new Mock<IUserService>();
		_characterService = new CharacterService(_mockStarWarsApiClient.Object, _mockUserService.Object);
	}

	[Fact]
	public void Constructor_NullStarWarsApiClient_ArgumentExceptionThrown() {
		// arrange act assert
		Assert.Throws<ArgumentException>(() =>
			new CharacterService(null!, _mockUserService.Object));
	}

	[Fact]
	public void Constructor_NullUserService_ArgumentExceptionThrown() {
		// arrange act assert
		Assert.Throws<ArgumentException>(() =>
			new CharacterService(_mockStarWarsApiClient.Object, null!));
	}

	[Fact]
	public void Constructor_ValidArguments_CreatedCorrectly() {
		// arrange act assert
		Assert.NotNull(_characterService);
	}

	[Fact]
	public async Task GetCharacterList_WithValidPage_ReturnsPagedResponse() {
		// arrange
		var page = 1;
		var mockCharacters = CreateMockCharacters(25);
		_mockStarWarsApiClient.Setup(x => x.GetCharacterList())
			.ReturnsAsync(mockCharacters);

		// act
		var result = await _characterService.GetCharacterList(page);

		// assert
		Assert.NotNull(result);
		Assert.Equal(page, result.Page);
		Assert.Equal(10, result.PageSize);
		Assert.Equal(25, result.TotalCount);
		Assert.Equal(3, result.TotalPages);
		Assert.True(result.HasNextPage);
		Assert.False(result.HasPreviousPage);
		Assert.Equal(10, result.Items.Count());
		_mockStarWarsApiClient.Verify(x => x.GetCharacterList(), Times.Once);
	}

	[Fact]
	public async Task GetCharacterList_WithLastPage_ReturnsRemainingItems() {
		// arrange
		var page = 3;
		var mockCharacters = CreateMockCharacters(25);
		_mockStarWarsApiClient.Setup(x => x.GetCharacterList())
			.ReturnsAsync(mockCharacters);

		// act
		var result = await _characterService.GetCharacterList(page);

		// assert
		Assert.NotNull(result);
		Assert.Equal(page, result.Page);
		Assert.Equal(5, result.Items.Count());
		Assert.False(result.HasNextPage);
		Assert.True(result.HasPreviousPage);

		var firstItem = result.Items.First();
		Assert.Equal("Character 21", firstItem.Name);
		_mockStarWarsApiClient.Verify(x => x.GetCharacterList(), Times.Once);
	}

	[Fact]
	public async Task GetCharacterList_WithEmptyList_ReturnsEmptyPagedResponse() {
		// arrange
		var page = 1;
		var emptyCharacters = new List<Character>();
		_mockStarWarsApiClient.Setup(x => x.GetCharacterList())
			.ReturnsAsync(emptyCharacters);

		// act
		var result = await _characterService.GetCharacterList(page);

		// assert
		Assert.NotNull(result);
		Assert.Equal(page, result.Page);
		Assert.Equal(0, result.TotalCount);
		Assert.Equal(0, result.TotalPages);
		Assert.False(result.HasNextPage);
		Assert.False(result.HasPreviousPage);
		Assert.Empty(result.Items);
		_mockStarWarsApiClient.Verify(x => x.GetCharacterList(), Times.Once);
	}

	[Fact]
	public async Task GetCharacterList_WithExactPageSize_ReturnsCorrectPagination() {
		// arrange
		var page = 1;
		var mockCharacters = CreateMockCharacters(10); // Exactly 10 characters
		_mockStarWarsApiClient.Setup(x => x.GetCharacterList())
			.ReturnsAsync(mockCharacters);

		// act
		var result = await _characterService.GetCharacterList(page);

		// assert
		Assert.NotNull(result);
		Assert.Equal(page, result.Page);
		Assert.Equal(10, result.TotalCount);
		Assert.Equal(1, result.TotalPages);
		Assert.False(result.HasNextPage);
		Assert.False(result.HasPreviousPage);
		Assert.Equal(10, result.Items.Count());
		_mockStarWarsApiClient.Verify(x => x.GetCharacterList(), Times.Once);
	}

	[Fact]
	public async Task GetCharacter_WithValidId_ReturnsCharacter() {
		// arrange
		var characterId = 1;
		var expectedCharacter = new Character {
			Id = "1",
			Name = "Luke Skywalker",
			Height = "172",
			Mass = "77",
			HairColor = "blond",
			SkinColor = "fair",
			EyeColor = "blue",
			BirthYear = "19BBY",
			Gender = "male",
			Homeworld = "https://swapi.info/api/planets/1",
			Url = "https://swapi.info/api/people/1"
		};

		_mockStarWarsApiClient.Setup(x => x.GetCharacter(characterId))
			.ReturnsAsync(expectedCharacter);

		// act
		var result = await _characterService.GetCharacter(characterId);

		// assert
		Assert.NotNull(result);
		Assert.Equal(expectedCharacter.Name, result.Name);
		Assert.Equal(expectedCharacter.Height, result.Height);
		Assert.Equal(expectedCharacter.Mass, result.Mass);
		Assert.Equal(expectedCharacter.Url, result.Url);
		_mockStarWarsApiClient.Verify(x => x.GetCharacter(characterId), Times.Once);
	}

	[Fact]
	public async Task GetCharacter_WithInvalidId_ThrowsException() {
		// arrange
		var characterId = 999;
		_mockStarWarsApiClient.Setup(x => x.GetCharacter(characterId))
			.ThrowsAsync(new ArgumentException("Character not found"));

		// act
		await Assert.ThrowsAsync<ArgumentException>(() => _characterService.GetCharacter(characterId));
		_mockStarWarsApiClient.Verify(x => x.GetCharacter(characterId), Times.Once);
	}

	[Fact]
	public async Task GetCharacterList_WithUserId_MarksFavoritesAndSorts() {
		// arrange
		var page = 1;
		var userId = "test-user-123";
		var mockCharacters = CreateMockCharacters(5);
		var user = new User {
			Id = userId,
			FavoriteCharacters = [mockCharacters[2].Id, mockCharacters[4].Id]
		};

		_mockStarWarsApiClient.Setup(x => x.GetCharacterList())
			.ReturnsAsync(mockCharacters);
		_mockUserService.Setup(x => x.GetUserAsync(userId))
			.ReturnsAsync(user);

		// act
		var result = await _characterService.GetCharacterList(page, userId);

		// assert
		Assert.NotNull(result);
		Assert.Equal(5, result.Items.Count());

		var items = result.Items.ToList();
		Assert.True(items[0].IsFavorite);
		Assert.True(items[1].IsFavorite);
		Assert.False(items[2].IsFavorite);
		Assert.False(items[3].IsFavorite);
		Assert.False(items[4].IsFavorite);

		_mockStarWarsApiClient.Verify(x => x.GetCharacterList(), Times.Once);
		_mockUserService.Verify(x => x.GetUserAsync(userId), Times.Once);
	}


	private static List<Character> CreateMockCharacters(int count) {
		var characters = new List<Character>();
		for (int i = 1; i <= count; i++) {
			characters.Add(new Character {
				Id = i.ToString(),
				Name = $"Character {i}",
				Height = $"{150 + i}",
				Mass = $"{50 + i}",
				HairColor = "brown",
				SkinColor = "light",
				EyeColor = "blue",
				BirthYear = "19BBY",
				Gender = "male",
				Homeworld = $"https://swapi.info/api/planets/{i}",
				Url = $"https://swapi.info/api/people/{i}"
			});
		}
		return characters;
	}
}
