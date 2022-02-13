using System;
using TanksGame.Base.Exceptions;
using TanksGame.Commands.Abstractions;

namespace TanksGame.Base.Event
{
    public class EventLoop
    {
        private ICommandQueue commandQueue;
        public EventLoop(ICommandQueue commandQueue)
        {
            this.commandQueue = commandQueue;
        }

        public void Run()
        {
            while (commandQueue.Peek() is not (null, null))
            {
                (ICommand, ICommand) command = commandQueue.Dequeue();
                try
                {
                    command.Item1.Execute();
                }
                catch (ExecutException)
                {
                    IoC.Resolve<object>("ExceptionHandler.CommandExecution", command.Item2);
                }
                catch (CollisionException)
                {
                     IoC.Resolve<object>("ExceptionHandler.Collision",
                     IoC.Resolve<IGameObject>("CollisionObject1"),
                     IoC.Resolve<IGameObject>("CollisionObject2"));
                }
            }
        }

    }
}