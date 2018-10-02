using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProcureEaseAPI.Tests.Controllers
{
    public class Mocker
    {
        public static void MockControllerContext(Controller controller, string server)
        {
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Request.Url).Returns(new Uri(server, UriKind.Absolute));
            context.Setup(x => x.Session).Returns(session.Object);
            var requestContext = new RequestContext(context.Object, new RouteData());
            controller.ControllerContext = new ControllerContext(requestContext, controller);
        }

        public static void MockContextHeader(Controller controller)
        {
            string email = "oaro@techspecialistlimited.com";
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection {
              {"Email", email}
                });
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
        }
    }
}