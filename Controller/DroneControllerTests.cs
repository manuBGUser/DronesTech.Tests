using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DronesTech.Controllers;
using DronesTech.DTO;
using DronesTech.Interfaces;
using FakeItEasy;
using FluentAssertions;

namespace DronesTech.Tests.Controller
{
    public class DroneControllerTests
    {
        public readonly IDroneRepository _droneRepository;
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;

        public DroneControllerTests()
        {
            _droneRepository = A.Fake<IDroneRepository>();
            _mapper = A.Fake<IMapper>();
            _medicineRepository = A.Fake<IMedicineRepository>();
        }

        [Fact]
        public void DroneController_GetDrones_ReturnOk()
        {
            //Arrange
            var drones = A.Fake<ICollection<DroneDTO>>();
            var dronesList = A.Fake<List<DroneDTO>>();
            A.CallTo(() => _mapper.Map<List<DroneDTO>>(drones)).Returns(dronesList);
            var droneController = new DroneController(_droneRepository, _mapper, _medicineRepository);

            //Act
            var result = droneController.GetDrones();

            //Assert
            result.Should().NotBeNull();
        }
    }
}
