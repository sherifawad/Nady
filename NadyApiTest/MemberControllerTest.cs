using Core.Models;
using DataBase;
using DataBase.Models;
using DataBase.UnitOfWork;
using FakeItEasy;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
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
        [Fact]
        public async Task GetMemberes_Return_The_Correct_Numberes_Of_Members()
        {
            // Arrange
            int count = 2;


            var fakeMembers = new List<Member>
            {
              new Member {Id = "046648ce-1760-4ffd-a176-201da2164db1", Name = "Sherif"},
              new Member {Id = "cae4dc36-fa4f-43a7-bb03-7d0ddcbabc44", Name = "Ahmed"}
            };
            ////expect
            //var fakeIdatacontext = A.Fake<IDatabaseContext>();
            //var fakeIunitOfWork = A.Fake<IUnitOfWork>();
            ////var fakeMembers = A.CollectionOfDummy<Member>(count).AsEnumerable().ToList();
            //var fakeunitOfWork = new UnitOfWork(fakeIdatacontext);
            ////A.CallTo(() => fakeunitOfWork.Repository<Member>().GetAllAsync());
            //var controller = new MembersController(fakeIunitOfWork);
            ////Act
            //var actionresult = await controller.GetAllMembers();
            ////Assert
            //var result = actionresult.Result as OkObjectResult;
            //var returnMembers = result.Value as List<Member>;
            //Assert.Equal(count, returnMembers.Count);
        }
    }
}
