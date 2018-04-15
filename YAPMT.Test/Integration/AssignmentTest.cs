using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using YAPMT.Api;
using YAPMT.Domain.CommandHandlers.Commands.Assignment;
using YAPMT.Domain.CommandHandlers.Commands.Project;
using YAPMT.Domain.Dtos;
using YAPMT.Domain.Exceptions;
using YAPMT.Framework.CommandHandlers;
using YAPMT.Framework.Middlewares;
using YAPMT.Framework.Test;

namespace YAPMT.Test
{
    public class AssignmentTest : IClassFixture<WebHostFixture<Startup>>
    {
        public AssignmentTest(WebHostFixture<Startup> webHostFixture)
        {
            WebHostFixture = webHostFixture;
        }

        public WebHostFixture<Startup> WebHostFixture { get; }

        private string path = "api/v1/Assignment";

        [Fact]
        public async Task test_insert_update_delete_done_assignment()
        {
            var insert = new AssignmentInsertCommand
            {
                Completed = false,
                Description = "Task 1",
                DueDate = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"),
                User = "allan",
                ProjectId = 1
            };

            var content = await this.insertAssignment(insert, HttpStatusCode.BadRequest);
            var command = await content.ReadAsObjectAsync<FailureResult>();
            Assert.False(command.IsSuccess, "Sucesso deveria ser falso");
            Assert.True(command.IsFailure, "Falha deveria ser true");

            await this.insertProject("Projeto 1");
            await this.insertAssignment(insert, HttpStatusCode.OK);

            insert.Description = "Task 2";
            insert.DueDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");
            await this.insertAssignment(insert, HttpStatusCode.OK);

            insert.Description = "Task 3";
            insert.DueDate = DateTime.Now.ToString("MM/dd/yyyy");
            await this.insertAssignment(insert, HttpStatusCode.OK);

            var all = await this.getAllAssignments();
            Assert.True(all.Count == 3, $"Deveria ter 3 tarefas mas tinha {all.Count}");

            Assert.True(all[1].IsLate, $"A tarefa {all[1].Description} deveria estar atrasada");
            Assert.True(all[1].RelativeTime == "yesterday", $"A tarefa {all[1].Description} deveria estar como yesterday mas estava {all[1].RelativeTime}");
            Assert.False(all[2].IsLate, $"A tarefa {all[2].Description} não deveria estar atrasada");
            Assert.True(all[0].RelativeTime == "tomorrow", $"A tarefa {all[1].Description} deveria estar como tomorrow mas estava {all[1].RelativeTime}");

            AssignmentUpdateCommand updateCommand = new AssignmentUpdateCommand
            {
                Id = 1,
                Completed = false,
                Description = "Task um",
                DueDate = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"),
                User = "allan",
                ProjectId = 1
            };
            await this.updateAssignment(updateCommand, HttpStatusCode.OK);

            var task = await this.getAssignmentById(1);
            Assert.True(task.Description == "Task um", $"O nome datask deveria ser Task um mas estava {task.Description}");
            Assert.False(task.IsLate, $"A tarefa {task.Description} não deveria estar atrasada");
            Assert.True(task.RelativeTime == "tomorrow", $"A tarefa {task.Description} deveria estar como tomorrow mas estava {task.RelativeTime}");

            await this.deleteAssignment(3, HttpStatusCode.OK);
            all = await this.getAllAssignments();
            Assert.True(all.Count == 2, $"Deveria ter 2 tarefas mas tinha {all.Count}");

            content = await this.deleteAssignment(5, HttpStatusCode.BadRequest);
            command = await content.ReadAsObjectAsync<FailureResult>();
            Assert.False(command.IsSuccess, "Sucesso deveria ser falso");
            Assert.True(command.IsFailure, "Falha deveria ser true");
            all = await this.getAllAssignments();
            Assert.True(all.Count == 2, $"Deveria ter 2 tarefas mas tinha {all.Count}");

            content = await this.doneAssignment(5, HttpStatusCode.InternalServerError);
            GlobalExceptionResult exceptionResult = await content.ReadAsObjectAsync<GlobalExceptionResult>();
            Assert.True(exceptionResult.ExceptionMessage == NotFoundedAssignmentException.DEFAUTL_MESSAGE,
                $"Mensagem de erro deveria ser {NotFoundedAssignmentException.DEFAUTL_MESSAGE}");

            content = await this.doneAssignment(1, HttpStatusCode.OK);
            DateTime doneDate = await content.ReadAsObjectAsync<DateTime>();
            Assert.True(doneDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"),
                $"Data de conslusão deveria ser {DateTime.Now.ToString("dd/MM/yyyy")} mas era {doneDate.ToString("dd/MM/yyyy")}");

            all = await this.getAllAssignments();
            Assert.True(all[0].Completed, $"Tarefa {all[0].Description} deveria estar como completa");
        }

        private int projectId = 0;
        private async Task insertProject(string name)
        {
            var insert = new ProjectInsertCommand
            {
                Name = name
            };

            var httpResponse = await this.WebHostFixture.TestClient.PostAsObjectAsync("api/v1/Project", insert);
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK, await httpResponse.Content.ReadAsStringAsync());
            this.projectId++;
        }

        private async Task<HttpContent> insertAssignment(AssignmentInsertCommand postObj, HttpStatusCode statusExpected)
        {
            var httpResponse = await this.WebHostFixture.TestClient.PostAsObjectAsync(this.path, postObj);
            Assert.True(httpResponse.StatusCode == statusExpected, await httpResponse.Content.ReadAsStringAsync());
            return httpResponse.Content;
        }

        private async Task<HttpContent> updateAssignment(AssignmentUpdateCommand updateObj, HttpStatusCode statusExpected)
        {
            var httpResponse = await this.WebHostFixture.TestClient.PutAsObjectAsync(this.path, updateObj);
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK, await httpResponse.Content.ReadAsStringAsync());
            return httpResponse.Content;
        }

        private async Task<IList<AssignmentDto>> getAllAssignments()
        {
            var httpResponse = await this.WebHostFixture.TestClient.GetAsync(this.path);
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK, await httpResponse.Content.ReadAsStringAsync());
            return await httpResponse.Content.ReadAsObjectAsync<IList<AssignmentDto>>();
        }

        private async Task<AssignmentDto> getAssignmentById(int id)
        {
            var httpResponse = await this.WebHostFixture.TestClient.GetAsync($"{this.path}/{id}");
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK, await httpResponse.Content.ReadAsStringAsync());
            return await httpResponse.Content.ReadAsObjectAsync<AssignmentDto>();
        }

        private async Task<HttpContent> deleteAssignment(int id, HttpStatusCode statusExpected)
        {
            var httpResponse = await this.WebHostFixture.TestClient.DeleteAsync($"{this.path}/{id}");
            Assert.True(httpResponse.StatusCode == statusExpected, await httpResponse.Content.ReadAsStringAsync());
            return httpResponse.Content;
        }

        private async Task<HttpContent> doneAssignment(int id, HttpStatusCode statusExpected)
        {
            var httpResponse = await  this.WebHostFixture.TestClient.GetAsync($"{this.path}/{id}/done");
            Assert.True(httpResponse.StatusCode == statusExpected, await httpResponse.Content.ReadAsStringAsync());
            return httpResponse.Content;
        }
    }
}
