using System;

namespace Platformer
{
   
    public interface IQuestStory : IDisposable
    {
        bool IsDone { get; }
    } 
}
