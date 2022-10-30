using FootballPools.Controllers;
using FootballPools.Data;
using FootballPools.Data.WorldCup;
using FootballPools.Models.WorldCup;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FootballPoolsTest;

public class UnitTests
{
    [Fact]
    public void GetTournaments()
    {
        var services = Moqqer.GetQueryableMockDbSet(
            new Tournament { Id = 1, Name = "Torneo 1" },
            new Tournament { Id = 2, Name = "Torneo 2" },
            new Tournament { Id = 3, Name = "Torneo 3" }
        );

        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(mc => mc.Tournaments).Returns(services);
        var tournamentController = new TournamentController(mockContext.Object);

        Assert.IsType<OkObjectResult>(tournamentController.Get());
        var getTournamentsResponse = tournamentController.Get() as OkObjectResult;
        var tournaments = (List<Tournament>)getTournamentsResponse!.Value!;
        Assert.True(tournaments.Count == services.Count());
    }

    [Fact]
    public void GetTournamentById()
    {
        var services = Moqqer.GetQueryableMockDbSet(
            new Tournament { Id = 1, Name = "Torneo 1" },
            new Tournament { Id = 2, Name = "Torneo 2" }
        );

        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(mc => mc.Tournaments).Returns(services);
        var serviceController = new TournamentController(mockContext.Object);

        Assert.IsType<OkResult>(serviceController.Get(1));
    }

    [Fact]
    public void GetTournamentByIdNotFound()
    {
        var services = Moqqer.GetQueryableMockDbSet(
            new Tournament { Id = 1, Name = "Torneo 1" },
            new Tournament { Id = 2, Name = "Torneo 2" }
        );

        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(mc => mc.Tournaments).Returns(services);
        var serviceController = new TournamentController(mockContext.Object);

        Assert.IsType<NotFoundResult>(serviceController.Get(55));
    }

    [Fact]
    public void InsertTournamentWithoutName()
    {
        var services = Moqqer.GetQueryableMockDbSet(
            new Tournament { Id = 1, Name = "Torneo 1" },
            new Tournament { Id = 2, Name = "Torneo 2" }
        );

        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(mc => mc.Tournaments).Returns(services);
        var serviceController = new TournamentController(mockContext.Object);

        Assert.IsType<BadRequestResult>(serviceController.Post(new CreateTournament()));
    }

    [Fact]
    public void InsertTournamentCorrectly()
    {
        var services = Moqqer.GetQueryableMockDbSet(
            new Tournament { Id = 1, Name = "Torneo 1" },
            new Tournament { Id = 2, Name = "Torneo 2" }
        );

        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(mc => mc.Tournaments).Returns(services);
        var serviceController = new TournamentController(mockContext.Object);

        Assert.IsType<OkObjectResult>(serviceController.Post(new CreateTournament()
        {
            Name = "Torneo 3"
        }));
    }
}