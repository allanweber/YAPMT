using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using YAPMT.Api;
using YAPMT.Domain.CommandHandlers.Commands.Assignment;
using YAPMT.Domain.CommandHandlers.Commands.Project;
using YAPMT.Domain.Dtos;
using YAPMT.Framework.Test;

namespace YAPMT.Test.Integration
{
    public class StatusProjectTests : IClassFixture<WebHostFixture<Startup>>
    {
        public StatusProjectTests(WebHostFixture<Startup> webHostFixture)
        {
            WebHostFixture = webHostFixture;
        }

        public WebHostFixture<Startup> WebHostFixture { get; }

        [Fact]
        public async Task get_status_project()
        {
            await this.insertProject("Projeto 1");

            AssignmentInsertCommand today = new AssignmentInsertCommand
            {
                Completed = false,
                Description = "Task 1",
                DueDate = DateTime.Now.ToString("MM/dd/yyyy"),
                User = "allan",
                ProjectId = 1
            };

            AssignmentInsertCommand tomorrow = new AssignmentInsertCommand
            {
                Completed = false,
                Description = "Task 1",
                DueDate = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"),
                User = "allan",
                ProjectId = 1
            };

            AssignmentInsertCommand yesterday = new AssignmentInsertCommand
            {
                Completed = false,
                Description = "Task 1",
                DueDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy"),
                User = "allan",
                ProjectId = 1
            };

            await this.insertAssignment(today);
            await this.insertAssignment(tomorrow);
            await this.insertAssignment(yesterday);

            var projectStatus = await this.getStatusProject(1);
            Assert.True(projectStatus.Total == 3, $"Projeto 1 deveria ter 3 tarefas mas tinha {projectStatus.Total}");
            Assert.True(projectStatus.Late == 1, $"Projeto 1 deveria ter 1 tarefa atrasada mas tinha {projectStatus.Late}");
            Assert.True(projectStatus.Completed == 0, $"Projeto 1 deveria ter 0 tarefas completas mas tinha {projectStatus.Completed}");

            AssignmentInsertCommand other = new AssignmentInsertCommand
            {
                Completed = false,
                Description = "Task 1",
                DueDate = DateTime.Now.AddDays(2).ToString("MM/dd/yyyy"),
                User = "allan",
                ProjectId = 1
            };
            await this.insertAssignment(other);

            await this.doneAssignment(1);

            projectStatus = await this.getStatusProject(1);
            Assert.True(projectStatus.Total == 4, $"Projeto 1 deveria ter 4 tarefas mas tinha {projectStatus.Total}");
            Assert.True(projectStatus.Late == 1, $"Projeto 1 deveria ter 1 tarefa atrasada mas tinha {projectStatus.Late}");
            Assert.True(projectStatus.Completed == 1, $"Projeto 1 deveria ter 1 tarefa completa mas tinha {projectStatus.Completed}");

            await this.insertProject("Projeto 2");
            today.ProjectId = 2;
            tomorrow.ProjectId = 2;
            yesterday.ProjectId = 2;
            other.ProjectId = 2;
            await this.insertAssignment(today);
            await this.insertAssignment(tomorrow);
            await this.insertAssignment(yesterday);
            await this.insertAssignment(other);

            AssignmentInsertCommand yesterday2 = new AssignmentInsertCommand
            {
                Completed = false,
                Description = "Task 1",
                DueDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy"),
                User = "allan",
                ProjectId = 2
            };
            await this.insertAssignment(yesterday2);
            projectStatus = await this.getStatusProject(2);
            Assert.True(projectStatus.Total == 5, $"Projeto 5 deveria ter 5 tarefas mas tinha {projectStatus.Total}");
            Assert.True(projectStatus.Late == 2, $"Projeto 2 deveria ter 2 tarefas atrasadas mas tinha {projectStatus.Late}");
            Assert.True(projectStatus.Completed == 0, $"Projeto 2 deveria ter 0 tarefas completas mas tinha {projectStatus.Completed}");

            projectStatus = await this.getStatusProject(1);
            Assert.True(projectStatus.Total == 4, $"Projeto 1 deveria ter 4 tarefas mas tinha {projectStatus.Total}");
            Assert.True(projectStatus.Late == 1, $"Projeto 1 deveria ter 1 tarefa atrasada mas tinha {projectStatus.Late}");
            Assert.True(projectStatus.Completed == 1, $"Projeto 1 deveria ter 1 tarefa completa mas tinha {projectStatus.Completed}");
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

        private async Task<ProjectStatusDto> getStatusProject(int projectId)
        {
            var httpResponse = await this.WebHostFixture.TestClient.GetAsync($"api/v1/Project/{projectId}/status");
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK, await httpResponse.Content.ReadAsStringAsync());
            return await httpResponse.Content.ReadAsObjectAsync<ProjectStatusDto>();
        }

        private async Task insertAssignment(AssignmentInsertCommand postObj)
        {
            var httpResponse = await this.WebHostFixture.TestClient.PostAsObjectAsync("api/v1/Assignment", postObj);
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK, await httpResponse.Content.ReadAsStringAsync());
        }

        private async Task<HttpContent> doneAssignment(int id)
        {
            var httpResponse = await this.WebHostFixture.TestClient.GetAsync($"api/v1/Assignment/{id}/done");
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK, await httpResponse.Content.ReadAsStringAsync());
            return httpResponse.Content;
        }
    }
}
