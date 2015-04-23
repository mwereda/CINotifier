using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CINotifier.Hubs;
using CINotifier.Logic.Projects;
using CINotifier.Models;
using EnsureThat;
using Microsoft.AspNet.SignalR;

namespace CINotifier.Controllers
{
    /// <summary>
    /// Exposes methods to add and get builds or projects
    /// </summary>
    public class BuildsController : ApiController
    {
        /// <summary>
        /// Adds new build to project. If project doesn't exist it is being created.
        /// </summary>
        /// <param name="projectBuild">Project and build data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/builds")]
        public Task<HttpResponseMessage> AddBuild([FromBody]ProjectBuild projectBuild)
        {
            try
            {
                Ensure.That(projectBuild).IsNotNull();

                NotifierWrapper.Instance.ReportBuildToProject(projectBuild.Build, projectBuild.Project);

                RefreshProjectsOnClients();
                
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
            }
            catch (ArgumentNullException ex)
            {
                return CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (ArgumentException ex)
            {
                return CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Returns all projects with last build history.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/builds")]
        public Task<HttpResponseMessage> GetAllProjects()
        {
            var projects = NotifierWrapper.Instance.GetProjects();

            var builds = new List<ProjectBuildsHistory>(projects.Count);
            builds.AddRange(
                projects.Select(
                    project =>
                        new ProjectBuildsHistory {Project = new Project(project.Name), Builds = project.ListBuilds()})
                    .OrderBy(x => x.Project.Name));

            return CreateResponse(HttpStatusCode.OK, builds);
        }

        /// <summary>
        /// Deletes information about whole project
        /// </summary>
        /// <param name="projectName">Project's name</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/builds")]
        public Task<HttpResponseMessage> Delete(string projectName)
        {
            try
            {
                Ensure.That(projectName).IsNotNullOrEmpty();

                NotifierWrapper.Instance.Delete(projectName);

                return CreateResponse(HttpStatusCode.NotFound, projectName);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Returns last build history within given project.
        /// </summary>
        /// <param name="projectName">Project's name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/builds/{projectName}")]
        public Task<HttpResponseMessage> GetBuildsFromProject(string projectName)
        {
            try
            {
                Ensure.That(projectName).IsNotNullOrEmpty();

                var builds = NotifierWrapper.Instance.GetBuildsForProject(new Project(projectName));
                
                return CreateResponse(HttpStatusCode.OK, builds);
            }
            catch (ArgumentException ex)
            {
                return CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private Task<HttpResponseMessage> CreateResponse<T>(HttpStatusCode statusCode, T value)
        {
            return Task.FromResult(Request.CreateResponse(statusCode, value));
        }

        private void RefreshProjectsOnClients()
        {
            GlobalHost.ConnectionManager.GetHubContext<BuildsHub>().Clients.All.addBuild();
        }
    }
}