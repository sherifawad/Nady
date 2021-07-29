using Core.Interfaces;
using Core.Models;
using FluentAssertions;
using Infrastructure.Dtos;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nady.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NadyApiTest
{
    public class MemberControllerTest
    {
        private readonly Mock<IMemberService> memberServiceStub = new Mock<IMemberService>();
        private readonly Random rand = new Random();

        [Fact]
        public async Task GetMemberById_WithUnExistingMember_ReturnNotFound()
        {
            // Arrange
            memberServiceStub.Setup(service => service.GetMemberAsync(It.IsAny<string>()))
                .ReturnsAsync((Member)null);

            var controller = new MembersController(memberServiceStub.Object);

            // Act
            var result = await controller.GetMemberById("Test");

            //Assert
            //Assert.IsType<NotFoundObjectResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetMemberById_WithExistingMember_ReturnExpectedMember()
        {
            // Arrange

            var expectedMember = CreateRandomMember();

            memberServiceStub.Setup(service => service.GetMemberAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedMember);

            var controller = new MembersController(memberServiceStub.Object);

            // Act
            var result = await controller.GetMemberById("Test");

            //Assert
            result.Value.FromDto().Should().BeEquivalentTo(expectedMember,
                option => option.ComparingByMembers<Member>());
            //Assert.IsType<Member>(result.Value);
            //var member = (result as ActionResult<Member>).Value;
            //Assert.Equal(expectedMember.Id, member.Id);
            //Assert.Equal(expectedMember.Name, member.Name);
            //Assert.Equal(expectedMember.Code, member.Code);
            //Assert.Equal(expectedMember.IsOwner, member.IsOwner);
            //Assert.Equal(expectedMember.MemberStatus, member.MemberStatus);
            //Assert.Equal(expectedMember.MemberDetails, member.MemberDetails);
            //Assert.Equal(expectedMember.MemberDetails.NickName, member.MemberDetails.NickName);
        }

        [Fact]
        public async Task GetAllMembers_WithExistingMembers_ReturnAllMember()
        {
            // Arrange

            var expectedMembers = new[] { CreateRandomMember(), CreateRandomMember(), CreateRandomMember() };

            memberServiceStub.Setup(service => service.GetMembersAsync(null, null))
                .ReturnsAsync(expectedMembers);

            var controller = new MembersController(memberServiceStub.Object);

            // Act
            var result = await controller.GetMembers();

            //Assert
            result.Select(x => x.FromDto()).Should().BeEquivalentTo(expectedMembers,
                option => option.ComparingByMembers<Member>());
        }
        
        [Fact]
        public async Task GetMembers_WithMatchingMembers_ReturnMatchingMember()
        {
            // Arrange

            string nameToMatch = "mohammed";

            var allembers = new[] 
            {
                new Member
                {
                    Name = "Ahmed"
                },
                new Member
                {
                    Name = "Mohammed"
                },
                new Member
                {
                    Name = "Sherif Mohammed"
                }
            };

            var expectedembers = new[] 
            {
                new Member
                {
                    Name = "Mohammed"
                },
                new Member
                {
                    Name = "Sherif Mohammed"
                }
            };

            memberServiceStub.Setup(service => service.GetMembersAsync(nameToMatch, null))
                .ReturnsAsync(expectedembers);

            var controller = new MembersController(memberServiceStub.Object);

            // Act


            var result = await controller.GetMembers(nameToMatch);
            //Assert
            result.Should().OnlyContain(
                member => member.Name == allembers[1].Name || member.Name == allembers[2].Name
                );
        }

        [Fact]
        public async Task CreateMember_WithMemberToCreate_ReturnsCreatedMember()
        {
            // Arrange

            var memberToCreateDto = CreateRandomMember().AsDto();


            memberServiceStub.Setup(service => service.CreateMemberAsync(memberToCreateDto.FromDto()))
                .ReturnsAsync(memberToCreateDto.FromDto());

            var controller = new MembersController(memberServiceStub.Object);

            // Act
            var result = await controller.CreateMember(memberToCreateDto);

            //Assert
            var createdMember = (result.Result as CreatedAtActionResult).Value as MemberDto;

            memberToCreateDto.Should().BeEquivalentTo(createdMember,
                option => option.ComparingByMembers<MemberDto>().ExcludingMissingMembers());

            createdMember.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpadateMember_WithExistingMember_ReturnsNoContent()
        {
            // Arrange

            var expectedMember = CreateRandomMember();

            memberServiceStub.Setup(service => service.GetMemberAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedMember);

            var controller = new MembersController(memberServiceStub.Object);

            var itemToUpdate = new Member
            {
                Name = Guid.NewGuid().ToString(),
                MemberDetails = new MemberDetails { NickName = "Eng." },
                MemberStatus = Core.Models.Enum.MemberStatus.OnHold,
                RelationShip = null

            };


            memberServiceStub.Setup(service => service.UpdateMemberAsync(itemToUpdate))
                .ReturnsAsync(itemToUpdate);

            // Act
            var result = await controller.UpadateMember(expectedMember.Id, itemToUpdate.AsDto());

            //Assert

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteMember_WithExistingMember_ReturnsNoContent()
        {
            // Arrange

            var existingMember = CreateRandomMember();

            memberServiceStub.Setup(service => service.GetMemberAsync(It.IsAny<string>()))
                .ReturnsAsync(existingMember);

            memberServiceStub.Setup(service => service.DeleteMemberAsync(existingMember.Id))
    .ReturnsAsync(true);

            var controller = new MembersController(memberServiceStub.Object);



            // Act
            var result = await controller.DeleteMember(existingMember.Id);

            //Assert

            result.Should().BeOfType<NoContentResult>();
        }

        private Member CreateRandomMember()
        {
            return new Member
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Code = $"{rand.Next(9999)}/{rand.Next(1970,2021)}",
                IsOwner = true,
                MemberDetails = new MemberDetails { NickName = "Mr."},
                MemberStatus = Core.Models.Enum.MemberStatus.Active,
                RelationShip = null
            };
        }
    }
}
