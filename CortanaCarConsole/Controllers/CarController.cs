using System;
using System.Web.Http;
using CortanaCarConsole.Services;

namespace CortanaCarConsole.Controllers
{
    [RoutePrefix("Car")]
    public class CarController : ApiController
    {
        [HttpPost, Route("StartMoveForward")]
        public void StartMoveForward()
        {
            Program.MortorController.Action(MortorActions.MoveForward);
        }

        [HttpPost, Route("StartMoveBackward")]
        public void StartMoveBackward()
        {
            Program.MortorController.Action(MortorActions.MoveBackward);
        }

        [HttpPost, Route("StartTurnLeft")]
        public void StartTurnLeft()
        {
            Program.MortorController.Action(MortorActions.TurnLeft);
        }

        [HttpPost, Route("StartTurnRight")]
        public void StartTurnRight()
        {
            Program.MortorController.Action(MortorActions.TurnRight);
        }

        [HttpPost, Route("StopAll")]
        public void StopAll()
        {
            Program.MortorController.Action(MortorActions.StopMoving);
        }
    }
}