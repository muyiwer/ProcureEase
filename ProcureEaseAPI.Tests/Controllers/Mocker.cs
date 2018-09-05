using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProcureEaseAPI.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProcureEaseAPI.Tests.Controllers
{
    public class Mocker
    {
        public static void MockControllerContext(UsersController controller, string server)
        {
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Request.Url).Returns(new Uri(server, UriKind.Absolute));
            context.Setup(x => x.Session).Returns(session.Object);
            var requestContext = new RequestContext(context.Object, new RouteData());
            controller.ControllerContext = new ControllerContext(requestContext, controller);
        }
    }
}