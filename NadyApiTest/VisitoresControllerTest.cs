using Core.Interfaces;
using Core.Models;
using Core.Models.Enum;
using FluentAssertions;
using Moq;
using Nady.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NadyApiTest
{
    public class VisitoresControllerTest
    {
        private readonly Mock<IVisitorService> visitorServiceStub = new Mock<IVisitorService>();
        private readonly Random rand = new Random();

        [Fact]
        public async Task GetVisitores_WithExistingVisitores_ReturnExpectedVisitors()
        {
            // Arrange

            var expectedVisitors = new[] { CreateRandomVisitor(), CreateRandomVisitor(), CreateRandomVisitor() };

            visitorServiceStub.Setup(service => service.GetVisitorsAsync(null, 1, 0))
                .ReturnsAsync(expectedVisitors);

            var controller = new VisitoresController(visitorServiceStub.Object);

            // Act
            var result = await controller.GetVisitoresAsync(type: 1);

            //Assert
            result.Should().BeEquivalentTo(expectedVisitors,
                option => option.ComparingByMembers<MemberVisitor>());
        }

        [Fact]
        public async Task GetVisitores_WithMemberId_ReturnExpectedVisitors()
        {
            // Arrange
            var memberId = Guid.NewGuid().ToString();
            var AllVisitors = new[] 
            {
                CreateRandomVisitor(memberId, VisitorType.Free), 
                CreateRandomVisitor(), 
                CreateRandomVisitor(memberId, VisitorType.Paid) 
            };

            var expectedVisitors = new[] 
            {
                AllVisitors[0],
                AllVisitors[2]
            };

            visitorServiceStub.Setup(service => service.GetVisitorsAsync(memberId,0,0))
                .ReturnsAsync(expectedVisitors);

            var controller = new VisitoresController(visitorServiceStub.Object);

            // Act
            var result = await controller.GetVisitoresAsync(memberId);

            //Assert
            result.Should().OnlyContain(
                visitor => visitor.MemberId == AllVisitors[0].MemberId || visitor.MemberId == AllVisitors[2].MemberId
                );
        }



        private MemberVisitor CreateRandomVisitor(string memberId = null, VisitorType type = VisitorType.Paid, VisitorStatus status = VisitorStatus.UnUsed)
        {
            string memId = memberId ?? Guid.NewGuid().ToString();
            return new MemberVisitor
            {
                Id = Guid.NewGuid().ToString(),
                MemberId = memId,
                VisitorStatus = status,
                AccessesDate = new DateTime(2020, rand.Next(1,12), rand.Next(1,29)),
                VisitorType = type,
                Note = null
            };
        }
    }
}
