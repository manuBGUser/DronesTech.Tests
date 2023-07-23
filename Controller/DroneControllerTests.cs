using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DronesTech.Controllers;
using DronesTech.DTO;
using DronesTech.Interfaces;
using DronesTech.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

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
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void DroneController_CreateDrone_ReturnOk()
        {
            //Arrange
            var drone = A.Fake<Drone>();
            var droneMap = A.Fake<DroneDTO>();
            var droneCreate = A.Fake<DroneDTO>();
            var drones = A.Fake<ICollection<DroneDTO>>();
            var dronesList = A.Fake<List<DroneDTO>>();
            A.CallTo(() => _droneRepository.GetDrones().Where(d => d.SerieNumber == droneCreate.SerieNumber).FirstOrDefault()).Returns(drone);
            A.CallTo(() => _mapper.Map<Drone>(droneCreate)).Returns(drone);
            A.CallTo(() => _droneRepository.CreateDrone(drone)).Returns(true);
            var droneController = new DroneController(_droneRepository, _mapper, _medicineRepository);

            //Act
            var result = droneController.CreateDrone(droneMap);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
