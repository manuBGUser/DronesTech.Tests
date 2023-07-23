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
using DronesTech.Models.Types;
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

        public void DroneController_GetAbledDrones_ReturnOk()
        {
            //Arrange
            var drones = A.Fake<ICollection<DroneDTO>>();
            var dronesList = A.Fake<List<DroneDTO>>();
            A.CallTo(() => _mapper.Map<List<DroneDTO>>(drones)).Returns(dronesList);
            var droneController = new DroneController(_droneRepository, _mapper, _medicineRepository);

            //Act
            var result = droneController.GetAbledDrones();

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

            A.CallTo(() => _droneRepository.GetDroneBySerieNumber(droneCreate.SerieNumber)).Returns(drone);
            A.CallTo(() => _mapper.Map<Drone>(droneCreate)).Returns(drone);
            drone.SerieNumber = "98656";
            drone.BatteryCapacity = 100;
            drone.Model = ModelType.HeavyWeight;
            drone.Status = StatusType.Inactive;
            drone.WeightLimit = 100;
            A.CallTo(() => _droneRepository.CreateDrone(drone)).Returns(true);
            var droneController = new DroneController(_droneRepository, _mapper, _medicineRepository);

            //Act
            droneMap.SerieNumber = "98656";
            droneMap.BatteryCapacity = 100;
            droneMap.Model = 4;
            droneMap.Status = 1;
            droneMap.WeightLimit = 100;
            var result = droneController.CreateDrone(droneMap);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void DroneController_GetDroneBattery_ReturnOk()
        {
            //Arrange
            var droneId = 1;
            var drone = A.Fake<Drone>();
            A.CallTo(() => _droneRepository.DroneExists(droneId)).Returns(true);
            A.CallTo(() => _droneRepository.GetDroneById(droneId)).Returns(drone);
            var droneController = new DroneController(_droneRepository, _mapper, _medicineRepository);

            //Act
            var result = droneController.GetDroneBattery(droneId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void DroneController_ChargeMedicineToDrone_ReturnOk()
        {
            //Arrange
            var droneId = 1;
            var drone = A.Fake<Drone>();
            var medicines = A.Fake<ICollection<Medicine>>();
            A.CallTo(() => _droneRepository.DroneExists(droneId)).Returns(true);
            A.CallTo(() => _droneRepository.IsDroneEmpty(droneId)).Returns(true);
            A.CallTo(() => _droneRepository.GetDroneById(droneId)).Returns(drone);
            A.CallTo(() => _droneRepository.GetAbledDrones().Contains(drone)).Returns(true);
            A.CallTo(() => _medicineRepository.GetMedicinesToCharge(drone.WeightLimit)).Returns(medicines);
            A.CallTo(() => _droneRepository.ChargeMedicines(drone, medicines)).Returns(drone);
            var droneController = new DroneController(_droneRepository, _mapper, _medicineRepository);

            //Act
            var result = droneController.ChargeMedicineToDrone(droneId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void DroneController_CheckChargedMedsWeight_ReturnOk()
        {
            //Arrange
            var droneId = 1;
            var drone = A.Fake<Drone>();
            A.CallTo(() => _droneRepository.DroneExists(droneId)).Returns(true);
            A.CallTo(() => _droneRepository.GetDroneById(droneId)).Returns(drone);
            var droneController = new DroneController(_droneRepository, _mapper, _medicineRepository);

            //Act
            var result = droneController.CheckChargedMedsWeight(droneId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }
    }
}
